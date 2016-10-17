do
    require("CLLData");

    local LuaUtl = require("LuaUtl");

    -- 第三方函数的local变量
    local bio2Int = NumEx.bio2Int;
    local setActive = NGUITools.SetActive;
    local lizGet = Localization.Get;
    local getChild = LuaUtl.getChild;
    local altxtAdd = NAlertTxt.add;

    -- 界面元素对象
    local cell = nil;
    local transform = nil;

    local Label;
    local ProgressBar;

    -- 属性值变量
    local mData = nil;

    local uiCell = {};

    function uiCell.init(go)
        transform = go.transform;
        cell = transform:GetComponent("CLCellLua");

        Label = getChild(transform, "Label"):GetComponent("UILabel");
        ProgressBar = getChild(transform, "Progress Bar"):GetComponent("UISlider");
    end

    function uiCell.refresh(go, data)
        mData = data;
        NGUITools.SetActive(ProgressBar.gameObject, true);
        Label.text = "";
    end

    function uiCell.LateUpdate()
        if(mData == nil or mData.www == nil) then
            NGUITools.SetActive(ProgressBar.gameObject, false);
            Label.text = "";
            return
        end;
--        Label.text = PStr.b():a(mData.url):a("..."):a(mData.www.progress*100):a("%"):e();
        Label.text = PStr.b():a(math.floor(mData.www.progress*100)):a("%"):e();
        ProgressBar.value = mData.www.progress;
    end

    function uiCell.getdata()
        return mData, cell;
    end
    ------------------------------------
    return uiCell;
end
