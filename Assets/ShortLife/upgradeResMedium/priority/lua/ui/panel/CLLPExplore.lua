do
    require("CLLExplore");

    CLLPExplore = {};

    local csSelf;
    local transform;
    local ButtonPlayAgain;
    local LabelStep;
    local isPause = false;

    function CLLPExplore.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        ButtonPlayAgain = getChild(transform, "ButtonPlayAgain").gameObject;
        LabelStep = getChild(transform, "AnchorTop", "LabelStep"):GetComponent("UILabel");
    end

    function CLLPExplore.show()
        CLLExplore.begain();
        NGUITools.SetActive(ButtonPlayAgain, false);
        LabelStep.text = 0;
    end

    function CLLPExplore.hide()
    end

    function CLLPExplore.refresh()
    end

    function CLLPExplore.refreshStep(val)
        LabelStep.text = val;
    end

    -- 处理ui上的事件，例如点击等
    function CLLPExplore.uiEventDelegate(go)
        local goName = go.name;
        if (goName == "ScreenCollider") then
            local pos = UICamera.currentTouch.pos;
            local halfW = Screen.width/2;
            local flag = 1;
            if(pos.x > halfW) then
                -- right
                flag = 1;
            else
                -- left
                flag = -1;
            end

            local angle = SCfg.self.player.transform.localEulerAngles;
            angle.y = angle.y + flag*60;
            SCfg.self.player.transform.localEulerAngles = angle;
        elseif goName == "ButtonPlayAgain" then
            CLLExplore.exit();
            CLLScene.clean();
            CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, {type="home"});
        elseif goName == "ButtonPause" then
            isPause = (not isPause);
            if(isPause) then
                Time:SetTimeScale(0);
            else
                Time:SetTimeScale(1);
            end
        end
    end

    -- 编辑器模式下使用
    function CLLPExplore.onKeyLeftArrow()
        local flag = -1;
        local angle = SCfg.self.player.transform.localEulerAngles;
        angle.y = angle.y + flag*60;
        SCfg.self.player.transform.localEulerAngles = angle;
    end

    -- 编辑器模式下使用
    function CLLPExplore.onKeyRightArrow()
        local flag = 1;
        local angle = SCfg.self.player.transform.localEulerAngles;
        angle.y = angle.y + flag*60;
        SCfg.self.player.transform.localEulerAngles = angle;
    end

    function CLLPExplore.onPlayerDead()
        NGUITools.SetActive(ButtonPlayAgain, true);
    end
    ----------------------------------
    return CLLPExplore;
end