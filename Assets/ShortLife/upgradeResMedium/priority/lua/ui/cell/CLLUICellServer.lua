-- 服务器单元
do

  local LuaUtl = require("LuaUtl");

  local cell = nil;
  local transform = nil;
  local gameObject = nil;
  local LabelServer = nil;
  local LabelState = nil;
  local mData = nil;

  local uiCell = {}
  function uiCell.init (go)
    csSelf = go;
    gameObject = go.gameObject;
    transform = go.transform;
    LabelServer = LuaUtl.getChild(transform, "LabelServer"):GetComponent("UILabel");
    LabelState = LuaUtl.getChild(transform, "LabelState"):GetComponent("UILabel");
  end;

  function uiCell.show ( go, data )
    mData = data;
    LabelServer.Text = mData.svname;
    LabelState.Text = Localization.Get("ServerStatus_" .. NumEx.bio2Int(mData.status));
  end;

  function uiCell.refresh( paras )
  end

  function uiCell.getData ( )
    return gameObject, mData;
  end;
  return uiCell;
end
