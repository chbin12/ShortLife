-- 切换场景管理器
do
    require("CLLPrefabInit");
    local dragSetting = CLUIDrag4World.self;
    local smoothFollow = SCfg.self.mainCamera:GetComponent("CLSmoothFollow");

    local bio2Int = NumEx.bio2Int;
    local int2Bio = NumEx.int2Bio;
    local csSelf = nil;
    local transform = nil;
    local progressBar = nil;
    local LabelTips = nil;
    local spriteBg = nil;
    local data = nil;
    --[[data.type = home, goPVE, showMainCity，rePlay, map
       data.callback
    --]]

    PanelLoadScene = {}

    function PanelLoadScene.init(go)
        transform = go.transform;
        csSelf = go;
        progressBar = getChild(transform, "Progress Bar"):GetComponent("UISlider");
        LabelTips = getChild(transform, "LabelTips"):GetComponent("UILabel");
        spriteBg = getChild(transform, "SpriteBg"):GetComponent("UISprite");
    end

    function PanelLoadScene.setData(pars)
        data = pars;
    end

    function PanelLoadScene.show()
        progressBar.value = 0;
        spriteBg.color = NewColor(0, 0, 0, 255);
        NGUITools.SetActive(progressBar.gameObject, false);
        LabelTips.text = Localization.Get("Loading");

        if(data.isGuid) then
            NGUITools.SetActive(LabelTips.gameObject, false);
            NGUITools.SetActive(spriteBg.gameObject, false);
        else
            NGUITools.SetActive(LabelTips.gameObject, true);
            NGUITools.SetActive(spriteBg.gameObject, true);
        end


        if (data.type == "home") then
            csSelf:invoke4Lua("gotoHome", 0.1);
        elseif (data.type == "battle") then
            csSelf:invoke4Lua("gotoBattle", 0.1);
        elseif (data.type == "explore") then
            csSelf:invoke4Lua("gotoExplore", 0.1);
        end
    end

    function PanelLoadScene.releaseRes(...)
        CLUIInit.self.emptAtlas:releaseAllTexturesImm();
        CLMain.self.lua:LuaGC(nil);
        SAssetsManager.self:releaseAsset(true);
        GC.Collect(0); -- 内存释放
        GC.Collect(1); -- 内存释放
        Resources.UnloadUnusedAssets();
    end

    function PanelLoadScene.onProgress(all, curr)
        NGUITools.SetActive(progressBar.gameObject, true);
        progressBar.value = curr / all;
    end

    function PanelLoadScene.hide()
    end

    function PanelLoadScene.refresh()
    end

    function PanelLoadScene.gotoHome(...)
        PanelLoadScene.releaseRes();

        spriteBg.color = NewColor(0, 0, 0, 64);
        -- 设置模式
        SCfg.self.mode = GameMode.normal;
        ---------------------------------
        smoothFollow.distance = 8;
        smoothFollow.height = 4;
        SCfg.self.mLookatTarget.localEulerAngles = Vector3(0, 0, 0);
        SCfg.self.mLookatTarget.position = Vector3(0, 9, -10);

        SCfg.self.mainCamera.fieldOfView = 50;
        SCfg.self.mainCamera.clearFlags = CameraClearFlags.Skybox;
        SCfg.self.mainCamera.transform.localEulerAngles = Vector3.zero;

        ---------------------------------
        dragSetting.canRotation = false;
        dragSetting.canScale = false;
        dragSetting.scaleMini = 5;
        dragSetting.scaleMax = 10;
        dragSetting.scaleHeightMini = 5;
        dragSetting.scaleHeightMax = 10;

        ---------------------------------
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = NewColor(154, 180, 255, 255);
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 300;
        RenderSettings.fog = true;
        -- RenderSettings.fogDensity = 0;

        ---------------------------------
        CLLCameraEffect.enabled(false);
        CLLCameraEffect.setFocalPoint(10);

        ---------------------------------
        SCfg.self.mode = GameMode.normal;
        --        PanelLoadScene.loadCityRes();
        CLLScene.stopSpin();
        local onLoadRole = function(name, role, ors)
            NGUITools.SetActive(role.gameObject, false);
            CLLScene.loadInfiniteMap(20, 8, 0.01, 5, -1, PanelLoadScene.onLoadMap);
        end

        local playerData = CLLData.player;
        local attr = CLLDBCfg.getRoleByGIDAndLev(bio2Int(playerData.gid), 1);
        CLRolePool.borrowUnitAsyn(attr.base.PrefabName, onLoadRole, { playerData, attr });
    end

    function PanelLoadScene.onLoadMap()
        PanelLoadScene.showCityUI();
    end

    function PanelLoadScene.gotoBattle()
        PanelLoadScene.releaseRes();

        --        spriteBg.color = NewColor(0, 0, 0, 64);
        -- 设置模式
        SCfg.self.mode = GameMode.normal;
        ---------------------------------
        smoothFollow.distance = 7;
        smoothFollow.height = 6;
        SCfg.self.mLookatTarget.localEulerAngles = Vector3(0, 0, 0);
        SCfg.self.mLookatTarget.position = Vector3(0, 0, -10);

        SCfg.self.mainCamera.fieldOfView = 60;
        SCfg.self.mainCamera.clearFlags = CameraClearFlags.Skybox;
        SCfg.self.mainCamera.transform.localEulerAngles = Vector3(38, 0, 0);

        ---------------------------------
        dragSetting.canRotation = false;
        dragSetting.canScale = false;
        dragSetting.scaleMini = 5;
        dragSetting.scaleMax = 10;
        dragSetting.scaleHeightMini = 5;
        dragSetting.scaleHeightMax = 10;

        ---------------------------------
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = NewColor(154, 180, 255, 255);
        RenderSettings.fogStartDistance = 8;
        RenderSettings.fogEndDistance = 30;
        RenderSettings.fog = true;
        -- RenderSettings.fogDensity = 0;

        ---------------------------------
        CLLCameraEffect.enabled(false);
        CLLCameraEffect.setFocalPoint(10);

        ---------------------------------
        CLLScene.stopSpin();

        ---------------------------------
        SCfg.self.mode = GameMode.battle;
        CLUIOtherObjPool.setPrefab("LifeBar", PanelLoadScene.onSetLifeBar, nil);
    end

    function PanelLoadScene.onSetLifeBar(...)
        -- load role
        local data = {}
        data.gid = 1;
        data.lev = 1;
        CLLPrefabInit.initRole(data, PanelLoadScene.showBattleUI, PanelLoadScene.onProgress, true);
    end

    function PanelLoadScene.gotoExplore()
        PanelLoadScene.releaseRes();

        --        spriteBg.color = NewColor(0, 0, 0, 64);
        -- 设置模式
        SCfg.self.mode = GameMode.normal;
        ---------------------------------
