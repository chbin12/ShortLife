using System;
using UnityEngine;
using System.Collections;
using LuaInterface;

public class CLBattleToolkitWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("getNearestTarget", getNearestTarget),
			new LuaMethod("getNearestTower", getNearestTower),
			new LuaMethod("getTargets", getTargets),
			new LuaMethod("New", _CreateCLBattleToolkit),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "CLBattleToolkit", typeof(CLBattleToolkit), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLBattleToolkit(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			CLBattleToolkit obj = new CLBattleToolkit();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBattleToolkit.New");
		}

		return 0;
	}

	static Type classType = typeof(CLBattleToolkit);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getNearestTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 1, typeof(SUnit));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			SUnit o = CLBattleToolkit.getNearestTarget(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			bool arg3 = LuaScriptMgr.GetBoolean(L, 4);
			SUnit o = CLBattleToolkit.getNearestTarget(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBattleToolkit.getNearestTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getNearestTower(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 1, typeof(SUnit));
		SUnit o = CLBattleToolkit.getNearestTower(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getTargets(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 1, typeof(SUnit));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			ArrayList o = CLBattleToolkit.getTargets(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(bool)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			bool arg2 = LuaDLL.lua_toboolean(L, 3);
			ArrayList o = CLBattleToolkit.getTargets(arg0,arg1,arg2);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(bool)))
		{
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			bool arg2 = LuaDLL.lua_toboolean(L, 3);
			ArrayList o = CLBattleToolkit.getTargets(arg0,arg1,arg2);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLBattleToolkit.getTargets");
		}

		return 0;
	}
}

