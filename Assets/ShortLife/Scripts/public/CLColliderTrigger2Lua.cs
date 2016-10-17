using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLColliderTrigger2Lua : CLBaseLua
{
//	public bool isAutoSetLua = false;
	public List<string> strs = new List<string>();
	public List<GameObject>  objs = new List<GameObject>();
	public EventDelegate onTriggerEnter;
	public EventDelegate onTriggerExit;
	public EventDelegate onTriggerStay;

	public void init() {
//		if (!isAutoSetLua)
//			return;
		setLua ();
		LuaInterface.LuaFunction f = getLuaFunction ("init");
		if (f != null) {
			f.Call(this);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (onTriggerEnter != null) {
			onTriggerEnter.Execute (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (onTriggerExit != null) {
			onTriggerExit.Execute (other.gameObject);
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (onTriggerStay != null) {
			onTriggerStay.Execute (other.gameObject);
		}
	}
}
