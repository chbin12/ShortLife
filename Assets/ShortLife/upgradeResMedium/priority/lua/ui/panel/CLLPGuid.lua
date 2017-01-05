--- 新手引导
do
  require("CLDBGuid");
  require("CLLData");
  local setActive = NGUITools.SetActive;
  local getChild = getChild;

  -- 界面属性
  local pName = nil;
  local panel = nil;
  local transform = nil;
  local gameObject = nil;

  local trsfMark;
  local MaskPanel = nil;
  local background = nil;
  local backgroundCollider = nil;
  local SpriteKuang = nil;
  local panelTalking = nil;
  local SpriteFingerClick = nil;
  local SpriteFingerDrag = nil;

  -- 属性变量
  local guidData = nil;
  local curStep4Pl = 0; -- 角色上记录的当前引导的步数
  local curStep = 0; -- 当前引导的步数
  local curGuid = nil;

  PanelGuid = {};

  function PanelGuid.init(go)
    pName = go.name;
    panel = go;
    transform = panel.transform;
    gameObject = panel.gameObject;

    local Camera = getChild(transform,"Camera");

    local rootMask = getChild(Camera, "PanelMask");
    background = getChild(Camera, "Panel", "SpriteBackGround").gameObject;
    backgroundCollider = getChild(Camera, "BackGroundCollider").gameObject;
    trsfMark = getChild(rootMask,"Mask"):GetComponent("UITexture");
    SpriteFingerClick = getChild(trsfMark.transform,"SpriteFinger"):GetComponent("TweenRotation");
    SpriteFingerDrag = SpriteFingerClick:GetComponent("TweenPosition");
    SpriteKuang = getChild(trsfMark.transform, "SpriteKuang"):GetComponent("UISprite");
    guidData = CLDBGuid.getGuidData(); -- 取得新手引导的数据
    panelTalking = getChild(Camera, "PanelRoleTalking"):GetComponent("CLPanelLua");
    MaskPanel = getChild(trsfMark.transform, "MaskPanel");
  end;

  function PanelGuid.setData(pars)
  end;

  function PanelGuid.show ()
    NGUITools.SetActive(background, false);
    NGUITools.SetActive(backgroundCollider, true);
    NGUITools.SetActive(trsfMark.gameObject, false);
    NGUITools.SetActive(panelTalking.gameObject, false);

    curStep4Pl = CLLData.getGuidStep();
    curStep = CLDBGuid.getStartStepByPlStep(curStep4Pl);
    PanelGuid.doGuid();
  end;

  function PanelGuid.hide ()
    panel:cancelInvoke4Lua(nil);
  end;

  function PanelGuid.refresh()
  end;

  function PanelGuid.procNetwork(cmd, succ, msg, pars)
  end;

  function PanelGuid.uiEventDelegate( go )
    local btnName = go.name;
    if(btnName == "Mask") then
      panel:cancelInvoke4Lua(PanelGuid.refreshGuidPosition);
      NGUITools.SetActive(go, false);
      NGUITools.SetActive(background, false);

      -- 记录步骤
      local eventCont = "新手步骤:" .. curStep;
      print(eventCont);
      ChnNetHandle.commonEvent(eventCont);

      PanelGuid.onClickGuid(); -- 执行点击事件

    end;
  end;

  -- 当需要等待的引导事件发生了，此时可以进行引导了
  function PanelGuid.guidTriggered( ... )
    PanelGuid._doGuid();
  end;

  -- 处理引导
  function PanelGuid.doGuid( )
    if(guidData.Count > curStep) then
      SCfg.self.isGuidMode = true;
      curGuid = guidData[curStep];
      if(curGuid.needEventTrigger) then -- 需要事件驱动引导
        return;
      end;
      PanelGuid._doGuid();
    else
      PanelGuid.stopGuid();
    end;
  end;

  function PanelGuid._doGuid( ... )
    if(not PanelGuid.checkPanelShowing()) then -- 先处理需要等待页面显示的判断
      return;
    end;

    if( curGuid.type == GuidType.clickUI or
      curGuid.type == GuidType.click3dObj) then
      if(curGuid.showTalkingSameTime ~= nil and curGuid.showTalkingSameTime == true) then
        PanelGuid.showTalking();
      else
        PanelGuid.showGuidPoint();
      end;
    elseif(curGuid.type == GuidType.gotoBattle) then
      PanelGuid.gotoBattle();
    elseif(curGuid.type == GuidType.talking) then
      PanelGuid.showTalking();
    end;
  end;

  -- 完成一次引导
  function PanelGuid.finishOneGuid( ... )
    curStep = curStep + 1;
    PanelGuid.doGuid();
  end;

  -- 停止引导
  function PanelGuid.stopGuid( ... )
    CLPanelManager.hidePanel(panel);
    SCfg.self.isGuidMode = false;
  end;

  -- 判断页面是否已经显示，是则开始引导，否则继续等待
  function PanelGuid.checkPanelShowing()
    if(curGuid.pName4Waiting == nil) then return true end;
    if(CLPanelManager.topPanel ~= nil
      and CLPanelManager.topPanel.name == curGuid.pName4Waiting
      and CLPanelManager.topPanel.isActive ) then
      return true;
    else
      panel:invoke4Lua(PanelGuid._doGuid, 1);
      return false;
    end;
  end;

  -- 取得3d物体对应该屏幕坐标
  function PanelGuid.getPos2ScreemPos(target)
    local pos = SCfg.self.mainCamera:WorldToViewportPoint(target.position);
    pos = SCfg.self.uiCamera:ViewportToWorldPoint(pos);
    pos.z = -10;
    return pos;
  end;

  -- 显示导引指针
  function PanelGuid.showGuidPoint(  )
    NGUITools.SetActive(background, true);
    NGUITools.SetActive(backgroundCollider, true);

    trsfMark.width = curGuid.guidW == nil and 120 or curGuid.guidW;
    trsfMark.height = curGuid.guidH == nil and 120 or curGuid.guidH;
    MaskPanel.localScale = Vector3(trsfMark.width/10, 1, trsfMark.height/10);
    SpriteKuang.width = trsfMark.width + 26;
    SpriteKuang.height = trsfMark.height + 31;
    NGUITools.SetActive(trsfMark.gameObject, true);
    trsfMark.gameObject.collider.enabled = true;
    NGUITools.SetActive(SpriteKuang.gameObject, true);
    NGUITools.AddWidgetCollider(trsfMark.gameObject);
    if(curGuid.isFingerDrag ~= nil and curGuid.isFingerDrag) then
      PanelGuid.showDragFinger();
    else
      PanelGuid.showClickFinger();
    end;

    PanelGuid.refreshGuidPosition();
  end;

  function PanelGuid.refreshGuidPosition( ... )
    local func = PanelGuid.getFuncByTrace(curGuid.getTargetFunc);
    if(curGuid.type == GuidType.click3dObj) then
      local pos = PanelGuid.getPos2ScreemPos(func());
      trsfMark.transform.position = pos;
    else
      trsfMark.transform.position = func().position;
    end;
    local pos = trsfMark.transform.localPosition;
    pos.z = -10;
    trsfMark.transform.localPosition = pos;

    panel:invoke4Lua(PanelGuid.refreshGuidPosition, 0.2)
  end;

  function PanelGuid.showClickFinger( ... )
    SpriteFingerClick.enabled = true;
    SpriteFingerDrag.enabled = false;
    SpriteFingerDrag.transform.localPosition = Vector3(47, -47, -10);
  end;

  function PanelGuid.showDragFinger( ... )
    SpriteFingerClick.enabled = false;
    SpriteFingerDrag.enabled = true;
    SpriteFingerDrag.transform.localEulerAngles = Vector3(0, 0, 30);
  end;

  -- 当拖动指针
  function PanelGuid.onDragFinger(btn, delta)
    PanelGuid.uiEventDelegate( btn );
  end;

  -- 点击了当前指针
  function PanelGuid.onClickGuid( ... )
    if(curGuid.showTalkingSameTime == true) then
      PanelRoleTalking.close();
    end;

    if(curGuid.sendServer == true) then
      -- 通知服务器
      curStep4Pl = curStep4Pl + 1;
      -- print("send server curStep4Pl===" .. curStep4Pl);
      Net.self:send(
        GboGmSvBuilder.callNet.changeGuidStep(
          curStep4Pl));
    end;
    --------------
    local func = PanelGuid.getFuncByTrace(curGuid.onClickCallback);
    if(func ~= nil) then
      func();
    end;
    PanelGuid.onClickGuidprocSepci(); -- 特殊处理
    --------------
    if(curGuid.dontAutoShowNext ~= true) then
      -- 加1放在后面
      PanelGuid.finishOneGuid();
    end;
  end;

  -- 显示对话
  function PanelGuid.showTalking( ... )
    local p = panelTalking;
    local data = Hashtable();
    data.content = curGuid;
    if(curGuid.showTalkingSameTime == true) then
      data.callback = PanelGuid.showGuidPoint;
    else
      data.callback = PanelGuid.onClickTalking;
    end;
    p:setData(data);

    CLPanelManager.showPanel(p);
    NGUITools.SetActive(background.gameObject, true);
    if(curGuid.showTalkingSameTime == true) then
      NGUITools.SetActive(backgroundCollider.gameObject, true);
    else
      NGUITools.SetActive(backgroundCollider.gameObject, false);
    end;
  end;

  function PanelGuid.onClickTalking( ... )
    -- 记录步骤
    local eventCont = "新手步骤:" .. curStep;
    print(eventCont);
    ChnNetHandle.commonEvent(eventCont);

    NGUITools.SetActive(background.gameObject, false);
    NGUITools.SetActive(backgroundCollider.gameObject, true);
    PanelGuid.onClickGuid();
  end;

  -- 特殊处理
  function PanelGuid.onClickGuidprocSepci( ... )
    if(curStep == 0) then
    end;
  end;

  -- 根据路径取得方法
  function PanelGuid.getFuncByTrace( trace )
    return LuaUtl.getLuaFunc(trace);
  end;

  function PanelGuid.gotoBattle()
    CLPanelManager.getPanelAsy ("PanelLoadScene", PanelGuid.onLoadPanelBattle);
  end;

  -- 战斗界面加载完成
  function PanelGuid.onLoadPanelBattle (p)
    local data =Hashtable();
    data.type = "goPVE";
    data.pveLevID = 1;
    p:setData(data);
    CLPanelManager.hideAllPanel();
    CLPanelManager.showTopPanel(p);
    LuaUtl.hideHotWheel();

    clBattle.pause();

    PanelGuid.finishOneGuid();
  end;

  function PanelGuid.playerMove( ... )
    clBattle.regain();
    NGUITools.SetActive(backgroundCollider, false);
    PanelGuid.checkisShowingMonster1();
    panel:invoke4Lua(PanelGuid.addMonster1, 2);
  end;

  function PanelGuid.pauseBattle( ... )
    clBattle.pause();
    PanelGuid.finishOneGuid();
  end;

  function PanelGuid.addMonster1( ... )
    local pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
  end;

  function PanelGuid.checkisShowingMonster1( ... )
    if(CLBattle.self.defense.Count < 1) then
      panel:invoke4Lua(PanelGuid.checkisShowingMonster1, 1);
    else
      panel:invoke4Lua(PanelGuid.pauseBattle, 1);
    end;
  end;

  function PanelGuid.playerAttack()
    clBattle.regain();
    -- PanelBattle.onPressAttack (nil, true);

    PanelGuid.showGuid2Attack();
    PanelGuid.checkAllMonsterDead();
  end;

  function PanelGuid.showGuid2Attack( ... )
    -- 重新把引导指向攻击
    NGUITools.SetActive(backgroundCollider, false);
    NGUITools.SetActive(trsfMark.gameObject, true);
    NGUITools.SetActive(SpriteKuang.gameObject, false);
    trsfMark.gameObject.collider.enabled = false;
    trsfMark.transform.position = PanelBattle.getBtnAttack4Guid().position;
  end;

  function PanelGuid.checkAllMonsterDead( ... )
    if(clBattle.defenseAliveCount() == 0) then
      NGUITools.SetActive(trsfMark.gameObject, false);
      trsfMark.gameObject.collider.enabled = true;
      PanelGuid.addMonster2();
      panel:invoke4Lua(PanelGuid.pauseBattle, 2);
    else
      panel:invoke4Lua(PanelGuid.checkAllMonsterDead, 1);
    end;
  end;

  function PanelGuid.addMonster2( ... )
    local pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
  end;

  function PanelGuid.playerSkill( ... )
    clBattle.regain();
    PanelBattle.onClickSkill4Guid();
    -- 重新把引导指向攻击
    PanelGuid.showGuid2Attack();

    PanelGuid.checkAllMonsterDead2();
  end;

  function PanelGuid.checkAllMonsterDead2( ... )
    if(clBattle.defenseAliveCount() == 0) then
      NGUITools.SetActive(trsfMark.gameObject, false);
      trsfMark.gameObject.collider.enabled = true;
      PanelGuid.addMonster3();
      panel:invoke4Lua(PanelGuid.pauseBattle, 2);
    else
      panel:invoke4Lua(PanelGuid.checkAllMonsterDead2, 1);
    end;
  end;

  function PanelGuid.addMonster3( ... )
    local pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(37), NumEx.int2Bio(1), NumEx.int2Bio(242), NumEx.int2Bio(1), false , pos);
  end

  -- 变身
  function PanelGuid.playerMorph( ... )
    clBattle.regain();
    PanelBattle.onClickMorph4Guid();
    -- PanelBattle.onPressAttack (nil, true);
    -- 重新把引导指向攻击
    PanelGuid.showGuid2Attack();

    PanelGuid.checkAllMonsterDead3();
  end;


  function PanelGuid.checkAllMonsterDead3( ... )
    if(clBattle.defenseAliveCount() == 0) then
      NGUITools.SetActive(trsfMark.gameObject, false);
      trsfMark.gameObject.collider.enabled = true;
      PanelBattle.showBossComing();
      panel:invoke4Lua(PanelGuid.addMonster4, 2);
      panel:invoke4Lua(PanelGuid.pauseBattle, 4);
    else
      panel:invoke4Lua(PanelGuid.checkAllMonsterDead3, 1);
    end;
  end;

  function PanelGuid.addMonster4( ... )
    local pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(4), NumEx.int2Bio(1), NumEx.int2Bio(240), NumEx.int2Bio(1), false , pos);
    pos = Vector3(NumEx.NextInt(12, 15), 0, NumEx.NextInt(8, 11));
    clBattle.doLoadMonster ( NumEx.int2Bio(71), NumEx.int2Bio(1), NumEx.int2Bio(242), NumEx.int2Bio(1), true , pos);
  end

  function PanelGuid.playerIntegrated( ... )
    clBattle.regain();
    PanelBattle.onClickIntegrated4Guid();
    NGUITools.SetActive(backgroundCollider, false);
    panel:invoke4Lua(PanelGuid.onFinishIntegrated, 5);
  end;

  -- 完成合体
  function PanelGuid.onFinishIntegrated()
    PanelBattle.onPressAttack (nil, true);
  end;

  -- function PanelGuid.onLoadPanelGoHome  ( p )
  --   CLBattle.self:exit();
  --   PanelGuid.stopGuid();
  --   local data =Hashtable();
  --   data.type = "goHome"
  --   p:setData(data);
  --   CLPanelManager.showTopPanel(p);
  -- end;

  -- function PanelGuid.OnClick2UpHero( ... )
  --   PanelMain.OnClick2UpHero();
  --   PanelGuid.stopGuid();
  --   PanelMain.resomeUIView();
  -- end;


  function PanelGuid.onClikcGotoBattle()
    PanelGuid.stopGuid(); -- 先结束引导，因为在引导时，不会加载小伙伴
    PanelPveMapFight.onClikcAttack4Guid( );
  end;

  function PanelGuid.onClikcSthBtn()
    PanelBattleResult.showSth();
    panel:invoke4Lua(PanelGuid.finishOneGuid, 1);
  end;

  function PanelGuid.invoke2NextStep( ... )
    panel:invoke4Lua(PanelGuid.finishOneGuid, 1);
  end;

  function PanelGuid.OnCloseEquip()
    CLPanelManager.hideAllPanel();
    CLPanelManager.showTopPanel(CLPanelManager.getPanel("PanelMain"));
  end;

  function PanelGuid.onClickGiftBox( ... )
    clBattle.onClickBox(clBattle.getGiftBox4Guid());
    panel:invoke4Lua(PanelGuid.finishOneGuid, 1.5);
  end;

  function PanelGuid.onClickGetGift()
    PanelPackage.clickBuy();
    -- PanelGuid.stopGuid();
    PanelGuid.showGuid2Attack();
    panel:invoke4Lua(PanelGuid.trigger4ShowtalkaboutChgMainrole, 3);
  end;
  function PanelGuid.trigger4ShowtalkaboutChgMainrole( ... )
    NGUITools.SetActive(background, false);
    NGUITools.SetActive(backgroundCollider, true);
    NGUITools.SetActive(trsfMark.gameObject, false);
    trsfMark.gameObject.collider.enabled = true;
    clBattle.pause();
    PanelGuid.guidTriggered();
  end;

  -- 切换陆小果
  function PanelGuid.onClickBtnMainRol4Guid( ... )
    PanelBattle.onClickBtnMainRol4Guid();
    PanelGuid.showGuid2Attack();
    -- PanelBattle.onPressAttack (nil, true);
    panel:invoke4Lua(PanelGuid.hideFinger, 5);
  end;

  function PanelGuid.hideFinger()
    NGUITools.SetActive(background, false);
    NGUITools.SetActive(trsfMark.gameObject, false);
    trsfMark.gameObject.collider.enabled = true;
  end;

  function PanelGuid.onClikcGotoBattle2()
    PanelGuid.onClikcGotoBattle( );
  end;

  function PanelGuid.onClickTalkingLXG()
    clBattle.regain();
    PanelGuid.showGuid2Attack();
    -- PanelBattle.onPressAttack (nil, true);
    panel:invoke4Lua(PanelGuid.trigger4ShowtalkaboutChgMainrole, 10);
  end;


  -- 变身
  function PanelGuid.playerMorphLev2( ... )
    clBattle.regain();
    PanelBattle.onClickMorph4Guid();
    -- PanelBattle.onPressAttack (nil, true);
    -- 重新把引导指向攻击
    PanelGuid.stopGuid();
  end;

  function PanelGuid.OnClick2UpSomething( ... )
    PanelPveEnd.OnClick2UpHero();
    PanelGuid.stopGuid();
  end;

  function PanelGuid.OnClickGuid4ReliveConfrim()
    clBattle.doReliveConfrim();
    PanelGuid.stopGuid();
  end;

  return PanelGuid;
end
