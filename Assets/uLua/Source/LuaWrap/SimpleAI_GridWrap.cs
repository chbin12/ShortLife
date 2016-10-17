using System;
using UnityEngine;
using LuaInterface;

public class SimpleAI_GridWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Awake", Awake),
			new LuaMethod("DebugDraw", DebugDraw),
			new LuaMethod("GetNearestCellCenter", GetNearestCellCenter),
			new LuaMethod("GetCellCenter", GetCellCenter),
			new LuaMethod("GetCellPosition", GetCellPosition),
			new LuaMethod("GetCellIndex", GetCellIndex),
			new LuaMethod("GetCellIndexClamped", GetCellIndexClamped),
			new LuaMethod("GetCellBounds", GetCellBounds),
			new LuaMethod("GetGridBounds", GetGridBounds),
			new LuaMethod("GetRow", GetRow),
			new LuaMethod("GetColumn", GetColumn),
			new LuaMethod("IsInBounds", IsInBounds),
			new LuaMethod("New", _CreateSimpleAI_Grid),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Width", get_Width, null),
			new LuaField("Height", get_Height, null),
			new LuaField("Origin", get_Origin, null),
			new LuaField("NumberOfCells", get_NumberOfCells, null),
			new LuaField("Left", get_Left, null),
			new LuaField("Right", get_Right, null),
			new LuaField("Top", get_Top, null),
			new LuaField("Bottom", get_Bottom, null),
			new LuaField("CellSize", get_CellSize, null),
		};

		LuaScriptMgr.RegisterLib(L, "SimpleAI.Grid", typeof(SimpleAI.Grid), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSimpleAI_Grid(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SimpleAI.Grid obj = new SimpleAI.Grid();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SimpleAI.Grid.New");
		}

		return 0;
	}

	static Type classType = typeof(SimpleAI.Grid);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Width(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Width on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Width);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Height on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Height);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Origin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Origin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Origin on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Origin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NumberOfCells(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NumberOfCells");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NumberOfCells on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.NumberOfCells);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Left(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Left");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Left on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Right(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Right on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Top(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Top");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Top on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Top);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Bottom(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Bottom");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Bottom on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Bottom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CellSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CellSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CellSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CellSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Awake(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		obj.Awake(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DebugDraw(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 4);
		Color arg4 = LuaScriptMgr.GetColor(L, 5);
		SimpleAI.Grid.DebugDraw(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNearestCellCenter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		Vector3 o = obj.GetNearestCellCenter(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCellCenter(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			Vector3 o = obj.GetCellCenter(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			Vector3 o = obj.GetCellCenter(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SimpleAI.Grid.GetCellCenter");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCellPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Vector3 o = obj.GetCellPosition(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCellIndex(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			int o = obj.GetCellIndex(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int o = obj.GetCellIndex(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SimpleAI.Grid.GetCellIndex");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCellIndexClamped(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		int o = obj.GetCellIndexClamped(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCellBounds(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Bounds o = obj.GetCellBounds(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGridBounds(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		Bounds o = obj.GetGridBounds();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRow(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetRow(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetColumn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetColumn(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInBounds(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(SimpleAI.Grid), typeof(LuaTable)))
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			bool o = obj.IsInBounds(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(SimpleAI.Grid), typeof(int)))
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			bool o = obj.IsInBounds(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			SimpleAI.Grid obj = (SimpleAI.Grid)LuaScriptMgr.GetNetObjectSelf(L, 1, "SimpleAI.Grid");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			bool o = obj.IsInBounds(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SimpleAI.Grid.IsInBounds");
		}

		return 0;
	}
}

