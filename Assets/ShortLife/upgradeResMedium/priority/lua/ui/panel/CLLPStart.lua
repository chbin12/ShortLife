--开始loading页面，处理资源更新，登陆注册
do
    require("CLLVerManager");

    local csSelf = nil;
    local transform = nil;
    local gameObject = nil;
    local progressBar = nil;
    local progressBarTotal = nil;
    local lbprogressBarTotal = nil;
    local upgradeUrl = nil; -- app更新url
    local LabelTip = nil;
    local LabelVer = nil;
    local isFinishLoadPanels = false;
    local loadedPanelCount = 0;
    local panelIndex = 0;
    local lbCustServer = nil;

    local www4UpgradeCell = nil; -- 更新时单个单元的www

    -- 预先加载的页面(在热更新完成后，先把必要的公共页面先加载了，后面的处理可能会用到)
    local beforeLoadPanels = {
        "PanelHotWheel",        -- 菊花
        "PanelBackplate",       -- 背板遮罩
        "PanelConfirm",         -- 确认提示页面
        "PanelMask4Panel",      -- 遮挡
        "PanelWWWProgress",     -- 显示网络请求资源的进度
    }

    -- 放在后面加载的页面
    local lateLoadPanels = {
        "PanelMain",        -- 主界面
        "PanelLoadScene",   -- 切换场景页面
    }

    PanelStart = {};

    function PanelStart.init(go)
        csSelf = go;
        transform = csSelf.transform;
        gameObject = csSelf.gameObject;
        progressBar = LuaUtl.getChild(transform, "Bottom", "Progress Bar");
        progressBar = progressBar:GetComponent("UISlider");
        NGUITools.SetActive(progressBar.gameObject, false);

        progressBarTotal = LuaUtl.getChild(transform, "Bottom", "Progress BarTotal");
        lbprogressBarTotal = LuaUtl.getChild(progressBarTotal, "Thumb", "Label"):GetComponent("UILabel");
        progressBarTotal = progressBarTotal:GetComponent("UISlider");
        NGUITools.SetActive(progressBarTotal.gameObject, false);

        LabelTip = LuaUtl.getChild(transform, "Bottom", "LabelTip");
        LabelTip = LabelTip:GetComponent("UILabel");
        NGUITools.SetActive(LabelTip.gameObject, false);
        LabelVer = LuaUtl.getChild(transform, "TopLeft", "LabelVer");
        LabelVer = LabelVer:GetComponent("UILabel");

        lbCustServer = LuaUtl.getChild(transform, "TopLeft", "LabelCustomerServer"):GetComponent("UILabel");
    end

    -- function PanelStart.setData (pars)
    -- end

    function PanelStart.show()
        NGUITools.SetActive(progressBar.gameObject, false);
        NGUITools.SetActive(progressBarTotal.gameObject, false);

        -- 设置哪些页面时可以点击3D场景
        -- CLUIDrag4World.setCanClickPanel("PanelBattle");

        -- 处理热更新
        csSelf:invoke4Lua("updateRes", 0.2);

        -- 添加屏蔽字
        -- PanelStart.addShieldWords();
    end

    -- 添加屏蔽字
    function PanelStart.addShieldWords()
        local path = PathCfg.self.basePath .. "/" .. PathCfg.upgradeRes .. "/priority/txt/shieldWords";
        CLVerManager.self:getNewestRes(path, CLAssetType.text, PanelStart.onGetShieldWords, nil);
    end

    function PanelStart.onGetShieldWords(path, content, originals)
        if (content ~= nil) then
            Trie.getInstanse():init(content);
        end
    end

    -- 处理热更新
    function PanelStart.updateRes()
        -- 先判断是否已经取得取渠道
        -- if(SCfg.channel == "none" or CLGboChn.getInstance().isFinishInited) then
        if (SCfg.Channel == "none") then
            -- 再验证是否只有某个渠道可热更新
            local chanelPath = "channels.txt";
            csSelf:StartCoroutine(FileEx.readNewAllText(chanelPath, PanelStart.onGetCanUpgradeChannels));
        else
            csSelf:invoke4Lua("updateRes", 0.3);
        end
    end

    -- 当得渠道数据
    function PanelStart.onGetCanUpgradeChannels(content)
        local channels = nil;
        if (content ~= nil) then
            channels = JSON.DecodeMap(content);
        else
            channels = Hashtable();
        end

        if (SCfg.Channel == "none" or
                (MapEx.getBool(channels, GLVar.dbData.SubChannel))) then
            PanelStart.doUpdateRes(true);
        else
            PanelStart.doUpdateRes(false);
        end
    end

    function PanelStart.onGetCanUpgradeChannelsFail(...)
        PanelStart.doUpdateRes(false);
    end

    function PanelStart.doUpdateRes(isDoUpgrade)
        -- 更新资源
        CLLVerManager.init(PanelStart.onProgress, PanelStart.onFinishResUpgrade, isDoUpgrade);
    end

    --设置进度条
    function PanelStart.onProgress(...)
        local args = { ... };
        local all = args[1]; -- 总量
        local v = args[2]; -- 当前值
        if (table.getn(args) >= 3) then
            www4UpgradeCell = args[3];
        else
            www4UpgradeCell = nil;
        end

        if (progressBarTotal ~= nil) then
            NGUITools.SetActive(progressBarTotal.gameObject, true);
            NGUITools.SetActive(LabelTip.gameObject, true);
            if (type(all) == "number") then
                if (all > 0) then
                    local value = v / all;
                    progressBarTotal.value = value;
                    if (www4UpgradeCell ~= nil) then -- 说明有单个资源
                    lbprogressBarTotal.text = v .. "/" .. all;
                    end
                    -- 单个资源的进度
                    PanelStart.onProgressCell();

                    -- 表明已经更新完成
                    if (value == 1) then
                        csSelf:cancelInvoke4Lua("onProgressCell");
                        NGUITools.SetActive(progressBarTotal.gameObject, false);
                        NGUITools.SetActive(LabelTip.gameObject, false);
                        NGUITools.SetActive(progressBar.gameObject, false);
                    end
                else
                    csSelf:cancelInvoke4Lua("onProgressCell");
                    progressBarTotal.value = 0;
                    NGUITools.SetActive(progressBarTotal.gameObject, false);
                    NGUITools.SetActive(LabelTip.gameObject, false);
                    NGUITools.SetActive(progressBar.gameObject, false);
                end
            else
                print("all====" .. all);
            end
        end
    end

    function PanelStart.onProgressCell(...)
        if (www4UpgradeCell ~= nil) then
            NGUITools.SetActive(progressBar.gameObject, true);
            progressBar.value = www4UpgradeCell.progress;
            csSelf:cancelInvoke4Lua("onProgressCell");
            csSelf:invoke4Lua("onProgressCell", 0.1);
        else
            NGUITools.SetActive(progressBar.gameObject, false);
            csSelf:cancelInvoke4Lua("onProgressCell");
        end
    end

    -- 重新启动lua
    function PanelStart.reLoadGame(...)
        CLMain.self:reStart();
    end

    -- 资源更新完成
    function PanelStart.onFinishResUpgrade(arg)
        local upgradeProcSuccess = arg;
        if (upgradeProcSuccess == false) then
            NAlertTxt.add(Localization.Get("UpgradeResFailed"), Color.red, 1);
            -- return;
        else
            if (CLLVerManager.isHaveUpgrade()) then
                -- 说明有更新，重新启动
                csSelf:cancelInvoke4Lua("");
                -- NAlertTxt.add("CLMain.self:reStart", Color.red, 1);
                csSelf:invoke4Lua("reLoadGame", 0.3);
                return;
            end
        end
        PanelStart.prepareStartGame();
    end

    function PanelStart.prepareStartGame()
        -- 加载配置lua
        local luaPath = PathCfg.luaBasePath .. PathCfg.self.basePath .. "/upgradeRes/priority/lua/cfg/CLLDBCfg.lua";
        Utl.doLua(CLMain.self.lua, luaPath);

        -- 加载lua
        Net.self:setLua();
        CLScene.self:setLua();
        CLScene.self.luaTable.init(CLScene.self);
        CLBattle.self:setLua();
        --------------------
        -- 处理摄像机效果
        local cameraEffect = SCfg.self.mainCamera:GetComponent("CLBaseLua");
        cameraEffect:setLua();
        CLLCameraEffect.init(cameraEffect);

        -- ui部分的初始化，主要是页面的生成器，atlase设置，font设置
