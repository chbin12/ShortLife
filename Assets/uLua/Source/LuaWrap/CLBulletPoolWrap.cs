using System;
using System.Collections;
using LuaInterface;

public class CLBulletPoolWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("clean", clean),
			new LuaMethod("havePrefab", havePrefab),
			new LuaMethod("setPrefab", setPrefab),
			new LuaMethod("realseAsset", realseAsset),
			new LuaMethod("borrowBullet", borrowBullet),
			new LuaMethod("borrowBulletAsyn", borrowBulletAsyn),
			new LuaMethod("onFinishSetPrefab", onFinishSetPrefab),
			new LuaMethod("returnBullet", returnBullet),
			new LuaMethod("New", _CreateCLBulletPool),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnSetPrefabCallbacks", get_OnSetPrefabCallbacks, set_OnSetPrefabCallbacks),
			new LuaField("poolMap", get_poolMap, set_poolMap),
			new LuaField("prefabMap", get_prefabMap, set_prefabMap),
		};

		LuaScriptMgr.RegisterLib(L, "CLBulletPool", typeof(CLBulletPool), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLBulletPool(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			CLBulletPool obj = new CLBulletPool();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBulletPool.New");
		}

		return 0;
	}

	static Type classType = typeof(CLBulletPool);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnSetPrefabCallbacks(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLBulletPool.OnSetPrefabCallbacks);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_poolMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLBulletPool.poolMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prefabMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CLBulletPool.prefabMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnSetPrefabCallbacks(IntPtr L)
	{
		CLBulletPool.OnSetPrefabCallbacks = (CLDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(CLDelegate));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_poolMap(IntPtr L)
	{
		CLBulletPool.poolMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prefabMap(IntPtr L)
	{
		CLBulletPool.prefabMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int clean(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CLBulletPool.clean();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int havePrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		bool o = CLBulletPool.havePrefab(arg0);
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
			CLBulletPool.setPrefab(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			object arg2 = LuaScriptMgr.GetVarObject(L, 3);
			CLBulletPool.setPrefab(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBulletPool.setPrefab");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int realseAsset(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		object[] objs0 = LuaScriptMgr.GetParamsObject(L, 1, count);
		CLBulletPool.realseAsset(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowBullet(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		CLBullet o = CLBulletPool.borrowBullet(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int borrowBulletAsyn(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			CLBulletPool.borrowBulletAsyn(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			object arg1 = LuaScriptMgr.GetVarObject(L, 2);
			object arg2 = LuaScriptMgr.GetVarObject(L, 3);
			CLBulletPool.borrowBulletAsyn(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBulletPool.borrowBulletAsyn");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onFinishSetPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object[] objs0 = LuaScriptMgr.GetArrayObject<object>(L, 1);
		CLBulletPool.onFinishSetPrefab(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int returnBullet(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLBullet arg0 = (CLBullet)LuaScriptMgr.GetUnityObject(L, 1, typeof(CLBullet));
		CLBulletPool.returnBullet(arg0);
		return 0;
	}
}

