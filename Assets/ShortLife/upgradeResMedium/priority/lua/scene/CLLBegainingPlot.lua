-- 开场场景控制
do
    CLLBegainingPlot = {}

    local smoothFollow = SCfg.self.mainCamera:GetComponent("CLSmoothFollow");
    local mLookatTarget = SCfg.self.mLookatTarget;
    local csSelf = nil;
    local transform = nil;
    local pulse = nil;
    local role = nil;

    local task1 = nil;
    local task2 = nil;
    local task3 = nil;
    -- 是否扫行任务
    local isDoTask1 = false;
    local isDoTask2 = false;
    local isDoTask3 = false;
    local totalDeltaVal1 = 0;
    local totalDeltaVal2 = 0;
    local totalDeltaVal3 = 0;

    -- 初始化，只会调用一次
    function CLLBegainingPlot.init(csObj)
        csSelf = csObj;
        transform = csObj.transform;
        pulse = getChild(transform, "pulse");
        role = getChild(transform, "bed", "Role1"):GetComponent("CLRole");
    end

    -- 设置数据
    function CLLBegainingPlot.setData(paras)
    end

    -- 显示，在c#中。show为调用refresh，show和refresh的区别在于，当页面已经显示了的情况，当页面再次出现在最上层时，只会调用refresh
    function CLLBegainingPlot.show4Start()
        mLookatTarget.localEulerAngles = Vector3(0, 135, 0);
        mLookatTarget.transform.position = Vector3(0, 13, 0);
        -- 移动 mLookatTarget 的y

        local task = {
            type = 1, -- 移动lookatTarget
            from = Vector3(0, 13, 0),
            to = Vector3(0, 3, 0),
            speed = 0.5,
            next = nil, -- 下一个任务
            nextDelaySec = 2, -- 下一个任务等待时间
            finishCallback = CLLBegainingPlot.onFinishMoveToShow,
        };
        CLLBegainingPlot.doTask1(task);
        NGUITools.SetActive(csSelf.gameObject, true);
    end

    function CLLBegainingPlot.onFinishMoveToShow()
        local task = {
            type=2, -- 改变smoothFollow的distance
            from = 8,
            to = 0.9,
            speed = 0.4,
            next=nil,
            nextDelaySec = 0,
        }

        local _task2 = {
            type=3, --改变smoothFollow的heigh
            from = 4,
            to = 0.5,
            speed = 0.4,
            next=nil,
            nextDelaySec = 0,
        }
        local _task3 = {
            type = 1, -- 移动lookatTarget
            from = Vector3(0, 3, 0),
            to = pulse.transform.localPosition,
            speed = 0.3,
            next = nil, -- 下一个任务
            nextDelaySec = 2, -- 下一个任务等待时间
            finishCallback = CLLBegainingPlot.onFinishShowPulse,
        };

        csSelf:invoke4Lua("doTask1", task, 4);
        csSelf:invoke4Lua("doTask2", _task2, 4);
        csSelf:invoke4Lua("doTask3", _task3, 4);
    end


    function CLLBegainingPlot.onFinishShowPulse()
        local _task = {
            type = 1, -- 移动lookatTarget
            from = pulse.transform.localPosition,
            to = Vector3(0.11, 0.68, -0.59),
            speed = 0.3,
            next = nil, -- 下一个任务
            nextDelaySec = 2, -- 下一个任务等待时间
            finishCallback = nil,
        };

        _task.next = {
            type = 1, -- 移动lookatTarget
            from = Vector3(0.11, 0.68, -0.59),
            to = Vector3.zero,
            speed = 0.3,
            next = nil, -- 下一个任务
            nextDelaySec = 0, -- 下一个任务等待时间
            finishCallback = nil,
        };
        csSelf:invoke4Lua("doTask1", _task, 2);

        local _task2 = {
            type=2, -- 改变smoothFollow的distance
            from = 0.9,
            to = 0.2,
            speed = 0.2,
            next=nil,
            nextDelaySec = 0,
        }

        local _task3 = {
            type=3, --改变smoothFollow的heigh
            from = 0.5,
            to = 10,
            speed = 0.2,
            next=nil,
            nextDelaySec = 0,
            finishCallback=nil,
        }
        _task3.next = {
            type=3, --改变smoothFollow的heigh
            from = 10,
            to = 1530,
            speed = 0.2,
            next=nil,
            nextDelaySec = 0,
            finishCallback=CLLBegainingPlot.onFinshShowPlot,
        }
        csSelf:invoke4Lua("doTask2", _task2, 3);
        csSelf:invoke4Lua("doTask3", _task3, 5);
    end

    function CLLBegainingPlot.onFinshShowPlot()

    end

        -- 关闭
    function CLLBegainingPlot.hide()
        NGUITools.SetActive(csSelf.gameObject, false);
        CLThingsPool.returnObj(csSelf.gameObject.name, csSelf.gameObject);
    end

    -- 处理ui上的事件，例如点击等
    function CLLBegainingPlot.uiEventDelegate(go)
        local goName = go.name;
        --[[
        if(goName == "xxx") then
          --TODO:
        end
        --]]
    end

    function CLLBegainingPlot.doTask1(task)
        totalDeltaVal1 = 0;
        task1 = task;
        isDoTask1 = true;
    end

    function CLLBegainingPlot.doTask2(task)
        totalDeltaVal2 = 0;
        task2 = task;
        isDoTask2 = true;
    end

    function CLLBegainingPlot.doTask3(task)
        totalDeltaVal3 = 0;
        task3 = task;
        isDoTask3 = true;
    end

    function CLLBegainingPlot.FixedUpdate()
        if (isDoTask1 and task1 ~= nil) then
            totalDeltaVal1 = totalDeltaVal1 + Time.fixedDeltaTime * task1.speed;
            CLLBegainingPlot._doTask(task1, totalDeltaVal1);
        end

        if (isDoTask2 and task2 ~= nil) then
            totalDeltaVal2 = totalDeltaVal2 + Time.fixedDeltaTime * task2.speed;
            CLLBegainingPlot._doTask(task2, totalDeltaVal2);
        end

        if (isDoTask3 and task3 ~= nil) then
            totalDeltaVal3 = totalDeltaVal3 + Time.fixedDeltaTime * task3.speed;
            CLLBegainingPlot._doTask(task3, totalDeltaVal3);
        end
    end

    function CLLBegainingPlot._doTask(task, totalDeltaVal)
        local val = task.from + (task.to - task.from) * totalDeltaVal;
        if (task.type == 1) then
            mLookatTarget.localPosition = val;
        elseif(task.type == 2) then
            smoothFollow.distance = val;
        elseif(task.type == 3) then
            smoothFollow.height= val;
        end

        if (totalDeltaVal >= 1) then
            -- 已经完成当前任务
            local i = 1;
            if (task == task1) then
                i = 1;
                isDoTask1 = false;
            elseif (task == task2) then
                i = 2;
                isDoTask2 = false;
            elseif (task == task3) then
                i = 3;
                isDoTask3 = false;
            end
            totalDeltaVal = 0;

            if (task.finishCallback ~= nil) then
                -- 完成回调
                task.finishCallback();
            end

            -- 处理下一个任务
            task = task.next;
            if (task ~= nil) then
                csSelf:invoke4Lua("doTask" .. i, task, task.nextDelaySec);
            end
        end
    end


    --------------------------------------------
    return CLLBegainingPlot;
end
