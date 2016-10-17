using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLMyGridWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("refreshPoint", refreshPoint),
			new LuaMethod("New", _CreateCLMyGrid),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("width", get_width, set_width),
			new LuaField("length", get_length, set_length),
			new LuaField("widthInterval", get_widthInterval, set_widthInterval),
			new LuaField("lengthInterval", get_lengthInterval, set_lengthInterval),
			new LuaField("needCreateObj", get_needCreateObj, set_needCreateObj),
			new LuaField("root", get_root, set_root),
			new LuaField("points", get_points, set_points),
			new LuaField("vectors", get_vectors, set_vectors),
			new LuaField("offsetType", get_offsetType, set_offsetType),
			new LuaField("offsetValue", get_offsetValue, set_offsetValue),
		};

		LuaScriptMgr.RegisterLib(L, "CLMyGrid", typeof(CLMyGrid), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLMyGrid(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLMyGrid class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLMyGrid);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_width(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.width);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_length(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name length");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index length on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.length);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_widthInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widthInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widthInterval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.widthInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lengthInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthInterval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lengthInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_needCreateObj(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needCreateObj");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needCreateObj on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.needCreateObj);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_root(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.root);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_points(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name points");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index points on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.points);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vectors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vectors on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.vectors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offsetType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.offsetType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offsetValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetValue on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.offsetValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_width(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}

		obj.width = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_length(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name length");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index length on a nil value");
			}
		}

		obj.length = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_widthInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name widthInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index widthInterval on a nil value");
			}
		}

		obj.widthInterval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lengthInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthInterval on a nil value");
			}
		}

		obj.lengthInterval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needCreateObj(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needCreateObj");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needCreateObj on a nil value");
			}
		}

		obj.needCreateObj = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_root(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}

		obj.root = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_points(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name points");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index points on a nil value");
			}
		}

		obj.points = LuaScriptMgr.GetArrayObject<Transform>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vectors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vectors on a nil value");
			}
		}

		obj.vectors = LuaScriptMgr.GetArrayObject<Vector3>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offsetType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetType on a nil value");
			}
		}

		obj.offsetType = (CLMyGrid.OffsetType)LuaScriptMgr.GetNetObject(L, 3, typeof(CLMyGrid.OffsetType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offsetValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMyGrid obj = (CLMyGrid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetValue on a nil value");
			}
		}

		obj.offsetValue = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int refreshPoint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLMyGrid obj = (CLMyGrid)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLMyGrid");
		obj.refreshPoint();
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

