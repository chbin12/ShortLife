using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class UILabelEachLine : MonoBehaviour {
	public UITexteara texteara;
	UILabel _label;

	public UILabel label {
		get {
			if(_label == null) {
				_label = GetComponent<UILabel> ();
			} 
			return _label;
		}
	}

	UITweener _tweener;
	public UITweener tweener {
		get {
			if(_tweener == null) {
				_tweener = GetComponent<UITweener>();
			}
			return _tweener;
		}
	}

	public string text {
		get {
			return label.text;
		}
		set {
			label.text = value;
			if(tweener != null) {
				tweener.ResetToBeginning();
				tweener.Play(true);
			}
		}
	}

	// Use this for initialization
	void Start () {
		if (texteara == null) {
			texteara = GetComponentInParent<UITexteara> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!label.isVisible) {
//			Debug.Log (label.text);
		}
	}
}
