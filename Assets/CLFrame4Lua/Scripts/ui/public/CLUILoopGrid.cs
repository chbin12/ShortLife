using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

/**
 * 1、脚本放在Scroll View下面的UIGrid的那个物体上 
 * 2、Scroll View上面的UIPanel的Cull勾选上
 * 3、每一个Item都放上一个UIWidget，调整到合适的大小（用它的Visiable） ，设置Dimensions
 * 4、需要提前把Item放到Grid下，不能动态加载进去
 * 5、注意元素的个数要多出可视范围至少4个
 **/
[RequireComponent(typeof(UIGrid))]
public class CLUILoopGrid : MonoBehaviour
{
	[HideInInspector]
	public bool isPlayTween = true;
	public TweenType twType = TweenType.position;
	public  float tweenSpeed = 0.01f;
	public  float twDuration = 0.5f;
	public UITweener.Method twMethod = UITweener.Method.EaseOut;
	private List<UIWidget> itemList = new List<UIWidget> ();
	private Vector4 posParam;
	private int wrapLineNum = 0;
	private Transform cachedTransform;
	UIGrid grid = null;
	GridVisiableChecker gridChecker = null;

//	void Awake ()
//	{
//		cachedTransform = this.transform;
//		grid = this.GetComponent<UIGrid> ();
//		float cellWidth = grid.cellWidth;
//		float cellHeight = grid.cellHeight;
//		posParam = new Vector4 (cellWidth, cellHeight, 
//			grid.arrangement == UIGrid.Arrangement.Horizontal ? 1 : 0,
//			grid.arrangement == UIGrid.Arrangement.Vertical ? 1 : 0);
//	}
	
	bool isFinishInit = false;
	int times = 0;
	int RealCellCount = 0;
	Vector3 oldGridPosition = Vector3.zero;
	Vector3 oldScrollViewPos = Vector3.zero;
	Vector2 oldClipOffset = Vector2.zero;
	UIScrollView _scrollView;

	public UIScrollView scrollView {
		get {
			if (_scrollView == null) {
				_scrollView = NGUITools.FindInParents<UIScrollView> (transform);
				if (_scrollView != null) {
					oldScrollViewPos = _scrollView.transform.localPosition;
					oldClipOffset = _scrollView.panel.clipOffset;
				}
			}
			return _scrollView;
		}
	}
	
	public void init ()
	{
		if (isFinishInit)
			return;

		cachedTransform = this.transform;
		grid = this.GetComponent<UIGrid> ();
		oldGridPosition = grid.transform.localPosition;
		_scrollView = scrollView;
		float cellWidth = grid.cellWidth;
		float cellHeight = grid.cellHeight;

		// 根据坐标系，水平方向增量是向右，x轴为+1,垂直方向增量是向下，y轴为-1，
		// 但 sign 计算的是反方向的 (从后向前移动为+1，从前向后移动为-1)
		// 这里的参数做反向设定。
		posParam = new Vector4 (cellWidth, cellHeight, 
			grid.arrangement == UIGrid.Arrangement.Horizontal ? -1 : 0,
			grid.arrangement == UIGrid.Arrangement.Vertical ? 1 : 0);
		wrapLineNum = grid.maxPerLine;
		if (wrapLineNum > 0 ) {
			posParam.z = -1;
			posParam.w = 1;
		}

		for (int i=0; i<cachedTransform.childCount; ++i) {
			Transform t = cachedTransform.GetChild (i);
			UIWidget uiw = t.GetComponent<UIWidget> ();
			uiw.name = string.Format ("{0:D5}", itemList.Count);
			itemList.Add (uiw);
		}
		RealCellCount = itemList.Count;
		grid.Reposition ();
		isFinishInit = true;		
		if (itemList.Count < 3) {
			Debug.LogError("The childCount < 3");
		}

		gridChecker = new GridVisiableChecker(grid);
	}

	public void resetClip ()
	{
		scrollView.panel.clipOffset = oldClipOffset;
		scrollView.transform.localPosition = oldScrollViewPos;
		grid.transform.localPosition = oldGridPosition;
	}
	
	object[] data = null;
	object initCellCallback;
	
	public void setList (object data, object initCellCallback)
	{
		setList (data, initCellCallback, true);
	}

	public void setList (object data, object initCellCallback, bool isNeedRePosition)
	{
		setList (data, initCellCallback, isNeedRePosition, isPlayTween);
	}
	
