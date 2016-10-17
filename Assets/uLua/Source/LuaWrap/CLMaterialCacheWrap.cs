using System;
using UnityEngine;
using LuaInterface;

public class CLMaterialCacheWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("setMaterial", setMaterial),
			new LuaMethod("getMaterial", getMaterial),
			new LuaMethod("clean", clean),
			new LuaMethod("New", _CreateCLMaterialCache),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaScriptMgr.RegisterLib(L, "CLMaterialCache", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCLMaterialCache(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CLMaterialCache class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(CLMaterialCache);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int setMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 1, typeof(Material));
		CLMaterialCache.setMaterial(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Texture arg0 = (Texture)LuaScriptMgr.GetUnityObject(L, 1, typeof(Texture));
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Material o = CLMaterialCache.getMaterial(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int clean(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		CLMaterialCache.clean();
		return 0;
	}
}

