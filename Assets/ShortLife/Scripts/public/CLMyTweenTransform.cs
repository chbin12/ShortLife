using UnityEngine;
using System.Collections;

public class CLMyTweenTransform : TweenTransform {
	public object onMovingCallback;
	public object onFinishCallback;
	
	public void init(object onMovingCallback, object onFinishCallback) {
		this.onMovingCallback = onMovingCallback;
		this.onFinishCallback = onFinishCallback;
	}

	protected override void OnUpdate (float factor, bool isFinished)
	{
		base.OnUpdate (factor, isFinished);
		if (isFinished) {
			Utl.doCallback(onFinishCallback, this);
		} else {
			Utl.doCallback(onMovingCallback, this);
		}
	}
}