	public void setList (object data, object initCellCallback, bool isNeedRePosition, bool isPlayTween)
	{
		setList (data, initCellCallback, isNeedRePosition, isPlayTween, tweenSpeed);
	}

	public void setList (object data, object initCellCallback, bool isNeedRePosition, bool isPlayTween, 
	                     float tweenSpeed)
	{
		setList (data, initCellCallback, isNeedRePosition, isPlayTween, tweenSpeed, twDuration);
	}

	public void setList (object data, object initCellCallback, bool isNeedRePosition, bool isPlayTween, 
	                     float tweenSpeed, float twDuration)
	{
		setList (data, initCellCallback, isNeedRePosition, isPlayTween, tweenSpeed, twDuration, twMethod);
	}
	
	public void setList (object data, object initCellCallback, bool isNeedRePosition, bool isPlayTween, 
	                     float tweenSpeed, float twDuration, UITweener.Method twMethod)
	{
		setList (data, initCellCallback, isNeedRePosition, isPlayTween, tweenSpeed, twDuration, twMethod, twType);
	}

	public void setList (object data, object initCellCallback, bool isNeedRePosition, bool isPlayTween, 
	                     float tweenSpeed, float twDuration, UITweener.Method twMethod, TweenType twType)
	{
		if (data == null) {
			Debug.LogError ("Data is null");
			return;
		}
		object[] list = null;
		if (data is LuaTable) {
			list = ((LuaTable)data).ToArray<object> ();
		} else if (data is ArrayList) {
			list = ((ArrayList)data).ToArray ();
		} else if (data is object[]) {
			list = (object[])(data);
		}
		_setList (list, initCellCallback, isNeedRePosition, isPlayTween, tweenSpeed, twDuration, twMethod, twType);
	}
	
	void _setList (object[] data, object initCellCallback, bool isNeedRePosition, bool isPlayTween, float tweenSpeed, 
	               float twDuration, UITweener.Method twMethod, TweenType twType)
	{
		try {
			this.data = data;
			if (data == null) {
				Debug.LogError ("Data is null");
				return;
			}
			this.initCellCallback = initCellCallback;
			if (!isFinishInit) {
				init ();
			}
			int tmpIndex = 0;
			if (isNeedRePosition) {
				times = 0;
				itemList.Clear ();
			}
			int offset = 0;
			if (!isNeedRePosition) {
				for (int i=0; i< cachedTransform.childCount; ++i) {
					tmpIndex = int.Parse (cachedTransform.GetChild (i).name);
					if (tmpIndex >= this.data.Length) {
						offset = 1;
						break;
					}
				}
				if (offset > 0) {
					for (int i=0; i< cachedTransform.childCount; ++i) {
						Transform t = cachedTransform.GetChild (i);
						tmpIndex = int.Parse (t.name);
						if (tmpIndex - offset < 0) {
							UIWidget uiw = t.GetComponent<UIWidget> ();
							if (uiw.isVisible) {
								offset = 0;
								isNeedRePosition = true;
								break;
							}
						}
					}
				}
			}

			for (int i=0; i< cachedTransform.childCount; ++i) {
				Transform t = cachedTransform.GetChild (i);
				UIWidget uiw = t.GetComponent<UIWidget> ();
				
				if (isNeedRePosition) {
					tmpIndex = i;
				} else {
					tmpIndex = int.Parse (uiw.name);
				}
				tmpIndex = tmpIndex - offset;
				uiw.name = string.Format ("{0:D5}", tmpIndex);
				if (tmpIndex >= 0 && tmpIndex < this.data.Length) {
					NGUITools.SetActive (t.gameObject, true);
					if (this.initCellCallback != null) {
						if (typeof(Callback) == this.initCellCallback.GetType ()) {
							((Callback)this.initCellCallback) (t.GetComponent<CLCellBase> (), data [tmpIndex]);
						} else if (typeof(LuaFunction) == this.initCellCallback.GetType ()) {
							((LuaFunction)this.initCellCallback).Call (t.GetComponent<CLCellBase> (), data [tmpIndex]);
						}
					}
				} else {
					NGUITools.SetActive (t.gameObject, false);
				}

				if (isNeedRePosition) {
					itemList.Add (uiw);
				}
			}
			if (isNeedRePosition) {
				resetClip ();
				if (!isPlayTween || twType == TweenType.alpha || twType == TweenType.scale) {
					grid.Reposition ();
					scrollView.ResetPosition ();	
				}
				if (isPlayTween) {
					for (int i=0; i < itemList.Count; i++) {
						CLUIUtl.resetCellTween (i, grid, itemList [i].gameObject, tweenSpeed, twDuration, twMethod, twType);
					}
				}
			}
		} catch (System.Exception e) {
			Debug.LogError (e);
		}
	}

