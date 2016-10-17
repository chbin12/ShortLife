-- 英雄单元
do
  local uiCell = {}

  local bio2Int = NumEx.bio2Int;
  local int2Bio = NumEx.int2Bio;
  local csSelf = nil;
  local transform = nil;
  local gameObject = nil;
  local Label = nil;
  local SpriteIcon = nil;
  local SpriteIconTween = nil;
  local SpriteDead = nil;
  local SpriteFlag = nil;
  local ProgressBar = nil;
  local mData = nil;
  local roleAttr = nil;
  local roleData = nil;
  uiCell.isCloned = false;
  -- local cloneSelf = nil;
  local canDragCell = false;
  local selfIsContainer = false;    -- 自身也是一个container
  local isCanPlaySkill = false;

  function uiCell.init (go)
    csSelf = go;
    gameObject = csSelf.gameObject;
    transform = go.transform;
    Label = getChild(transform, "Label"):GetComponent("UILabel");
    SpriteIcon = getChild(transform, "SpriteIcon"):GetComponent("UISprite");
    SpriteIconTween = SpriteIcon:GetComponent("TweenAlpha");
    SpriteDead = getChild(transform, "SpriteDead");
    if(SpriteDead ~= nil) then
      SpriteDead = SpriteDead.gameObject;
    end
    SpriteFlag = getChild(transform, "SpriteFlag");
    if(SpriteFlag ~= nil) then
      SpriteFlag = SpriteFlag:GetComponent("UISprite");
      ProgressBar = getChild(SpriteFlag.transform, "Progress BarAtk"):GetComponent("UISlider");
    end

    selfIsContainer = csSelf:GetComponent("UIDragDropContainer");
    selfIsContainer = selfIsContainer ~= nil and true or false;
  end

  function uiCell.show ( go, data )
    mData = data;
    isCanPlaySkill = false;
    if(SpriteIconTween ~= nil) then
      SpriteIconTween.enabled = false;
      local color = SpriteIcon.color;
      color.a = 1;
      SpriteIcon.color = color;
    end;

    if(mData == nil) then
      canDragCell = false;
      -- SpriteIcon:setGray();
      if(SpriteDead ~= nil) then
        NGUITools.SetActive(SpriteDead, false);
      end

      NGUITools.SetActive(Label.gameObject, false);
      NGUITools.SetActive(SpriteIcon.gameObject, true);
      if(SpriteIcon.height > 200) then
        SpriteIcon.spriteName = "fightBefore_HeroFrameEmpty";
      else
        NGUITools.SetActive(SpriteIcon.gameObject, false);
      end

      if(SpriteFlag ~= nil) then
        NGUITools.SetActive(SpriteFlag.gameObject, false);
        NGUITools.SetActive(ProgressBar.gameObject, false);
      end
      return;
    end

    if(type(mData) == "table") then -- 说明只传入了属性
      canDragCell = true;
      roleAttr = mData;

      NGUITools.SetActive(Label.gameObject, true);
      NGUITools.SetActive(SpriteIcon.gameObject, true);
      if(SpriteFlag ~= nil) then
        NGUITools.SetActive(SpriteFlag.gameObject, true);
        NGUITools.SetActive(ProgressBar.gameObject, true);
      end

      SpriteIcon:unSetGray();
      if(SpriteIcon.height > 200) then
        SpriteIcon.spriteName = roleAttr.base.HalfBodyIcon;
      else
        SpriteIcon.spriteName = roleAttr.base.Icon;
      end
      if(SpriteDead ~= nil) then
        NGUITools.SetActive(SpriteDead, false);
      end
      -- SpriteFlag.spriteName = "";
      -- local HP = bio2Int(roleAttr.vals.HP);
      -- Label.text = HP;
      Label.text = Localization.Get(roleAttr.base.NameKey);
      if(ProgressBar ~= nil) then
        ProgressBar.value = 1;
      end
    else
      canDragCell = false;
      roleAttr = mData:getLuaFunction("getAttr")();
      roleData = mData:getLuaFunction("getData")();

      NGUITools.SetActive(Label.gameObject, true);
      NGUITools.SetActive(SpriteIcon.gameObject, true);
      NGUITools.SetActive(SpriteFlag.gameObject, true);
      NGUITools.SetActive(ProgressBar.gameObject, true);

      SpriteIcon:unSetGray();
      SpriteIcon.spriteName = roleAttr.base.Icon;
      NGUITools.SetActive(SpriteDead, false);
      -- SpriteFlag.spriteName = "";
      local HP = bio2Int(roleData.HP);
      Label.text = HP;
      ProgressBar.value = 1;
    end
  end

  -- 技能状态
  function uiCell.setSkillState( canSkill )
    isCanPlaySkill = canSkill;
    if(SpriteIconTween == nil) then return end
    if(canSkill) then
      SpriteIconTween.enabled = true;
    else
      SpriteIconTween.enabled = false;
      local color = SpriteIcon.color;
      color.a = 1;
      SpriteIcon.color = color;
    end
  end

  function uiCell.refresh( paras )
    if(paras == 1) then   -- 刷新血
      local currHP = bio2Int(roleData.currHP);
      local HP = bio2Int(roleData.HP);
      Label.text = HP;
      ProgressBar.value = currHP/HP;
    elseif(paras == 2) then   --死亡
      if(SpriteIconTween ~= nil) then
        SpriteIconTween.enabled = false;
        local color = SpriteIcon.color;
        color.a = 1;
        SpriteIcon.color = color;
      end;
    
      SpriteIcon:setGray();
      Label.text = "";
      NGUITools.SetActive(SpriteDead, true);
    elseif(paras == 3) then -- hide
      if(SpriteDead ~= nil) then
        NGUITools.SetActive(SpriteDead, false);
      end
      NGUITools.SetActive(Label.gameObject, false);
      if(SpriteIcon.height > 200) then
        SpriteIcon.spriteName = "fightBefore_HeroFrameEmpty";
      else
        NGUITools.SetActive(SpriteIcon.gameObject, false);
      end
      if(SpriteFlag ~= nil) then
        NGUITools.SetActive(SpriteFlag.gameObject, false);
        NGUITools.SetActive(ProgressBar.gameObject, false);
      end
    end
  end

  function uiCell.canClick( ... )
    if(mData == nil or mData.isDead) then return false end
    return true;
  end

  function uiCell.getData ( )
    return mData, isCanPlaySkill;
  end

  function uiCell.OnPress( go, isPressed )
    if(isPressed) then

    else
      if(not uiCell.isCloned) then return end
      local lastHitObj = UICamera.hoveredObject;
      if(lastHitObj ~= nil) then
        local tmpData = mData;  -- 先缓存起来，因为有可能 csSelf == hitCell;
        -- if(selfIsContainer) then
        --   csSelf:init(nil, nil)
        -- end

        local dragContainer = lastHitObj:GetComponent("UIDragDropContainer");
        local hitCell = lastHitObj:GetComponent("CLCellLua");
        if(dragContainer ~= nil and hitCell ~= nil) then
          -- if(PanelBattleBeforeTest.canDragDropCell(csSelf, hitCell)) then
            hitCell:init(tmpData, nil);
          -- end
        end
      else
        -- if(selfIsContainer) then
        --   csSelf:init(nil, nil)
        -- end
      end

      NGUITools.Destroy(gameObject);
    end
  end

  function uiCell.OnDrag( go, delta )
    if(not canDragCell) then return end
    local detalY = math.abs(UICamera.currentTouch.totalDelta.y);
    local detalX = math.abs(UICamera.currentTouch.totalDelta.x);
    if(not uiCell.isCloned and detalY < detalX or
      detalY < UICamera.current.dragThreshold) then
      -- 说明是水平拖动
      return;
    end
    if(not uiCell.isCloned) then
      local clone = uiCell.cloneSelf(); -- 先clone
      local pos = UICamera.lastHit.point;
      clone.transform.position = pos;
      UICamera.current:ProcessRelease();
      -- UICamera.current:Update();
      -- UICamera.current:LateUpdate();

      UICamera.currentTouch.current = clone;
      UICamera.currentTouch.pressed = clone;
      UICamera.currentTouch.dragged = clone;
      UICamera.currentTouch.last = clone;

      -- UICamera.current:ProcessTouch(true, false);

      if(selfIsContainer) then
        -- uiCell.refresh(3);
        csSelf:init(nil, nil);
      end
    else
      local pos = UICamera.lastHit.point;
      transform.position = pos;
    end
  end

  function uiCell.cloneSelf()
    cloneSelf = NGUITools.AddChild(SCfg.self.dragDropRoot.gameObject, gameObject);
    local boxCollider = cloneSelf:GetComponent("BoxCollider");
    if(boxCollider ~= nil) then
      boxCollider.enabled = false;
    end
    local cell = cloneSelf:GetComponent("CLCellLua");
    cell:init(mData, nil);
    cell.luaTable.isCloned = true;
    local dragScrollView = cloneSelf:GetComponent("UIDragScrollView");
    if(dragScrollView ~= nil) then
      dragScrollView.enabled = false;
    end

    cloneSelf.transform.localPosition = transform.localPosition;
    cloneSelf.transform.localRotation = transform.localRotation;
    cloneSelf.transform.localScale = transform.localScale;
    return cloneSelf;
  end

  return uiCell;
end
