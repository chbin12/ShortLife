-- 数据

do
    CLLDB = {};

    -- 数据源地址
    local pathBase = Utl.chgToSKCard(PathCfg.persistentDataPath .. "/.ShortLife/");
    local pathDB = pathBase .. "db/";
    local pathPlayer = pathDB .. "player";

    function CLLDB.savePlayer()
        local m = CLLDB.luaTable2Map(CLLData.player);
        Utl.saveData(pathPlayer, m);
    end

    function CLLDB.getPlayer()
        local m = Utl.getData(pathPlayer);
        return CLLDB.map2LuaTable(m);
    end

    function CLLDB.saveAll()
        print("CLLDB.saveAll");
        if(CLLData.needSave) then
            print("CLLData.needSave");
            CLLData.needSave = false;
            CLLDB.savePlayer();
        end
    end

    function CLLDB.cleanDB()
        if File.Exists(pathPlayer) then
            File.Delete(pathPlayer);
        end;
    end

    function CLLDB.luaTable2Map(d)
        local m = Hashtable();
        for k,v in pairs(d) do
            if(type(v) == "table") then
                m:set_Item(k, CLLDB.luaTable2Map(v));
            else
                m:set_Item(k, v);
            end
        end
        return m;
    end

    function CLLDB.map2LuaTable(d)
        local ret = {};
        local keys = MapEx.keys2List(d);
        local count = keys.Count;
        local k, v;
        for i=0, count -1 do
            k = keys:get_Item(i);
            v = d:get_Item(k);
            if(MapEx.isHashtable(v)) then
                ret[k]= CLLDB.map2LuaTable(v);
            else
                ret[k]= v;
            end
        end
        return ret;
    end

    ------------------------------------
    return CLLDB;
end

module("CLLDB", package.seeall);