	void initCellData(UIWidget uiw,object celldata) {
		if (this.initCellCallback != null) {
			if (typeof(Callback) == this.initCellCallback.GetType ()) {
				((Callback)this.initCellCallback) (uiw.GetComponent<CLCellBase> (), celldata);
			} else if (typeof(LuaFunction) == this.initCellCallback.GetType ()) {
				((LuaFunction)this.initCellCallback).Call (uiw.GetComponent<CLCellBase> (), celldata);
			}
		}
	}
	// 从原顺序位置移动到指定位置
	void moveCellPos(UIWidget moved, UIWidget target, int newIdx, int targetIdx) {
		int offx = 0;
		int offy = 0;
		if (wrapLineNum > 0) {
			if (grid.arrangement == UIGrid.Arrangement.Vertical) {
				offx = (targetIdx / wrapLineNum) - (newIdx / wrapLineNum);
				offy = (targetIdx % wrapLineNum) - (newIdx % wrapLineNum);
			} else {
				offx = (targetIdx % wrapLineNum) - (newIdx % wrapLineNum);
				offy = (targetIdx / wrapLineNum) - (newIdx / wrapLineNum);
			}
		} else {
			offx = targetIdx - newIdx;
			offy = offx;
		}
		Vector3 offset = new Vector3 (offx * posParam.x * posParam.z, offy * posParam.y * posParam.w, 0);
		moved.cachedTransform.localPosition = target.cachedTransform.localPosition + offset;
	}

	public int ResortItemListByName(List<UIWidget> cache,int minIdx, int maxIdx) {
		int itemCnt = cache.Count;
		int cnt = System.Math.Min ( maxIdx-minIdx+1, itemCnt);
		List<UIWidget> temp = new List<UIWidget>();
		Dictionary<int,UIWidget> mapWidget = new Dictionary<int, UIWidget>();
		for (int i = 0; i<cnt; ++i) {
			UIWidget w = cache[i];
			int dataIdx = int.Parse(w.name); // 每个对象在init 时就会对应一个dataIndex
			// 只记录会重用的
			if (dataIdx >= minIdx && dataIdx < minIdx+cnt)
				mapWidget[dataIdx] = w;
			else
				temp.Add(w);
		}
		if (cnt < itemCnt) {
			int dataIdx = data.Length;
			for(int i = cnt; i < itemCnt; ++i) {
				UIWidget w = cache[i];
				w.name = string.Format("{0:D5}", dataIdx); dataIdx++;
				NGUITools.SetActive (w.gameObject, false);
			}
		}
		cache.Clear();
		int tempIdx = 0;
		for (int i = 0; i < cnt; ++i) {
			int dataIdx = minIdx + i;
			UIWidget w = null;
			if (dataIdx >= minIdx && dataIdx < minIdx+cnt)
			{
				if (mapWidget.ContainsKey(dataIdx) ) {
					w = mapWidget[dataIdx];
				} else {
					w = temp[tempIdx]; tempIdx++;
				}
				cache.Add(w);
				NGUITools.SetActive (w.gameObject, true);
			}
		}
		return cnt;
	}

