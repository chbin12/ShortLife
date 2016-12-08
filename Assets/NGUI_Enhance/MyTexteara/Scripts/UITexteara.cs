using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITexteara : MonoBehaviour {
	public enum EffectMode {
		none,
		scale,
		alpha,
	}

	public UILabel mLabel;
	public UIScrollView scrollView;
	public EffectMode effectMode = EffectMode.none;
	public UITweener.Method method = UITweener.Method.Linear;
	public float duration =1;

	Vector2 mSize = Vector2.zero;
	public List<UILabelEachLine> labelList = new List<UILabelEachLine>();


	void Start() {
		if (mLabel == null) {
			Debug.LogError ("Then label is null, please drag a UILabel into here");
			return;
		}
		mLabel.onChange += onLabelChange;
		mLabel.alpha = 0;

		if (scrollView == null) {
			scrollView = GetComponentInParent<UIScrollView> ();
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void refresh() {
		refresh(true);
	}
	public void refresh(bool force) {
		if(force) {
			getProcText ();
		}
		show();
	}

	string[] _textLines;
	public string[] textLines {
		get {
			return _textLines;
		}
	}

	[ContextMenu("Refresh")]
	public void onLabelChange() {
		refresh (true);
	}

	[ContextMenu("show Proc Text")]
	public string[] getProcText() {
		string text = mLabel.processedText;
		string[] strs = text.Split ('\n');
		for(int i = 0; i < strs.Length; i++) {
			Debug.Log (strs[i]);
		}
		_textLines = strs;
		mSize = mLabel.printedSize;
		return strs;
	}

	public void setEffect(UILabel label) {
		switch(effectMode) {
			case EffectMode.scale:
				TweenScale twScale = label.gameObject.AddComponent<TweenScale>();
				twScale.from = new Vector3(1,0,1);
				twScale.to = Vector3.one;
				twScale.method = method;
				twScale.duration = duration;
				break;
			case EffectMode.alpha:
				TweenAlpha twAlpha = label.gameObject.AddComponent<TweenAlpha>();
				twAlpha.from = 0;
				twAlpha.to = 1;
				twAlpha.method = method;
				twAlpha.duration = duration;
				break;
		}
	}

	public void show() {
		int count = textLines.Length;
		UILabelEachLine eachLine = null;
		for(int i = labelList.Count; i < count; i++) {
			UILabel label = NGUITools.AddChild (gameObject, mLabel.gameObject).GetComponent<UILabel> ();
			setEffect(label);
			label.alpha = 1;
			label.overflowMethod = UILabel.Overflow.ResizeFreely;
			eachLine =  label.gameObject.AddComponent<UILabelEachLine> ();
			eachLine.texteara = this;
			NGUITools.SetActive(label.gameObject, false);
			labelList.Add (eachLine);
		}
		float heightOffset = mSize.y / count;
		int labelCount = labelList.Count;
		Vector3 pos = mLabel.transform.localPosition;
		float flag = -1;
		if(mLabel.pivot == UIWidget.Pivot.Center || mLabel.pivot == UIWidget.Pivot.Left || mLabel.pivot == UIWidget.Pivot.Right) {
			pos.y += (mSize.y  - mLabel.fontSize - mLabel.spacingY)/2;
		} else if(mLabel.pivot == UIWidget.Pivot.Bottom || mLabel.pivot == UIWidget.Pivot.BottomLeft || mLabel.pivot == UIWidget.Pivot.BottomRight) {
			pos.y += (mSize.y  - mLabel.fontSize - mLabel.spacingY);
		}
		for(int i = 0; i < labelCount; i++) {
			eachLine = labelList [i];
			if(i < count) {
				eachLine.text = textLines [i];
				eachLine.tweener.delay = i*0.1f;
				NGUITools.SetActive (eachLine.gameObject, true);
				eachLine.transform.localPosition = pos + new Vector3(0, i*flag*heightOffset, 0);
			} else {
				NGUITools.SetActive (eachLine.gameObject, false);
			}
		}
	}

	[ContextMenu("Clean")]
	public void clean() {
		int labelCount = labelList.Count;
		UILabelEachLine label = null;
		for (int i = 0; i < labelCount; i++) {
			label = labelList [i];
			if(label != null)
			GameObject.DestroyImmediate (label.gameObject);
		}
		labelList.Clear ();
	}
}


