using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;
using System.Collections.Generic;

//把mobobehaviour的处理都转到lua层
using Toolkit;


public class CLBaseLua : MonoBehaviour
{
	public bool isPause = false;
	public string luaPath;
	public static LuaScriptMgr mainLua = new LuaScriptMgr ();
	public LuaScriptMgr lua;

	public virtual void setLua ()
	{
		doSetLua (false);
	}

	LuaTable _luaTable;
	public LuaTable luaTable;
	//	{
	//		get {
	//			if(_luaTable == null) {
	//				setLua();
	//			}
	//			return _luaTable;
	//		}
	//		set {
	//			_luaTable = value;
	//		}
	//	}
	
	public object[] doSetLua (bool Independent)
	{
		object[] ret = null;
		try {
			luaFuncMap.Clear ();
			if (Independent) {
				lua = new LuaScriptMgr ();
				lua.Start ();
			} else {
				lua = mainLua;
			}
			if (!string.IsNullOrEmpty (luaPath)) {
				ret = Utl.doLua (lua, PathCfg.persistentDataPath + "/" + luaPath);
				if (ret != null && ret.Length > 0) {
					luaTable = (LuaTable)(ret [0]);
				}
			}
		} catch (System.Exception e) {
			Debug.LogError (e);
		}
		return ret;
	}

	Transform _tr;
	//缓存transform
	public Transform transform {
		get {
			if (_tr == null) {
				_tr = gameObject.transform;
			}
			return _tr;
		}
	}

	public void onNotifyLua (GameObject go, string funcName, object paras)
	{
		LuaFunction lfunc = null;
		if (!string.IsNullOrEmpty (funcName)) {
			lfunc = getLuaFunction (funcName);
		} else {
			lfunc = getLuaFunction ("onNotifyLua");
		}
		if (lfunc != null) {
			lfunc.Call (go, paras);
		}
	}

	public Dictionary<string, LuaFunction> luaFuncMap = new Dictionary<string, LuaFunction> ();

	public virtual LuaFunction getLuaFunction (string funcName)
	{
		if (string.IsNullOrEmpty (funcName))
			return null;
		LuaFunction ret = null;
		if (luaFuncMap.ContainsKey (funcName)) {
			ret = luaFuncMap [funcName]; 
		}
		if (ret == null && luaTable != null) {
			ret = (LuaFunction)(luaTable [funcName]);
			if (ret != null) {
				luaFuncMap [funcName] = ret;
			}
		}
		return ret;
	}

	public object getLuaVar (string name)
	{
		if (luaTable == null)
			return null;
		return  luaTable [name];
	}

	/// <summary>
	/// Invoke4s the lua.回调lua函数， 等待时间
	/// </summary>
	/// <param name='callbakFunc'>
	/// Callbak func.
	/// </param>
	/// <param name='sec'>
	/// Sec.
	/// </param>
	Hashtable coroutineMap = Hashtable.Synchronized (new Hashtable ());
	Hashtable coroutineIndex = Hashtable.Synchronized (new Hashtable ());

	public Coroutine invoke4Lua (object callbakFunc, float sec)
	{
		return invoke4Lua (callbakFunc, "", sec);
	}

	public Coroutine invoke4Lua (object callbakFunc, object orgs, float sec)
	{
		return invoke4Lua (callbakFunc, orgs, sec, false);
	}

	/// <summary>
	/// Invoke4s the lua.
	/// </summary>
	/// <param name="callbakFunc">Callbak func.lua函数</param>
	/// <param name="orgs">Orgs.参数</param>
	/// <param name="sec">Sec.等待时间</param>
	public Coroutine invoke4Lua (object callbakFunc, object orgs, float sec, bool onlyOneCoroutine)
	{
		if (callbakFunc == null) {
			Debug.LogError ("callbakFunc is null ......");
			return null;
		}
		try {
			Coroutine ct = null;
			if (onlyOneCoroutine) {
				cleanCoroutines (callbakFunc);
			}
			int index = getCoroutineIndex (callbakFunc);
			ct = StartCoroutine (doInvoke4Lua (callbakFunc, sec, orgs, index));
			setCoroutine (callbakFunc, ct, index);
			return ct;
		} catch (System.Exception e) {
			Debug.LogError (callbakFunc + ":" + e);
			return null;
		}
	}