	public void updateList(object[] data) {
		this.data = data;
		if (data == null) {
			Debug.LogError ("Data is null");
			return;
		}
		if (!isFinishInit) {
			init ();
		}

		List<UIWidget> cache = new List<UIWidget>();
		for (int i=0; i< cachedTransform.childCount; ++i) {
			Transform t = cachedTransform.GetChild (i);
			UIWidget uiw = t.GetComponent<UIWidget> ();
			cache.Add(uiw);
		}
		int dataLen = data.Length;
		int itemCnt = cache.Count;
		if (dataLen > 0 && itemCnt > 0)
		{
			IntRange result = new IntRange();
			List<Vector3> poslist = new List<Vector3>();
			bool found = gridChecker.Check(cache[0], dataLen, result, poslist);
			// 当没有发现有在可视的对象，那么就从后向前排
			if (!found) {
				for (int i = 0; i < itemCnt; ++i) {
					int v = dataLen-1-i;
					if (v>=0) result.Push(v);
				}
			}
			// print("-a-" + result.count);
			result.Expand(itemCnt, dataLen-1); // 扩展到数量为 itemCnt, 但值不能超过 dataLen-1
			// print("-b-" + result.count);
			// 对子对象列表优化，包括：排序，删除用不了的， SetActive.
			int cnt = ResortItemListByName(cache, result.minIdx, result.maxIdx );
			for (int i = 0; i < cnt; ++i) {
				int dataIdx = result.minIdx + i;
				UIWidget w = cache[i];
				Debug.Assert(w);
				initCellData(w, data[dataIdx]);
				w.name = string.Format("{0:D5}", dataIdx);
				w.cachedTransform.localPosition = poslist[dataIdx];
			}
			itemList = cache;
		} else {
			for(int i=0, imax=itemList.Count; i < imax; ++i) {
				UIWidget w = cache[i];
				w.name = string.Format("{0:D5}", i);
				NGUITools.SetActive (w.gameObject, false);
			}
			itemList.Clear();
		}
		RealCellCount = itemList.Count;
		// 用以保证滚动框自动对齐排列在尾的子项，避免形成空行。
		scrollView.Press(true);
		StartCoroutine( SimulationPressScrollView());		
	}
/*
	public void updateList1(object[] data) {
		this.data = data;
		if (data == null) {
			Debug.LogError ("Data is null");
			return;
		}
		if (!isFinishInit) {
			init ();
		}

		List<UIWidget> cache = new List<UIWidget>();
		for (int i=0; i< cachedTransform.childCount; ++i) {
			Transform t = cachedTransform.GetChild (i);
			UIWidget uiw = t.GetComponent<UIWidget> ();
			cache.Add(uiw);
		}

		int dataLen = data.Length;
		int itemCnt = cache.Count;
		if (dataLen > 0 && itemCnt > 0)
		{
			IntRange result = new IntRange();
			List<Vector3> poslist = new List<Vector3>();
			bool found = gridChecker.Check(cache[0], dataLen, result, poslist);
			// 当没有发现有在可视的对象，那么就从后向前排
			if (!found) {
				for (int i = 0; i < itemCnt; ++i) {
					int v = dataLen-1-i;
					if (v>=0) result.Push(v);
				}
			}
			print("-a-" + result.count);
			result.Expand(itemCnt, dataLen-1); // 扩展到数量为 itemCnt, 但值不能超过 dataLen-1
			print("-b-" + result.count);
			if(result.count >= itemCnt) {
				for (int i = 0; i < result.count; ++i) {
					int dataIdx = result.minIdx + i;
					UIWidget w = cache[i];
					if (w)
					{
						initCellData(w, data[dataIdx]);
						w.name = string.Format("{0:D5}", dataIdx);
						w.cachedTransform.localPosition = poslist[dataIdx];
						NGUITools.SetActive (w.gameObject, true);
					}
				}
				itemList = cache;
			} else { // result.cout < itemCnt
				List<UIWidget> temp = new List<UIWidget>();
				int pos = 0;
				for (int i = 0; i < result.count; ++i) {
					int dataIdx = result.minIdx + i;
					UIWidget w = cache[i];
					Debug.Assert(w, string.Format("ItemList must not null, idx={0}.", i) );
					initCellData(w, data[dataIdx]);
					w.name = string.Format("{0:D5}", dataIdx);
					w.cachedTransform.localPosition = poslist[dataIdx];
					NGUITools.SetActive (w.gameObject, true);
					temp.Add(w);
					pos = i+1;
				}
				for (int i = pos; i < itemCnt; ++i) {
					UIWidget w = cache[i];
					NGUITools.SetActive (w.gameObject, false);
				}
				itemList = temp;
			}
		} else {
			for(int i=0, imax=itemList.Count; i < imax; ++i) {
				UIWidget w = cache[i];
				w.name = string.Format("{0:D5}", i);
				NGUITools.SetActive (w.gameObject, false);
			}
			itemList.Clear();
		}
		RealCellCount = itemList.Count;
		// 用以保证滚动框自动对齐排列在尾的子项，避免形成空行。
		scrollView.Press(true);
		StartCoroutine( SimulationPressScrollView());		
	}

	// 只更新子项信息，不调整滚动条位置
	public void updateList2(object[] data) {
		try {
			this.data = data;
			if (data == null) {
				Debug.LogError ("Data is null");
				return;
			}
			if (!isFinishInit) {
				init ();
			}

			List<UIWidget> deleted = new List<UIWidget> (); // 不可见或需要变更name的子项
			List<UIWidget> cache = new List<UIWidget> ();  // 可见并name不用变更的子项
			int tmpIndex = 0;
			int dataLen = data.Length;
			int minIdx = int.MaxValue;
			int maxIdx = int.MinValue;
			UIWidget minWidget = null;
			UIWidget maxWidget = null;
			// 分类需要修改和不需要修改两类
			for (int i=0; i< cachedTransform.childCount; ++i) {
				Transform t = cachedTransform.GetChild (i);
				UIWidget uiw = t.GetComponent<UIWidget> ();

				tmpIndex = int.Parse (uiw.name);
				if (tmpIndex >= 0 && tmpIndex < dataLen) {
					NGUITools.SetActive (uiw.gameObject, true);
					initCellData(uiw, data[tmpIndex]);
					cache.Add(uiw);
					if (tmpIndex < minIdx) {
						minIdx = tmpIndex; minWidget = uiw;
					}
					if (tmpIndex > maxIdx) {
						maxIdx = tmpIndex; maxWidget = uiw;
					}
				} else {
					deleted.Add(uiw);
				}
			}
			
			if (cache.Count > 1 ) {
				// 排序，以保证 itemList 的前小后大的顺序，才能实现滚动回收效果
				cache.Sort (delegate(UIWidget x, UIWidget y) {
					int a = int.Parse (x.name);
					int b = int.Parse (y.name);
					return a.CompareTo(b);
				});
			}

//			if (cache.Count > 2 ) {
//				print(string.Format("C>cache: head={0} tail={1} count={2}\n", cache[0].name, 
//					cache[cache.Count-1].name, cache.Count));
//			} else {
//				print(string.Format("C>cache cnt={0}, deleted cnt= {1}\n", cache.Count, deleted.Count));
//			}
			// 需要修改的都向前移动，超出数据长度的冻结起来
			int deleteLen = deleted.Count;
			if (minWidget != null && maxWidget != null) {
				for (int i=0; i < deleteLen; i++) {
					UIWidget uiw = deleted[i];
					if (minIdx-1 >= 0) {
						int newIdx = minIdx-1;
//						print(string.Format("S> new={0} min={1} max={2}\n", newIdx, minIdx, maxIdx));
						uiw.name = string.Format ("{0:D5}", newIdx);
						moveCellPos(uiw, minWidget, newIdx, minIdx);
						initCellData(uiw, data[newIdx]);
						minWidget = uiw;
						minIdx = newIdx;
						cache.Insert (0, uiw);
						NGUITools.SetActive (uiw.gameObject, true);
					} else {
						// 加入添加到末端的处理
						int newIdx = maxIdx + 1;
//						print(string.Format("B> new={0} min={1} max={2}\n", newIdx, minIdx, maxIdx));
						uiw.name = string.Format("{0:D5}", newIdx);
						moveCellPos(uiw, maxWidget, newIdx, maxIdx);
						if (newIdx >= 0 && newIdx < dataLen ) {
							initCellData(uiw, data[newIdx]);
							cache.Insert (cache.Count, uiw);
							NGUITools.SetActive (uiw.gameObject, true);
						}
						else
						{	// 进入冻结状态
							NGUITools.SetActive (uiw.gameObject, false);
						}
						maxWidget = uiw;
						maxIdx = newIdx;
					}
				}
				itemList = cache;
			} else {
				// 无数据的情况，简化处理
				grid.Reposition();
				itemList.Clear();
				for (int i=0; i< cachedTransform.childCount; ++i) {
					Transform t = cachedTransform.GetChild (i);
					UIWidget uiw = t.GetComponent<UIWidget> ();
					uiw.name = string.Format("{0:D5}", i);
					NGUITools.SetActive (uiw.gameObject, false);
					itemList.Add(uiw);
				}
			}
			RealCellCount = itemList.Count;
			// 用以保证滚动框自动对齐排列在尾的子项，避免形成空行。
			scrollView.Press(true);
			StartCoroutine( SimulationPressScrollView());
		} catch (System.Exception e) {
			Debug.LogError (e);
		}			
	}
	*/
	
