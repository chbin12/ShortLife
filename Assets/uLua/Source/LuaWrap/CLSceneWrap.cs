﻿using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLSceneWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("getJson", getJson),
			new LuaMethod("New", _CreateCLScene),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("terrainInfor", get_terrainInfor, set_terrainInfor),
			new LuaField("currTerrain", get_currTerrain, set_currTerrain),
			new LuaField("self", get_self, set_self),
		};

		LuaScriptMgr.RegisterLib(L, "CLScene", typeof(CLScene), regs, fields, typeof(CLBaseLua));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLScene(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLScene class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLScene);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_terrainInfor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLScene obj = (CLScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name terrainInfor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index terrainInfor on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.terrainInfor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currTerrain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLScene obj = (CLScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currTerrain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currTerrain on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.currTerrain);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_self(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLScene.self);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_terrainInfor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLScene obj = (CLScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name terrainInfor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index terrainInfor on a nil value");
			}
		}

		obj.terrainInfor = (List<CLTerrainInfor>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<CLTerrainInfor>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_currTerrain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLScene obj = (CLScene)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currTerrain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currTerrain on a nil value");
			}
		}

		obj.currTerrain = (CLTerrainInfor)LuaScriptMgr.GetNetObject(L, 3, typeof(CLTerrainInfor));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_self(IntPtr L)
	{
		CLScene.self = (CLScene)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLScene));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getJson(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLScene obj = (CLScene)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLScene");
		string o = obj.getJson();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

