-- xx界面
do
  CLLPPackage = {}

  local csSelf = nil;
  local transform = nil;
  local grid = nil;

  -- 初始化，只会调用一次
  function CLLPPackage.init(csObj)
    csSelf = csObj;
    transform = csObj.transform;
    grid = getChild(transform, "AnchorBottom", "offset", "PanelList", "Grid"):GetComponent("CLUILoopGrid");
  end

  -- 设置数据
  function CLLPPackage.setData(paras)
  end

  -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
  function CLLPPackage.show()
    grid:setList(CLLData.package, CLLPPackage.initCell);
  end

  function CLLPPackage.initCell(cell, obj)
    cell:init(obj, CLLPPackage.onClickCell);
  end

  function CLLPPackage.onClickCell(cell)
    local data = cell.luaTable.getData();
    SCfg.self.scene.luaTable.useStuff(data)
    CLPanelManager.hideTopPanel();
  end

  -- 刷新
  function CLLPPackage.refresh()
  end

  -- 关闭页面
  function CLLPPackage.hide()
  end

  -- 网络请求的回调；cmd：指命，succ：成功失败，msg：消息；paras：服务器下行数据
  function CLLPPackage.procNetwork (cmd, succ, msg, paras)
    --[[
    if(succ == 0) then
      if(cmd == "xxx") then
        -- TODO:
      end
    end
    --]]
  end

  -- 处理ui上的事件，例如点击等
  function CLLPPackage.uiEventDelegate( go )
    local goName = go.name;
    --[[
    if(goName == "xxx") then
      --TODO:
    end
    --]]
  end

  -- 当按了返回键时，关闭自己（返值为true时关闭）
  function CLLPPackage.hideSelfOnKeyBack( )
    return true;
  end

  --------------------------------------------
  return CLLPPackage;
end