	public int getCoroutineIndex (object callbakFunc)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineIndex);
		int ret = Toolkit.MapEx.getInt (coroutineIndex, callbakFunc);
		coroutineIndex [key] = ret + 1;
		return ret;
	}

	public void setCoroutineIndex (object callbakFunc, int val)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineIndex);
		coroutineIndex [key] = val;
	}

	/// <summary>
	/// Gets the key4 invoke map.当直接传luafunction时，不能直接用，用Equals查找一下key
	/// </summary>
	/// <returns>The key4 invoke map.</returns>
	/// <param name="callbakFunc">Callbak func.</param>
	/// <param name="map">Map.</param>
	public object getKey4InvokeMap (object callbakFunc, Hashtable map)
	{
		if (callbakFunc == null || map == null)
			return callbakFunc;
		object key = callbakFunc;
		if (callbakFunc is LuaFunction) {
			NewList keys = listPool.borrowObject ();
			keys.AddRange (map.Keys);
			for (int i = 0; i < keys.Count; i++) {
				if (callbakFunc.Equals ((keys [i]))) {
					key = keys [i];
					break;
				}
			}
			listPool.returnObject (keys);
			keys = null;
		}
		return key;
	}

	public Hashtable getCoroutines (object callbakFunc)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineMap);
		if (coroutineMap [key] == null) {
			coroutineMap [key] = new Hashtable ();
		}
		return (Hashtable)(coroutineMap [key]);
	}

	public void setCoroutine (object callbakFunc, Coroutine ct, int index)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineMap);
		Hashtable map = getCoroutines (callbakFunc);
		map [index] = ct;
		coroutineMap [key] = map;
	}

	public void cleanCoroutines (object callbakFunc)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineMap);
		Hashtable list = getCoroutines (callbakFunc);
		foreach (DictionaryEntry cell in list) {
			StopCoroutine ((Coroutine)(cell.Value));
		}
		list.Clear ();
		setCoroutineIndex (callbakFunc, 0);
		coroutineMap.Remove (key);
	}

	public void rmCoroutine (object callbakFunc, int index)
	{
		object key = getKey4InvokeMap (callbakFunc, coroutineMap);
		Hashtable list = getCoroutines (callbakFunc);
		list.Remove (index);
		coroutineMap [key] = list;
	}

	public void cancelInvoke4Lua ()
	{
		cancelInvoke4Lua (null);
	}

	public void cancelInvoke4Lua (object callbakFunc)
	{
		if (callbakFunc == null) {
			#if UNITY_4_6 || UNITY_5
			Hashtable list = null;

			foreach (DictionaryEntry item in coroutineMap) {
				list = getCoroutines ((LuaFunction)(item.Key));
				foreach (DictionaryEntry cell in list) {
					StopCoroutine ((Coroutine)(cell.Value));
				}
				list.Clear ();
			}
			#endif
			StopCoroutine ("doInvoke4Lua");
			coroutineMap.Clear ();
			coroutineIndex.Clear ();
		} else {
			cleanCoroutines (callbakFunc);
		}
	}

	Queue invokeFuncs = new Queue ();

	IEnumerator doInvoke4Lua (object callbakFunc, float sec, object orgs, int index)
	{
		yield return new WaitForSeconds (sec);
		try {
			rmCoroutine (callbakFunc, index);
			LuaFunction func = null;
			if (callbakFunc is string) {
				func = getLuaFunction (callbakFunc.ToString ());
			} else {
				func = (LuaFunction)callbakFunc;
			}
			if (func != null) {
				if (!isPause) {
					func.Call (orgs);
				} else {
					ArrayList list = new ArrayList ();
					list.Add (func);
					list.Add (orgs);
					list.Add (index);
					invokeFuncs.Enqueue (list);
				}
			}
		} catch (System.Exception e) {
			string msg = "call err:doInvoke4Lua" + ",callbakFunc=[" + callbakFunc + "]";
			NAlertTxt.add (msg, Color.red, -1);
			Debug.LogError (msg);
			Debug.LogError (e);
		}
	}

	public virtual void pause ()
	{
		isPause = true;
	}

	public virtual void regain ()
	{
		isPause = false;
		LuaFunction f = null;
		ArrayList invokeList = null;
		try {
			while (invokeFuncs.Count > 0) {
				invokeList = (ArrayList)(invokeFuncs.Dequeue ());
				f = (LuaFunction)(invokeList [0]);
				if (f != null) {
					f.Call (invokeList [1]);
				}
				invokeList.Clear ();
				invokeList = null;
			}
		} catch (System.Exception e) {
			Debug.LogError (f != null ? f.name : "" + "==" + e);
		}
	}

	public virtual void OnDestroy ()
	{
		destoryLua ();
	}

	public void destoryLua ()
	{
		foreach (var cell in luaFuncMap) {
			if (cell.Value != null) {
				cell.Value.Release ();
			}
		}
		luaFuncMap.Clear ();
		if (luaTable != null) {
			luaTable.Dispose ();
			luaTable = null;
		}
	}
	//================================================
	// Fixed invoke 4 lua
	//================================================
	bool _doFixedUpdate = false;

	public bool canFixedInvoke {
		get {
			return _doFixedUpdate;
		}
		set {
			_doFixedUpdate = value;
			if (value) {
				if (fixedInvokeMap == null) {
					fixedInvokeMap = Hashtable.Synchronized (_fixedInvokeMap);
				}
			}
			if (!_doFixedUpdate) {
				frameCounter = 0;
			}
		}
	}
	//	public Dictionary<long, List<LuaFunction>> fixedInvokeMap = new Dictionary<long, List<LuaFunction>> ();
	Hashtable _fixedInvokeMap = new Hashtable ();
	public Hashtable fixedInvokeMap = null;

	public void fixedInvoke4Lua (object luaFunc, float waitSec)
	{
		fixedInvoke (luaFunc, null, waitSec);
	}

	public void fixedInvoke4Lua (object luaFunc, object paras, float waitSec)
	{
		fixedInvoke (luaFunc, paras, waitSec);
	}

	public void fixedInvoke (object callback, object paras, float waitSec)
	{
		if (fixedInvokeMap == null) {
			fixedInvokeMap = Hashtable.Synchronized (_fixedInvokeMap);
		}
		int waiteFrame = Mathf.CeilToInt (waitSec / Time.fixedDeltaTime);
		waiteFrame = waiteFrame <= 0 ? 1 : waiteFrame; //至少有帧
		long key = frameCounter + waiteFrame; 
		object[] content = new object[2];
		//		print (waiteFrame + "===" + key +"====" + luaFunc);
		List<object[]> funcList = (List<object[]>)(fixedInvokeMap [key]);
		if (funcList == null) {
			funcList = new List<object[]> ();
		}
		content [0] = callback;
		content [1] = paras;
		funcList.Add (content);
		fixedInvokeMap [key] = funcList;
	}

	public void cancelFixedInvoke4Lua ()
	{
		cancelFixedInvoke4Lua (null);
	}

	public void cancelFixedInvoke4Lua (object func)
	{
		if (func == null) {
			if (fixedInvokeMap != null) {
				fixedInvokeMap.Clear ();
			}
			return;
		}
		List<object[]> list = null;
		int count = 0;
		object[] content = null;
		foreach (DictionaryEntry item in fixedInvokeMap) {
			list = (List<object[]>)(item.Value);
			count = list.Count;
			for (int i = count - 1; i >= 0; i--) {
				content = list [i];
				if (func.Equals (content [0])) {
					list.RemoveAt (i);
				}
			}
		}
	}

	void doFixedInvoke (long key)
	{
		if (fixedInvokeMap == null && fixedInvokeMap.Count <= 0)
			return;
		object[] content = null;
		List<object[]> funcList = (List<object[]>)(fixedInvokeMap [key]);
		object callback = null;
		if (funcList != null) {
			for (int i = 0; i < funcList.Count; i++) {
				content = funcList [i];
				callback = content [0];
				if (callback is LuaFunction) {
					((LuaFunction)callback).Call (content [1]);
				} else if (callback is string) {
					LuaFunction func = getLuaFunction (callback.ToString ());
					func.Call (content [1]);
				} else if (callback is Callback) {
					((Callback)callback) (content [1]);
				}
			}
			funcList.Clear ();
			funcList = null;
			fixedInvokeMap.Remove (key);
		}
	}

	//================================================
	// FixedUpdate
	//================================================
	public long frameCounter = 0;
	//帧统计
	public virtual void FixedUpdate ()
	{
		if (canFixedInvoke) {
			frameCounter++;
			doFixedInvoke (frameCounter);
		}
	}

	//================================================
	// Update
	//================================================
	ArrayList _invokeByUpdateList = null;

	ArrayList invokeByUpdateList {
		get {
			if (_invokeByUpdateList == null) {
				_invokeByUpdateList = ArrayList.Synchronized (new ArrayList ());
			}
			return _invokeByUpdateList;
		}
	}

	static ListPool listPool = new ListPool ();

	public void invokeByUpdate (object callbakFunc, float sec)
	{
		invokeByUpdate (callbakFunc, null, sec);
	}

	/// <summary>
	/// Invoke4s the lua.
	/// </summary>
	/// <param name="callbakFunc">Callbak func.lua函数</param>
	/// <param name="orgs">Orgs.参数</param>
	/// <param name="sec">Sec.等待时间</param>
	public void invokeByUpdate (object callbakFunc, object orgs, float sec)
	{
		if (callbakFunc == null)
			return;
		NewList list = listPool.borrowObject ();
		list.add (callbakFunc);
		list.add (orgs);
		list.add (Time.unscaledTime + sec);
		invokeByUpdateList.Add (list);
	}

	public void cancelInvokeByUpdate ()
	{
		cancelInvokeByUpdate (null);
	}
	public void cancelInvokeByUpdate (object callbakFunc)
	{
		NewList list = null;
		int count = invokeByUpdateList.Count;
		if (callbakFunc == null) {
			for (int i = 0; i < count; i++) {
				list = (NewList)(invokeByUpdateList [i]);
				listPool.returnObject (list);
			}
			list = null;
			invokeByUpdateList.Clear ();
			return;
		}
		for (int i = count - 1; i >= 0; i--) {
			list = (NewList)(invokeByUpdateList [i]);
			if (callbakFunc.Equals (list [0])) {
				invokeByUpdateList.RemoveAt (i);
				listPool.returnObject (list);
			}
		}
		list = null;
	}

	void doInvokeByUpdate ()
	{
		int count = invokeByUpdateList.Count;
		NewList list = null;
		object callbakFunc;
		object orgs;
		float sec;
		int index = 0;
		LuaFunction func = null;
		while (index < invokeByUpdateList.Count) {
			list = (NewList)(invokeByUpdateList [index]);
			callbakFunc = list [0];
			orgs = list [1];
			sec = (float)(list [2]);
			if (sec <= Time.unscaledTime) {
				if (callbakFunc is string) {
					func = getLuaFunction (callbakFunc.ToString ());
					Utl.doCallback (func, orgs);
				} else if (callbakFunc is LuaFunction) {
					func = (LuaFunction)callbakFunc;
					Utl.doCallback (func, orgs);
				} else if (callbakFunc is Callback) {
					((Callback)callbakFunc) (orgs);
				}
				invokeByUpdateList.RemoveAt (index);
				listPool.returnObject (list);
			} else {
				index++;
			}
		}
		list = null;
	}

	public virtual void Update ()
	{
		if (invokeByUpdateList.Count > 0) {
			doInvokeByUpdate ();
		}
	}
}
