-- 地表单元
do
    local _cell = {}
    local csSelf = nil;
    local transform = nil;
    local mData = nil;
    local part1;
    local part2;

    -- 初始化，只调用一次
    function _cell.init(csObj)
        csSelf = csObj;
        transform = csSelf.transform;
--        part1 = getChild(transform, "Part1"):GetComponent("MyTween");
--        part1.from = Vector3(-200, 0, 0);
--        part2 = getChild(transform, "Part2"):GetComponent("MyTween");
--        part2.from = Vector3(200, 0, 0);
    end

    -- 显示，
    -- 注意，c#侧不会在调用show时，调用refresh
    function _cell.show(go, data)
        mData = data;
        --[[
        TODO:
        --]]

    end

    -- 注意，c#侧不会在调用show时，调用refresh
    function _cell.refresh(paras)
        --[[
        if(paras == 1) then   -- 刷新血
          -- TODO:
        elseif(paras == 2) then -- 刷新状态
          -- TODO:
        end
        --]]
    end

    -- 取得数据
    function _cell.getData()
        return mData;
    end

    function _cell.playMoving()
--        part1:flyout(Vector3(-400, 0, 0), 0.001,0,nil,_cell.onNotifyLua, false);
--        part2:flyout(Vector3(-200, 0, 0), 0.001,0,nil,_cell.onNotifyLua, false);
    end

    function _cell.onNotifyLua(go)
        local goName = go.name;
        if (goName == "Part1") then
            part1.transform.localPosition = Vector3(200, 0, 0);
            part1:flyout(Vector3(-200, 0, 0), 0.001,0,nil,_cell.onNotifyLua, false);
        elseif (goName == "Part2") then
            part2.transform.localPosition = Vector3(200, 0, 0);
            part2:flyout(Vector3(-200, 0, 0), 0.001,0,nil,_cell.onNotifyLua, false);
        end
    end

    --------------------------------------------
    return _cell;
end
