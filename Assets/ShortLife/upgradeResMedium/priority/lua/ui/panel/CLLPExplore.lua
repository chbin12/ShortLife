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
            local unit =SCfg.self.player;
            unit.transform.localEulerAngles = angle;
            unit.luaTable.onTurn(unit.transform.position);
        elseif goName == "ButtonPlayAgain" then
            CLLExplore.clean();
            CLLScene.clean();
            CLPanelManager.getPanelAsy("PanelLevels", onLoadedPanel);
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
        if(SCfg.self.player == nil) then return end;
        local flag = -1;
        local angle = SCfg.self.player.transform.localEulerAngles;
        angle.y = angle.y + flag*60;
        local unit =SCfg.self.player;
        unit.transform.localEulerAngles = angle;
        unit.luaTable.onTurn(unit.transform.position);
    end

    -- 编辑器模式下使用
    function CLLPExplore.onKeyRightArrow()
        if(SCfg.self.player == nil) then return end;
        local flag = 1;
        local angle = SCfg.self.player.transform.localEulerAngles;
        angle.y = angle.y + flag*60;
        local unit =SCfg.self.player;
        unit.transform.localEulerAngles = angle;
        unit.luaTable.onTurn(unit.transform.position);
    end

    function CLLPExplore.onPlayerDead()
        NGUITools.SetActive(ButtonPlayAgain, true);
    end
    ----------------------------------
    return CLLPExplore;
end