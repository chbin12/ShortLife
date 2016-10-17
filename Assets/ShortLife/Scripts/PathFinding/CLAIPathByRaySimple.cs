using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// CLAI path by ray.寻路通过射线
/// </summary>

[ExecuteInEditMode]
public class CLAIPathByRaySimple : CLBehaviour4Lua
{
	public LayerMask obstructLayer;			//障碍
	public float rayDistance = 0.5f;			//射线长度
	public float selfSize = 1;
	public float rayHeight = 0.5f;
	public SearchDirs dirs = SearchDirs._8;
	public float speed = 3;
	public float turningSpeed = 5;
	public float canSearchMaxDis = 100;		//可以寻路的最远距离
	public int maxSearchTimes = 100;
	public Transform target;
	public List<Vector3> vectorList = new List<Vector3> ();
	public Vector3 toPos = Vector3.zero;
	//---------------------------------------------
	List< Vector3> pointsDirLeft = new List<Vector3> ();	//左边的方向的点
	List< Vector3> pointsDirRight = new List<Vector3> ();	//右边的方向的点
	bool canLeftSearch = true;
	bool canRightSearch = true;
	RaycastHit hitInfor;
	int searchTime = 0;
	//---------------------------------------------
	//几个方向
	public enum SearchDirs
	{
		_4,
		_8,
		_16,
		_32,
	}

	public enum _Dir
	{
		up,
		bottom,
		left,
		right,
	}

	public int dirNum {
		get {
			switch (dirs) {
			case SearchDirs._4:
				return 4;
			case SearchDirs._8:
				return 8;
			case SearchDirs._16:
				return 16;
			case SearchDirs._32:
				return 32;
			}
			return 8;
		}
	}

	Transform _transform;

	public Transform transform {
		get {
			if (_transform == null) {
				_transform = gameObject.transform;
			}
			return _transform;
		}
	}

	/// <summary>
	/// Gets the origin position.起点
	/// </summary>
	/// <value>The origin position.</value>
	public Vector3 originPosition {
		get {
			Vector3 pos = transform.position;
			pos.y = rayHeight;
			return pos;
		}
	}
	
	public override void Start ()
	{
		base.Start ();
		resetPoints ();
	}

	[ContextMenu("Refresh Dir")]
	public void resetPoints ()
	{
		pointsDirLeft = getPoints (originPosition, transform.eulerAngles, selfSize, true);
		pointsDirRight = getPoints (originPosition, transform.eulerAngles, selfSize, false);
	}

	public List<Vector3> getPoints (Vector3 fromPos, Vector3 eulerAngles, float r, bool isLeft)
	{
//		float angle = 360.0f / (dirNum + 1);
		float angle = 360.0f / (dirNum);
		int half = dirNum / 2;
		Vector3 pos = Vector3.zero;
		List<Vector3> points = new List<Vector3> ();
		if (isLeft) {
			for (int i=1; i <= half; i++) {
				pos = VectorToolkits.getCirclePointStartWithYV3 (fromPos, r, eulerAngles.y - i * angle);
				pos.y = rayHeight;
				points.Add (pos);
			}
		} else {
			for (int i= dirNum-1; i >= half; i--) {
				pos = VectorToolkits.getCirclePointStartWithYV3 (fromPos, r, eulerAngles.y - i * angle);
				pos.y = rayHeight;
				points.Add (pos);
			}
		}
		return points;
	}

