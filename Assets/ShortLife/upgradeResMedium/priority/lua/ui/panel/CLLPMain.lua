-- 主界面
do
    CLLPMain = {}

    local csSelf = nil;
    CLLPMain.transform = nil;
    local GridMenu = nil;
    local isHideMenu = false;

    -- 初始化，只会调用一次
    function CLLPMain.init(csObj)
        csSelf = csObj;
        CLLPMain.transform = csObj.transform;
        GridMenu = getChild(CLLPMain.transform, "AnchorTopRight/GridMenu"):GetComponent("TweenPosition");
        isHideMenu = (not isHideMenu);
        GridMenu:Play(isHideMenu);
    end

    -- 设置数据
    function CLLPMain.setData(paras)
    end

    -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
    function CLLPMain.show()
        -- CLUIDrag4World.setCanClickPanel(csSelf.name);
        CLUIDrag4World.removeCanClickPanel(csSelf.name);
        CLLScene.startSpin();
    end

    local tileObj = nil;
    -- 刷新
    function CLLPMain.refresh()
    end

    -- 关闭页面
    function CLLPMain.hide()
    end

    -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
    function CLLPMain.procNetwork(cmd, succ, msg, paras)
        --[[
        if(succ == 0) then
          if(cmd == "xxx") then
            -- TODO:
          end
        end
        --]]
    end

    -- 处理ui上的事件，例如点击等
    function CLLPMain.uiEventDelegate(go)
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
        elseif (goName == "ButtonStartGame") then
--            CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, {type="battle"});
            CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, {type="explore"});

--            CLLScene.addRightSideTiles(0.02, nil);
        end
    end

    function CLLPMain.hideMenu()
        if(not isHideMenu) then
            isHideMenu = (not isHideMenu);
            GridMenu:Play(isHideMenu);
        end
    end

    -- 当按了返回键时，关闭自己（返值为true时关闭）
    function CLLPMain.hideSelfOnKeyBack()
        return false;
    end

    --------------------------------------------
    return CLLPMain;
end
