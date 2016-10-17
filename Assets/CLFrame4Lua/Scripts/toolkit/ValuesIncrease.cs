using UnityEngine;
using System.Collections;
using Toolkit;

public class ValuesIncrease : MonoBehaviour
{
	
	public TweenPosition tweener;
	public UILabel lbNumber;
	public GameObject parentOfSelf;
	public GameObject childContent;
	public GameObject moveToGo;
	public UILabel lbValus;
	public Camera CameraOfSelf;
	public Camera moveTargetCm = null;
	public bool playNow = false;
	Vector3 moveToPos;
	bool isdoIncrease = false;
	int values = 0;
	System.Threading.Timer timer;
	bool isNeedInit = true;
	Vector3 oldFromPos;
	Vector3 oldToPos;
	string mValues;
	
	void Awake ()
	{
		
		if (!isNeedInit)
			return;
		if (moveToGo != null && moveTargetCm == null) {
			Transform tf = moveToGo.transform;
			while (tf != null) {
				if (tf.GetComponent<Camera>() != null) {
					moveTargetCm = tf.GetComponent<Camera>();
					break;
				}
				tf = tf.parent;
			}
		}
		
		oldFromPos = tweener.from;
		oldToPos = tweener.to;
		isNeedInit = false;
	}

	void getMoveToPos ()
	{
		if (CameraOfSelf != null && moveTargetCm != null) {
			moveToPos = moveToGo.transform.position;
			moveToPos = moveTargetCm.WorldToScreenPoint (moveToGo.transform.position);
			moveToPos.z = CameraOfSelf.transform.localPosition.z;
			moveToPos = CameraOfSelf.ScreenToWorldPoint (moveToPos);
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (playNow) {
			playNow = false;
			play ("0");
		}
		if (isdoIncrease) {
			//isdoIncrease = false;
			values++;
			lbNumber.text = "+" + values;
			//timer = TimerEx.schedule (doIncrease, null, 200);
		}
	}
	
	int finishCallTimes = 0;

	void OnFinishTweenMove ()
	{
		finishCallTimes++;
		if (timer != null) {
			timer.Dispose ();
		}
		
		if (finishCallTimes == 1) {
			getMoveToPos ();
			transform.parent = null;
			Vector3 tmpv3 = transform.localPosition;
			tweener.from = tmpv3;
			tweener.to = moveToPos;
			tweener.method = UITweener.Method.EaseIn;
			tweener.duration = 0.7f;
			tweener.Reset ();
			tweener.Play (true);
		} else if (finishCallTimes == 2) {
			NGUITools.SetActive (childContent, false);
			if (moveToGo.GetComponent<TweenScale> () == null) {
				moveToGo.AddComponent<TweenScale> ();
				TweenScale sc = moveToGo.GetComponent<TweenScale> ();
				sc.from = moveToGo.transform.localScale * (1 + 0.3f);
				sc.to = moveToGo.transform.localScale;
			} else {
				TweenScale sc = moveToGo.GetComponent<TweenScale> ();
				sc.Reset ();
				sc.Play (true);
			}
			if(lbValus != null) {
				lbValus.text = mValues;
			}
		}
	}

	void init ()
	{
		isdoIncrease = false;
		values = 0;
		finishCallTimes = 0;
		lbNumber.text = "0";
		transform.parent = parentOfSelf.transform;
		transform.localEulerAngles = Vector3.zero;
		tweener.transform.localScale = Vector3.one;
		tweener.from = oldFromPos;
		tweener.to = oldToPos;
		tweener.method = UITweener.Method.BounceOut;
		tweener.duration = 2f;
		tweener.Reset ();
	}

	public void play (string val)
	{
		init ();
		mValues = val;
		if (!tweener.enabled) {
			tweener.enabled = true;
		}
		NGUITools.SetActive (childContent, true);
		tweener.Play (true);
		timer = TimerEx.schedule (doIncrease, null, 10);
	}
	
	void doIncrease (object obj)
	{
		isdoIncrease = true;
	}
}