	protected IEnumerator SimulationPressScrollView ()
	{
		yield return new WaitForEndOfFrame();
		if (scrollView != null) scrollView.Press(false);
	}

	// 只更新可视范围以内的对象
	void UpdateAllVisableItems() {
		int dataLen = this.data.Length;
		int itemCnt = itemList.Count;
		// 无内容情况下会忽略
		if (dataLen==0 || itemCnt==0 || gridChecker==null)
			return;

		IntRange result = new IntRange();
		List<Vector3> poslist = new List<Vector3>();
		bool found = gridChecker.Check(itemList[0], dataLen, result, poslist);
		// 当没有发现有在可视的对象，那么就直接忽略，这种情况是常见的，一般出现
		// 在快速拖动到列表首尾的时候。
		if (found) {
			// print("-a-" + result.count);
			result.Expand(itemCnt, dataLen-1);
			// print("-b-" + result.count);
			Debug.Assert(result.count==itemCnt, "IntRange Expand result must eq itemList count.");
			for (int i = 0; i < result.count; ++i) {
				int dataIdx = result.minIdx + i;
				UIWidget w = itemList[i];
				initCellData(w, data[dataIdx]);
				w.name = string.Format("{0:D5}", dataIdx);
				w.cachedTransform.localPosition = poslist[dataIdx];
			}
		}
	}
	// 检查是否需要整体更新
	bool CheckUpdateAllCondtion() {
		if (scrollView.isDragging) {
			return false;
		}
		int usingcnt = itemList.Count;
		for (int i=0; i<usingcnt; ++i) {
			UIWidget wid = itemList[i];
			if ( wid.isVisible ) {
				return false;
			}
		}
		// print ("to CheckUpdateAllCondtion.");
		UpdateAllVisableItems();
		return true;
	}

