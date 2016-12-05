-- 探索处理
do
    local json = require "cjson";
    CLLExplore = {}
    local smoothFollow4Camera = SCfg.self.mainCamera:GetComponent("CLSmoothFollow");
    local smoothFollow = SCfg.self.mLookatTarget:GetComponent("CLSmoothFollow");
    local smoothFollowTween = SCfg.self.mLookatTarget:GetComponent("MyTween");

    local csSelf = nil;
    local transform = nil;
    local MaxStep = 300;
    local offense = ArrayList();
    -- 方块掉落距离
    local FallTileDistance = 8;

    -- 初始化，只会调用一次
    function CLLExplore.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        --[[
        上的组件：getChild(transform, "offset", "Progress BarHong"):GetComponent("UISlider");
        --]]
    end

    -- 设置数据
    function CLLExplore.setData(paras)
    end

    function CLLExplore.begain()
        -- load player
        local playerData = CLLData.player;

        local curLev = bio2Int(playerData.lev);
        local levAttr = CLLDBCfg.getLevByID(curLev);
        local passStep = bio2Int(levAttr.Steps);
        CLLScene.setMaxLevLength(passStep);
        local roleid = bio2Int(levAttr.Role);

        --        local attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(playerData.gid), 1);
        local attr = CLLDBCfg.getRoleByGIDAndLev(roleid, 1);
        CLRolePool.borrowUnitAsyn(attr.base.PrefabName, CLLExplore.onLoadedPlayer, { playerData, attr, levAttr});
    end

    function CLLExplore.onLoadedPlayer(name, unit, orgs)
        SCfg.self.player = unit;
        local playerData = orgs[1];
        local attr = orgs[2];
        local levAttr = orgs[3];
        ---------------------------------
        SCfg.self.player.transform.parent = CLBattle.self.transform;
        SCfg.self.player.transform.position = CLLScene.getCenterTile().transform.position;
        SCfg.self.player.transform.localScale = Vector3.one * 1.5;
        SCfg.self.player.transform.localEulerAngles = Vector3(0, 90, 0);

        smoothFollowTween:flyout(SCfg.self.player.transform.position, 1, 0, nil, CLLExplore.moveLookatTarget, true);
        smoothFollow4Camera:tween(Vector3(8, 4, 0), Vector3(20, 10, 0), 1, nil);
        NGUITools.SetActive(SCfg.self.player.gameObject, true);
        SCfg.self.player:init(bio2Int(attr.base.GID), 0, 1, true, nil);
        SCfg.self.player.luaTable.setFollower(nil);
        SCfg.self.player.luaTable.setLeader(nil);
        offense:Add(SCfg.self.player);
        ---------------------------------
        CLLExplore.loadFollowers(levAttr);
        ---------------------------------
        -- 地图块掉落
        CLLScene.checkLeftSideTilesTimeout(4);
    end

    function CLLExplore.moveLookatTarget()
        SCfg.self.mLookatTarget.transform.parent = CLBattle.self.transform;
        SCfg.self.mLookatTarget.transform.localEulerAngles = Vector3(0, 70, 0);
        SCfg.self.mLookatTarget.transform.position = SCfg.self.player.transform.position;
        smoothFollow.target = SCfg.self.player.transform;
    end

    -- 加载跟随者
    function CLLExplore.loadFollowers(levAttr)
        if(levAttr.Followers == "") then
            return;
        end

        local list = json.decode(levAttr.Followers);
        if(list ~= nil and #list > 0) then
            local attr = CLLDBCfg.getRoleByGIDAndLev(list[1], 1);
            CLRolePool.borrowUnitAsyn(attr.base.PrefabName, CLLExplore.onLoadedFollower, { playerData, attr, 1, SCfg.self.player, list});
        end
    end

    function CLLExplore.onLoadedFollower(name, unit, orgs)
        local playerData = orgs[1];
        local attr = orgs[2];
        local index = orgs[3];
        local leader = orgs[4];
        local list = orgs[5];

        local tile = CLLScene.getCenterTile();
        local x = tile.mapX - index-1;
        local y = tile.mapY;
        tile = CLLScene.GetTileAt(x, y, 0);
        unit.transform.parent = CLBattle.self.transform;
        unit.transform.position = tile.transform.position;
        unit.transform.localScale = Vector3.one * 1.5;
        unit.transform.localEulerAngles = Vector3(0, 90, 0);

        NGUITools.SetActive(unit.gameObject, true);
        unit:init(list[index], 0, 1, true, nil);
        leader.luaTable.setFollower(unit);
        unit.luaTable.setLeader(leader);
        offense:Add(unit);

        if (index < #list) then
            local attr2 = CLLDBCfg.getRoleByGIDAndLev(list[index+1], 1);
            CLRolePool.borrowUnitAsyn(attr2.base.PrefabName, CLLExplore.onLoadedFollower, { playerData, attr2, index + 1, unit, list});
        end
    end

    function CLLExplore.onMoving(role)
        -- 处理加载地图
        local tile = CLLScene.getTileByLocalPos(role.transform.localPosition);
        if (tile == nil) then return end;
        local x = tile.mapX;
        local right = CLLScene.getRightSide();
        if (right - x < FallTileDistance) then
            local curStep = CLLScene.getSteps();
            local offset = curStep / MaxStep;
            -- 增加一列地块
            CLLScene.addRightSideTiles(0.0, nil, offset);

            -- 重新设置角色移动速度
            local count = offense.Count;
            local unit;
            for i = 0, count - 1 do
                unit = offense:get_Item(i);
                unit.luaTable.setMoveSpeed(bio2Int(unit.luaTable.attr.base.MoveSpeed) * (1 + offset));
            end

            -- 重新计算步数
            local step = CLLExplore.getSteps();
            CLLPExplore.refreshStep(step);

            -- 计算是否能过关
            CLLExplore.checkCanPassLev(step);
        end
    end

    function CLLExplore.getSteps()
        local step = CLLScene.getSteps() - FallTileDistance;
        step = step < 0 and 0 or step;
        return step;
    end

    -- 计算是否能过关
    function CLLExplore.checkCanPassLev(val)
        local playerData = CLLData.player;
        local curLev = bio2Int(playerData.lev);
        local levAttr = CLLDBCfg.getLevByID(curLev);
        local passStep = bio2Int(levAttr.Steps);
        if (val >= passStep) then
            -- 说明过关了
            Time:SetTimeScale(0.5);
            CLLData.setLev(curLev + 1);
            local onFinishFollowCamera = function()
                CLLExplore.clean();
                CLLScene.clean();
                Time:SetTimeScale(1);
                CLPanelManager.getPanelAsy("PanelLevels", onLoadedPanel);
            end
            smoothFollow4Camera:tween(Vector3(20, 10, 0), Vector3(8, 4, 0),  1, onFinishFollowCamera);
        end
    end

    function CLLExplore.someOnDead(csSelf)
        CLLPExplore.onPlayerDead();
        offense:Remove(csSelf);
    end

    function CLLExplore.clean()
        smoothFollow.target = nil;

        local count = offense.Count;
        local unit;
        for i = 0, count - 1 do
            unit = offense:get_Item(i);
            unit:clean();
            NGUITools.SetActive(unit.gameObject, false);
            CLRolePool.returnUnit(unit);
        end
        offense:Clear();
    end

    --------------------------------------------
    return CLLExplore;
end

module("CLLExplore", package.seeall)
