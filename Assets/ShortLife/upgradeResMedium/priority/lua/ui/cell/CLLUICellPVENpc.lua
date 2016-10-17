-- npc单元
do
  local uiCell = {}
  local csSelf = nil;
  local transform = nil;
  local spriteCity = nil;
  local Label = nil;
  local SpriteFlag = nil;
  local SpriteLock = nil;
  local starRoot = nil;
  local SpriteStar1 = nil;
  local SpriteStar2 = nil;
  local SpriteStar3 = nil;
  local dragPage = nil;

  local mData = nil;

  -- 初始化，只调用一次
  function uiCell.init (csObj)
    csSelf = csObj;
    transform = csSelf.transform;

    spriteCity = transform:GetComponent("UISprite");
    Label = getChild(transform, "Label"):GetComponent("UILabel");
    SpriteFlag =  getChild(transform, "SpriteFlag").gameObject;
    SpriteLock =  getChild(transform, "SpriteLock").gameObject;
    starRoot =  getChild(transform, "starRoot");
    SpriteStar1 =  getChild(starRoot, "SpriteStar1").gameObject;
    SpriteStar2 =  getChild(starRoot, "SpriteStar2").gameObject;
    SpriteStar3 =  getChild(starRoot, "SpriteStar3").gameObject;
    starRoot = starRoot.gameObject;
  end

  -- 显示，
  -- 注意，c#侧不会在调用show时，调用refresh
  function uiCell.show ( go, data )
    mData = data[1];
    dragPage = data[2];

    if(mData.state == 1) then
      uiCell.refresh(1);
    elseif(mData.state == 2) then
      uiCell.refresh(2);
    elseif(mData.state == 3) then
      uiCell.refresh(3);
    end
  end

  -- 注意，c#侧不会在调用show时，调用refresh
  function uiCell.refresh( paras )
    if(paras == 1) then   -- 占领
      CLUIUtl.setSpriteFit(spriteCity, "PVEMap_pveCity");
      CLUIUtl.setSpriteGray(spriteCity, false);
      Label.text = mData.name;
      NGUITools.SetActive(SpriteFlag, true);
      NGUITools.SetActive(SpriteLock, false);
      NGUITools.SetActive(starRoot, true);
      NGUITools.SetActive(SpriteStar1, true);
      NGUITools.SetActive(SpriteStar2, true);
      NGUITools.SetActive(SpriteStar3, true);
    elseif(paras == 2) then -- 当前可以进攻
      CLUIUtl.setSpriteFit(spriteCity, "PVEMap_pveCity");
      CLUIUtl.setSpriteGray(spriteCity, false);
      Label.text = mData.name;
      NGUITools.SetActive(SpriteFlag, false);
      NGUITools.SetActive(SpriteLock, false);
      NGUITools.SetActive(starRoot, false);
    elseif(paras == 3) then -- 未解锁
      CLUIUtl.setSpriteFit(spriteCity, "PVEMap_pveCity");
      CLUIUtl.setSpriteGray(spriteCity, true);
      Label.text = "";
      NGUITools.SetActive(SpriteFlag, false);
      NGUITools.SetActive(SpriteLock, true);
      NGUITools.SetActive(starRoot, false);
    end
  end

  -- 取得数据
  function uiCell.getData ( )
    return mData;
  end

  function uiCell.OnPress(go, isPressed)
    dragPage:OnPress(isPressed);
  end
  
  function uiCell.OnDrag( go, delta )
    dragPage:OnDrag(delta);
  end

  --------------------------------------------
  return uiCell;
end
