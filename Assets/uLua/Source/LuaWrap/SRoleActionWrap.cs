using System;
using System.Collections;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class SRoleActionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("getActByName", getActByName),
			new LuaMethod("pause", pause),
			new LuaMethod("regain", regain),
			new LuaMethod("setSpeedAdd", setSpeedAdd),
			new LuaMethod("setAction", setAction),
			new LuaMethod("doSetActionWithCallback", doSetActionWithCallback),
			new LuaMethod("exeCallback", exeCallback),
			new LuaMethod("CallFunc", CallFunc),
			new LuaMethod("New", _CreateSRoleAction),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("currAction", get_currAction, set_currAction),
			new LuaField("currActionValue", get_currActionValue, set_currActionValue),
			new LuaField("animator", get_animator, null),
		};

		LuaScriptMgr.RegisterLib(L, "SRoleAction", typeof(SRoleAction), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSRoleAction(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SRoleAction class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(SRoleAction);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAction obj = (SRoleAction)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currAction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.currAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currActionValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAction obj = (SRoleAction)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currActionValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currActionValue on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.currActionValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAction obj = (SRoleAction)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animator on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.animator);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_currAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAction obj = (SRoleAction)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currAction on a nil value");
			}
		}

		obj.currAction = (SRoleAction.Action)LuaScriptMgr.GetNetObject(L, 3, typeof(SRoleAction.Action));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_currActionValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAction obj = (SRoleAction)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currActionValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currActionValue on a nil value");
			}
		}

		obj.currActionValue = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getActByName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		SRoleAction.Action o = SRoleAction.getActByName(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		obj.pause();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int regain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		obj.regain();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setSpeedAdd(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.setSpeedAdd(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setAction(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(SRoleAction), typeof(int), typeof(object)))
		{
			SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			object arg1 = LuaScriptMgr.GetVarObject(L, 3);
			obj.setAction(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(SRoleAction), typeof(SRoleAction.Action), typeof(object)))
		{
			SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
			SRoleAction.Action arg0 = (SRoleAction.Action)LuaScriptMgr.GetLuaObject(L, 2);
			object arg1 = LuaScriptMgr.GetVarObject(L, 3);
			obj.setAction(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SRoleAction.setAction");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int doSetActionWithCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		ArrayList arg1 = (ArrayList)LuaScriptMgr.GetNetObject(L, 3, typeof(ArrayList));
		obj.doSetActionWithCallback(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int exeCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		IEnumerator o = obj.exeCallback(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CallFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SRoleAction obj = (SRoleAction)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAction");
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 2);
		object o = obj.CallFunc(arg0);
		LuaScriptMgr.PushVarObject(L, o);
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