--        if (CLUIInit.self:init()) then
            -- 初始化需要提前加载的页面
            local count = #(beforeLoadPanels);
            loadedPanelCount = 0;
            for i = 1, count do
                CLPanelManager.getPanelAsy(beforeLoadPanels[i],
                    PanelStart.onLoadPanelBefore);
            end

            if (progressBar ~= nil) then
                csSelf:cancelInvoke4Lua("onProgressCell");
                NGUITools.SetActive(progressBar.gameObject, false);
                NGUITools.SetActive(progressBarTotal.gameObject, false);
                NGUITools.SetActive(LabelTip.gameObject, false);
            end

            -- 播放背景音乐---------------
            -- SoundEx.playMainMusic();
            ----------------------------
--        end
    end

    function PanelStart.onLoadPanelBefore(p)
        p:init();
        loadedPanelCount = loadedPanelCount + 1;
        if (p.name == "PanelConfirm" or
                p.name == "PanelHotWheel" or
                p.name == "PanelMask4Panel" or
                p.name == "PanelWWWProgress") then
            p.transform.parent = CLUIInit.self.uiPublicRoot;
            p.transform.localScale = Vector3.one;
        end

        if(p.name == "PanelWWWProgress") then
            CLPanelManager.showPanel(p);
        end
        PanelStart.onProgress(table.getn(beforeLoadPanels), loadedPanelCount);
        if (loadedPanelCount >= table.getn(beforeLoadPanels)) then
            -- 页面已经加载完成，加载地图资源
            csSelf:invoke4Lua("checkSignCode", 0.5);
        end
    end

    function PanelStart.checkSignCode()
        -- 把热更新及加载ui完了后，再做验证签名
        if (not PanelStart.isSignCodeValid()) then
            CLUIUtl.showConfirm(Localization.Get("MsgTheVerIsNotCorrect"), nil);
            -- CLUIUtl.showConfirm("亲爱的玩家你所下载的版本可能是非官方版本，请到xxx去下载。非常感谢！", nil);
            return;
        end

        PanelStart.connectServer();
    end

    -- 签名是否有效
    function PanelStart.isSignCodeValid(...)
        if(SCfg.self.singinMd5Code == "" or SCfg.self.singinMd5Code == nil) then
            return true;
        end
        -- 取得签名串
        local code = Utl.getSingInCodeAndroid();

        if (code ~= 0) then
            local md5Code = Utl.MD5Encrypt(code);
            if (md5Code ~= SCfg.self.singinMd5Code) then
                return false;
            end
        end
        return true;
    end

    -- 连接服务器相关处理
    function PanelStart.connectServer()
        -- 继续处理其它
        if (SCfg.self.isNetMode and Net.self.isReallyUseNet) then
            -- 连接网关
            PanelStart.connectGate();
        else
            PanelStart.finishConnectServer();
        end
    end

    -- 连接网关
    function PanelStart.connectGate(...)
        LuaUtl.showHotWheel();
        Net.self:connectGate();
    end

    -- 关闭页面
    function PanelStart.hide()
        csSelf:cancelInvoke4Lua("");
    end

    -- 刷新页面
    function PanelStart.refresh()
        LabelVer.text = Localization.Get("Version") .. __version__;
    end

    -- 处理网络接口
    function PanelStart.procNetwork(cmd, succ, msg, pars)
        LuaUtl.hideHotWheel();
        if (succ == 0) then
            -- 接口处理成功
            if (cmd == "connectCallback") then
                if (pars == Net.self.gateTcp) then
                    -- 网关连接成功
                    -- 取得服务器版本号
                    Net.self:sendGate(CallNet.verifyVer(SCfg.Channel, __version__));
                end
            elseif (cmd == "verifyVer") then
                if (SCfg.self.isNetMode) then
                    LuaUtl.showHotWheel();
                    -- 取得公告(注意是在消息分发的地方处理的显示)
                    Net.self:sendGate(CallNet.getNotices(SCfg.Channel, __version__));

                    -- send to server
                    Net.self:sendGate(CallNet.lgRegUser(Utl.uuid(), "", Utl.uuid(), SCfg.Channel,
                        SystemInfo.deviceModel, __version__));
                else
                    local p = CLPanelManager.getPanel("PanelEnterGame");
                    CLPanelManager.showTopPanel(p, true, true);
                end
            elseif (cmd == "registUser"
                    or cmd == "lgUser"
                    or cmd == "lgRegUser") then
                local data = pars;
                local user = data[0];
                -- local lastNsv = data[1];
                -- 判断用户是否已经被封号
                if (NumEx.bio2Int(user.status) == GboConstant.PubAttr.Type_User_Close) then
                    CLUIUtl.showConfirm(Localization.Get("MsgUserIsColsed"), nil);
                    return;
                end
                local p = CLPanelManager.getPanel("PanelEnterGame");
                p:setData(data);
                CLPanelManager.showTopPanel(p, true, true);

            elseif (cmd == "entryGame") then
                -- 进入游戏
                -- PanelStart.enterGame(nil, nil);
            elseif (cmd == "lastLoginSv") then
                Net.self.gateTcp:stop(); -- 关掉网关连接
            elseif (cmd == "getChatsByHttp") then
                local list = pars[1];
                local chat = list.list:get_Item(0);
                print(chat.type);
                print(chat.fpcid);
                print(chat.fpname);
                print(chat.content);
                print(chat.creattime);
                PanelStart.gotoBattle();
            end
        else
            -- 接口返回不成功
            if (cmd == "outofNetConnect") then
                CLUIUtl.showConfirm(Localization.Get("MsgOutofConnect"),
                    PanelStart.connectGate);
            elseif (cmd == "verifyVer") then
                -- 版本号验证失败
                if (succ ~= GboConstant.ResultStatus.R_User_ChnFalse) then
                    local data = pars;
                    upgradeUrl = data.nstr;
                    CLUIUtl.showConfirm(Localization.Get("MsgHaveNewVersion"),
                        PanelStart.upgradeGame);
                else
                    NAlertTxt.add(msg, Color.red, 1);
                end
            else
                NAlertTxt.add(msg, Color.red, 1);
            end
        end
    end

    -- 更新安装游戏
    function PanelStart.upgradeGame(...)
        if (upgradeUrl ~= nil and upgradeUrl ~= "") then
            Application.OpenURL(upgradeUrl);
        end
    end

    -- 点击返回键关闭自己（页面）
    function PanelStart.hideSelfOnKeyBack()
        return false;
    end


    function PanelStart.finishConnectServer()
        PanelStart.createPanel();
    end

    -- 创建ui
    function PanelStart.createPanel()
        local count = table.getn(lateLoadPanels);
        if (count > 0) then
            for i = 1, count do
                local name = lateLoadPanels[i];
                CLPanelManager.getPanelAsy(name, PanelStart.onLoadPanelAfter);
            end
        else
            PanelStart.doEnterGame();
        end
    end

    function PanelStart.onLoadPanelAfter(p)
        p:init();
        panelIndex = panelIndex + 1;
        local count = table.getn(lateLoadPanels);
        PanelStart.onProgress(count, panelIndex);
        if (panelIndex >= count) then
            --已经加载完
             csSelf:invoke4Lua("doEnterGame", 0.01);
