﻿-- xx界面
do
  CLLPOpeningPlot = {}

  local csSelf = nil;
  local transform = nil;

  -- 初始化，只会调用一次
  function CLLPOpeningPlot.init(csObj)
    csSelf = csObj;
    transform = csObj.transform;
    --[[
    上的组件：getChild(transform, "offset", "Progress BarHong"):GetComponent("UISlider");
    --]]
  end

  -- 设置数据
  function CLLPOpeningPlot.setData(paras)
  end

  -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
  function CLLPOpeningPlot.show()
  end

  -- 刷新
  function CLLPOpeningPlot.refresh()
  end

  -- 关闭页面
  function CLLPOpeningPlot.hide()
  end

  -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
  function CLLPOpeningPlot.procNetwork (cmd, succ, msg, paras)
    --[[
    if(succ == 0) then
      if(cmd == "xxx") then
        -- TODO:
      end
    end
    --]]
  end

  -- 处理ui上的事件，例如点击等
  function CLLPOpeningPlot.uiEventDelegate( go )
    local goName = go.name;
    --[[
    if(goName == "xxx") then
      --TODO:
    end
    --]]
  end

  -- 当按了返回键时，关闭自己（返值为true时关闭）
  function CLLPOpeningPlot.hideSelfOnKeyBack( )
    return true;
  end

  --------------------------------------------
  return CLLPOpeningPlot;
end
