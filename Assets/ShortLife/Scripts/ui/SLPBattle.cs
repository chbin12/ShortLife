using UnityEngine;
using System.Collections;
using LuaInterface;

public class SLPBattle : MonoBehaviour {
	public CLPanelLua panelLua;
#if UNITY_EDITOR
	LuaFunction onKeyLeftArrow;
	LuaFunction onKeyRightArrow;

	// Update is called once per frame
	void Update () {
		if(onKeyLeftArrow == null) {
			onKeyLeftArrow = panelLua.getLuaFunction ("onKeyLeftArrow");
			onKeyRightArrow = panelLua.getLuaFunction ("onKeyRightArrow");
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.A)) {
			if(onKeyLeftArrow != null) {
				onKeyLeftArrow.Call();
			}
		}
		if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D)) {
			if(onKeyRightArrow != null) {
				onKeyRightArrow.Call();
			}
		}
	}
#endif
}
