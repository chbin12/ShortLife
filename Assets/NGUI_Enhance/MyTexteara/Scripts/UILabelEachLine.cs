using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class UILabelEachLine : MonoBehaviour {
	public UITexteara texteara;
	UILabel _label;

	UILabel label {
		get {
			if(_label == null) {
				_label = GetComponent<UILabel> ();
			} 
			return _label;
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
