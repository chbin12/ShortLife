using System;
using System.Collections;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLBattleWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateCLBattle),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isAutoBattle", get_isAutoBattle, set_isAutoBattle),
			new LuaField("isEndBattle", get_isEndBattle, set_isEndBattle),
			new LuaField("offense", get_offense, set_offense),
			new LuaField("defense", get_defense, set_defense),
			new LuaField("self", get_self, set_self),
		};

		LuaScriptMgr.RegisterLib(L, "CLBattle", typeof(CLBattle), regs, fields, typeof(CLBaseLua));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLBattle(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLBattle class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLBattle);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isAutoBattle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isAutoBattle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isAutoBattle on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isAutoBattle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isEndBattle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isEndBattle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isEndBattle on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isEndBattle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offense on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.offense);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name defense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index defense on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.defense);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_self(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLBattle.self);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isAutoBattle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isAutoBattle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isAutoBattle on a nil value");
			}
		}

		obj.isAutoBattle = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isEndBattle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isEndBattle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isEndBattle on a nil value");
			}
		}

		obj.isEndBattle = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offense on a nil value");
			}
		}

		obj.offense = (ArrayList)LuaScriptMgr.GetNetObject(L, 3, typeof(ArrayList));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLBattle obj = (CLBattle)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name defense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index defense on a nil value");
			}
		}

		obj.defense = (ArrayList)LuaScriptMgr.GetNetObject(L, 3, typeof(ArrayList));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_self(IntPtr L)
	{
		CLBattle.self = (CLBattle)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLBattle));
		return 0;
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

