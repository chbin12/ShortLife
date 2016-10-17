using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLMapTileWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateCLMapTile),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OffsetX", get_OffsetX, null),
			new LuaField("OffsetY", get_OffsetY, null),
			new LuaField("RowOffsetX", get_RowOffsetX, null),
			new LuaField("OffsetZ", get_OffsetZ, null),
			new LuaField("tileType", get_tileType, set_tileType),
			new LuaField("height", get_height, set_height),
			new LuaField("mapX", get_mapX, set_mapX),
			new LuaField("mapY", get_mapY, set_mapY),
			new LuaField("mapZ", get_mapZ, set_mapZ),
			new LuaField("ornamentOnTop", get_ornamentOnTop, set_ornamentOnTop),
			new LuaField("mapTileBelow", get_mapTileBelow, set_mapTileBelow),
			new LuaField("shakeCurve", get_shakeCurve, set_shakeCurve),
			new LuaField("fallCurve", get_fallCurve, set_fallCurve),
			new LuaField("_canMoveTo", get__canMoveTo, set__canMoveTo),
			new LuaField("tweenPosition", get_tweenPosition, null),
			new LuaField("IsEmpty", get_IsEmpty, null),
			new LuaField("CanMoveTo", get_CanMoveTo, null),
			new LuaField("posStr", get_posStr, null),
			new LuaField("pos", get_pos, null),
		};

		LuaScriptMgr.RegisterLib(L, "CLMapTile", typeof(CLMapTile), regs, fields, typeof(CLBaseLua));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLMapTile(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLMapTile class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLMapTile);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OffsetX(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLMapTile.OffsetX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OffsetY(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLMapTile.OffsetY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RowOffsetX(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLMapTile.RowOffsetX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OffsetZ(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLMapTile.OffsetZ);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tileType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tileType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tileType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tileType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.height);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapX(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapX");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapX on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mapX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapY(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapY");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapY on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mapY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapZ(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapZ");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapZ on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mapZ);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ornamentOnTop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ornamentOnTop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ornamentOnTop on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ornamentOnTop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapTileBelow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapTileBelow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapTileBelow on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mapTileBelow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shakeCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shakeCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shakeCurve on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.shakeCurve);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fallCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fallCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fallCurve on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fallCurve);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__canMoveTo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _canMoveTo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _canMoveTo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._canMoveTo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweenPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweenPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweenPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tweenPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsEmpty(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsEmpty");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsEmpty on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsEmpty);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CanMoveTo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanMoveTo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanMoveTo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CanMoveTo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_posStr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name posStr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index posStr on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.posStr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pos on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tileType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tileType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tileType on a nil value");
			}
		}

		obj.tileType = (MapTileType)LuaScriptMgr.GetNetObject(L, 3, typeof(MapTileType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		obj.height = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapX(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapX");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapX on a nil value");
			}
		}

		obj.mapX = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapY(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapY");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapY on a nil value");
			}
		}

		obj.mapY = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapZ(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapZ");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapZ on a nil value");
			}
		}

		obj.mapZ = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ornamentOnTop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ornamentOnTop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ornamentOnTop on a nil value");
			}
		}

		obj.ornamentOnTop = (CLMapTile)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLMapTile));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapTileBelow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapTileBelow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapTileBelow on a nil value");
			}
		}

		obj.mapTileBelow = (CLMapTile)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLMapTile));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shakeCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shakeCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shakeCurve on a nil value");
			}
		}

		obj.shakeCurve = (AnimationCurve)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationCurve));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fallCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fallCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fallCurve on a nil value");
			}
		}

		obj.fallCurve = (AnimationCurve)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationCurve));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__canMoveTo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLMapTile obj = (CLMapTile)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _canMoveTo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _canMoveTo on a nil value");
			}
		}

		obj._canMoveTo = LuaScriptMgr.GetBoolean(L, 3);
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