--        smoothFollow.distance = 10;
--        smoothFollow.height = 17;
--        SCfg.self.mLookatTarget.localEulerAngles = Vector3(0, 0, 0);
--        SCfg.self.mLookatTarget.position = Vector3(0, 0, -10);

        SCfg.self.mainCamera.fieldOfView = 60;
        SCfg.self.mainCamera.clearFlags = CameraClearFlags.Skybox;
--        SCfg.self.mainCamera.transform.localEulerAngles = Vector3(38, 0, 0);

        ---------------------------------
        dragSetting.canRotation = false;
        dragSetting.canScale = false;
        dragSetting.scaleMini = 5;
        dragSetting.scaleMax = 10;
        dragSetting.scaleHeightMini = 5;
        dragSetting.scaleHeightMax = 10;

        ---------------------------------
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = NewColor(154, 180, 255, 255);
        RenderSettings.fogStartDistance = 8;
        RenderSettings.fogEndDistance = 30;
        RenderSettings.fog = false;
        -- RenderSettings.fogDensity = 0;

        ---------------------------------
        CLLCameraEffect.enabled(false);
        CLLCameraEffect.setVal(0.1, 0.7, 22)

        ---------------------------------
        CLLScene.stopSpin();

        ---------------------------------
        SCfg.self.mode = GameMode.explore;

        CLPanelManager.getPanelAsy("PanelExplore", onLoadedPanel);
    end

    function PanelLoadScene.showCityUI(...)
        if (data.isGuid) then
            csSelf:invoke4Lua("showExplore4Guid", 1);
        else
            CLPanelManager.hideTopPanel();
            CLPanelManager.getPanelAsy("PanelMain", onLoadedPanel);
        end
    end

    function PanelLoadScene.showBattleUI()
        CLLBattle.init();
        CLPanelManager.getPanelAsy("PanelBattle", onLoadedPanel);
    end

    function PanelLoadScene.showExplore4Guid()
        NAlertTxt.add("进入后，角色一开始还是全部绑了纱布，一个特效变身成背着书包的学生娃娃，接着才开始移动", Color.red, 2);
        CLPanelManager.hideTopPanel();
        CLPanelManager.getPanelAsy("PanelLoadScene", onLoadedPanel, { type = "explore", isGuid = true });
    end
    --------------------------------
    return PanelLoadScene;
end
