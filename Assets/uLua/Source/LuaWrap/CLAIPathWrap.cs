using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLAIPathWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("init", init),
			new LuaMethod("startRepeatSearchPath", startRepeatSearchPath),
			new LuaMethod("stopRepeatSearchPath", stopRepeatSearchPath),
			new LuaMethod("moveToDelay", moveToDelay),
			new LuaMethod("stopMoveToDelay", stopMoveToDelay),
			new LuaMethod("moveTo", moveTo),
			new LuaMethod("canMoveLine", canMoveLine),
			new LuaMethod("moveLine", moveLine),
			new LuaMethod("moveWithPath", moveWithPath),
			new LuaMethod("OnPathComplete", OnPathComplete),
			new LuaMethod("Update", Update),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("OnTargetReached", OnTargetReached),
			new LuaMethod("pause", pause),
			new LuaMethod("regain", regain),
			new LuaMethod("stop", stop),
			new LuaMethod("New", _CreateCLAIPath),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("obstrucLayer", get_obstrucLayer, set_obstrucLayer),
			new LuaField("canRotation", get_canRotation, set_canRotation),
			new LuaField("mOnPathComplete", get_mOnPathComplete, set_mOnPathComplete),
			new LuaField("mOnMoving", get_mOnMoving, set_mOnMoving),
			new LuaField("mOnTargetReached", get_mOnTargetReached, set_mOnTargetReached),
		};

		LuaScriptMgr.RegisterLib(L, "CLAIPath", typeof(CLAIPath), regs, fields, typeof(AIPath));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLAIPath(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLAIPath class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLAIPath);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_obstrucLayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name obstrucLayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index obstrucLayer on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.obstrucLayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canRotation on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.canRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mOnPathComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnPathComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnPathComplete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mOnPathComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mOnMoving(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnMoving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnMoving on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mOnMoving);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mOnTargetReached(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnTargetReached");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnTargetReached on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mOnTargetReached);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_obstrucLayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name obstrucLayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index obstrucLayer on a nil value");
			}
		}

		obj.obstrucLayer = (LayerMask)LuaScriptMgr.GetNetObject(L, 3, typeof(LayerMask));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_canRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canRotation on a nil value");
			}
		}

		obj.canRotation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mOnPathComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnPathComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnPathComplete on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.mOnPathComplete = (OnPathDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnPathDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.mOnPathComplete = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mOnMoving(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnMoving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnMoving on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.mOnMoving = (Callback)LuaScriptMgr.GetNetObject(L, 3, typeof(Callback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.mOnMoving = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushArray(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mOnTargetReached(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLAIPath obj = (CLAIPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mOnTargetReached");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mOnTargetReached on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.mOnTargetReached = (Callback)LuaScriptMgr.GetNetObject(L, 3, typeof(Callback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.mOnTargetReached = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushArray(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		OnPathDelegate arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (OnPathDelegate)LuaScriptMgr.GetNetObject(L, 2, typeof(OnPathDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		Callback arg1 = null;
		LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (Callback)LuaScriptMgr.GetNetObject(L, 3, typeof(Callback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
			arg1 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushArray(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		Callback arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Callback)LuaScriptMgr.GetNetObject(L, 4, typeof(Callback));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushArray(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.init(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int startRepeatSearchPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.startRepeatSearchPath(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int stopRepeatSearchPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.stopRepeatSearchPath();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveToDelay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.moveToDelay(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int stopMoveToDelay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.stopMoveToDelay();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveTo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(CLAIPath), typeof(Transform)))
		{
			CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			obj.moveTo(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(CLAIPath), typeof(LuaTable)))
		{
			CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			obj.moveTo(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLAIPath.moveTo");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int canMoveLine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		bool o = obj.canMoveLine(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveLine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		obj.moveLine(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveWithPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		obj.moveWithPath(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPathComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		Pathfinding.Path arg0 = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.Path));
		obj.OnPathComplete(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.FixedUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnTargetReached(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.OnTargetReached();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.pause();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int regain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.regain();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int stop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLAIPath obj = (CLAIPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLAIPath");
		obj.stop();
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

