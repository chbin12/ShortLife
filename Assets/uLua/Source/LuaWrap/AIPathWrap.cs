using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AIPathWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnDisable", OnDisable),
			new LuaMethod("TrySearchPath", TrySearchPath),
			new LuaMethod("SearchPath", SearchPath),
			new LuaMethod("OnTargetReached", OnTargetReached),
			new LuaMethod("OnPathComplete", OnPathComplete),
			new LuaMethod("GetFeetPosition", GetFeetPosition),
			new LuaMethod("Update", Update),
			new LuaMethod("New", _CreateAIPath),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("repathRate", get_repathRate, set_repathRate),
			new LuaField("target", get_target, set_target),
			new LuaField("canSearch", get_canSearch, set_canSearch),
			new LuaField("canMove", get_canMove, set_canMove),
			new LuaField("speed", get_speed, set_speed),
			new LuaField("turningSpeed", get_turningSpeed, set_turningSpeed),
			new LuaField("slowdownDistance", get_slowdownDistance, set_slowdownDistance),
			new LuaField("pickNextWaypointDist", get_pickNextWaypointDist, set_pickNextWaypointDist),
			new LuaField("forwardLook", get_forwardLook, set_forwardLook),
			new LuaField("endReachedDistance", get_endReachedDistance, set_endReachedDistance),
			new LuaField("closestOnPathCheck", get_closestOnPathCheck, set_closestOnPathCheck),
			new LuaField("TargetReached", get_TargetReached, null),
		};

		LuaScriptMgr.RegisterLib(L, "AIPath", typeof(AIPath), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAIPath(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AIPath class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(AIPath);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_repathRate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name repathRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index repathRate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.repathRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.target);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canSearch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canSearch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canSearch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.canSearch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canMove(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canMove on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.canMove);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_speed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index speed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.speed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_turningSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name turningSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index turningSpeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.turningSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_slowdownDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slowdownDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slowdownDistance on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.slowdownDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pickNextWaypointDist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pickNextWaypointDist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pickNextWaypointDist on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pickNextWaypointDist);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forwardLook(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forwardLook");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forwardLook on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.forwardLook);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_endReachedDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endReachedDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endReachedDistance on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.endReachedDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_closestOnPathCheck(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name closestOnPathCheck");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index closestOnPathCheck on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.closestOnPathCheck);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TargetReached(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TargetReached");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TargetReached on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.TargetReached);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_repathRate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name repathRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index repathRate on a nil value");
			}
		}

		obj.repathRate = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		obj.target = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_canSearch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canSearch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canSearch on a nil value");
			}
		}

		obj.canSearch = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_canMove(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canMove on a nil value");
			}
		}

		obj.canMove = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_speed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index speed on a nil value");
			}
		}

		obj.speed = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_turningSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name turningSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index turningSpeed on a nil value");
			}
		}

		obj.turningSpeed = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_slowdownDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slowdownDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slowdownDistance on a nil value");
			}
		}

		obj.slowdownDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pickNextWaypointDist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pickNextWaypointDist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pickNextWaypointDist on a nil value");
			}
		}

		obj.pickNextWaypointDist = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_forwardLook(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forwardLook");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forwardLook on a nil value");
			}
		}

		obj.forwardLook = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_endReachedDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endReachedDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endReachedDistance on a nil value");
			}
		}

		obj.endReachedDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_closestOnPathCheck(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIPath obj = (AIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name closestOnPathCheck");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index closestOnPathCheck on a nil value");
			}
		}

		obj.closestOnPathCheck = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDisable(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		obj.OnDisable();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrySearchPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		float o = obj.TrySearchPath();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SearchPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		obj.SearchPath();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnTargetReached(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		obj.OnTargetReached();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPathComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		Pathfinding.Path arg0 = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.Path));
		obj.OnPathComplete(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFeetPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		Vector3 o = obj.GetFeetPosition();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIPath obj = (AIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AIPath");
		obj.Update();
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

