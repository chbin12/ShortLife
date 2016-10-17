using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

/// <summary>
/// CLAI path. A*寻路
/// </summary>

public class CLAIPath : AIPath
{
	public LayerMask obstrucLayer;	// 障碍层
	public bool canRotation = true;	//是否旋转
	public OnPathDelegate mOnPathComplete;
	public Callback mOnMoving;
	public Callback mOnTargetReached;
	Coroutine repeateSerachPathCoroutine;	//重复寻路的携程

	protected override void Start ()
	{
		// can not call base.Start
	}

	/// <summary>
	/// Init the specified onPathComplete, onMoving and onTargetReached.
	/// </summary>
	/// <param name="onPathComplete">On path complete.</param>
	/// 寻路完后的回调
	/// <param name="onMoving">On moving.</param>
	/// 移动过程中的回调
	/// <param name="onTargetReached">On target reached.</param>
	/// 到达目标后的回调
	/// 
	public void init (OnPathDelegate onPathComplete, 
	                 Callback onMoving, 
	                 Callback onTargetReached)
	{
		canMove = false;
		targetReached = true;
		mOnPathComplete = onPathComplete;
		mOnMoving = onMoving;
		mOnTargetReached = onTargetReached;
	}

	public void startRepeatSearchPath (Transform target, float repathRate)
	{
		if (target == null)
			return;
		stopMoveToDelay();
		this.target = target;
		this.repathRate = repathRate;
		lastRepath = -9999;
		canSearchAgain = true;
		canSearch = true;
		targetReached = false;
		
		lastFoundWaypointPosition = GetFeetPosition ();
		
		//Make sure we receive callbacks when paths complete
//		seeker.pathCallback += OnPathComplete;

//		stopRepeatSearchPath ();
		repeateSerachPathCoroutine = StartCoroutine (RepeatTrySearchPath ());
	}

	public void stopRepeatSearchPath ()
	{
		canSearchAgain = false;
		canSearch = false;
		canMove = false;
		if (repeateSerachPathCoroutine != null) {
			StopCoroutine (repeateSerachPathCoroutine);
			repeateSerachPathCoroutine = null;
		}
	}

	Coroutine moveDelayCoroutine;
	/// <summary>
	/// Moves to delay.
	/// </summary>
	/// <param name="toPos">To position.</param>
	/// <param name="delaySec">Delay sec.</param>
	public void moveToDelay (Vector3 toPos, float delaySec)
	{
		stopMoveToDelay();
		if (delaySec > 0) {
			moveDelayCoroutine = StartCoroutine (doMoveToDelay (toPos, delaySec));
		} else {
			moveTo (toPos);
		}
	}

	public void stopMoveToDelay() {
		if(moveDelayCoroutine != null) {
			StopCoroutine(moveDelayCoroutine);
			moveDelayCoroutine = null;
		}
	}
	
	IEnumerator doMoveToDelay (Vector3 toPos, float delaySec)
	{
		yield return new WaitForSeconds (delaySec);
		moveTo (toPos);
	}

	public void moveTo (Vector3 toPos)
	{
		stopMoveToDelay ();
		if (canMoveLine (toPos)) {
			moveLine (toPos);
			return;
		}
		lastRepath = Time.time;
		//This is where we should search to
		Vector3 targetPosition = toPos;
		
		canMove = false;
		canSearchAgain = false;
		targetReached = false;
		
		//We should search from the current position
		seeker.StartPath (GetFeetPosition (), targetPosition);
	}

	public void moveTo (Transform target)
	{
		stopMoveToDelay ();
		this.target = target;
		Vector3 toPos = target.position;
		if (canMoveLine (toPos)) {
			moveLine (toPos);
			return;
		}
		
		canSearchAgain = false;
		canMove = false;
		targetReached = false;
		SearchPath ();
	}

