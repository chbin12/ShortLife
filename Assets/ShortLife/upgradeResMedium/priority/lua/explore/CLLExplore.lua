-- 探索处理
do
    CLLExplore = {}

    local smoothFollow = SCfg.self.mLookatTarget:GetComponent("CLSmoothFollow");

    local csSelf = nil;
    local transform = nil;
    local MaxStep = 300;

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
--        SCfg.self.mLookatTarget.transform.parent = SCfg.self.player.transform;
        SCfg.self.mLookatTarget.transform.parent = CLBattle.self.transform;
        SCfg.self.mLookatTarget.transform.position = SCfg.self.player.transform.position;
        SCfg.self.mLookatTarget.transform.localEulerAngles = Vector3(0, 70, 0);
--        local smoothFollow = SCfg.self.mLookatTarget:GetComponent("CLSmoothFollow");
        smoothFollow.target = SCfg.self.player.transform;

        NGUITools.SetActive(SCfg.self.player.gameObject, true);
        SCfg.self.player:init(bio2Int(playerData.gid), 0, bio2Int(playerData.lev), true, nil);
        ---------------------------------
        -- 地图块掉落
        CLLScene.checkLeftSideTilesTimeout(4);
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
            role.luaTable.setMoveSpeed(bio2Int(role.luaTable.attr.base.MoveSpeed) * (1 + offset));
        end
    end

    function CLLExplore.someOnDead(csSelf)
        CLLPExplore.onPlayerDead();
    end

    function CLLExplore.exit()
        smoothFollow.target = nil;
    end

    --------------------------------------------
    return CLLExplore;
end

module("CLLExplore", package.seeall)