	int sourceIndex = -1;
	int targetIndex = -1;
	int sign = 0;
	bool firstVislable = false;
	bool lastVisiable = false;
	UIWidget head;
	UIWidget tail;
	UIWidget checkHead;
	UIWidget checkTail;


//	void LateUpdate ()
	void Update ()
	{
		if (itemList.Count < 3) {
			return;
		}
		// BUGFIX: 快速拖动滚动, 可出现无内容的情况，这里做出修正
		if (CheckUpdateAllCondtion())
			return;

		sourceIndex = -1;
		targetIndex = -1;
		sign = 0;
		head = itemList [0];
		tail = itemList [itemList.Count - 1];
		checkHead = itemList [1];
		checkTail = itemList [itemList.Count - 2];
		firstVislable = checkHead.isVisible;
		lastVisiable = checkTail.isVisible;
		int dataLen = data.Length;
		// if first and last both visiable or invisiable then return	
		if (firstVislable == lastVisiable) {
			return;
		}
		if (firstVislable && int.Parse (head.name) > 0) {
			times--;
			// move last to first one
			sourceIndex = itemList.Count - 1;
			targetIndex = 0;
			sign = 1;
		} else if (lastVisiable && int.Parse (tail.name) < dataLen - 1) {
			times++;
			// move first to last one
			sourceIndex = 0;
			targetIndex = itemList.Count - 1;
			sign = -1;
		}
		if (sourceIndex > -1) {
			UIWidget movedWidget = itemList [sourceIndex];

			int oldIndex = int.Parse (movedWidget.name);
			int newIndex = 0;
			if (sign < 0) {
//				newIndex = ((times - 1) / RealCellCount + 1) * RealCellCount + oldIndex % RealCellCount;
				newIndex = oldIndex + RealCellCount;
			} else {
//				newIndex = ((times) / RealCellCount) * RealCellCount + oldIndex % RealCellCount;
				newIndex = oldIndex - RealCellCount;
			}
//			print(string.Format("u> Count={0} times={1} oldIndex={2} sign={3} newIndex={4}\ndelta={5}",
//				RealCellCount, times, oldIndex, sign, newIndex, newIndex-oldIndex));

//			print(string.Format("u> newIndex={0} oldIndex={1} sign={2}", newIndex, oldIndex, sign));

			movedWidget.name = string.Format ("{0:D5}", newIndex);
			moveCellPos(movedWidget, itemList[targetIndex], newIndex, newIndex + sign);

/*			if (wrapLineNum > 0) {
				int destIdx = newIndex + sign;
				int offx = (newIndex % wrapLineNum) - (destIdx % wrapLineNum);
				int offy = (newIndex / wrapLineNum) - (destIdx / wrapLineNum);
				Vector3 offset = new Vector3 (offx * posParam.x * posParam.z, offy * posParam.y * posParam.w, 0);
				movedWidget.cachedTransform.localPosition = itemList [targetIndex].cachedTransform.localPosition + offset;
			} else {
				Vector3 offset = new Vector3 (sign * posParam.x * posParam.z, sign * posParam.y * posParam.w, 0);
				movedWidget.cachedTransform.localPosition = itemList [targetIndex].cachedTransform.localPosition + offset;
			}*/

			// change item index
			itemList.RemoveAt (sourceIndex);
			itemList.Insert (targetIndex, movedWidget);

			Debug.Assert(newIndex >= 0 && newIndex < dataLen);
			initCellData(movedWidget, data[newIndex]);
		}
	}

