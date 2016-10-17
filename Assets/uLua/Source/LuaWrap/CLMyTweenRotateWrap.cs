using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLMyTweenRotateWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("init", init),
			new LuaMethod("New", _CreateCLMyTweenRotate),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onRotatingCallback", get_onRotatingCallback, set_onRotatingCallback),
			new LuaField("onFinishCallback", get_onFinishCallback, set_onFinishCallback),
		};

		LuaScriptMgr.RegisterLib(L, "CLMyTweenRotate", typeof(CLMyTweenRotate), regs, fields, typeof(TweenRotation));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLMyTweenRotate(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLMyTweenRotate class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLMyTweenRotate);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRotatingCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyTweenRotate obj = (CLMyTweenRotate)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRotatingCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRotatingCallback on a nil value");
			}
		}

		LuaScriptMgr.PushVarObject(L, obj.onRotatingCallback);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFinishCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyTweenRotate obj = (CLMyTweenRotate)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFinishCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFinishCallback on a nil value");
			}
		}

		LuaScriptMgr.PushVarObject(L, obj.onFinishCallback);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRotatingCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyTweenRotate obj = (CLMyTweenRotate)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRotatingCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRotatingCallback on a nil value");
			}
		}

		obj.onRotatingCallback = LuaScriptMgr.GetVarObject(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFinishCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyTweenRotate obj = (CLMyTweenRotate)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFinishCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFinishCallback on a nil value");
			}
		}

		obj.onFinishCallback = LuaScriptMgr.GetVarObject(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLMyTweenRotate obj = (CLMyTweenRotate)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLMyTweenRotate");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		object arg1 = LuaScriptMgr.GetVarObject(L, 3);
		obj.init(arg0,arg1);
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

