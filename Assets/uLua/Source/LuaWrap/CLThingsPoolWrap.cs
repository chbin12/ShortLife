using System;
using System.Collections;
using UnityEngine;
using LuaInterface;

public class CLThingsPoolWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("clean", clean),
			new LuaMethod("initPool", initPool),
			new LuaMethod("havePrefab", havePrefab),
			new LuaMethod("setPrefab", setPrefab),
			new LuaMethod("wrapPath", wrapPath),
			new LuaMethod("getObjPool", getObjPool),
			new LuaMethod("borrowObj", borrowObj),
			new LuaMethod("borrowObjAsyn", borrowObjAsyn),
			new LuaMethod("onFinishSetPrefab", onFinishSetPrefab),
			new LuaMethod("returnObj", returnObj),
			new LuaMethod("New", _CreateCLThingsPool),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnSetPrefabCallbacks", get_OnSetPrefabCallbacks, set_OnSetPrefabCallbacks),
			new LuaField("isFinishInitPool", get_isFinishInitPool, set_isFinishInitPool),
			new LuaField("objPubPool", get_objPubPool, set_objPubPool),
			new LuaField("prefabMap", get_prefabMap, set_prefabMap),
		};

		LuaScriptMgr.RegisterLib(L, "CLThingsPool", typeof(CLThingsPool), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLThingsPool(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLThingsPool class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLThingsPool);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnSetPrefabCallbacks(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLThingsPool.OnSetPrefabCallbacks);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isFinishInitPool(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLThingsPool.isFinishInitPool);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_objPubPool(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLThingsPool.objPubPool);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prefabMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLThingsPool.prefabMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnSetPrefabCallbacks(IntPtr L)
	{
		CLThingsPool.OnSetPrefabCallbacks = (CLDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(CLDelegate));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isFinishInitPool(IntPtr L)
	{
		CLThingsPool.isFinishInitPool = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_objPubPool(IntPtr L)
	{
		CLThingsPool.objPubPool = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prefabMap(IntPtr L)
	{
		CLThingsPool.prefabMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int clean(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CLThingsPool.clean();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initPool(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CLThingsPool.initPool();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int havePrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = CLThingsPool.havePrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object arg1 = LuaScriptMgr.GetVarObject(L, 2);
		object arg2 = LuaScriptMgr.GetVarObject(L, 3);
		CLThingsPool.setPrefab(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int wrapPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = CLThingsPool.wrapPath(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getObjPool(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		OtherObjPubPool o = CLThingsPool.getObjPool(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowObj(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o = CLThingsPool.borrowObj(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowObjAsyn(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			CLThingsPool.borrowObjAsyn(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			object arg2 = LuaScriptMgr.GetVarObject(L, 3);
			CLThingsPool.borrowObjAsyn(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLThingsPool.borrowObjAsyn");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onFinishSetPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object[] objs0 = LuaScriptMgr.GetArrayObject<object>(L, 1);
		CLThingsPool.onFinishSetPrefab(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int returnObj(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject arg1 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		CLThingsPool.returnObj(arg0,arg1);
		return 0;
	}
}

