-- 血条
do

    local bio2Int = NumEx.bio2Int;
    local int2Bio = NumEx.int2Bio;
    local csSelf = nil;
    local transform = nil;
    local gameObject = nil;
    local SpriteIcon = nil;
    local ProgressBar = nil;
    local ProgressBarForeground = nil;
    local LabelNmae = nil;
    local LabelHP = nil;
    local mRole = nil;
    local roleAttr = nil;
    local roleData = nil;
    local isFinishInit = false;

    local uiCell = {}
    function uiCell.init(go)
        if (isFinishInit) then return end
        csSelf = go:GetComponent("SBSliderBar");
        isFinishInit = true;
        gameObject = go.gameObject;
        transform = go.transform;

--        SpriteIcon = getChild(transform, "SpriteIcon"):GetComponent("UISprite");
        ProgressBar = getChild(transform, "Progress Bar"):GetComponent("UISlider");
        ProgressBarForeground = ProgressBar.foregroundWidget:GetComponent("UISprite");

        LabelNmae = getChild(transform, "LabelNmae"):GetComponent("UILabel");
        LabelHP = getChild(transform, "LabelHP"):GetComponent("UILabel");
        NGUITools.SetActive(LabelNmae.gameObject, false);
    end

    function uiCell.show(go, data)
        mRole = data;
        roleAttr = mRole.luaTable.getAttr();
        roleData = mRole.luaTable.getData();

        if (mRole.isOffense) then
            --      ProgressBarForeground.spriteName = "fight_xuetiaohong";
            ProgressBarForeground.color = Color.green;
        else
            --      ProgressBarForeground.spriteName = "fight_xuetiaonlan";
            ProgressBarForeground.color = Color.yellow;
        end

--        LabelNmae.text = Localization.Get(roleAttr.base.NameKey);
        ProgressBar.value = 1;
--        LabelHP.text = PStr.b():a(bio2Int(roleData.currHP)):a("/"):a(bio2Int(roleData.HP)):e();
        LabelHP.text = "";
        NGUITools.SetActive(LabelNmae.gameObject, false);
--        NGUITools.SetActive(SpriteIcon.gameObject, false);
    end

    function uiCell.refresh(paras)
    end

    function uiCell.cutHP(hurt)
        local color = Color.red;
        if(hurt > 0) then
            color = Color.green;
        end
        csSelf:addHudtxt(hurt, color, 0, 1.5);
        local currHP = bio2Int(roleData.currHP);
        local HP = bio2Int(roleData.HP);
        ProgressBar.value = currHP / HP;
--        LabelHP.text = PStr.b():a(currHP):a("/"):a(HP):e();
    end

    function uiCell.getData()
        return mRole;
    end

    return uiCell;
end
