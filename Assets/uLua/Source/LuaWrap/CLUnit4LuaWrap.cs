using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CLUnit4LuaWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("init", init),
			new LuaMethod("setLua", setLua),
			new LuaMethod("initGetLuaFunc", initGetLuaFunc),
			new LuaMethod("Start", Start),
			new LuaMethod("onPathComplete", onPathComplete),
			new LuaMethod("onMoving", onMoving),
			new LuaMethod("onArrived", onArrived),
			new LuaMethod("doSearchTarget", doSearchTarget),
			new LuaMethod("onBeTarget", onBeTarget),
			new LuaMethod("onRelaseTarget", onRelaseTarget),
			new LuaMethod("onHurt", onHurt),
			new LuaMethod("onHurtHP", onHurtHP),
			new LuaMethod("onHurtFinish", onHurtFinish),
			new LuaMethod("doAttack", doAttack),
			new LuaMethod("onDead", onDead),
			new LuaMethod("moveTo", moveTo),
			new LuaMethod("moveToDelay", moveToDelay),
			new LuaMethod("moveToTarget", moveToTarget),
			new LuaMethod("pause", pause),
			new LuaMethod("regain", regain),
			new LuaMethod("setUnitTrans", setUnitTrans),
			new LuaMethod("setOutlineShader", setOutlineShader),
			new LuaMethod("setToonShader", setToonShader),
			new LuaMethod("setGrayShader", setGrayShader),
			new LuaMethod("setToonShaderColor", setToonShaderColor),
			new LuaMethod("setRimLight", setRimLight),
			new LuaMethod("New", _CreateCLUnit4Lua),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isFinishSetLua", get_isFinishSetLua, set_isFinishSetLua),
			new LuaField("isTower", get_isTower, set_isTower),
			new LuaField("action", get_action, set_action),
			new LuaField("tween", get_tween, set_tween),
			new LuaField("aiPath", get_aiPath, set_aiPath),
			new LuaField("ejector", get_ejector, set_ejector),
			new LuaField("avata", get_avata, set_avata),
			new LuaField("shadow", get_shadow, set_shadow),
			new LuaField("shadow2", get_shadow2, set_shadow2),
			new LuaField("lfinit", get_lfinit, set_lfinit),
			new LuaField("lfonPathComplete", get_lfonPathComplete, set_lfonPathComplete),
			new LuaField("lfonMoving", get_lfonMoving, set_lfonMoving),
			new LuaField("lfonArrived", get_lfonArrived, set_lfonArrived),
			new LuaField("lfdoSearchTarget", get_lfdoSearchTarget, set_lfdoSearchTarget),
			new LuaField("lfonBeTarget", get_lfonBeTarget, set_lfonBeTarget),
			new LuaField("lfonRelaseTarget", get_lfonRelaseTarget, set_lfonRelaseTarget),
			new LuaField("lfonHurt", get_lfonHurt, set_lfonHurt),
			new LuaField("lfonHurtHP", get_lfonHurtHP, set_lfonHurtHP),
			new LuaField("lfonHurtFinish", get_lfonHurtFinish, set_lfonHurtFinish),
			new LuaField("lfdoAttack", get_lfdoAttack, set_lfdoAttack),
			new LuaField("lfonDead", get_lfonDead, set_lfonDead),
			new LuaField("lfpause", get_lfpause, set_lfpause),
			new LuaField("lfregain", get_lfregain, set_lfregain),
		};

		LuaScriptMgr.RegisterLib(L, "CLUnit4Lua", typeof(CLUnit4Lua), regs, fields, typeof(SUnit));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLUnit4Lua(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLUnit4Lua class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLUnit4Lua);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isFinishSetLua(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isFinishSetLua");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isFinishSetLua on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isFinishSetLua);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isTower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isTower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isTower on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isTower);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_action(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name action");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index action on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.action);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tween(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tween");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tween on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tween);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_aiPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name aiPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index aiPath on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.aiPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ejector(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ejector");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ejector on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ejector);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_avata(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name avata");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index avata on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.avata);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadow on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shadow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadow2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadow2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadow2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shadow2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfinit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfinit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfinit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfinit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonPathComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonPathComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonPathComplete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonPathComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonMoving(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonMoving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonMoving on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonMoving);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonArrived(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonArrived");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonArrived on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonArrived);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfdoSearchTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfdoSearchTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfdoSearchTarget on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfdoSearchTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonBeTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonBeTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonBeTarget on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonBeTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonRelaseTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonRelaseTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonRelaseTarget on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonRelaseTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonHurt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonHurt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonHurtHP(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurtHP");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurtHP on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonHurtHP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonHurtFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurtFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurtFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonHurtFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfdoAttack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfdoAttack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfdoAttack on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfdoAttack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfonDead(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonDead");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonDead on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfonDead);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfpause(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfpause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfpause on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfpause);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lfregain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfregain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfregain on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lfregain);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isFinishSetLua(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isFinishSetLua");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isFinishSetLua on a nil value");
			}
		}

		obj.isFinishSetLua = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isTower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isTower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isTower on a nil value");
			}
		}

		obj.isTower = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_action(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name action");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index action on a nil value");
			}
		}

		obj.action = (SRoleAction)LuaScriptMgr.GetUnityObject(L, 3, typeof(SRoleAction));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tween(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tween");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tween on a nil value");
			}
		}

		obj.tween = (MyTween)LuaScriptMgr.GetUnityObject(L, 3, typeof(MyTween));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_aiPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name aiPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index aiPath on a nil value");
			}
		}

		obj.aiPath = (CLAIPath)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLAIPath));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ejector(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ejector");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ejector on a nil value");
			}
		}

		obj.ejector = (CLEjector)LuaScriptMgr.GetUnityObject(L, 3, typeof(CLEjector));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_avata(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name avata");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index avata on a nil value");
			}
		}

		obj.avata = (SRoleAvata)LuaScriptMgr.GetUnityObject(L, 3, typeof(SRoleAvata));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadow on a nil value");
			}
		}

		obj.shadow = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadow2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadow2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadow2 on a nil value");
			}
		}

		obj.shadow2 = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfinit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfinit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfinit on a nil value");
			}
		}

		obj.lfinit = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonPathComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonPathComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonPathComplete on a nil value");
			}
		}

		obj.lfonPathComplete = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonMoving(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonMoving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonMoving on a nil value");
			}
		}

		obj.lfonMoving = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonArrived(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonArrived");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonArrived on a nil value");
			}
		}

		obj.lfonArrived = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfdoSearchTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfdoSearchTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfdoSearchTarget on a nil value");
			}
		}

		obj.lfdoSearchTarget = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonBeTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonBeTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonBeTarget on a nil value");
			}
		}

		obj.lfonBeTarget = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonRelaseTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonRelaseTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonRelaseTarget on a nil value");
			}
		}

		obj.lfonRelaseTarget = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonHurt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurt on a nil value");
			}
		}

		obj.lfonHurt = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonHurtHP(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurtHP");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurtHP on a nil value");
			}
		}

		obj.lfonHurtHP = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonHurtFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonHurtFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonHurtFinish on a nil value");
			}
		}

		obj.lfonHurtFinish = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfdoAttack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfdoAttack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfdoAttack on a nil value");
			}
		}

		obj.lfdoAttack = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfonDead(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfonDead");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfonDead on a nil value");
			}
		}

		obj.lfonDead = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfpause(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfpause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfpause on a nil value");
			}
		}

		obj.lfpause = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lfregain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lfregain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lfregain on a nil value");
			}
		}

		obj.lfregain = LuaScriptMgr.GetLuaFunction(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		bool arg3 = LuaScriptMgr.GetBoolean(L, 5);
		object arg4 = LuaScriptMgr.GetVarObject(L, 6);
		obj.init(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setLua(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.setLua();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initGetLuaFunc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.initGetLuaFunc();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Start(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.Start();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onPathComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Pathfinding.Path arg0 = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.Path));
		obj.onPathComplete(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onMoving(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		object[] objs0 = LuaScriptMgr.GetArrayObject<object>(L, 2);
		obj.onMoving(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onArrived(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		object[] objs0 = LuaScriptMgr.GetArrayObject<object>(L, 2);
		obj.onArrived(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int doSearchTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		SUnit o = obj.doSearchTarget();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onBeTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 2, typeof(SUnit));
		obj.onBeTarget(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onRelaseTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 2, typeof(SUnit));
		obj.onRelaseTarget(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		object arg1 = LuaScriptMgr.GetVarObject(L, 3);
		SUnit arg2 = (SUnit)LuaScriptMgr.GetUnityObject(L, 4, typeof(SUnit));
		bool o = obj.onHurt(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurtHP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		object arg1 = LuaScriptMgr.GetVarObject(L, 3);
		obj.onHurtHP(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurtFinish(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		SUnit arg1 = (SUnit)LuaScriptMgr.GetUnityObject(L, 3, typeof(SUnit));
		obj.onHurtFinish(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int doAttack(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.doAttack();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onDead(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.onDead();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveTo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			obj.moveTo(arg0);
			return 0;
		}
		else if (count == 3)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			obj.moveTo(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			obj.moveTo(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLUnit4Lua.moveTo");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveToDelay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		obj.moveToDelay(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveToTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			obj.moveToTarget(arg0);
			return 0;
		}
		else if (count == 3)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			obj.moveToTarget(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			obj.moveToTarget(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
			obj.moveToTarget(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLUnit4Lua.moveToTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.pause();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int regain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.regain();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setUnitTrans(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		obj.setUnitTrans(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setOutlineShader(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		Color arg1 = LuaScriptMgr.GetColor(L, 3);
		obj.setOutlineShader(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setToonShader(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		obj.setToonShader(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setGrayShader(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		obj.setGrayShader();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setToonShaderColor(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Color arg0 = LuaScriptMgr.GetColor(L, 2);
			Color arg1 = LuaScriptMgr.GetColor(L, 3);
			obj.setToonShaderColor(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
			Color arg0 = LuaScriptMgr.GetColor(L, 2);
			Color arg1 = LuaScriptMgr.GetColor(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			obj.setToonShaderColor(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CLUnit4Lua.setToonShaderColor");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setRimLight(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		CLUnit4Lua obj = (CLUnit4Lua)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CLUnit4Lua");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		Color arg1 = LuaScriptMgr.GetColor(L, 3);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
		obj.setRimLight(arg0,arg1,arg2);
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

