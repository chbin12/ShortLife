using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class SRoleAvataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("getBoneByName", getBoneByName),
			new LuaMethod("setMapindex", setMapindex),
			new LuaMethod("switch2xx", switch2xx),
			new LuaMethod("New", _CreateSRoleAvata),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("bonesNames", get_bonesNames, set_bonesNames),
			new LuaField("bonesList", get_bonesList, set_bonesList),
			new LuaField("bodyPartNames", get_bodyPartNames, set_bodyPartNames),
			new LuaField("bodyParts", get_bodyParts, set_bodyParts),
			new LuaField("bonesMap", get_bonesMap, null),
			new LuaField("animator", get_animator, null),
		};

		LuaScriptMgr.RegisterLib(L, "SRoleAvata", typeof(SRoleAvata), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSRoleAvata(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SRoleAvata class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(SRoleAvata);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bonesNames(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bonesNames");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bonesNames on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bonesNames);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bonesList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bonesList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bonesList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bonesList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bodyPartNames(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyPartNames");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyPartNames on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bodyPartNames);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bodyParts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyParts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyParts on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bodyParts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bonesMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bonesMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bonesMap on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bonesMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

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
	static int set_bonesNames(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bonesNames");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bonesNames on a nil value");
			}
		}

		obj.bonesNames = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bonesList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bonesList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bonesList on a nil value");
			}
		}

		obj.bonesList = (List<Transform>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Transform>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bodyPartNames(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyPartNames");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyPartNames on a nil value");
			}
		}

		obj.bodyPartNames = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bodyParts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SRoleAvata obj = (SRoleAvata)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyParts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyParts on a nil value");
			}
		}

		obj.bodyParts = (List<CLBodyPart>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<CLBodyPart>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getBoneByName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SRoleAvata obj = (SRoleAvata)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAvata");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = obj.getBoneByName(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMapindex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SRoleAvata obj = (SRoleAvata)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAvata");
		obj.setMapindex();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int switch2xx(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SRoleAvata obj = (SRoleAvata)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SRoleAvata");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.switch2xx(arg0,arg1);
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

