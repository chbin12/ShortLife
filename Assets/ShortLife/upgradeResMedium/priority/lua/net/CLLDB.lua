-- 数据

do
    CLLDB = {};

    -- 数据源地址
    local pathBase = Utl.chgToSKCard(PathCfg.persistentDataPath .. "/.ShortLife/");
    local pathDB = pathBase .. "db/";
    local pathPlayer = pathDB .. "player";

    function CLLDB.savePlayer(d)

    end

    function CLLDB.getPlayer()

    end

    function CLLDB.cleanDB()
    end

    ------------------------------------
    return CLLDB;
end

module("CLLDB", package.seeall);
