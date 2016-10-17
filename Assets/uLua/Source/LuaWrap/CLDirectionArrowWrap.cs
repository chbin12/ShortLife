using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLDirectionArrowWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("init", init),
			new LuaMethod("SetPosition", SetPosition),
			new LuaMethod("SetEndPosition", SetEndPosition),
			new LuaMethod("SetStartPosition", SetStartPosition),
			new LuaMethod("New", _CreateCLDirectionArrow),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("line", get_line, set_line),
			new LuaField("arrow", get_arrow, set_arrow),
			new LuaField("arrowRender", get_arrowRender, set_arrowRender),
		};

		LuaScriptMgr.RegisterLib(L, "CLDirectionArrow", typeof(CLDirectionArrow), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLDirectionArrow(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLDirectionArrow class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLDirectionArrow);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_line(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name line");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index line on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.line);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_arrow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name arrow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index arrow on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.arrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_arrowRender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name arrowRender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index arrowRender on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.arrowRender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_line(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name line");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index line on a nil value");
			}
		}

		obj.line = (LineRenderer)LuaScriptMgr.GetUnityObject(L, 3, typeof(LineRenderer));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_arrow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name arrow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index arrow on a nil value");
			}
		}

		obj.arrow = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_arrowRender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLDirectionArrow obj = (CLDirectionArrow)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name arrowRender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index arrowRender on a nil value");
			}
		}

		obj.arrowRender = (Renderer)LuaScriptMgr.GetUnityObject(L, 3, typeof(Renderer));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		CLDirectionArrow obj = (CLDirectionArrow)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLDirectionArrow");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		Color arg2 = LuaScriptMgr.GetColor(L, 4);
		Color arg3 = LuaScriptMgr.GetColor(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		obj.init(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLDirectionArrow obj = (CLDirectionArrow)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLDirectionArrow");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
		obj.SetPosition(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEndPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLDirectionArrow obj = (CLDirectionArrow)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLDirectionArrow");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		obj.SetEndPosition(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStartPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLDirectionArrow obj = (CLDirectionArrow)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLDirectionArrow");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		obj.SetStartPosition(arg0);
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

