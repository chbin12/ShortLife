using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLEjectorWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("fire", fire),
			new LuaMethod("New", _CreateCLEjector),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("role", get_role, set_role),
			new LuaField("firePoints", get_firePoints, set_firePoints),
			new LuaField("transform", get_transform, null),
		};

		LuaScriptMgr.RegisterLib(L, "CLEjector", typeof(CLEjector), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLEjector(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLEjector class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLEjector);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLEjector obj = (CLEjector)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.role);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_firePoints(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLEjector obj = (CLEjector)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firePoints");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firePoints on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.firePoints);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transform(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLEjector obj = (CLEjector)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transform on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.transform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLEjector obj = (CLEjector)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role on a nil value");
			}
		}

		obj.role = (SUnit)LuaScriptMgr.GetUnityObject(L, 3, typeof(SUnit));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_firePoints(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLEjector obj = (CLEjector)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firePoints");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firePoints on a nil value");
			}
		}

		obj.firePoints = LuaScriptMgr.GetArrayObject<Transform>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int fire(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 9)
		{
			CLEjector obj = (CLEjector)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLEjector");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			SUnit arg3 = (SUnit)LuaScriptMgr.GetUnityObject(L, 5, typeof(SUnit));
			SUnit arg4 = (SUnit)LuaScriptMgr.GetUnityObject(L, 6, typeof(SUnit));
			object arg5 = LuaScriptMgr.GetVarObject(L, 7);
			object arg6 = LuaScriptMgr.GetVarObject(L, 8);
			object arg7 = LuaScriptMgr.GetVarObject(L, 9);
			obj.fire(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7);
			return 0;
		}
		else if (count == 10)
		{
			CLEjector obj = (CLEjector)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLEjector");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
			SUnit arg4 = (SUnit)LuaScriptMgr.GetUnityObject(L, 6, typeof(SUnit));
			SUnit arg5 = (SUnit)LuaScriptMgr.GetUnityObject(L, 7, typeof(SUnit));
			object arg6 = LuaScriptMgr.GetVarObject(L, 8);
			object arg7 = LuaScriptMgr.GetVarObject(L, 9);
			object arg8 = LuaScriptMgr.GetVarObject(L, 10);
			obj.fire(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLEjector.fire");
		}

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

