using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITexteara : MonoBehaviour {
	public UILabel mLabel;
	public UIScrollView scrollView;

	Vector2 mSize = Vector2.zero;
	public List<UILabel> labelList = new List<UILabel>();


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


	[ContextMenu("Refresh")]
	public void refresh(bool force = false) {
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

	public void onLabelChange() {
		Debug.Log ("onLabelChange");
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

	public void show() {
		int count = textLines.Length;
		UILabel label = null;
		for(int i = labelList.Count; i < count; i++) {
			label = NGUITools.AddChild (gameObject, mLabel.gameObject).GetComponent<UILabel> ();
			UILabelEachLine eachLine =  label.gameObject.AddComponent<UILabelEachLine> ();
			eachLine.texteara = this;
			label.alpha = 1;
			label.overflowMethod = UILabel.Overflow.ResizeFreely;
			labelList.Add (label);
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
			label = labelList [i];
			if(i < count) {
				label.text = textLines [i];
				NGUITools.SetActive (label.gameObject, true);
				label.transform.localPosition = pos + new Vector3(0, i*flag*heightOffset, 0);
			} else {
				NGUITools.SetActive (label.gameObject, false);
			}
		}
	}

	[ContextMenu("Clean")]
	public void clean() {
		int labelCount = labelList.Count;
		UILabel label = null;
		for (int i = 0; i < labelCount; i++) {
			label = labelList [i];
			if(label != null)
			GameObject.DestroyImmediate (label.gameObject);
		}
		labelList.Clear ();
	}
}