	[ContextMenu("Test Search Path")]
	public void testPath ()
	{
		if (target == null)
			return;
		Vector3 toPos = target.position;
		__searchPath(toPos, null);
	}
	public void searchPath(Vector3 toPos, object finishSearchCallback) {
		StartCoroutine (_searchPath(toPos, finishSearchCallback));
	}
	public IEnumerator _searchPath(Vector3 toPos, object finishSearchCallback) {
		yield return null;
		__searchPath(toPos, finishSearchCallback);
	}
	public void __searchPath(Vector3 toPos, object finishSearchCallback) {
		searchTime = 0;
		this.toPos = toPos;
		List<Vector3> leftPath = trySearchPathLeft (toPos);
		List<Vector3> rightPath = trySearchPathRight (toPos);
		if (leftPath != null && leftPath.Count > 0 && (rightPath == null || rightPath.Count == 0)) {
			vectorList = leftPath;
		} else if ((leftPath == null || leftPath.Count == 0) && rightPath != null && rightPath.Count > 0) {
			vectorList = rightPath;
		} else if (leftPath != null && leftPath.Count > 0 && rightPath != null && rightPath.Count > 0) {
			vectorList = getShortList (leftPath, rightPath);
		} else {
			vectorList = null;
			leftPath = filterPath(tmpVectorList1);
			leftPath.Insert(0, originPosition);
			rightPath = filterPath(tmpVectorList2);
			rightPath.Insert(0, originPosition);
			float dis1 = Vector3.Distance(leftPath[leftPath.Count -1] , toPos);
			float dis2 = Vector3.Distance(rightPath[rightPath.Count -1] , toPos);
			if(dis1 < dis2)  {
				vectorList = leftPath;
			} else {
				vectorList = rightPath;
			}
			Debug.LogWarning("Cannot search path");
		}
		if(finishSearchCallback != null) {
			if(finishSearchCallback is LuaInterface.LuaFunction) {
				((LuaInterface.LuaFunction)finishSearchCallback).Call(vectorList);
			} else if(finishSearchCallback is Callback) {
				((Callback)finishSearchCallback)(vectorList);
			}
		}
	}
	
	[ContextMenu("Test Search Path Left")]
	public void testTrySearchPathLeft() {
		if (target == null)
			return;

		vectorList = trySearchPathLeft (target.position);
	}
	public List<Vector3> trySearchPathLeft(Vector3 toPos) {
		searchTime = 0;
		canLeftSearch = true;
		canRightSearch = false;
		tmpVectorList1.Clear ();
		return doSearchPath (toPos);
	}
	
	[ContextMenu("Test Search Path Right")]
	public void testTrySearchPathRight() {
		if (target == null)
			return;
		vectorList = trySearchPathRight (target.position);
	}
	public List<Vector3> trySearchPathRight(Vector3 toPos) {
		searchTime = 0;
		canLeftSearch = false;
		canRightSearch = true;
		tmpVectorList2.Clear ();
		return doSearchPath (toPos);
	}

	List<Vector3> tmpVectorList1 = new List<Vector3> ();
	List<Vector3> tmpVectorList2 = new List<Vector3> ();

	public List<Vector3> doSearchPath (Vector3 toPos)
	{
		List<Vector3> ret = null;

		if (canReach (originPosition, toPos, out hitInfor)) {
			ret = new List<Vector3>();
			ret.Add (originPosition);
			ret.Add (toPos);
			return ret;
		}

		List<Vector3> list = searchPathByRay (originPosition, toPos, Utl.getAngle(transform.eulerAngles, toPos));
		ret = filterPath (list);
		if (ret != null && ret.Count > 0) {
			ret.Insert (0, originPosition);
		}
		return ret;
	}

	public List<Vector3> filterPath(List<Vector3> list) {
		List<Vector3> ret = new List<Vector3> ();
		Vector3 from = originPosition;
		for(int i = 0; list != null && i < list.Count; i++) {
			if(!canReach(from, list[i], out hitInfor)) {
				ret.Add(list[i-1]);
				from = list[i-1];
			}
			if(i == list.Count-1) {
				ret.Add(list[i]);
			}
		}
		return ret;
	}

