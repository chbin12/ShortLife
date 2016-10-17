using System;
using LuaInterface;

public class CLRoleStateWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("idel", Getidel),
		new LuaMethod("walkAround", GetwalkAround),
		new LuaMethod("formation", Getformation),
		new LuaMethod("waitAttack", GetwaitAttack),
		new LuaMethod("attack", Getattack),
		new LuaMethod("searchTarget", GetsearchTarget),
		new LuaMethod("beakBack", GetbeakBack),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "CLRoleState", typeof(CLRoleState), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getidel(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.idel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetwalkAround(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.walkAround);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getformation(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.formation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetwaitAttack(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.waitAttack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getattack(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.attack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetsearchTarget(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.searchTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetbeakBack(IntPtr L)
	{
		LuaScriptMgr.Push(L, CLRoleState.beakBack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		CLRoleState o = (CLRoleState)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

