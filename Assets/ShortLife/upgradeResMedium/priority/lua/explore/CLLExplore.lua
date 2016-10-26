-- 探索处理
do
    CLLExplore = {}
    local smoothFollow4Camera = SCfg.self.mainCamera:GetComponent("CLSmoothFollow");
    local smoothFollow = SCfg.self.mLookatTarget:GetComponent("CLSmoothFollow");
    local smoothFollowTween = SCfg.self.mLookatTarget:GetComponent("MyTween");

    local csSelf = nil;
    local transform = nil;
    local MaxStep = 300;
    local offense = ArrayList();

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
        local attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(playerData.gid), bio2Int(playerData.lev));
        CLRolePool.borrowUnitAsyn(attr.base.PrefabName, CLLExplore.onLoadedPlayer, { playerData, attr });
    end

    function CLLExplore.onLoadedPlayer(name, unit, orgs)
        SCfg.self.player = unit;
        local playerData = orgs[1];
        --        local attr = orgs[2];
        ---------------------------------
        SCfg.self.player.transform.parent = CLBattle.self.transform;
        SCfg.self.player.transform.position = CLLScene.getCenterTile().transform.position;
        SCfg.self.player.transform.localScale = Vector3.one*1.5;
        SCfg.self.player.transform.localEulerAngles = Vector3(0, 90, 0);

        smoothFollowTween:flyout(SCfg.self.player.transform.position,1, 0, nil, CLLExplore.moveLookatTarget, true);
        smoothFollow4Camera:tween(Vector3(8,4,0), Vector3(10, 17,0), 1, nil);
        NGUITools.SetActive(SCfg.self.player.gameObject, true);
        SCfg.self.player:init(bio2Int(playerData.gid), 0, bio2Int(playerData.lev), true, nil);
        SCfg.self.player.luaTable.setFollower(nil);
        SCfg.self.player.luaTable.setLeader(nil);
        offense:Add(SCfg.self.player);
        ---------------------------------
        CLLExplore.loadFollowers();
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
    function CLLExplore.loadFollowers()
        local playerData = CLLData.player;
        local attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(playerData.gid), bio2Int(playerData.lev));
        CLRolePool.borrowUnitAsyn(attr.base.PrefabName, CLLExplore.onLoadedFollower, { playerData, attr, 1, SCfg.self.player});
    end

    function CLLExplore.onLoadedFollower(name, unit, orgs)
        local playerData = orgs[1];
        local attr = orgs[2];
        local index = orgs[3];
        local leader = orgs[4];

        local tile =  CLLScene.getCenterTile();
        local x = tile.mapX - index;
        local y = tile.mapY;
        tile = CLLScene.GetTileAt(x, y, 0);
        unit.transform.parent = CLBattle.self.transform;
        unit.transform.position = tile.transform.position;
        unit.transform.localScale = Vector3.one*1.5;
        unit.transform.localEulerAngles = Vector3(0, 90, 0);

        NGUITools.SetActive(unit.gameObject, true);
        unit:init(bio2Int(playerData.gid), 0, bio2Int(playerData.lev), true, nil);
        leader.luaTable.setFollower(unit);
        unit.luaTable.setLeader(leader);
        offense:Add(unit);

        if(index < 4) then
            CLRolePool.borrowUnitAsyn(attr.base.PrefabName, CLLExplore.onLoadedFollower, { playerData, attr, index+1, unit});
        end
    end

    function CLLExplore.onMoving(role)
        -- 处理加载地图
        local tile = CLLScene.getTileByLocalPos(role.transform.localPosition);
        if(tile == nil) then return end;
        local x = tile.mapX;
        local right = CLLScene.getRightSide();
        if(right - x < 8) then
            local curStep = CLLScene.getSteps();
            local offset = curStep/MaxStep;
            -- 增加一列地块
            CLLScene.addRightSideTiles(0.0, nil, offset);
            CLLPExplore.refreshStep(curStep);
            -- 重新设置角色移动速度

            local count = offense.Count;
            local unit;
            for i = 0, count - 1 do
                unit = offense:get_Item(i);
                unit.luaTable.setMoveSpeed(bio2Int(unit.luaTable.attr.base.MoveSpeed) * (1 + offset));
            end
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
