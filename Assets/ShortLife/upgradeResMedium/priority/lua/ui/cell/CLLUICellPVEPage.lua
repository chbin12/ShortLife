-- xx单元
do
  local uiCell = {}
  local csSelf = nil;
  local transform = nil;
  local points = {};
  local npcObjs = {};
  local LabelMapName = nil;
  local dragPage = nil;

  local mData = nil;

  -- 初始化，只调用一次
  function uiCell.init (csObj)
    csSelf = csObj;
    transform = csSelf.transform;
    dragPage = transform:GetComponent("UIDragPage4Lua");
    LabelMapName = getChild(transform,  "SpriteTitle","LabelMapName"):GetComponent("UILabel");
    local pointRoot = getChild(transform, "pointRoot");
    for i=1,10 do
      table.insert(points, pointRoot:FindChild(i));
    end

    local npcRoot = getChild(transform, "npcRoot");
    for i=1,10 do
      table.insert(npcObjs, npcRoot:FindChild(i):GetComponent("CLCellLua"));
    end
  end

  -- 显示，
  -- 注意，c#侧不会在调用show时，调用refresh
  -- function uiCell.show ( go, data )
  -- end
  
  -- 注意，c#侧不会在调用show时，调用refresh
  function uiCell.refresh( data )
    mData = data;
    uiCell.setNpcs();
  end

  -- 刷新当前页面
  function uiCell.refreshCurrent( pageIndex, data )
    uiCell.refresh(data);
  end
  --[[
  武威
  天水
  安定
  --]]

  function uiCell.setNpcs()
    local cell = nil;
    for i=1,10 do
      local d = {};
      if(i == 1 and mData == 1) then
        d.state = 1;
        d.name = "武威";
      elseif(i == 2 and mData == 1) then
        d.state = 2;
        d.name = "天水";
      elseif(i == 3 and mData == 1) then
        d.state = 2;
        d.name = "安定";
      else
        d.state = 3;
      end
      d.id = i;
      ---------------------------------
      cell = npcObjs[i];
      cell.transform.parent = points[i];
      cell.transform.localPosition = Vector3.zero;
      cell.transform.localScale = Vector3.one;
      cell:init({d, dragPage}, uiCell.onClickNpc);
    end
  end

  function uiCell.onClickNpc( cell )
    local data = cell.luaTable.getData();
    if(data.state == 3) then
      NAlertTxt.add(Localization.Get("MsgLocked"), Color.red, 1, 1);
      return;
    end
    local offense = {};
    local defense = {};

    if(data.id == 2) then
      local role = {};
      role.gid = int2Bio(2);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 3;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(offense, role);

      role = {};
      role.gid = int2Bio(5);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 3;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(defense, role);
    else
      local role = {};
      role.gid = int2Bio(1);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 4;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(offense, role);
      role = {};
      role.gid = int2Bio(5);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 5;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(offense, role);
      ---------------------------
      role = {};
      role.gid = int2Bio(4);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 2;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(defense, role);

      role = {};
      role.gid = int2Bio(3);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 3;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(defense, role);

      role = {};
      role.gid = int2Bio(1);
      role.star = int2Bio(1);
      role.lev = int2Bio(1);
      role.skill1Lev = int2Bio(1);
      role.pos = 4;
      role.attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(role.gid), bio2Int(role.lev));
      table.insert(defense, role);
    end

    CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, {type="battle", offense=offense, defense=defense, pveid=data.id});
  end
  -- 取得数据
  function uiCell.getData ( )
    return mData;
  end

  --------------------------------------------
  return uiCell;
end
