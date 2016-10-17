--- 3GU 新手引导
-- author : canyon
-- time : 2015-04-22
do
  require("LuaUtl");

  -- 引用C#脚本
  local netDB = GLVar.dbData; -- 网络层数据对象

  -- 引导类型
  GuidType = {
    captions=1,       --字幕
    talking=2,        --对话
    clickUI=3,        --点击UI
    click3dObj=4,     --点击3d对象
    gotoBattle = 5,     --去战斗
    setName=6,        --设置用户名
  };
  -- c# 方法体
  --[[

    public GuidContentType type;        //引导类型

    public int id;                      //通用的id,可为建筑的id，npc的id，对话id 等等

    public string talkingContent;       //对话内容

    public int roleGid;                 //说话的角色GID

    public TalkingDir talkingDir;       //对话方向

    public GetTarget getTargetFunc;     //取得对像的方法,返回值类型为Transfrom，例如 取得要引导的按钮（对于中心点不在中心的也可以在那个按扭上放在空gameobject）

    public DelegateWithParam onClickCallback;   //点击回调

    public object obj;                  //备用字段

    public string pName4Waiting;        //显示引导当xx页面显示后(显示)

    public bool showNextSameTime;       //同时显示下一条引导

    public bool showTalkingSameTime;       //同时显示下一条引导

    public int guidW;                   //引导指针的宽

    public int guidH;                   //引导指针的高

    public bool sendServer;             //通知服务器记录步骤

    public bool dontAutoShowNext;       //不要自动显示下一引导

    public bool isFingerDrag;           //指针是否是拖动    

    --]]

  CLDBGuid = {};

  -- 取得新手引导数据
  function CLDBGuid.getGuidData( ... )
    if(netDB.guidData == nil) then
      netDB.guidData = ArrayList();
      local guid = nil;
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.gotoBattle; -- 进入战斗
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getgo4Guid";
      guid.onClickCallback = "PanelGuid.playerMove";
      guid.guidW = 180;
      guid.guidH = 180;
      guid.pName4Waiting = "PanelBattle";
      guid.isFingerDrag = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking1");
      guid.talkingDir = "right";
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnAttack4Guid";
      guid.onClickCallback = "PanelGuid.playerAttack";
      guid.guidW = 180;
      guid.guidH = 180;
      guid.dontAutoShowNext = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking2");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnskillBtn4";
      guid.onClickCallback = "PanelGuid.playerSkill";
      guid.guidW = 130;
      guid.guidH = 130;
      guid.dontAutoShowNext = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking3");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnMorph4Guid";
      guid.onClickCallback = "PanelGuid.playerMorph";
      guid.guidW = 130;
      guid.guidH = 130;
      guid.dontAutoShowNext = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking4");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnIntegrated4Guid";
      guid.onClickCallback = "PanelGuid.playerIntegrated";
      guid.guidW = 150;
      guid.guidH = 150;
      -- guid.dontAutoShowNext = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking5");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      -- guid = Hashtable();
      -- guid.type = GuidType.clickUI;
      -- guid.getTargetFunc = "PanelBattleResult.getCard4Guid";
      -- guid.onClickCallback = "PanelBattleResult.onClickCard4Guid"
      -- guid.guidW = 106;
      -- guid.guidH = 147;
      -- guid.needEventTrigger = true;
      -- guid.pName4Waiting = "PanelBattleResult";
      -- netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking6");
      guid.talkingDir = "left";
      guid.pName4Waiting = "PanelPveEnd";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnUp4Guid";
      guid.onClickCallback = "PanelPveEnd.OnClick2UpHero"
      guid.guidW = 200;
      guid.guidH = 120;
      -- guid.dontAutoShowNext = true;
      guid.pName4Waiting = "PanelPveEnd";
      guid.sendServer = true; -- 通知服务器
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnHome4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnHome4Guid"
      guid.guidW = 220;
      guid.guidH = 110;
      guid.pName4Waiting = "PanelPveEnd";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelMain.getBtnFightAll";
      guid.onClickCallback = "PanelMain.OnClickFight"
      guid.guidW = 180;
      guid.guidH = 180;
      guid.showTalkingSameTime = true;
      guid.pName4Waiting = "PanelMain";
      guid.talkingContent = Localization.Get("GuidTalking7");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveMap.getBtnLev4Guid";
      guid.onClickCallback = "PanelPveMap.doBattle4Guid"
      guid.guidW = 120;
      guid.guidH = 120;
      guid.pName4Waiting = "PanelPveMap";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveMapFight.getgo4Guid";
      guid.onClickCallback = "PanelPveMapFight.onClickHero4Guid"
      guid.guidW = 140;
      guid.guidH = 140;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking8");
      guid.talkingDir = "right";
      guid.pName4Waiting = "PanelPveMapFight";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveMapFight.getBtnAttack4Guid";
      guid.onClickCallback = "PanelGuid.onClikcGotoBattle"
      guid.guidW = 180;
      guid.guidH = 180;
      guid.dontAutoShowNext = true;
      guid.sendServer = true; -- 通知服务器
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking16");
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.click3dObj;
      guid.getTargetFunc = "clBattle.getGiftBox4Guid";
      guid.onClickCallback = "PanelGuid.onClickGiftBox"
      guid.guidW = 160;
      guid.guidH = 160;
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPackage.getBtn4Guid";
      guid.onClickCallback = "PanelGuid.onClickGetGift"
      guid.guidW = 360;
      guid.guidH = 110;
      guid.pName4Waiting = "PanelPackage";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.needEventTrigger = true;
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking17");
      guid.roleGid = 3;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnMainRol4Guid";
      guid.onClickCallback = "PanelGuid.onClickBtnMainRol4Guid";
      guid.guidW = 100;
      guid.guidH = 100;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      -- guid = Hashtable();
      -- guid.type = GuidType.clickUI;
      -- guid.getTargetFunc = "PanelBattleResult.getCard24Guid";
      -- guid.onClickCallback = "PanelBattleResult.onClickCard24Guid"
      -- guid.guidW = 106;
      -- guid.guidH = 147;
      -- guid.needEventTrigger = true;
      -- guid.pName4Waiting = "PanelBattleResult";
      -- netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking18");
      guid.roleGid = 3;
      guid.pName4Waiting = "PanelPveEnd";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnTabPar4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnTabPar4Guid"
      guid.guidW = 150;
      guid.guidH = 80;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnUp4Guid";
      guid.onClickCallback = "PanelPveEnd.OnClick2UpHero";
      guid.guidW = 200;
      guid.guidH = 90;
      guid.sendServer = true; -- 通知服务器
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnNext4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnNext4Guid";
      guid.guidW = 160;
      guid.guidH = 110;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking19");
      guid.roleGid = 3;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveMapFight.getgo24Guid";
      guid.onClickCallback = "PanelPveMapFight.onClickHero24Guid"
      guid.guidW = 140;
      guid.guidH = 140;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking20");
      guid.talkingDir = "right";
      guid.pName4Waiting = "PanelPveMapFight";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveMapFight.getBtnAttack4Guid";
      guid.onClickCallback = "PanelGuid.onClikcGotoBattle2";
      guid.guidW = 180;
      guid.guidH = 180;
      guid.dontAutoShowNext = true;
      guid.sendServer = true; -- 通知服务器
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking21");
      guid.onClickCallback = "PanelGuid.onClickTalkingLXG";
      guid.roleGid = 3;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.needEventTrigger = true;
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking22");
      guid.roleGid = 3;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnMainRol24Guid";
      guid.onClickCallback = "PanelBattle.onClickBtnMainRol24Guid";
      guid.guidW = 100;
      guid.guidH = 100;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.needEventTrigger = true;
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelBattle.getBtnMorph4Guid";
      guid.onClickCallback = "PanelGuid.playerMorphLev2";
      guid.guidW = 130;
      guid.guidH = 130;
      guid.dontAutoShowNext = true;
      guid.showTalkingSameTime = true;
      guid.talkingContent = Localization.Get("GuidTalking23");
      guid.talkingDir = "left";
      guid.roleGid = 1;
      guid.sendServer = true;
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking11");
      guid.roleGid = 1;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnTabEquip4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnTabEquip4Guid"
      guid.pName4Waiting = "PanelPveEnd";
      guid.guidW = 150;
      guid.guidH = 80;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnUpEquip4Guid";
      guid.onClickCallback = "PanelGuid.OnClick2UpSomething";
      guid.guidW = 200;
      guid.guidH = 90;
      guid.sendServer = true; -- 通知服务器
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking24");
      guid.roleGid = 1;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnTabBase4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnTabBase4Guid"
      guid.pName4Waiting = "PanelPveEnd";
      guid.guidW = 150;
      guid.guidH = 80;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnUp4Guid";
      guid.onClickCallback = "PanelGuid.OnClick2UpSomething";
      guid.guidW = 200;
      guid.guidH = 90;
      guid.sendServer = true; -- 通知服务器
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking25");
      guid.roleGid = 1;
      guid.talkingDir = "left";
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnTabSkill4Guid";
      guid.onClickCallback = "PanelPveEnd.onClickBtnTabSkill4Guid"
      guid.pName4Waiting = "PanelPveEnd";
      guid.guidW = 150;
      guid.guidH = 80;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.clickUI;
      guid.getTargetFunc = "PanelPveEnd.getBtnUpSkill4Guid";
      guid.onClickCallback = "PanelGuid.OnClick2UpSomething";
      guid.guidW = 200;
      guid.guidH = 90;
      guid.sendServer = true; -- 通知服务器
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
      --------------------------------
      --------------------------------
      --------------------------------
      --------------------------------
      guid = Hashtable();
      guid.type = GuidType.talking;
      guid.talkingContent = Localization.Get("GuidTalking26");
      guid.onClickCallback = "PanelGuid.OnClickGuid4ReliveConfrim";
      guid.roleGid = 1;
      guid.talkingDir = "left";
      guid.sendServer = true; -- 通知服务器
      guid.dontAutoShowNext = true;
      netDB.guidData:Add(guid);
    end;
    return netDB.guidData;
  end;

  function CLDBGuid.getStartStepByPlStep( plStep )
    if(plStep == 0) then
      return 0;
    elseif(plStep == 1) then
      return 9;
    elseif(plStep == 2) then
      return 13;
    elseif(plStep == 3) then
      return 13;
    elseif(plStep == 4) then
      return 24;
    elseif(plStep == 5) then
      return 28;
    elseif(plStep == 6) then
      return 31;
    elseif(plStep == 7) then
      return 34;
    elseif(plStep == 8) then
      return 37;
    end;
    return 1000;
  end;
end;

module("CLDBGuid",package.seeall);
