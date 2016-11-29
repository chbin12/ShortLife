using UnityEngine;
using System.Collections;
using LuaInterface;

//[ExecuteInEditMode]
public class MyTween : MonoBehaviour
{
	public float speed = 1;
	public float turningSpeed = 1;
	public float high = 0;
	public LayerMask obstrucLayer;
	public float obsDistance = 0.5f;
	public Transform shadow;//影子，当移动时，影子的高度不变
	public float shadowHeight = 0;
	public AnimationCurve curveSpeed = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0));
	public AnimationCurve curveHigh = new AnimationCurve(new Keyframe(0, 0, 0, 4), new Keyframe(0.5f, 1, 0, 0), new Keyframe(1, 0, -4, 0));
	object onFinishCallback;
	object onMovingCallback;
	Vector3 origin = Vector3.zero;
	Vector3 v3Diff = Vector3.zero;
	float curveTime = 0;
	float curveTime2 = 0;
	public bool isMoveNow = false;
	bool isWoldPos = true;
	bool isTurning = false;
	Vector3 subDiff = Vector3.zero;

	public bool runOnStart = false;
	public UITweener.Style style = UITweener.Style.Once;
	public Vector3 from = Vector3.zero;
	public Vector3 to = Vector3.zero;
	int flag = 1;

	// cach transform
	Transform _transform;

	public Transform transform {
		get {
			if(_transform == null) {
				_transform = gameObject.transform;
			}
			return _transform;
		}
	}
	
	void Start()
	{
//		isMoveNow = false;
//		enabled = false;

		if(runOnStart) {
			doTween();
		}
	}
		
	void doTween() {
		float dis = Vector3.Distance(from, to);
		object onFinishCallback = null;
		if (style == UITweener.Style.Loop) {
			flag = 1;
			transform.position = from;
			flyout(from, to, dis, speed, high, null, onFinishCallback, true);
		} else if (style == UITweener.Style.PingPong) {
			if(flag > 0) {
				transform.position = from;
				flyout(from, to, dis, speed, high, null, onFinishCallback, true);
			} else {
				transform.position = to;
				flyout(to, from, dis, speed, high, null, onFinishCallback, true);
			}
		} else if (style == UITweener.Style.Once) {
			return;
		} else {
			return;
		}
	}
	
	public void flyout(Vector3 dirFrom, Vector3 dirTo, float distance, float speed, float hight, object onMovingCallback,object finishCallback, bool isWoldPos = true)
	{
		flyout(dirTo - dirFrom, distance, speed, hight, onMovingCallback, finishCallback, isWoldPos);
	}

	public void flyout(Vector3 dir, float distance, float speed, float hight, object onMovingCallback, object finishCallback, bool isWoldPos = true)
	{
		Vector3 v2 = Vector3.zero;
		if (isWoldPos) {
			v2 = transform.position;
		} else {
			v2 = transform.localPosition;
		}
		dir.y = 0;
		Vector3 v3 = v2 + dir.normalized * distance;
		flyout(v3, speed, hight, onMovingCallback,finishCallback, isWoldPos);
	}

	//弹出
	public  void flyout(Vector3 toPos, float speed, float ihight, object onMovingCallback, object finishCallback, bool isWoldPos = true)
	{
		Debug.Log ("flyout=======" + name);
		this.onFinishCallback = finishCallback;
		this.onMovingCallback = onMovingCallback;
		this.speed = speed;
		this.high = ihight;
		this.isWoldPos = isWoldPos;
		isMoveForward = false;

		if (isWoldPos) {
			origin = transform.position;
			v3Diff = toPos - transform.position;
		} else {
			origin = transform.localPosition;
			v3Diff = toPos - transform.localPosition;
		}
		curveTime = 0;
		isMoveNow = true;
		enabled = true;
	}

	public void onFinishTween()
	{
//		print("onFinishTween");
		enabled = false;
		isMoveNow = false;
		if (onFinishCallback != null) {
			if (onFinishCallback.GetType() == typeof(Callback)) {
				((Callback)onFinishCallback)(this);
			} else if (onFinishCallback.GetType() == typeof(LuaFunction)) {
				((LuaFunction)onFinishCallback).Call(this);
			}
		}
		
		if (style == UITweener.Style.PingPong) {
			flag = -1 * flag;
		}
		doTween();
	}

	void print(string msg) {
		SUnit unit = GetComponent<SUnit>();
		if (unit != null && unit.isOffense) {
			Debug.LogWarning(msg);
		}
	}

	public void stop()
	{
//		print("tween stop");
		isMoveNow = false;
//		enabled = false;
	}
	
	RaycastHit hitInfor;
	Vector3 dis = Vector3.zero;
	Vector3 shadowPos = Vector3.zero;
	// Update is called once per frame
	public void FixedUpdate()
	{
		if(isMoveForward) {
			if (!Physics.Raycast (transform.position, transform.forward, out hitInfor, obsDistance, obstrucLayer.value)) {
				transform.Translate (Vector3.forward.x * 0.017f * speed, 0, Vector3.forward.z * 0.017f * speed);
				if(onMovingCallback != null) {
					if (onMovingCallback.GetType() == typeof(Callback)) {
						((Callback)onMovingCallback)();
					} else if (onMovingCallback.GetType() == typeof(LuaFunction)) {
						((LuaFunction)onMovingCallback).Call(this);
					}
				}
			}
		} else {
			if (!isMoveNow) {
				return;
			}
			curveTime += Time.fixedDeltaTime * speed;
			curveTime = curveTime > 1 ? 1 : curveTime;
			subDiff = v3Diff * curveSpeed.Evaluate(curveTime);//*SCfg.self.fps.fpsRate;
			
			curveTime2 = curveTime*flag;
			if(curveTime2 < 0) {
				curveTime2 = 1 + curveTime2;
			}
	//		subDiff = v3Diff.normalized*curveSpeed.Evaluate(curveTime)*Time.deltaTime;
			subDiff.y += high * curveHigh.Evaluate(curveTime2);//*SCfg.self.fps.fpsRate;
			if (isWoldPos) {
				dis = origin + subDiff - transform.position;
			} else {
				dis = origin + subDiff - transform.localPosition;
			}
	//		Debug.DrawLine(transform.position, transform.position + v3Diff.normalized*(dis.magnitude + obsDistance));
			if (!Physics.Raycast(transform.position, v3Diff, out hitInfor, dis.magnitude + obsDistance, obstrucLayer.value)) {
				if (isWoldPos) {
					transform.position = origin + subDiff;
	//			transform.Translate(subDiff, Space.World);
				} else {
					transform.localPosition = origin + subDiff;
				}

				if(shadow != null) {
					shadowPos = shadow.position;
					shadowPos.y = shadowHeight;
					shadow.position = shadowPos;
				}
				
				if(onMovingCallback != null) {
					if (onMovingCallback.GetType() == typeof(Callback)) {
						((Callback)onMovingCallback)();
					} else if (onMovingCallback.GetType() == typeof(LuaFunction)) {
						((LuaFunction)onMovingCallback).Call(this);
					}
				}
			}

			if (isTurning) {
				Utl.RotateTowards(transform, v3Diff, turningSpeed);
			}
			if (curveTime >= 1) {
				onFinishTween();
			}
		}
	}

	bool isMoveForward = false;
	public void moveForward(float speed) {
		moveForward (speed, null);
	}

	public void moveForward(float speed, object onMovingCallback) {
		this.onMovingCallback = onMovingCallback;
		this.speed = speed;
		isMoveForward = true;
		enabled = true;
	}

	public void stopMoveForward() {
		isMoveForward = false;
//		enabled = false;
	}
}