	public List<Vector3> searchPathByRay (Vector3 fromPos, Vector3 toPos, Vector3 angle)
	{
		searchTime++;
		if (maxSearchTimes > 0 &&  searchTime > maxSearchTimes )
			return null;
		Vector3 dir = toPos - fromPos;
		List<Vector3> pathList = null;
		if (canReach (fromPos, toPos, out hitInfor)) {
			pathList = new List<Vector3> ();
			pathList.Add (toPos);
		} else {
			Vector3 angle2 = angle;
			Vector3 from = fromPos;
			//left
			List<Vector3> left = getPoints (from, angle2, rayDistance, true);
			//right
			List<Vector3> right = getPoints (from, angle2, rayDistance, false);
			List<Vector3> leftResult = null;
			List<Vector3> rightResult = null;
			//------------------------------------------------------------------
			int count = left.Count;
			Vector3 oldPos = Vector3.zero;
			bool isFirstLeft = true;
			bool isFirstRight = true;

			for (int i=0; i < count; i++) {
				if (canLeftSearch && canReach (from, left [i], out hitInfor)) {
					float dis1 = Vector3.Distance(from, toPos);
					float dis2 = Vector3.Distance(left [i], toPos);
					if(dis1 >= dis2) {
						angle2 = Utl.getAngle (from, toPos);
					} else {
						Vector3 tmpPos1 = left [i];//from;
						Vector3 tmpPos2 = toPos;
						if(isFirstLeft) {
							isFirstLeft = false;
							oldPos = left [i];
						} else {
							tmpPos2 = oldPos;
							tmpPos1 = left [i]; 
						}
						_Dir targetDir = getTargetDir(left[i], toPos);
						if(targetDir == _Dir.left ||
						   targetDir == _Dir.right) {
							tmpPos1.z = 0;
							tmpPos2.z = 0;
						} else {
							tmpPos1.x = 0;
							tmpPos2.x = 0;
						}

						tmpPos1.y = 0;
						tmpPos2.y = 0;
						angle2 =  Utl.getAngle (tmpPos1, tmpPos2);
//						angle2 =  Utl.getAngle (toPos, left[i] );

					}
//					Debug.Log("angle2==" + angle2);
					tmpVectorList1.Add(left [i]);
					leftResult = searchPathByRay (left [i], toPos, angle2);
				}
				if (canRightSearch && canReach (from, right [i], out hitInfor)) {
					float dis1 = Vector3.Distance(from, toPos);
					float dis2 = Vector3.Distance(right [i], toPos);
					if(dis1 >= dis2) {
						angle2 = Utl.getAngle (from, toPos);
					} else {
						Vector3 tmpPos1 = right [i];//from;
						Vector3 tmpPos2 = toPos;
						if(isFirstRight) {
							isFirstRight = false;
							oldPos = right [i];
						} else {
							tmpPos2 = oldPos;
							tmpPos1 = right [i]; 
						}
						_Dir targetDir = getTargetDir(right[i], toPos);
						if(targetDir == _Dir.left ||
						   targetDir == _Dir.right) {
							tmpPos1.z = 0;
							tmpPos2.z = 0;
						} else {
							tmpPos1.x = 0;
							tmpPos2.x = 0;
						}
						
						tmpPos1.y = 0;
						tmpPos2.y = 0;
						angle2 =  Utl.getAngle (tmpPos1, tmpPos2);
					}
					tmpVectorList2.Add(right [i]);
					rightResult = searchPathByRay (right [i], toPos, angle2);
				}

				if (leftResult != null && rightResult == null) {
					leftResult.Insert (0, left [i]);
					leftResult.Insert (0, from);
					pathList = leftResult;
					break;
				} else if (leftResult == null && rightResult != null) {
					rightResult.Insert (0, right [i]);
					rightResult.Insert (0, from);
					pathList = rightResult;
					break;
				} else if (leftResult != null && rightResult != null) {
					leftResult.Insert (0, left [i]);
					rightResult.Insert (0, right [i]);
					pathList = getShortList (leftResult, rightResult);
					pathList.Insert (0, from);
					break;
				}
			}
		}
		return pathList;
	}

	/// <summary>
	/// Cans the reach.能否到达
	/// </summary>
	/// <returns><c>true</c>, if reach was caned, <c>false</c> otherwise.</returns>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	/// <param name="hitInfor">Hit infor.</param>
	public bool canReach (Vector3 from, Vector3 to, out RaycastHit hitInfor)
	{
		Vector3 _to = to;
		_to.y = rayHeight;
		Vector3 dir = _to - from;
		if (!Physics.Raycast (from, dir, out hitInfor, Vector3.Distance (from, to), obstructLayer.value)) {
			return true;
		}
		return false;
	}

	public Vector3 getNewFromPos (Vector3 oldPos, Vector3 hitPoint)
	{
		Vector3 diff = hitPoint - oldPos;
		diff = diff.normalized;
		Vector3 newPos = oldPos + diff * (Vector3.Distance (oldPos, hitPoint) - selfSize);
		float dis1 = Vector3.Distance (oldPos, hitPoint);
		float dis2 = Vector3.Distance (newPos, hitPoint);
		return dis1 > dis2 ? newPos : oldPos;
	}

	/// <summary>
	/// Gets the short list.取得最短路径
	/// </summary>
	/// <returns>The short list.</returns>
	/// <param name="list1">List1.</param>
	/// <param name="list2">List2.</param>
	public List<Vector3> getShortList (List<Vector3> list1, List<Vector3> list2)
	{
		int count = list1.Count;
		float dis1 = 0;
		float dis2 = 0;
		for (int i = 0; i < count-1; i++) {
			dis1 += Vector3.Distance (list1 [i], list1 [i + 1]);
		}
		count = list2.Count;
		for (int i = 0; i < count-1; i++) {
			dis2 += Vector3.Distance (list2 [i], list2 [i + 1]);
		}
		return dis1 > dis2 ? list2 : list1;
	}

