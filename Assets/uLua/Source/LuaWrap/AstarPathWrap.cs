using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AstarPathWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetTagNames", GetTagNames),
			new LuaMethod("FindTagNames", FindTagNames),
			new LuaMethod("GetNextPathID", GetNextPathID),
			new LuaMethod("QueueWorkItemFloodFill", QueueWorkItemFloodFill),
			new LuaMethod("EnsureValidFloodFill", EnsureValidFloodFill),
			new LuaMethod("AddWorkItem", AddWorkItem),
			new LuaMethod("QueueGraphUpdates", QueueGraphUpdates),
			new LuaMethod("UpdateGraphs", UpdateGraphs),
			new LuaMethod("FlushGraphUpdates", FlushGraphUpdates),
			new LuaMethod("FlushWorkItems", FlushWorkItems),
			new LuaMethod("FlushThreadSafeCallbacks", FlushThreadSafeCallbacks),
			new LuaMethod("LogProfiler", LogProfiler),
			new LuaMethod("ResetProfiler", ResetProfiler),
			new LuaMethod("CalculateThreadCount", CalculateThreadCount),
			new LuaMethod("Awake", Awake),
			new LuaMethod("VerifyIntegrity", VerifyIntegrity),
			new LuaMethod("SetUpReferences", SetUpReferences),
			new LuaMethod("OnDestroy", OnDestroy),
			new LuaMethod("FloodFill", FloodFill),
			new LuaMethod("GetNewNodeIndex", GetNewNodeIndex),
			new LuaMethod("InitializeNode", InitializeNode),
			new LuaMethod("DestroyNode", DestroyNode),
			new LuaMethod("BlockUntilPathQueueBlocked", BlockUntilPathQueueBlocked),
			new LuaMethod("Scan", Scan),
			new LuaMethod("ScanLoop", ScanLoop),
			new LuaMethod("ApplyLinks", ApplyLinks),
			new LuaMethod("WaitForPath", WaitForPath),
			new LuaMethod("RegisterSafeUpdate", RegisterSafeUpdate),
			new LuaMethod("StartPath", StartPath),
			new LuaMethod("OnApplicationQuit", OnApplicationQuit),
			new LuaMethod("ReturnPaths", ReturnPaths),
			new LuaMethod("GetNearest", GetNearest),
			new LuaMethod("New", _CreateAstarPath),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Distribution", get_Distribution, null),
			new LuaField("Branch", get_Branch, null),
			new LuaField("HasPro", get_HasPro, null),
			new LuaField("astarData", get_astarData, set_astarData),
			new LuaField("active", get_active, set_active),
			new LuaField("showNavGraphs", get_showNavGraphs, set_showNavGraphs),
			new LuaField("showUnwalkableNodes", get_showUnwalkableNodes, set_showUnwalkableNodes),
			new LuaField("debugMode", get_debugMode, set_debugMode),
			new LuaField("debugFloor", get_debugFloor, set_debugFloor),
			new LuaField("debugRoof", get_debugRoof, set_debugRoof),
			new LuaField("manualDebugFloorRoof", get_manualDebugFloorRoof, set_manualDebugFloorRoof),
			new LuaField("showSearchTree", get_showSearchTree, set_showSearchTree),
			new LuaField("unwalkableNodeDebugSize", get_unwalkableNodeDebugSize, set_unwalkableNodeDebugSize),
			new LuaField("logPathResults", get_logPathResults, set_logPathResults),
			new LuaField("maxNearestNodeDistance", get_maxNearestNodeDistance, set_maxNearestNodeDistance),
			new LuaField("scanOnStartup", get_scanOnStartup, set_scanOnStartup),
			new LuaField("fullGetNearestSearch", get_fullGetNearestSearch, set_fullGetNearestSearch),
			new LuaField("prioritizeGraphs", get_prioritizeGraphs, set_prioritizeGraphs),
			new LuaField("prioritizeGraphsLimit", get_prioritizeGraphsLimit, set_prioritizeGraphsLimit),
			new LuaField("colorSettings", get_colorSettings, set_colorSettings),
			new LuaField("heuristic", get_heuristic, set_heuristic),
			new LuaField("heuristicScale", get_heuristicScale, set_heuristicScale),
			new LuaField("threadCount", get_threadCount, set_threadCount),
			new LuaField("maxFrameTime", get_maxFrameTime, set_maxFrameTime),
			new LuaField("minAreaSize", get_minAreaSize, set_minAreaSize),
			new LuaField("limitGraphUpdates", get_limitGraphUpdates, set_limitGraphUpdates),
			new LuaField("maxGraphUpdateFreq", get_maxGraphUpdateFreq, set_maxGraphUpdateFreq),
			new LuaField("PathsCompleted", get_PathsCompleted, set_PathsCompleted),
			new LuaField("lastScanTime", get_lastScanTime, set_lastScanTime),
			new LuaField("debugPath", get_debugPath, set_debugPath),
			new LuaField("inGameDebugPath", get_inGameDebugPath, set_inGameDebugPath),
			new LuaField("isScanning", get_isScanning, set_isScanning),
			new LuaField("OnAwakeSettings", get_OnAwakeSettings, set_OnAwakeSettings),
			new LuaField("OnGraphPreScan", get_OnGraphPreScan, set_OnGraphPreScan),
			new LuaField("OnGraphPostScan", get_OnGraphPostScan, set_OnGraphPostScan),
			new LuaField("OnPathPreSearch", get_OnPathPreSearch, set_OnPathPreSearch),
			new LuaField("OnPathPostSearch", get_OnPathPostSearch, set_OnPathPostSearch),
			new LuaField("OnPreScan", get_OnPreScan, set_OnPreScan),
			new LuaField("OnPostScan", get_OnPostScan, set_OnPostScan),
			new LuaField("OnLatePostScan", get_OnLatePostScan, set_OnLatePostScan),
			new LuaField("OnGraphsUpdated", get_OnGraphsUpdated, set_OnGraphsUpdated),
			new LuaField("On65KOverflow", get_On65KOverflow, set_On65KOverflow),
			new LuaField("OnDrawGizmosCallback", get_OnDrawGizmosCallback, set_OnDrawGizmosCallback),
			new LuaField("euclideanEmbedding", get_euclideanEmbedding, set_euclideanEmbedding),
			new LuaField("showGraphs", get_showGraphs, set_showGraphs),
			new LuaField("isEditor", get_isEditor, set_isEditor),
			new LuaField("lastUniqueAreaIndex", get_lastUniqueAreaIndex, set_lastUniqueAreaIndex),
			new LuaField("Version", get_Version, null),
			new LuaField("graphs", get_graphs, set_graphs),
			new LuaField("maxNearestNodeDistanceSqr", get_maxNearestNodeDistanceSqr, null),
			new LuaField("debugPathData", get_debugPathData, null),
			new LuaField("NumParallelThreads", get_NumParallelThreads, null),
			new LuaField("IsUsingMultithreading", get_IsUsingMultithreading, null),
			new LuaField("IsAnyGraphUpdatesQueued", get_IsAnyGraphUpdatesQueued, null),
		};

		LuaScriptMgr.RegisterLib(L, "AstarPath", typeof(AstarPath), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAstarPath(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AstarPath class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(AstarPath);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Distribution(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.Distribution);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Branch(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.Branch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HasPro(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.HasPro);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_astarData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name astarData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index astarData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.astarData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_active(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.active);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showNavGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showNavGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showNavGraphs on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showNavGraphs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showUnwalkableNodes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showUnwalkableNodes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showUnwalkableNodes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showUnwalkableNodes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_debugMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugMode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.debugMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_debugFloor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugFloor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugFloor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.debugFloor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_debugRoof(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugRoof");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugRoof on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.debugRoof);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_manualDebugFloorRoof(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name manualDebugFloorRoof");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index manualDebugFloorRoof on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.manualDebugFloorRoof);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showSearchTree(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showSearchTree");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showSearchTree on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showSearchTree);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unwalkableNodeDebugSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unwalkableNodeDebugSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unwalkableNodeDebugSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unwalkableNodeDebugSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_logPathResults(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logPathResults");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logPathResults on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.logPathResults);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxNearestNodeDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxNearestNodeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxNearestNodeDistance on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxNearestNodeDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scanOnStartup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scanOnStartup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scanOnStartup on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scanOnStartup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fullGetNearestSearch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullGetNearestSearch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullGetNearestSearch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.fullGetNearestSearch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prioritizeGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prioritizeGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prioritizeGraphs on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.prioritizeGraphs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prioritizeGraphsLimit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prioritizeGraphsLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prioritizeGraphsLimit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.prioritizeGraphsLimit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colorSettings(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorSettings");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorSettings on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.colorSettings);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_heuristic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name heuristic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index heuristic on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.heuristic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_heuristicScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name heuristicScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index heuristicScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.heuristicScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_threadCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threadCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threadCount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.threadCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxFrameTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxFrameTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxFrameTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxFrameTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minAreaSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minAreaSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minAreaSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.minAreaSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_limitGraphUpdates(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limitGraphUpdates");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limitGraphUpdates on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.limitGraphUpdates);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxGraphUpdateFreq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxGraphUpdateFreq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxGraphUpdateFreq on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxGraphUpdateFreq);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PathsCompleted(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.PathsCompleted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lastScanTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lastScanTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lastScanTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lastScanTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_debugPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugPath on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.debugPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inGameDebugPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inGameDebugPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inGameDebugPath on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.inGameDebugPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isScanning(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isScanning");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isScanning on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isScanning);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnAwakeSettings(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnAwakeSettings);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnGraphPreScan(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnGraphPreScan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnGraphPostScan(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnGraphPostScan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnPathPreSearch(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnPathPreSearch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnPathPostSearch(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnPathPostSearch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnPreScan(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnPreScan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnPostScan(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnPostScan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnLatePostScan(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnLatePostScan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnGraphsUpdated(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.OnGraphsUpdated);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_On65KOverflow(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.On65KOverflow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnDrawGizmosCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnDrawGizmosCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnDrawGizmosCallback on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.OnDrawGizmosCallback);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_euclideanEmbedding(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name euclideanEmbedding");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index euclideanEmbedding on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.euclideanEmbedding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showGraphs on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showGraphs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isEditor(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.isEditor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lastUniqueAreaIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lastUniqueAreaIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lastUniqueAreaIndex on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lastUniqueAreaIndex);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Version(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, AstarPath.Version);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name graphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index graphs on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.graphs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxNearestNodeDistanceSqr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxNearestNodeDistanceSqr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxNearestNodeDistanceSqr on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxNearestNodeDistanceSqr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_debugPathData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugPathData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugPathData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.debugPathData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NumParallelThreads(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.NumParallelThreads);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsUsingMultithreading(IntPtr L)
	{
		LuaScriptMgr.Push(L, AstarPath.IsUsingMultithreading);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsAnyGraphUpdatesQueued(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsAnyGraphUpdatesQueued");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsAnyGraphUpdatesQueued on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsAnyGraphUpdatesQueued);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_astarData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name astarData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index astarData on a nil value");
			}
		}

		obj.astarData = (Pathfinding.AstarData)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.AstarData));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_active(IntPtr L)
	{
		AstarPath.active = (AstarPath)LuaScriptMgr.GetUnityObject(L, 3, typeof(AstarPath));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showNavGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showNavGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showNavGraphs on a nil value");
			}
		}

		obj.showNavGraphs = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showUnwalkableNodes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showUnwalkableNodes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showUnwalkableNodes on a nil value");
			}
		}

		obj.showUnwalkableNodes = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_debugMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugMode on a nil value");
			}
		}

		obj.debugMode = (GraphDebugMode)LuaScriptMgr.GetNetObject(L, 3, typeof(GraphDebugMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_debugFloor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugFloor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugFloor on a nil value");
			}
		}

		obj.debugFloor = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_debugRoof(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugRoof");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugRoof on a nil value");
			}
		}

		obj.debugRoof = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_manualDebugFloorRoof(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name manualDebugFloorRoof");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index manualDebugFloorRoof on a nil value");
			}
		}

		obj.manualDebugFloorRoof = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showSearchTree(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showSearchTree");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showSearchTree on a nil value");
			}
		}

		obj.showSearchTree = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unwalkableNodeDebugSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unwalkableNodeDebugSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unwalkableNodeDebugSize on a nil value");
			}
		}

		obj.unwalkableNodeDebugSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_logPathResults(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logPathResults");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logPathResults on a nil value");
			}
		}

		obj.logPathResults = (PathLog)LuaScriptMgr.GetNetObject(L, 3, typeof(PathLog));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxNearestNodeDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxNearestNodeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxNearestNodeDistance on a nil value");
			}
		}

		obj.maxNearestNodeDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scanOnStartup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scanOnStartup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scanOnStartup on a nil value");
			}
		}

		obj.scanOnStartup = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fullGetNearestSearch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullGetNearestSearch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullGetNearestSearch on a nil value");
			}
		}

		obj.fullGetNearestSearch = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prioritizeGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prioritizeGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prioritizeGraphs on a nil value");
			}
		}

		obj.prioritizeGraphs = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prioritizeGraphsLimit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prioritizeGraphsLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prioritizeGraphsLimit on a nil value");
			}
		}

		obj.prioritizeGraphsLimit = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_colorSettings(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorSettings");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorSettings on a nil value");
			}
		}

		obj.colorSettings = (Pathfinding.AstarColor)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.AstarColor));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_heuristic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name heuristic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index heuristic on a nil value");
			}
		}

		obj.heuristic = (Heuristic)LuaScriptMgr.GetNetObject(L, 3, typeof(Heuristic));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_heuristicScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name heuristicScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index heuristicScale on a nil value");
			}
		}

		obj.heuristicScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_threadCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threadCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threadCount on a nil value");
			}
		}

		obj.threadCount = (ThreadCount)LuaScriptMgr.GetNetObject(L, 3, typeof(ThreadCount));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxFrameTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxFrameTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxFrameTime on a nil value");
			}
		}

		obj.maxFrameTime = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minAreaSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minAreaSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minAreaSize on a nil value");
			}
		}

		obj.minAreaSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_limitGraphUpdates(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limitGraphUpdates");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limitGraphUpdates on a nil value");
			}
		}

		obj.limitGraphUpdates = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxGraphUpdateFreq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxGraphUpdateFreq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxGraphUpdateFreq on a nil value");
			}
		}

		obj.maxGraphUpdateFreq = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PathsCompleted(IntPtr L)
	{
		AstarPath.PathsCompleted = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lastScanTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lastScanTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lastScanTime on a nil value");
			}
		}

		obj.lastScanTime = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_debugPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name debugPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index debugPath on a nil value");
			}
		}

		obj.debugPath = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.Path));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_inGameDebugPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inGameDebugPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inGameDebugPath on a nil value");
			}
		}

		obj.inGameDebugPath = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isScanning(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isScanning");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isScanning on a nil value");
			}
		}

		obj.isScanning = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnAwakeSettings(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnAwakeSettings = (OnVoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnVoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnAwakeSettings = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnGraphPreScan(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnGraphPreScan = (OnGraphDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnGraphDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnGraphPreScan = (param0) =>
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
	static int set_OnGraphPostScan(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnGraphPostScan = (OnGraphDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnGraphDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnGraphPostScan = (param0) =>
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
	static int set_OnPathPreSearch(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnPathPreSearch = (OnPathDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnPathDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnPathPreSearch = (param0) =>
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
	static int set_OnPathPostSearch(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnPathPostSearch = (OnPathDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnPathDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnPathPostSearch = (param0) =>
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
	static int set_OnPreScan(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnPreScan = (OnScanDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnScanDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnPreScan = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnPostScan(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnPostScan = (OnScanDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnScanDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnPostScan = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnLatePostScan(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnLatePostScan = (OnScanDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnScanDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnLatePostScan = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnGraphsUpdated(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.OnGraphsUpdated = (OnScanDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnScanDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.OnGraphsUpdated = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_On65KOverflow(IntPtr L)
	{
		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			AstarPath.On65KOverflow = (OnVoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnVoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			AstarPath.On65KOverflow = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnDrawGizmosCallback(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnDrawGizmosCallback");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnDrawGizmosCallback on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.OnDrawGizmosCallback = (OnVoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(OnVoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.OnDrawGizmosCallback = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_euclideanEmbedding(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name euclideanEmbedding");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index euclideanEmbedding on a nil value");
			}
		}

		obj.euclideanEmbedding = (Pathfinding.EuclideanEmbedding)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.EuclideanEmbedding));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showGraphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showGraphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showGraphs on a nil value");
			}
		}

		obj.showGraphs = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isEditor(IntPtr L)
	{
		AstarPath.isEditor = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lastUniqueAreaIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lastUniqueAreaIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lastUniqueAreaIndex on a nil value");
			}
		}

		obj.lastUniqueAreaIndex = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_graphs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AstarPath obj = (AstarPath)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name graphs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index graphs on a nil value");
			}
		}

		obj.graphs = LuaScriptMgr.GetArrayObject<Pathfinding.NavGraph>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTagNames(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		string[] o = obj.GetTagNames();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindTagNames(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string[] o = AstarPath.FindTagNames();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextPathID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		ushort o = obj.GetNextPathID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int QueueWorkItemFloodFill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.QueueWorkItemFloodFill();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnsureValidFloodFill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.EnsureValidFloodFill();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddWorkItem(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		AstarPath.AstarWorkItem arg0 = (AstarPath.AstarWorkItem)LuaScriptMgr.GetNetObject(L, 2, typeof(AstarPath.AstarWorkItem));
		obj.AddWorkItem(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int QueueGraphUpdates(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.QueueGraphUpdates();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateGraphs(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(LuaTable)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Bounds arg0 = LuaScriptMgr.GetBounds(L, 2);
			obj.UpdateGraphs(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(Pathfinding.GraphUpdateObject)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Pathfinding.GraphUpdateObject arg0 = (Pathfinding.GraphUpdateObject)LuaScriptMgr.GetLuaObject(L, 2);
			obj.UpdateGraphs(arg0);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(LuaTable), typeof(float)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Bounds arg0 = LuaScriptMgr.GetBounds(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			obj.UpdateGraphs(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(Pathfinding.GraphUpdateObject), typeof(float)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Pathfinding.GraphUpdateObject arg0 = (Pathfinding.GraphUpdateObject)LuaScriptMgr.GetLuaObject(L, 2);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 3);
			obj.UpdateGraphs(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AstarPath.UpdateGraphs");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FlushGraphUpdates(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.FlushGraphUpdates();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FlushWorkItems(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.FlushWorkItems(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FlushThreadSafeCallbacks(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.FlushThreadSafeCallbacks();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogProfiler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.LogProfiler();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetProfiler(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.ResetProfiler();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateThreadCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ThreadCount arg0 = (ThreadCount)LuaScriptMgr.GetNetObject(L, 1, typeof(ThreadCount));
		int o = AstarPath.CalculateThreadCount(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Awake(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.Awake();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int VerifyIntegrity(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.VerifyIntegrity();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetUpReferences(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.SetUpReferences();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDestroy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.OnDestroy();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FloodFill(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			obj.FloodFill();
			return 0;
		}
		else if (count == 2)
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Pathfinding.GraphNode arg0 = (Pathfinding.GraphNode)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.GraphNode));
			obj.FloodFill(arg0);
			return 0;
		}
		else if (count == 3)
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Pathfinding.GraphNode arg0 = (Pathfinding.GraphNode)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.GraphNode));
			uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
			obj.FloodFill(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AstarPath.FloodFill");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNewNodeIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		int o = obj.GetNewNodeIndex();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitializeNode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		Pathfinding.GraphNode arg0 = (Pathfinding.GraphNode)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.GraphNode));
		obj.InitializeNode(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyNode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		Pathfinding.GraphNode arg0 = (Pathfinding.GraphNode)LuaScriptMgr.GetNetObject(L, 2, typeof(Pathfinding.GraphNode));
		obj.DestroyNode(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BlockUntilPathQueueBlocked(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.BlockUntilPathQueueBlocked();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Scan(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.Scan();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScanLoop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		OnScanStatus arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (OnScanStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(OnScanStatus));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushValue(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.ScanLoop(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ApplyLinks(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.ApplyLinks();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WaitForPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Pathfinding.Path arg0 = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 1, typeof(Pathfinding.Path));
		AstarPath.WaitForPath(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterSafeUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		OnVoidDelegate arg0 = null;
		LuaTypes funcType1 = LuaDLL.lua_type(L, 1);

		if (funcType1 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (OnVoidDelegate)LuaScriptMgr.GetNetObject(L, 1, typeof(OnVoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 1);
			arg0 = () =>
			{
				func.Call();
			};
		}

		AstarPath.RegisterSafeUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartPath(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Pathfinding.Path arg0 = (Pathfinding.Path)LuaScriptMgr.GetNetObject(L, 1, typeof(Pathfinding.Path));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 2);
		AstarPath.StartPath(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnApplicationQuit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		obj.OnApplicationQuit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReturnPaths(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.ReturnPaths(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNearest(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(LuaTable)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Ray arg0 = LuaScriptMgr.GetRay(L, 2);
			Pathfinding.GraphNode o = obj.GetNearest(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AstarPath), typeof(LuaTable)))
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Pathfinding.NNInfo o = obj.GetNearest(arg0);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 3)
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Pathfinding.NNConstraint arg1 = (Pathfinding.NNConstraint)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.NNConstraint));
			Pathfinding.NNInfo o = obj.GetNearest(arg0,arg1);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 4)
		{
			AstarPath obj = (AstarPath)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AstarPath");
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
			Pathfinding.NNConstraint arg1 = (Pathfinding.NNConstraint)LuaScriptMgr.GetNetObject(L, 3, typeof(Pathfinding.NNConstraint));
			Pathfinding.GraphNode arg2 = (Pathfinding.GraphNode)LuaScriptMgr.GetNetObject(L, 4, typeof(Pathfinding.GraphNode));
			Pathfinding.NNInfo o = obj.GetNearest(arg0,arg1,arg2);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AstarPath.GetNearest");
		}

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

