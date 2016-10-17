-- xx单元
do
    local uiCell = {}
    local csSelf = nil
    local transform = nil
    local mData = nil
    local label = nil
    local Point = nil;
    local attr = nil;
    local stuffGo = nil;

    -- 初始化，只调用一次
    function uiCell.init(csObj)
        csSelf = csObj
        transform = csSelf.transform;
        label = getChild(transform, "Label"):GetComponent("UILabel");
        Point = getChild(transform, "Point");
    end

    -- 显示，
    -- 注意，c#侧不会在调用show时，调用refresh
    function uiCell.show(go, data)
        mData = data;
        attr = CLLDBCfg.getStuffByID(mData.id)
        label.text = Localization.Get(attr.NameKey);
        if(stuffGo ~= nil) then
            uiCell.releaseStuff();
        end
        CLThingsPool.borrowObjAsyn(attr.PrefabName, uiCell.onGetStuff);
    end

    function uiCell.onGetStuff(goName, go, paras)
        stuffGo = go;
        stuffGo.transform.parent = Point;
        stuffGo.transform.localPosition = Vector3.zero;
        stuffGo.transform.localScale = Vector3.one;
        stuffGo.transform.localEulerAngles = Vector3.zero;
        NGUITools.SetLayer(stuffGo, LayerMask.NameToLayer("UI"));
        NGUITools.SetActive(stuffGo, true);
    end

    function uiCell.releaseStuff()
        if(stuffGo == nil) then return end
        CLThingsPool.returnObj(stuffGo.name, stuffGo);
        NGUITools.SetLayer(stuffGo, LayerMask.NameToLayer("Stuff"));
        NGUITools.SetActive(stuffGo, false);
        stuffGo = nil;
    end

    -- 注意，c#侧不会在调用show时，调用refresh
    function uiCell.refresh(paras)
        --[[
        if(paras == 1) then   -- 刷新血
          -- TODO:
        elseif(paras == 2) then -- 刷新状态
          -- TODO:
        end
        --]]
    end

    -- 取得数据
    function uiCell.getData()
        return mData;
    end

    --------------------------------------------
    return uiCell;
end