	public override void LateUpdate ()
	{
		base.LateUpdate ();
		#if UNITY_EDITOR
		if(oldRayDis != selfSize || oldDirs != dirs ||
		   oldRayHeight != rayHeight || oldeulerAngles != transform.eulerAngles ||
		   oldPosition != transform.position) {
			resetPoints();
			oldRayDis = selfSize;
			oldDirs = dirs;
			oldRayHeight = rayHeight;
			oldeulerAngles = transform.eulerAngles;
			oldPosition = transform.position;
		}
		#endif
	}

	public _Dir getTargetDir(Vector3 fromPos, Vector3 toPos) {
		Vector3 euAngle = Utl.getAngle (fromPos, toPos);
		float angle = euAngle.y;
		if (angle < 0) {
			angle = 360 + angle;
		}
		if ((angle >= 0 && angle <= 45) ||
			(angle >= 315 && angle <= 360)) {
			return _Dir.up;
		} else if ((angle >= 45 && angle <= 135)) {
			return _Dir.right;
		} else if ((angle >= 135 && angle <= 225)) {
			return _Dir.bottom;
		} else if ((angle >= 225 && angle <= 315)) {
			return _Dir.bottom;
		} else {
			Debug.LogError("This angle not in switch case!!!! angle===" + angle);
		}
		return _Dir.up;
	}

	#if UNITY_EDITOR
	float oldRayDis = 0;
	SearchDirs oldDirs = SearchDirs._8;
	float oldRayHeight = 0;
	Vector3 oldeulerAngles = Vector3.zero;
	Vector3 oldPosition = Vector3.zero;
	Matrix4x4  boundsMatrix;
	void OnDrawGizmos() {
		//		if(center == null) return;
		
		//		boundsMatrix.SetTRS (center.position, Quaternion.Euler (girdRotaion),new Vector3 (aspectRatio,1,1));
		//		AstarPath.active.astarData.gridGraph.SetMatrix();
		//		Gizmos.matrix =  AstarPath.active.astarData.gridGraph.boundsMatrix;
		//		Gizmos.matrix = boundsMatrix;
		Gizmos.color = Color.red;

		for(int i =0; canLeftSearch && i <  pointsDirLeft.Count; i++) {
			Gizmos.DrawWireCube(pointsDirLeft[i], Vector3.one*(0.04f  +i*0.005f));
			Debug.DrawLine (originPosition, pointsDirLeft[i]);
		}
		
		for(int i = 0; canRightSearch &&  i < pointsDirRight.Count; i++) {
			Gizmos.DrawWireCube(pointsDirRight[i], Vector3.one*(0.04f  +i*0.005f));
			Debug.DrawLine (originPosition, pointsDirRight[i]);
		}

		List<Vector3> list = tmpVectorList1;// tmpVectorList1;  //vectorList
		if(list != null && list.Count > 1) {
			int i =0;
			for(i =0; i < list.Count-1; i++) {
				Gizmos.DrawWireCube(list[i], Vector3.one*0.06f);
				Debug.DrawLine (list[i], list[i+1]);
			}
			Gizmos.DrawWireCube(list[i], Vector3.one*0.06f);
		}

		list = tmpVectorList2;  //vectorList
		if(list != null && list.Count > 1) {
			int i =0;
			for(i =0; i < list.Count-1; i++) {
				Gizmos.DrawWireCube(list[i], Vector3.one*0.06f);
				Debug.DrawLine (list[i], list[i+1]);
			}
			Gizmos.DrawWireCube(list[i], Vector3.one*0.06f);
		}
		
		list = vectorList;  //vectorList
		if(list != null && list.Count > 1) {
			int i =0;
			for(i =0; i < list.Count-1; i++) {
//				Gizmos.DrawWireCube(list[i], Vector3.one*0.04f);
				Debug.DrawLine (list[i], list[i+1]);
			}
			Gizmos.DrawWireCube(list[i], Vector3.one*0.04f);
		}
		//		Gizmos.matrix = Matrix4x4.identity;

		//		Gizmos.matrix = Matrix4x4.identity;
//		Gizmos.color = Color.white;
	}
	#endif
}
