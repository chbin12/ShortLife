
do
    CLLData = {};
    CLLData.player = nil;
    CLLData.needSave = false;
    -- 场景数据
    --[[
    --key:sceneName
    --val:{}
    --val.stuffs：物品的信息
    --val.lastPos:最后位置
    --val.lastLookAtTargetEulerAngles :最后lookattarget的旋转
    --val.lastSubIndex:最后的子房间
    --]]
    CLLData.sceneData = {};
    --[[
    --背包
    --value.id->物品ID
    --value.num->数量
    --]]
    CLLData.package = {};
    -- 增加物品
    function CLLData.addStuff(id, name)
        local val , i = CLLData.getStuff(id);
        if(val == nil) then
            val = {}
            val.num = 0;
        end
        val.num = val.num + 1;
        val.id = id;
        if(i > 0) then
            CLLData.package[i] = val;
        else
            table.insert(CLLData.package, val);
        end
        local scene = CLLData.getSceneData();
        if(scene ~= nil) then
            if(scene.stuffs == nil) then
                scene.stuffs = {};
            end
            local stuff = scene.stuffs[name];
            if(stuff == nil) then
                stuff = {};
            end
            stuff.isPicked = true;
            scene.stuffs[name] = stuff;
        end
    end
     -- 使用物品
    function CLLData.useStuff(id)
        local val , i = CLLData.getStuff(id);
        if(val ~= nil) then
            val.num = val.num - 1;
            if(val.num <= 0) then
                table.remove(CLLData.package, i)
            end
        end
    end
    -- 取得物品
    function CLLData.getStuff(id)
        for i,v in ipairs(CLLData.package) do
            if(v.id == id) then
                return v, i;
            end
        end
        return nil, 0;
    end

    -- 取得新手步骤
    function CLLData.getGuidStep()
        return bio2Int(CLLData.player.guidStep);
    end

    function CLLData.setGuidStep(step)
        CLLData.player.guidStep = int2Bio(step);
        CLLDB.savePlayer();
    end

    function CLLData.setLev(val)
        CLLData.player.lev = int2Bio(val);
        CLLDB.savePlayer();
    end

    function CLLData.login()
        CLLData.player = CLLDB.getPlayer();
        if(CLLData.player == nil or CLLData.player.gid == nil) then
            CLLData.player = {};
            CLLData.player.gid = int2Bio(1);
            -- 可以理解为关卡进度
            CLLData.player.lev = int2Bio(1);
            CLLData.player.guidStep = int2Bio(0);
            CLLData.needSave = true;
        end
    end

    ------------------------------------
    return CLLData;
end

module("CLLData", package.seeall);