	public class IntRange {
		public int minIdx;
		public int maxIdx;
		public int count;
		public IntRange () {
			minIdx = int.MaxValue;
			maxIdx = int.MinValue;
			count = 0;
		}
		// 加入一条值
		public void Push(int value) {
			if (value < minIdx) {
				minIdx = value;
			}
			if (value > maxIdx) {
				maxIdx = value;
			}
			count++;			
		}
		// 判断是否有值添加
		public bool isValid() {
			return (minIdx < int.MaxValue && maxIdx > int.MinValue);
		}
		// 自膨胀到数量为 endCnt， 一下一上的增加方式，需要保证值不超过 maxVal.
		public int Expand(int endCnt, int maxVal) {
			Debug.Assert(count>0, "IntRange Expand need one value or more.");

			if (count>=endCnt)
				return 0;
			int deltaCnt = endCnt-count;
			int pos = 0;
			int oldpos;
			while (pos < deltaCnt) {
				oldpos = pos;
				if (maxIdx < maxVal) {
					Push(maxIdx+1);
					pos++;
					if(pos >= deltaCnt) break;
				}
				if (minIdx > 0) {
					Push(minIdx-1);
					pos++;
				}
				if(pos == oldpos)
					break;
			}
			return pos;
		}
	}
	// 检查 UIGrid ，获取可见的子部件合集，不支持 CellSnap 模式
	public class GridVisiableChecker
	{
		int maxPerLine = 0;
		float cellWidth = 200f;
		float cellHeight = 200f;
		Transform transform;
		UIGrid.Arrangement arrangement = UIGrid.Arrangement.Horizontal;
		public GridVisiableChecker(UIGrid g) {
			transform = g.transform;
			arrangement = g.arrangement;
			maxPerLine = g.maxPerLine;
			cellWidth = g.cellWidth;
			cellHeight = g.cellHeight;
		}
		// 模仿 UIGrid 的方式进行排列
		public bool Check(UIWidget wid,int num,IntRange result, List<Vector3> poslist) {
			bool ret = false;
			if (arrangement == UIGrid.Arrangement.CellSnap)
				return ret;
			Vector3 bak = wid.cachedTransform.localPosition;
			UIPanel p = wid.panel;
			if (p==null) {
				p=NGUITools.FindInParents<UIPanel>(wid.gameObject);
			}
			Debug.Assert(p, "UIWidget need have one UIPanel!");
			int x = 0;
			int y = 0;
			int maxX = 0;
			int maxY = 0;
			Vector3 pos = Vector3.zero;
			// string log = "";
			Vector3[] corners = null;
			
			for (int i = 0; i < num; ++i)
			{
				pos = (arrangement == UIGrid.Arrangement.Horizontal) ?
					new Vector3(cellWidth * x, -cellHeight * y, 0f) :
					new Vector3(cellWidth * y, -cellHeight * x, 0f);
				// TODO: 暂时只支持UIGrid 的对齐方式为TopLeft ，其他方式以后再说

				wid.cachedTransform.localPosition = pos;
				poslist.Add(pos);
			 	corners = wid.worldCorners;
				if (p.IsVisible(corners[0], corners[1], corners[2], corners[3])) {
					// log = log + string.Format("[{0}] {1}\n", i, pos);
					result.Push(i);
					ret = true;
				}
				maxX = Mathf.Max(maxX, x);
				maxY = Mathf.Max(maxY, y);
				if (++x >= maxPerLine && maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
			// print(log);
			wid.cachedTransform.localPosition = bak;
			return ret;
		}
	}

}