--            clPrefabInit.init4MainCityRoles(PanelStart.doEnterGame, PanelStart.onProgress);
        end
    end

    function PanelStart.doEnterGame()
        CLLData.login();
        local guidStep = CLLData.getGuidStep();
        if guidStep <= 0 then
            -- 动画
--            Handheld.PlayFullScreenMovie("Sequence 03.mp4", Color.black, FullScreenMovieControlMode.Hidden);
            -- 新手引导里面跳入

            local onLoadPlot =  function(name, go, orgs)
                go.transform.position = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.localEulerAngles = Vector3(0, 0, 0);
                local plot = go:GetComponent("CLBehaviour4Lua");
                if(plot.luaTable == nil) then
                    plot:setLua();
                    plot.luaTable.init(plot);
                end
                NGUITools.SetActive(go, false);
                CLPanelManager.getPanelAsy("PanelOpeningPlot", PanelStart.onloadedGuid);
            end
            CLThingsPool.borrowObjAsyn("BegainingPlot", onLoadPlot );
        else
            -- CLPanelManager.getPanelAsy ("PanelMain", PanelStart.onLoadedPanel);
            CLPanelManager.getPanelAsy("PanelLoadScene", PanelStart.onLoadedPanel);
        end
    end

    function PanelStart.onLoadedPanel(p)
        CLPanelManager.hideTopPanel();
        p:setData({ type = "home" });
        CLPanelManager.showTopPanel(p);
    end

    function PanelStart.onloadedGuid(p)
        -- LuaUtl.hideHotWheel();
        CLPanelManager.hideTopPanel();
        CLPanelManager.hideTopPanel();
        CLPanelManager.showTopPanel(p);
    end

    ----------------------------------------------
    return PanelStart;
end
