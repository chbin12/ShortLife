-- xx单元
do
  local uiCell = {}
  local csSelf = nil;
  local transform = nil;
  local lbName = nil;
  local lbPos = nil;
  local followTarget = nil;

  local mData = nil;

  -- 初始化，只调用一次
  function uiCell.init (csObj)
    csSelf = csObj;
    transform = csSelf.transform;
    --[[
    上的组件：getChild(transform, "offset", "Progress BarHong"):GetComponent("UISlider");
    --]]
    lbName = getChild(transform, "L_name"):GetComponent("UILabel");
    lbPos = getChild(transform, "L_pos"):GetComponent("UILabel");
    followTarget = transform:GetComponent("UIFollowTarget");
    followTarget:setCamera(SCfg.self.mainCamera, SCfg.self.uiCamera);
  end

  -- 显示，
  -- 注意，c#侧不会在调用show时，调用refresh
  function uiCell.show ( go, data )
    mData = data;
    followTarget:setTarget(go.transform, Vector3.zero);
    followTarget:LateUpdate();
    followTarget.enabled = true;
    if(mData.index == CLLMap.myCityIndex) then
      lbName.text = "我的城池"
    else
      lbName.text = "一个城池"
    end
    lbPos.text = string.format('(%d, %d)', mData.x, mData.y)
  end

  -- 注意，c#侧不会在调用show时，调用refresh
  function uiCell.refresh( paras )
    --[[
    if(paras == 1) then   -- 刷新血
      -- TODO:
    elseif(paras == 2) then -- 刷新状态
      -- TODO:
    end
    --]]
  end

  -- 取得数据
  function uiCell.getData ( )
    return mData;
  end

  --------------------------------------------
  return uiCell;
end
