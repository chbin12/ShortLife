using System;
using UnityEngine;
using System.Collections;
using LuaInterface;
using Object = UnityEngine.Object;

public class SUnitWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("createLifeBar", createLifeBar),
			new LuaMethod("hiddenLifeBar", hiddenLifeBar),
			new LuaMethod("clean", clean),
			new LuaMethod("setMatOutLine", setMatOutLine),
			new LuaMethod("setMatIceEffect", setMatIceEffect),
			new LuaMethod("setMatViolent", setMatViolent),
			new LuaMethod("setMatOutLineWithColor", setMatOutLineWithColor),
			new LuaMethod("setMatToonWithColor", setMatToonWithColor),
			new LuaMethod("getBodyMat", getBodyMat),
			new LuaMethod("setBodyMat", setBodyMat),
			new LuaMethod("setMatToon", setMatToon),
			new LuaMethod("pause", pause),
			new LuaMethod("regain", regain),
			new LuaMethod("Start", Start),
			new LuaMethod("initRandomFactor", initRandomFactor),
			new LuaMethod("fakeRandom", fakeRandom),
			new LuaMethod("init", init),
			new LuaMethod("doSearchTarget", doSearchTarget),
			new LuaMethod("onBeTarget", onBeTarget),
			new LuaMethod("onRelaseTarget", onRelaseTarget),
			new LuaMethod("doAttack", doAttack),
			new LuaMethod("onHurtHP", onHurtHP),
			new LuaMethod("onHurt", onHurt),
			new LuaMethod("onHurtFinish", onHurtFinish),
			new LuaMethod("onDead", onDead),
			new LuaMethod("moveTo", moveTo),
			new LuaMethod("moveToTarget", moveToTarget),
			new LuaMethod("New", _CreateSUnit),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("SCANRange", get_SCANRange, set_SCANRange),
			new LuaField("instanceID", get_instanceID, set_instanceID),
			new LuaField("type", get_type, set_type),
			new LuaField("id", get_id, set_id),
			new LuaField("mTarget", get_mTarget, set_mTarget),
			new LuaField("mAttacker", get_mAttacker, set_mAttacker),
			new LuaField("_sliderLifeBar", get__sliderLifeBar, set__sliderLifeBar),
			new LuaField("isDead", get_isDead, set_isDead),
			new LuaField("hudAnchor", get_hudAnchor, set_hudAnchor),
			new LuaField("state", get_state, set_state),
			new LuaField("isOffense", get_isOffense, set_isOffense),
			new LuaField("isCopyBody", get_isCopyBody, set_isCopyBody),
			new LuaField("mbody", get_mbody, set_mbody),
			new LuaField("matMap", get_matMap, set_matMap),
			new LuaField("RandomFactor", get_RandomFactor, set_RandomFactor),
			new LuaField("lev", get_lev, set_lev),
			new LuaField("lifeBar", get_lifeBar, set_lifeBar),
			new LuaField("isDefense", get_isDefense, null),
			new LuaField("collider", get_collider, null),
			new LuaField("materials", get_materials, null),
		};

		LuaScriptMgr.RegisterLib(L, "SUnit", typeof(SUnit), regs, fields, typeof(CLBehaviour4Lua));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSUnit(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SUnit class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(SUnit);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SCANRange(IntPtr L)
	{
		LuaScriptMgr.Push(L, SUnit.SCANRange);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instanceID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instanceID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instanceID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.instanceID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mTarget on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mAttacker(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mAttacker");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mAttacker on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mAttacker);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__sliderLifeBar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _sliderLifeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _sliderLifeBar on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._sliderLifeBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isDead(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDead");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDead on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isDead);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hudAnchor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hudAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hudAnchor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hudAnchor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_state(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name state");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index state on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.state);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isOffense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isOffense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isOffense on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isOffense);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isCopyBody(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isCopyBody");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isCopyBody on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isCopyBody);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mbody(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mbody");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mbody on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mbody);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matMap(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, SUnit.matMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RandomFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RandomFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RandomFactor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.RandomFactor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lev(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lev");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lev on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lev);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lifeBar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lifeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lifeBar on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lifeBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isDefense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDefense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDefense on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isDefense);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_collider(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name collider");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index collider on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.collider);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_materials(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materials on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.materials);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SCANRange(IntPtr L)
	{
		SUnit.SCANRange = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instanceID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instanceID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instanceID on a nil value");
			}
		}

		obj.instanceID = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mTarget on a nil value");
			}
		}

		obj.mTarget = (SUnit)LuaScriptMgr.GetUnityObject(L, 3, typeof(SUnit));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mAttacker(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mAttacker");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mAttacker on a nil value");
			}
		}

		obj.mAttacker = (SUnit)LuaScriptMgr.GetUnityObject(L, 3, typeof(SUnit));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__sliderLifeBar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _sliderLifeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _sliderLifeBar on a nil value");
			}
		}

		obj._sliderLifeBar = (SBSliderBar)LuaScriptMgr.GetUnityObject(L, 3, typeof(SBSliderBar));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isDead(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDead");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDead on a nil value");
			}
		}

		obj.isDead = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hudAnchor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hudAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hudAnchor on a nil value");
			}
		}

		obj.hudAnchor = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_state(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name state");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index state on a nil value");
			}
		}

		obj.state = (CLRoleState)LuaScriptMgr.GetNetObject(L, 3, typeof(CLRoleState));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isOffense(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isOffense");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isOffense on a nil value");
			}
		}

		obj.isOffense = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isCopyBody(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isCopyBody");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isCopyBody on a nil value");
			}
		}

		obj.isCopyBody = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mbody(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mbody");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mbody on a nil value");
			}
		}

		obj.mbody = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matMap(IntPtr L)
	{
		SUnit.matMap = (Hashtable)LuaScriptMgr.GetNetObject(L, 3, typeof(Hashtable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RandomFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RandomFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RandomFactor on a nil value");
			}
		}

		obj.RandomFactor = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lev(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lev");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lev on a nil value");
			}
		}

		obj.lev = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lifeBar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SUnit obj = (SUnit)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lifeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lifeBar on a nil value");
			}
		}

		obj.lifeBar = (SBSliderBar)LuaScriptMgr.GetUnityObject(L, 3, typeof(SBSliderBar));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int createLifeBar(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.createLifeBar();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int hiddenLifeBar(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.hiddenLifeBar();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int clean(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.clean();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatOutLine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.setMatOutLine();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatIceEffect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.setMatIceEffect();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatViolent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.setMatViolent();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatOutLineWithColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		Color arg1 = LuaScriptMgr.GetColor(L, 3);
		obj.setMatOutLineWithColor(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatToonWithColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		obj.setMatToonWithColor(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getBodyMat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Material o = obj.getBodyMat(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setBodyMat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Material arg1 = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		obj.setBodyMat(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMatToon(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.setMatToon();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.pause();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int regain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.regain();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Start(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.Start();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initRandomFactor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		float o = obj.initRandomFactor();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int fakeRandom(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		int o = obj.fakeRandom(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		bool arg3 = LuaScriptMgr.GetBoolean(L, 5);
		object arg4 = LuaScriptMgr.GetVarObject(L, 6);
		obj.init(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int doSearchTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		SUnit o = obj.doSearchTarget();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onBeTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 2, typeof(SUnit));
		obj.onBeTarget(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onRelaseTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		SUnit arg0 = (SUnit)LuaScriptMgr.GetUnityObject(L, 2, typeof(SUnit));
		obj.onRelaseTarget(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int doAttack(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.doAttack();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurtHP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		object arg1 = LuaScriptMgr.GetVarObject(L, 3);
		obj.onHurtHP(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		object arg1 = LuaScriptMgr.GetVarObject(L, 3);
		SUnit arg2 = (SUnit)LuaScriptMgr.GetUnityObject(L, 4, typeof(SUnit));
		bool o = obj.onHurt(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onHurtFinish(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		SUnit arg1 = (SUnit)LuaScriptMgr.GetUnityObject(L, 3, typeof(SUnit));
		obj.onHurtFinish(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int onDead(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		obj.onDead();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveTo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		obj.moveTo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int moveToTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SUnit obj = (SUnit)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SUnit");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		obj.moveToTarget(arg0);
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

