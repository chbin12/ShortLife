using UnityEngine;
using System.Collections;

public class CLMyTweenRotate : TweenRotation {
	
	public object onRotatingCallback;
	public object onFinishCallback;

	public void init(object onRotatingCallback, object onFinishCallback) {
		this.onRotatingCallback = onRotatingCallback;
		this.onFinishCallback = onFinishCallback;
	}

	protected override void OnUpdate (float factor, bool isFinished)
	{
		base.OnUpdate (factor, isFinished);
		if (isFinished) {
			Utl.doCallback(onFinishCallback, this);
		} else {
			Utl.doCallback(onRotatingCallback, this);
		}
	}

}
