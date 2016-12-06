-- 关卡界面
do
    CLLPLevels = {}

    local csSelf = nil;
    local transform = nil;
    local GridMenu = nil;
    local isHideMenu = false;
    local RootSpin;
    local plotObj = nil;
    local blur = nil;
    local cellList = {};


    -- 初始化，只会调用一次
    function CLLPLevels.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;

        GridMenu = getChild(transform, "AnchorTopRight/GridMenu"):GetComponent("TweenPosition");
        isHideMenu = (not isHideMenu);
        GridMenu:Play(isHideMenu);


        RootSpin = getChild(transform, "CameraBlur", "Plot", "RootSpin");
        blur = getChild(transform, "CameraBlur"):GetComponent("Blur");
        blur.blurShader = Shader.Find("Hidden/FastBlur");

        local tableObj = getChild(transform, "Table");
        for i = 1, 6 do
            table.insert(cellList, getChild(tableObj, i):GetComponent("CLCellLua"));
        end
    end

    -- 设置数据
    function CLLPLevels.setData(paras)
    end

    -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
    function CLLPLevels.show()
        CLLPLevels.loadPlot();
    end

    function CLLPLevels.loadPlot()
        if (plotObj ~= nil) then
            return;
        end
        CLThingsPool.borrowObjAsyn("BegainingPlot", CLLPLevels.onloadedPlot);
    end

    function CLLPLevels.onloadedPlot(name, obj, orgs)
        plotObj = obj;
        plotObj.transform.parent = RootSpin;
        plotObj.transform.localPosition = Vector3.zero;
        plotObj.transform.localScale = Vector3.one;
        plotObj.transform.localEulerAngles = Vector3.zero;
        NGUITools.SetLayer(plotObj.gameObject, LayerMask.NameToLayer("UI2"));
        NGUITools.SetActive(plotObj.gameObject, true);
    end

    -- 刷新
    function CLLPLevels.refresh()
        for i, v in ipairs(cellList) do
            v:init(i, CLLPLevels.onClickCell);
        end
    end

    function CLLPLevels.onClickCell(cell)
        local d = cell.luaTable.getData();

        local lev = bio2Int(CLLData.player.lev);
        if (lev < d) then
            NAlertTxt.add(Localization.Get("NotOpen"), Color.red, 1);
            return;
        end
        CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, { type = "home", lev=d});
    end

    -- 关闭页面
    function CLLPLevels.hide()
        if (plotObj ~= nil) then
            NGUITools.SetActive(plotObj.gameObject, false);
            NGUITools.SetLayer(plotObj.gameObject, LayerMask.NameToLayer("Default"));
            CLThingsPool.returnObj(plotObj.gameObject.name, plotObj.gameObject);
            plotObj = nil;
        end
    end

    -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
    function CLLPLevels.procNetwork(cmd, succ, msg, paras)
        --[[
        if(succ == 0) then
          if(cmd == "xxx") then
            -- TODO:
          end
        end
        --]]
    end

    -- 处理ui上的事件，例如点击等
    function CLLPLevels.uiEventDelegate(go)
        local goName = go.name;
        if (goName == "ButtonMenu") then
            isHideMenu = (not isHideMenu);
            GridMenu:Play(isHideMenu);
        elseif (goName == "ButtonCell1") then
            -- help
        elseif (goName == "ButtonCell2") then
            -- share
        elseif (goName == "ButtonCell3") then
            -- setting
            CLPanelManager.getPanelAsy("PanelSet", onLoadedPanelTT);
        end
    end

--    function CLLPLevels.hideMenu()
--        if(not isHideMenu) then
--            isHideMenu = (not isHideMenu);
--            GridMenu:Play(isHideMenu);
--        end
--    end

    -- 当按了返回键时，关闭自己（返值为true时关闭）
    function CLLPLevels.hideSelfOnKeyBack()
        return false;
    end

    --------------------------------------------
    return CLLPLevels;
end
