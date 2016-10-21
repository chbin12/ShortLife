-- 开场剧情
do
    CLLPOpeningPlot = {}

    local dragSetting = CLUIDrag4World.self;
    local csSelf = nil;
    local transform = nil;
    local LabelContentTyperWriter = nil;
    local LabelContentAlphaTw = nil;
    local LabelContentPositionTw;

    -- 初始化，只会调用一次
    function CLLPOpeningPlot.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        LabelContentTyperWriter = getChild(transform, "AnchorTop", "LabelContent"):GetComponent("TypewriterEffect");
        LabelContentAlphaTw = LabelContentTyperWriter:GetComponent("TweenAlpha");
        LabelContentPositionTw = LabelContentTyperWriter:GetComponent("TweenPosition");
    end

    -- 设置数据
    function CLLPOpeningPlot.setData(paras)
    end

    -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
    function CLLPOpeningPlot.show()
        dragSetting.canRotation = false;
        dragSetting.canScale = false;
        LabelContentTyperWriter.text = Localization.Get("OpeningPlot");
        csSelf:invoke4Lua("showPlot", 5);
    end

    function CLLPOpeningPlot.showPlot()
        CLLBegainingPlot.show4Start();
    end

    -- 刷新
    function CLLPOpeningPlot.refresh()
    end

    -- 关闭页面
    function CLLPOpeningPlot.hide()
    end

    -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
    function CLLPOpeningPlot.procNetwork(cmd, succ, msg, paras)
        --[[
        if(succ == 0) then
          if(cmd == "xxx") then
            -- TODO:
          end
        end
        --]]
    end

    -- 处理ui上的事件，例如点击等
    function CLLPOpeningPlot.uiEventDelegate(go)
        local goName = go.name;
        if (goName == "LabelContent") then
            -- typewriter finish
            csSelf:invoke4Lua("hideLabelContent", 2);
        end
    end

    function CLLPOpeningPlot.hideLabelContent()
        LabelContentAlphaTw:Play(true);
        LabelContentPositionTw:Play(true);
    end

    -- 当按了返回键时，关闭自己（返值为true时关闭）
    function CLLPOpeningPlot.hideSelfOnKeyBack()
        return true;
    end

    function CLLPOpeningPlot.onFinshShowPlot()
        CLLBegainingPlot.hide();
        CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, {type = "home"});
    end

    --------------------------------------------
    return CLLPOpeningPlot;
end
