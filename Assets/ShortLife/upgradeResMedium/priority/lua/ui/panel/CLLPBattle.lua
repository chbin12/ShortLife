-- 战斗界面
do
    CLLPBattle = {}

    local csSelf = nil;
    local transform = nil;
    local GridMenu = nil;
    local isHideMenu = false;
    local ProgressBarHP;
    local ProgressBarLP;
    local ProgressBarHPlb;
    local ProgressBarLPlb;
    local LabelStepValue;
    local ButtonAttack;
    local isDoAttack = false;


    -- 初始化，只会调用一次
    function CLLPBattle.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        local joy = getChild(transform, "AnchorBottomLeft", "joyCollider"):GetComponent("CLJoystick");
        joy:init(CLLPBattle.onPressJoy, CLLPBattle.onClickJoy, CLLPBattle.onDragJoy);

        GridMenu = getChild(transform, "AnchorTopRight/GridMenu"):GetComponent("TweenPosition");
        isHideMenu = (not isHideMenu);
        GridMenu:Play(isHideMenu);

        local AnchorTopLeft = getChild(transform, "AnchorTopLeft", "offset");
        ProgressBarHP = getChild(AnchorTopLeft, "Progress Bar HP"):GetComponent("UISlider");
        ProgressBarLP = getChild(AnchorTopLeft, "Progress Bar LP"):GetComponent("UISlider");
        ProgressBarHPlb = getChild(ProgressBarHP.transform, "Label"):GetComponent("UILabel");
        ProgressBarLPlb = getChild(ProgressBarLP.transform, "Label"):GetComponent("UILabel");
        LabelStepValue = getChild(transform, "AnchorTop", "LabelStepValue"):GetComponent("UILabel");
        ButtonAttack = getChild(transform, "AnchorBottomRight", "offset", "ButtonAttack"):GetComponent("TweenScale");
    end

    -- 设置数据
    function CLLPBattle.setData(paras)
    end

    -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
    function CLLPBattle.show()
        isDoAttack = false;
        CLLBattle.begain();
        ProgressBarHP.value = 1;
        LabelStepValue.text = 0;
    end

    -- 刷新
    function CLLPBattle.refresh()
    end

    -- 关闭页面
    function CLLPBattle.hide()
    end

    -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
    function CLLPBattle.procNetwork(cmd, succ, msg, paras)
        --[[
        if(succ == 0) then
          if(cmd == "xxx") then
            -- TODO:
          end
        end
        --]]
    end

    -- 处理ui上的事件，例如点击等
    function CLLPBattle.uiEventDelegate(go)
        local goName = go.name;
        if (goName == "ButtonAttack") then
            -- 攻击
        elseif (goName == "ButtonMenu") then
            isHideMenu = (not isHideMenu);
            GridMenu:Play(isHideMenu);
        elseif (goName == "ButtonCell1") then
            -- help
        elseif (goName == "ButtonCell2") then
            -- share
        elseif (goName == "ButtonCell3") then
            -- exit
            CLPanelManager.getPanelAsy("PanelLoadScene", CLLPBattle.onLoadedPanel);
        end
    end

    function CLLPBattle.onLoadedPanel(p)
        CLLBattle.exit();
        CLLScene.clean();
        local data ={type = "home" }
        p:setData(data);
        CLPanelManager.showTopPanel(p);
    end

    -- 当按了返回键时，关闭自己（返值为true时关闭）
    function CLLPBattle.hideSelfOnKeyBack()
        return false;
    end

    function CLLPBattle.onPressJoy(isPressed)
        if (not isPressed) then
            if (SCfg.self.player.action.currActionValue == LuaUtl.getAction("run")) then
                SCfg.self.player.luaTable.setAction("idel");
            end
            SCfg.self.player.tween:stopMoveForward();
            SCfg.self.player.aiPath:stop();
            SCfg.self.player.luaTable.onArrived(nil);
        end
    end

    function CLLPBattle.onClickJoy()
    end

    function CLLPBattle.onDragJoy(currentTouch)
        if (CLLBattle.isPaused or SCfg.self.player == nil or SCfg.self.player.isDead) then return end
        local dir = currentTouch; --currentTouch.totalDelta.normalized;
        dir = Vector3(dir.x, 0, dir.y);
        if (SCfg.self.player.gameObject.activeSelf) then
            SCfg.self.player.luaTable.moveForward(dir);
        end
    end

    function CLLPBattle.onPressButtonAttack(go, orgs)
        CLLPBattle.onPressAttack(go, true)
    end

    function CLLPBattle.onReleaseButtonAttack(go, orgs)
        CLLPBattle.onPressAttack(go, false)
    end

    function CLLPBattle.onPressAttack(buton, isPressed)
        if(isPressed) then
            isDoAttack = true;
            CLLPBattle.playAttackRepeat();
        else
            isDoAttack = false;
            csSelf:cancelInvoke4Lua(CLLPBattle.playAttackRepeat);
        end
    end

    function CLLPBattle.playAttackRepeat()
        if(SCfg.self.player == nil or SCfg.self.player.isDead) then
            csSelf:cancelInvoke4Lua(CLLPBattle.playAttackRepeat);
            return;
        end

        if(isDoAttack) then
            SCfg.self.player.luaTable.playAttack();
            csSelf:cancelInvoke4Lua(CLLPBattle.playAttackRepeat);
            csSelf:invoke4Lua(CLLPBattle.playAttackRepeat, 0.2);
            CLLPBattle.onMainRoleAttack();
        else
            csSelf:cancelInvoke4Lua(CLLPBattle.playAttackRepeat);
        end
    end

    -- 当主角攻击时
    function CLLPBattle.onMainRoleAttack()
        ButtonAttack:ResetToBeginning();
        ButtonAttack:Play(true);
    end

    -- 当主角受伤害
    function CLLPBattle.onPlayerHurt()
        local data = SCfg.self.player.luaTable.data;
        local persent = bio2Int(data.currHP)/ bio2Int(data.HP);
        ProgressBarHP.value = persent;
    end

    function CLLPBattle.someOnDead(unit)
        if(not unit.isOffense) then
            LabelStepValue.text = CLLScene.getSteps();
        end
    end

    -- 战斗结束
    function CLLPBattle.endBattle()
        CLPanelManager.getPanelAsy("PanelLoadScene", CLLPBattle.onLoadedPanel);
    end
    --------------------------------------------
    return CLLPBattle;
end
