-- 场景
do
    local json = require "cjson";
    CLLScene = {}

    local csSelf = nil;
    local transform = nil;
    -- 当在图增加n列后，切换地图的风格
    local SwitchMapStyleLen = 15;
    local oldSwitchMapeStep = 0;

    local mapSizeX = 0;
    local mapSizeY = 0;
    local sideLeft = 0;
    local sideRight = 0;
    local MAX_Length = 30;
    local currTerrain = nil;
    local creatureCount = 0;
    local ground;
    local spin;
    local tiles = {};
    local groundOranments = {};
    local groundOranmentHeadLen = 30;
    local groundOranmentsLeftSide = 0;
    local MaxGroundOranmentLen = 30;
    -- 最右边的一列的状态
    local lastRightSideState = {};
    local mLevLength = 0;
    local topLeftPosition = Vector3.zero;
    local skyOranment;
    local terrainInfor;
    local specifiedShowIndex = 1;
    local specifiedShow;

    -- 初始化，只会调用一次
    function CLLScene.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        spin = transform:GetComponent("Spin");
        spin.enabled = false;

        terrainInfor = CLLScene.getCfg("Terrain.cfg");
    end

    function CLLScene.getCfg(cfgName)
        local upgradeRes = "/upgradeRes"
        if (SCfg.self.isEditMode) then
            upgradeRes = "/upgradeRes4Publish";
        end;
        local priorityPath = PStr.b():a(PathCfg.persistentDataPath):a("/"):a(PathCfg.self.basePath):a(upgradeRes):a("/priority/"):e();
        local cfgBasePath = PStr.b():a(priorityPath):a("cfg/"):e();
        local cfgPath = PStr.b():a(cfgBasePath):a(cfgName):e();
        local cfgStr = File.ReadAllText(cfgPath);
        local cfgResult = json.decode(cfgStr);
        return cfgResult;
    end

    function CLLScene.getCurrTerrain()
        return currTerrain;
    end

    -- 加载无限地图
    function CLLScene.loadInfiniteMap(x, y, speed, tileTimeout, defalutTerrainIndex, onFinishLoadMap)
        if (defalutTerrainIndex == nil) then
            defalutTerrainIndex = -1;
        end
        mapSizeX = x;
        mapSizeY = y;
        sideLeft = 0;
        sideRight = 0;
        groundOranmentsLeftSide = 0;
        topLeftPosition = CLLScene.getTopLeftPosition();
        if (defalutTerrainIndex < 0) then
            currTerrain = terrainInfor[NumEx.NextInt(1, #terrainInfor+1)];
        else
            currTerrain = terrainInfor[defalutTerrainIndex];
        end
        sideRight = mapSizeX - 1;

        specifiedShowIndex = 1;
        specifiedShow = CLLScene.getCfg("MapShowData.json");

        -- set skybox
        if(currTerrain.skyMaterial ~= nil and currTerrain.skyMaterial ~= "") then
            CLMaterialPool.borrowMatAsyn(Path.GetFileNameWithoutExtension(currTerrain.skyMaterial), function(name, mat)
                mat.shader = Shader.Find(mat.shader.name);
                RenderSettings.skybox = mat;
            end);
        end

        -- load sky oranment
        if(currTerrain.skyOranment ~= nil and currTerrain.skyOranment ~= "") then
            local rootPath = PStr.b():a(PathCfg.self.basePath):a("/"):a("upgradeResMedium"):a("/other/things/"):e();
            local thingName = string.gsub(currTerrain.skyOranment, rootPath, "");
            thingName = string.gsub(thingName, ".prefab", "");
            CLThingsPool.borrowObjAsyn(thingName,
                function(name, obj, orgs)
                    skyOranment = obj;
                    skyOranment.transform.parent = csSelf.transform;
                    local reposition = skyOranment:GetComponent("SLGroundReposition");
                    reposition.target = SCfg.self.mainCamera.transform;
                    NGUITools.SetActive(skyOranment, true);
                end);
        end

        -- 地表
        CLLScene.loadGround(currTerrain);

        CLLScene.newMap(currTerrain, speed, onFinishLoadMap);
        --        csSelf:invoke4Lua("checkLeftSideTilesTimeout", tileTimeout, tileTimeout);
    end

    -- 加载地表（水，草地。。。）
    function CLLScene.loadGround(terrainCfg)
        if(terrainCfg == nil or terrainCfg.ground == nil or terrainCfg.ground == "") then
            return;
        end

        local onLoadGround = function(name, obj)
            ground = obj;
            ground.transform.parent = csSelf.transform;
            local groundReposition = ground:GetComponent("SLGroundReposition");
            groundReposition.high = terrainCfg.groundHigh;
            groundReposition.target = SCfg.self.mainCamera.transform;
            NGUITools.SetActive(ground, true);
            local groundCS = ground:GetComponent("CLBaseLua");
            if(groundCS.luaTable == nil) then
                groundCS:setLua();
                groundCS.luaTable.init(groundCS);
            end
        end

        local rootPath = PStr.b():a(PathCfg.self.basePath):a("/"):a("upgradeResMedium"):a("/other/things/"):e();
        local thingName = string.gsub(terrainCfg.ground, rootPath, "");
        thingName = string.gsub(thingName, ".prefab", "");
        CLThingsPool.borrowObjAsyn(thingName, onLoadGround);
    end

    function CLLScene.newMap(terrainInfor, speed, onFinishLoadMap)

        creatureCount = 0;
        local tileInforList = {}
        for y = 0, mapSizeY - 1 do
            for x = 0, mapSizeX - 1 do
--                if((mapShow[x+1])[y+1] == 1) then
                    table.insert(tileInforList, { x, y, terrainInfor, x, false, 1});
--                else
--                    table.insert(tileInforList, { x, y, terrainInfor, x, true, 1});
--                end
            end
        end

        for x = 0, mapSizeX-1+groundOranmentHeadLen do
            CLLScene.addGroundOranment(x, currTerrain);
        end

        local b = 0;
        if (SCfg.self.mode == GameMode.explore) then
            b = 3
        end
        for y = b, mapSizeY - 1 do
            lastRightSideState[sideRight .. "_" .. y] = true;
        end

        CLLScene.createTile({ tileInforList, 1, speed, onFinishLoadMap });
    end

    -- 增加地表的装饰
    function CLLScene.addGroundOranment(side, terrainCfg)
        local x = side;

        if(NumEx.NextBool()) then
            local y1 = NumEx.NextInt(-10, -2);
            CLLScene.doAddGroundOranment(x, y1, terrainCfg)
        end

        if(NumEx.NextBool()) then
            local y2 = NumEx.NextInt(mapSizeY + 2, mapSizeY + 10);
            CLLScene.doAddGroundOranment(x, y2, terrainCfg)
        end
    end

    function CLLScene.doAddGroundOranment(x, y, terrainCfg)
        local index = NumEx.NextInt(1, #(terrainCfg.ornament4Ground)+1);
        local oranmentName = terrainCfg.ornament4Ground[index];

        local rootPath = PStr.b():a(PathCfg.self.basePath):a("/"):a("upgradeResMedium"):a("/other/things/"):e();
        oranmentName = string.gsub(oranmentName, rootPath, "");
        oranmentName = string.gsub(oranmentName, ".prefab", "");

        local onLoadGroundOranment = function(name, obj, orgs)
            local pos = CLLScene.getPos(x, y, 0);
            pos.y = terrainCfg.ornament4GroundHigh;
            obj.transform.parent = csSelf.transform;
            obj.transform.localPosition = pos;
            NGUITools.SetActive(obj, true);
            local baseLua = obj:GetComponent("CLBaseLua");
            if(baseLua ~= nil) then
                if(baseLua.luaTable == nil) then
                    baseLua:setLua();
                    baseLua.luaTable.init(baseLua);
                end
                baseLua.luaTable.distort(true, true);
            end

            local posStr = CLLScene.getPosStr(x, y, 0);
            groundOranments[posStr] = obj;
        end
        CLThingsPool.borrowObjAsyn(oranmentName, onLoadGroundOranment, {x, y, terrainCfg});
    end

    function CLLScene.createTile(orgs)
        local list = orgs[1];
        local i = orgs[2];
        local speed = orgs[3];
        local onFinishLoadMap = orgs[4];

        local data = list[i];
        local x = data[1];
        local y = data[2];
        local terrainInfor = data[3];
        --        local _sideRight = data[4];
        -- 是否空tile
        local isEmptyTile = data[5];
        local fallSpeed = data[6];

        local index = NumEx.NextInt(1, #(terrainInfor.tileTypes)+1);
        local tileType = terrainInfor.tileTypes[index];
        local tileName = "";
        if (isEmptyTile) then
            tileName = "tiles/s_00";
        else
            tileName = "tiles/" .. tileType;
        end

        CLThingsPool.borrowObjAsyn(tileName, CLLScene.onGetTile, { x, y, speed, terrainInfor, list, i, onFinishLoadMap, fallSpeed, isEmptyTile});
    end

    function CLLScene.onGetTile(name, obj, orgs)
        local tile = obj:GetComponent("CLMapTile");
        if (tile.luaTable == nil) then
            tile:setLua();
            tile.luaTable.init(tile);
        end
        local isEmptyTile = orgs[9];
        if(not isEmptyTile) then
            local index = NumEx.NextInt(1, #(currTerrain.tileMaterials)+1);
            tile.luaTable.setBody(currTerrain.tileMaterials[index]);
        end

        CLLScene.addMapTileToMap(tile, orgs[1], orgs[2], 0, orgs[8]);
        local speed = orgs[3];
        local list = orgs[5];
        local i = orgs[6];
        local onFinishLoadMap = orgs[7];
        local data = list[i];
        local _sideRight = data[4];

        if (#(list) > i) then
            csSelf:invoke4Lua("createTile", { list, i + 1, speed, onFinishLoadMap }, speed);
        else
            -- 已经加载完成
            list = nil;
            if (onFinishLoadMap ~= nil) then
                onFinishLoadMap(_sideRight);
            end
        end
    end

    -- 右边增加一列
    function CLLScene.addRightSideTiles(speed, onFinishAddSideTiles, coefficient)
        sideRight = sideRight + 1;
        --切换地图风格
        if (SCfg.self.mode ~= GameMode.explore) then
            local switchStep = NumEx.getIntPart(CLLScene.getSteps() / 10);
            if (switchStep ~= oldSwitchMapeStep) then
                oldSwitchMapeStep = switchStep;
                currTerrain = terrainInfor[NumEx.NextInt(1, #terrainInfor+1)];
            end
        end

        local isEmptyTile = false;
        local tileInforList = {};
        local tmpLastRightSideState = {};

        local list = CLLScene.getTileList(coefficient);
        local fallSpeed = 1;
        if(coefficient ~= nil) then
            fallSpeed = 1- coefficient;
            if(fallSpeed <= 0.2) then
                fallSpeed = 0.2;
            end
        end

        for i = 0, mapSizeY - 1 do
            local key = sideRight .. "_" .. i;
            --            local key2 = (sideRight - 1) .. "_" .. i;

            if (SCfg.self.mode ~= GameMode.explore and (i == 0 or i == 1)) then
                isEmptyTile = false;
                tmpLastRightSideState[key] = false;
            else
                if (list[key]) then
                    isEmptyTile = false;
                    tmpLastRightSideState[key] = true;
                else
                    isEmptyTile = true;
                    tmpLastRightSideState[key] = false;
                end
            end
            table.insert(tileInforList, { sideRight, i, currTerrain, sideRight, isEmptyTile,  fallSpeed});
        end
        lastRightSideState = tmpLastRightSideState;

        CLLScene.createTile({ tileInforList, 1, speed, onFinishAddSideTiles });

        CLLScene.addGroundOranment(sideRight+groundOranmentHeadLen, currTerrain);
    end

    -- 显示特定地图
    function CLLScene.procSpecifiedShow()
        if(specifiedShow == nil or specifiedShowIndex > #(specifiedShow)) then
            specifiedShowIndex = 1;
            specifiedShow = nil;
            return nil;
        end

        local list = specifiedShow[specifiedShowIndex];
        local key = "";
        local ret = {};
        for i, v in ipairs(list) do
            key = PStr.b():a(sideRight):a("_"):a(i-1):e();
            ret[key] = (v == 1 and true or false);
        end
        specifiedShowIndex = specifiedShowIndex + 1;
        return ret;
    end

    --
    function CLLScene.getTileList(coefficient)
        local ret = {};
        if(sideRight == mapSizeX or (mLevLength > 0 and CLLScene.getSteps() >= mLevLength)) then
            for i = 0, mapSizeY-1 do
                ret[PStr.b():a(sideRight):a("_"):a(i):e()] = true;
            end
            return ret;
        end

        ret = CLLScene.procSpecifiedShow();
        if(ret ~= nil) then
            return ret;
        end
        ret = {};
        -- 至少有一个tile
        local n = NumEx.NextInt(1, mapSizeY);
        if (coefficient == nil) then
            n = NumEx.NextInt(1, mapSizeY);
        else
            n = (mapSizeY) * (1 - coefficient);
            local s = n / 2;
            s = s < 1 and 1 or s;
            n = n < 1 and 1 or n;
            n = NumEx.getIntPart(n + 0.5);
            n = NumEx.NextInt(s, n);
            if(n < 2) then
                n = NumEx.NextInt(1, mapSizeY);
            end
        end

        local index = NumEx.NextInt(0, mapSizeY);
        local endIndex = mapSizeY - 1;
        local key = "";
        local key2 = "";
        local key3 = "";
        local key4 = "";
        local key5 = "";
        local key6 = "";
        local count = 0;
        local i = 0;

        -- 先取得一个可以通行的格子
        for _i = 0, endIndex do
            i = _i + index;

            if (i > endIndex) then
                i = i - mapSizeY;
            end

            key = PStr.b():a(sideRight):a("_"):a(i):e();
            key2 = PStr.b():a(sideRight):a("_"):a(i + 1):e();
            key3 = PStr.b():a(sideRight):a("_"):a(i - 1):e();

            key4 = PStr.b():a((sideRight - 1)):a("_"):a(i):e();
            key5 = PStr.b():a((sideRight - 1)):a("_"):a(i + 1):e();
            key6 = PStr.b():a((sideRight - 1)):a("_"):a(i - 1):e();
            if (i % 2 == 1 or i % 2 == -1) then
                if (lastRightSideState[key4]) then
                    count = count + 1;
                    ret[key] = true;
                    index = i + 1;
                    break;
                end
            else
                if (lastRightSideState[key4] or lastRightSideState[key5] or lastRightSideState[key6]) then
                    count = count + 1;
                    ret[key] = true;
                    index = i + 1;
                    break;
                end
            end
        end

        -- 递增或递减
        local random = NumEx.NextBool();
        for _i = 0, endIndex do
            if (random) then
                i = _i;
            else
                i = endIndex - _i;
            end

            i = i + index;

            if (i > endIndex) then
                i = i - endIndex;
            end

            if (count >= n) then
                break;
            end

            key = PStr.b():a(sideRight):a("_"):a(i):e();
            key2 = PStr.b():a(sideRight):a("_"):a((i + 1)):e();
            key3 = PStr.b():a(sideRight):a("_"):a((i - 1)):e();

            key4 = PStr.b():a((sideRight - 1)):a("_"):a(i):e();
            key5 = PStr.b():a((sideRight - 1)):a("_"):a((i + 1)):e();
            key6 = PStr.b():a((sideRight - 1)):a("_"):a((i - 1)):e();

            if (i % 2 == 1 or i % 2 == -1) then
                if (lastRightSideState[key4] or ret[key2] or ret[key3]) then
                    count = count + 1;
                    ret[key] = true;
                end
            else
                if (lastRightSideState[key4] or lastRightSideState[key5] or lastRightSideState[key6] or ret[key2] or ret[key3]) then
                    count = count + 1;
                    ret[key] = true;
                end
            end
        end
        return ret;
    end

    function CLLScene.addMapTileToMap(tile, x, y, z, fallSpeed)
        if (z == nil) then
            z = 0;
        end
        tile:GetComponent("Rigidbody").isKinematic = true;
--        local topLeftPosition = Vector3(-0.5 * mapSizeX * CLMapTile.OffsetX, 0, 0.5 * mapSizeY * CLMapTile.OffsetY);
        local tilePos = CLLScene.getPos(x, y, z);
        tile.mapX = x;
        tile.mapY = y;
        tile.mapZ = z;
        tile.ornamentOnTop = nil;
        tile.mapTileBelow = nil;
        tiles[tile.posStr] = tile;
        tile.transform.parent = transform;
        tile.transform.localEulerAngles = Vector3(-90, 90, 0);
        tile.transform.localScale = Vector3.one * 2;
        tile.luaTable.effectNew(tilePos, fallSpeed, CLLScene.onFinishLoadOneTile);
        NGUITools.SetActive(tile.gameObject, true);
    end

    function CLLScene.getPos(x, y, z)
        y = -y;
        local pos = Vector3(x * CLMapTile.OffsetX, z * CLMapTile.OffsetZ, y * CLMapTile.OffsetY);
        local off = y % 2;
        local rowIndexIsUneven = (off == 1 or off == -1);
        if (rowIndexIsUneven) then
            pos.x = pos.x + CLMapTile.RowOffsetX;
        end
        return pos + topLeftPosition;
    end

    function CLLScene.onFinishLoadOneTile(tile)
        -- tiles/s_00 是一个空六边形
        if (tile.name == "tiles/s_00") then return end;

        if ( SCfg.self.mode ~= GameMode.explore and (tile.mapY == 0 or tile.mapY == 1)) then
            -- 说明是最边上的两排，添加装饰物品
--            local index = NumEx.NextInt(0, currTerrain.ornTypes.Count);
--            local ornName = "tiles/" .. currTerrain.ornTypes[index]:ToString();
--            CLThingsPool.borrowObjAsyn(ornName, CLLScene.addOrnament, tile);
        else
            local createOrn = false;
            if (SCfg.self.mode == GameMode.explore) then
                createOrn = false;
            else
--                createOrn = NumEx.NextBool(0.02);
                createOrn = false;
            end

            if (createOrn) then
                -- 有概率出现障碍物
                local index = NumEx.NextInt(1, #(currTerrain.ornTypes)+1);
                local ornName = "tiles/" .. currTerrain.ornTypes[index];
                CLThingsPool.borrowObjAsyn(ornName, CLLScene.addOrnament, tile);
            else
                creatureCount = creatureCount + 1;
            end
        end
    end

    function CLLScene.addOrnament(name, obj, orgs)
        local orn = obj:GetComponent("CLMapTile");
        if (orn.luaTable == nil) then
            orn:setLua();
            orn.luaTable.init(orn);
        end
        local tile = orgs;
        if (orn ~= nil) then
            orn.mapX = tile.mapX;
            orn.mapY = tile.mapY;
            orn.mapZ = tile.mapZ;
            orn.transform.position = tile.transform.position;
            orn.transform.parent = transform;
            orn.transform.localScale = Vector3.one * 2;
            orn.transform.localEulerAngles = Vector3(-90, 90, 0);
            orn.ornamentOnTop = nil;
            orn.mapTileBelow = tile;
            tile.ornamentOnTop = orn;
            tile.mapTileBelow = nil;
            NGUITools.SetActive(orn.gameObject, true);

            creatureCount = creatureCount + 1;
        end
    end

    -- Checks the left side tiles timeout.
    function CLLScene.checkLeftSideTilesTimeout(tileTimeout)
        local tile;
        local tile2;
        for i = 0, mapSizeY - 1 do
            tile = CLLScene.GetTileAt(sideLeft, i);
            if (tile ~= nil) then
                CLLScene.fallTile(tile);
            end
        end
        sideLeft = sideLeft + 1;

        while (sideLeft < sideRight) do
            tile = CLLScene.GetTileAt(sideLeft, 0);
            tile2 = CLLScene.GetTileAt(sideRight, 0);
            if (tile2 ~= nil and Vector3.Distance(tile.transform.position, tile2.transform.position) > MAX_Length) then
                for i = 0, mapSizeY - 1 do
                    tile = CLLScene.GetTileAt(sideLeft, i);
                    if (tile ~= nil) then
                        CLLScene.fallTile(tile);
                    end
                end
                sideLeft = sideLeft + 1;
            else
                break;
            end
        end

        CLLScene.releaseGroundOranmentWhenSoFar();

        csSelf:invoke4Lua("checkLeftSideTilesTimeout", tileTimeout, tileTimeout);
    end

    function CLLScene.releaseGroundOranmentWhenSoFar()
        while(groundOranmentsLeftSide + MaxGroundOranmentLen < sideRight) do
            local posStr = "";
            local oranment;
            for i=-10, -2 do
                posStr = CLLScene.getPosStr(groundOranmentsLeftSide, i, 0);
                oranment = groundOranments[posStr];
                if(oranment ~= nil) then
                    CLThingsPool.returnObj(oranment.name, oranment.gameObject);
                    NGUITools.SetActive(oranment.gameObject, false);
                    groundOranments[posStr] = nil;
                end
            end
            for i=mapSizeY + 2, mapSizeY + 10 do
                posStr = CLLScene.getPosStr(groundOranmentsLeftSide, i, 0);
                oranment = groundOranments[posStr];
                if(oranment ~= nil) then
                    CLThingsPool.returnObj(oranment.name, oranment.gameObject);
                    NGUITools.SetActive(oranment.gameObject, false);
                    groundOranments[posStr] = nil;
                end
            end

            groundOranmentsLeftSide = groundOranmentsLeftSide + 1;
        end
    end

    function CLLScene.fallTile(tile)
        tiles[tile.posStr] = nil;
        tile.luaTable.effectFall(CLLScene.onFinishFall);
    end

    function CLLScene.onFinishFall(tile)
        NGUITools.SetActive(tile.gameObject, false);
        tile.luaTable.clean();
        CLThingsPool.returnObj(tile.name, tile.gameObject);
    end

    function CLLScene.clean()
        specifiedShowIndex = 1;
        specifiedShow = nil;

        RenderSettings.skybox = nil;
        csSelf:cancelInvoke4Lua("");
        for k, tile in pairs(tiles) do
            NGUITools.SetActive(tile.gameObject, false);
            tile.luaTable.clean();
            CLThingsPool.returnObj(tile.name, tile.gameObject);
        end
        tiles = {};
        oldSwitchMapeStep = 0;

        for k, oranment in pairs(groundOranments) do
            NGUITools.SetActive(oranment.gameObject, false);
--            tile.luaTable.clean();
            CLThingsPool.returnObj(oranment.name, oranment.gameObject);
        end
        groundOranments = {};

        if(ground ~= nil) then
            NGUITools.SetActive(ground, false);
            CLThingsPool.returnObj(ground.name, ground);
            ground = nil;
        end

        if(skyOranment ~= nil) then
            NGUITools.SetActive(skyOranment, false);
            skyOranment.transform.parent = nil;
            CLThingsPool.returnObj(skyOranment.name, skyOranment);
            skyOranment = nil;
        end
    end

    function CLLScene.GetTileAt(x, y, z)
        if (z == nil) then
            z = 0;
        end
        local key = CLLScene.getPosStr(x, y, z);
        local obj = tiles[key];
        if (obj ~= nil) then
            return obj;
        end
        return nil;
    end


    function CLLScene.getPosStr(x, y, z)
        if (z == nil) then
            z = 0;
        end
        return PStr.begin():a(x):a("_"):a(y):a("_"):a(z):e();
    end

    function CLLScene.startSpin()
        spin.enabled = true;
    end

    function CLLScene.stopSpin()
        spin.enabled = fasle;
        transform.localEulerAngles = Vector3.zero;
    end

    -- 取得中心点的tile
    function CLLScene.getCenterTile()
        local x = NumEx.getIntPart(sideLeft + (sideRight - sideLeft) / 2);
        local y = NumEx.getIntPart(mapSizeY / 2);
        return CLLScene.GetTileAt(x, y);
    end

    -- 取得右边列中的一个空闲地块
    function CLLScene.getRightSieFreeTile(defaultSideRight)
        local x = 0;
        if (defaultSideRight == nil) then
            x = sideRight;
        else
            x = defaultSideRight;
        end
        local y = NumEx.NextInt(2, mapSizeY);

        local tile = CLLScene.GetTileAt(x, y);
        if (tile ~= nil) then
            if (tile.CanMoveTo) then
                return tile;
            else
                return CLLScene.getRightSieFreeTile();
            end
        else
            print("x====" .. x .. " ===" .. y);
            return nil;
        end
    end

    -- 取得空闲的tile位置
    function CLLScene.getFreeTile()
        if (sideLeft >= sideRight) then
            return nil;
        end
        local x = NumEx.NextInt(sideLeft, sideRight);
        local y = NumEx.NextInt(0, mapSizeY);

        local tile = CLLScene.GetTileAt(x, y);
        if (tile ~= nil) then
            if (tile.CanMoveTo) then
                return tile;
            else
                return CLLScene.getFreeTile();
            end
        else
            return nil;
        end
    end

    function CLLScene.getTopLeftPosition()
        local _topLeftPosition = Vector3(-0.5 * mapSizeX * CLMapTile.OffsetX, 0, 0.5 * mapSizeY * CLMapTile.OffsetY);
        return _topLeftPosition;
    end
    --[[
    /// <summary>
    /// Gets the map position.根据坐标取得在地图格子中的坐标
    /// </summary>
    /// <returns>
    /// The map position.
    /// </returns>
    /// <param name='pos'>
            /// Position.
    /// </param>
    --]]
    function CLLScene.getMapPos(pos)
        local flagX = 1;
        local flagY = 1
        local flagZ = 1;
        local x = 0
        local y = 0;
        if (pos.x >= 0) then
            flagX = 1;
        else
            flagX = -1;
        end

        if (pos.z >= 0) then
            flagY = 1;
        else
            flagY = -1;
        end
        if (pos.y >= 0) then
            flagZ = 1;
        else
            flagZ = -1;
        end

--        local topLeftPosition = Vector3(-0.5 * mapSizeX * CLMapTile.OffsetX, 0, 0.5 * mapSizeY * CLMapTile.OffsetY);
        --        local tilePos = topLeftPosition + CLLScene.getPos(x, -y, z);
        pos = pos - topLeftPosition;

        --        y = (pos.z + flagY * (CLMapTile.OffsetY / 2)) / CLMapTile.OffsetY;
        y = (pos.z - (CLMapTile.OffsetY / 2)) / CLMapTile.OffsetY;
        y = NumEx.getIntPart(y);

        local off = NumEx.getIntPart(y % 2);
        local rowIndexIsUneven = (off == 1 or off == -1);
        if (rowIndexIsUneven) then
            x = (pos.x + flagX * (-CLMapTile.RowOffsetX + CLMapTile.OffsetX / 2)) / CLMapTile.OffsetX;
        else
            --            x = (pos.x + flagX * (CLMapTile.OffsetX / 2)) / CLMapTile.OffsetX;
            x = (pos.x + (CLMapTile.OffsetX / 2)) / CLMapTile.OffsetX;
        end
        x = NumEx.getIntPart(x);

        local z = ((pos.y + flagZ * (CLMapTile.OffsetZ / 2)) / CLMapTile.OffsetZ);
        z = NumEx.getIntPart(z);
        return Vector3(x, -y, z);
    end

    -- 根据localPosition取得tile
    function CLLScene.getTileByLocalPos(pos)
        local mpos = CLLScene.getMapPos(pos);
        return CLLScene.GetTileAt(mpos.x, mpos.y, mpos.z);
    end

    -- 取得向前移动了几个步
    function CLLScene.getSteps()
        return sideRight - mapSizeX + 1;
    end

    -- 取得边界
    function CLLScene.getRightSide()
        return sideRight;
    end

    -- 设置过关长度
    function CLLScene.setMaxLevLength(val)
        mLevLength = val;
    end

    --------------------------------------------
    return CLLScene;
end
