-- xx单元
do
    local _cell = {}
    local csSelf = nil;
    local transform = nil;
    local mData = nil;
    local label = nil;
    local backGround = nil;
    local attr = nil;


    -- 初始化，只调用一次
    function _cell.init(csObj)
        csSelf = csObj;
        transform = csSelf.transform;
        label = getChild(transform, "Label"):GetComponent("UILabel");
        backGround = getChild(transform, "Background"):GetComponent("UISprite");
    end

    -- 显示，
    -- 注意，c#侧不会在调用show时，调用refresh
    function _cell.show(go, data)
        mData = data;
        attr = CLLDBCfg.getLevByID(mData);
        label.text = Localization.Get(attr.NameKey);
        local lev = bio2Int(CLLData.player.lev);
        if (lev >= mData) then
            backGround:unSetGray();
        else
            backGround:setGray();
        end
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

    --------------------------------------------
    return _cell;
end
