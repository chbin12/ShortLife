using System;
using System.Collections;
using LuaInterface;

public class CLRolePoolWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("clean", clean),
			new LuaMethod("havePrefab", havePrefab),
			new LuaMethod("isNeedDownload", isNeedDownload),
			new LuaMethod("setPrefab", setPrefab),
			new LuaMethod("borrowUnit", borrowUnit),
			new LuaMethod("borrowUnitAsyn", borrowUnitAsyn),
			new LuaMethod("onFinishSetPrefab", onFinishSetPrefab),
			new LuaMethod("returnUnit", returnUnit),
			new LuaMethod("New", _CreateCLRolePool),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnSetPrefabCallbacks", get_OnSetPrefabCallbacks, set_OnSetPrefabCallbacks),
			new LuaField("poolMap", get_poolMap, set_poolMap),
			new LuaField("prefabMap", get_prefabMap, set_prefabMap),
		};

		LuaScriptMgr.RegisterLib(L, "CLRolePool", typeof(CLRolePool), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLRolePool(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			CLRolePool obj = new CLRolePool();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLRolePool.New");
		}

		return 0;
	}

	static Type classType = typeof(CLRolePool);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnSetPrefabCallbacks(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLRolePool.OnSetPrefabCallbacks);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_poolMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLRolePool.poolMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prefabMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLRolePool.prefabMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnSetPrefabCallbacks(IntPtr L)
	{
		CLRolePool.OnSetPrefabCallbacks = (CLDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(CLDelegate));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_poolMap(IntPtr L)
	{
		CLRolePool.poolMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prefabMap(IntPtr L)
	{
		CLRolePool.prefabMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int clean(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CLRolePool.clean();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int havePrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = CLRolePool.havePrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isNeedDownload(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = CLRolePool.isNeedDownload(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setPrefab(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			CLRolePool.setPrefab(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			object arg2 = LuaScriptMgr.GetVarObject(L, 3);
			CLRolePool.setPrefab(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLRolePool.setPrefab");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowUnit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		SUnit o = CLRolePool.borrowUnit(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowUnitAsyn(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			CLRolePool.borrowUnitAsyn(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			object arg2 = LuaScriptMgr.GetVarObject(L, 3);
			CLRolePool.borrowUnitAsyn(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLRolePool.borrowUnitAsyn");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onFinishSetPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object[] objs0 = LuaScriptMgr.GetArrayObject<object>(L, 1);
		CLRolePool.onFinishSetPrefab(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int returnUnit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 1, typeof(SUnit));
		CLRolePool.returnUnit(arg0);
		return 0;
	}
}