	public bool canMoveLine (Vector3 toPos)
	{
		if (Physics.Raycast (transform.position, 
		                     toPos - transform.position, 
		                     Vector3.Distance (toPos, transform.position), 
		                     obstrucLayer.value)) {
			//说明中间有障碍
			return false;
		} else {
			return true;
		}
	}

	/// <summary>
	/// Moves the line.
	/// </summary>
	/// <param name="toPos">To position.</param>
	public void moveLine (Vector3 toPos)
	{
		stopMoveToDelay();
		canMove = false;
		targetReached = false;
		target = null;
//		this.targetPos = toPos;
		
//		ABPath path = ABPath.Construct (GetFeetPosition (), toPos, null);
		ABPath path = PathPool<ABPath>.GetPath ();
		if (path.vectorPath == null) {
			path.vectorPath = new List<Vector3> ();
		}
		path.vectorPath.Clear ();
		path.vectorPath.Add (GetFeetPosition ());
		path.vectorPath.Add (toPos);
		OnPathComplete (path);
	}

	public void moveWithPath (object vestors) {
		if (vestors == null)
			return;

		stopMoveToDelay ();
		canMove = false;
		targetReached = false;
		target = null;

//		ABPath path = ABPath.Construct (GetFeetPosition (), (Vector3)(vestors [vestors.Count]), null);
		ABPath path = PathPool<ABPath>.GetPath ();
		if (path.vectorPath == null) {
			path.vectorPath = new List<Vector3> ();
		}
		path.vectorPath.Clear ();
		if(vestors is List<Vector3>) {
			int count = ((List<Vector3>)vestors).Count;
			for(int i=0; i < count ; i++) {
				path.vectorPath.Add(((List<Vector3>)vestors)[i]);
			}
		} else if (vestors is LuaInterface.LuaTable) {
			int count = ((LuaInterface.LuaTable)vestors).Count;
			for(int i=1; i <= count; i++) {
				path.vectorPath.Add((Vector3)(((LuaInterface.LuaTable)vestors)[i]));
			}
		}
		OnPathComplete (path);
	}

	public override void OnPathComplete (Path _p)
	{
		base.OnPathComplete (_p);

		if (mOnPathComplete != null) {
			mOnPathComplete (_p);
		}
//		canMove = true;
	}

	Vector3 tmpDir = Vector3.zero;

	public override void Update ()
	{
		//base.Update ();		// move logic to fixedUpdate
	}

	public  void FixedUpdate ()
	{
		
		if (!canMove) {
			return;
		}
		tmpDir = CalculateVelocity (GetFeetPosition ());

		//Rotate towards targetDirection (filled in by CalculateVelocity)
		if (canRotation) {
			if(!TargetReached) {
				RotateTowards (targetDirection);
			}
			tr.Translate (tmpDir * Time.fixedDeltaTime, Space.World);
		} else {
			float targetDist = targetDirection.magnitude;
			float slowdown = Mathf.Clamp01 (targetDist / slowdownDistance);
			Vector3 forward = targetDirection.normalized;
			float dot = Vector3.Dot (forward,forward);
			sp = speed * Mathf.Max (dot,minMoveScale) * slowdown;
			if (Time.fixedDeltaTime	> 0) {
				sp = Mathf.Clamp (sp,0,targetDist/(Time.fixedDeltaTime*2));
			}
			tr.Translate (targetDirection.normalized * sp * Time.fixedDeltaTime, Space.World);
		}

		if (canMove && !TargetReached) {
			if (mOnMoving != null) {
				mOnMoving ();
			}
		}
	}

	public override void OnTargetReached ()
	{
		base.OnTargetReached ();
		if (!canSearchAgain || !canSearch) {
			canMove = false;
		}
		if (mOnTargetReached != null) {
			mOnTargetReached ();
		}
	}

	public void pause ()
	{
		enabled = false;
	}

	public void regain ()
	{
		enabled = true;
	}

	public void stop ()
	{
		stopMoveToDelay ();
		stopRepeatSearchPath ();
		canMove = false;
	}
}
