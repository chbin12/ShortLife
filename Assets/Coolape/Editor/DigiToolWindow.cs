using UnityEngine;
using UnityEditor;
using System.Collections;
using Toolkit;
using System.IO;
using System.Collections.Generic;
using System;


public class DigiToolWindow : EditorWindow
{
	Vector2 scrollPos = Vector2.zero;
	SerializedObject mSerialObj;

	public Color mTestColor = Color.white;
	public string mTestColorCode = "";
	public string mNewSpriteName = "";
	public string mWidgetFixDepth = "1";

	GameObject m_dbgSource = null;
	GameObject m_dbgDestination = null;

	bool m_dbgSynTweenFromTo = true;
	bool m_dbgSynTweenDurDelay = false;
	bool m_dbgSynSpriteName = true;


	string tip_pal_reset = "重置CLPanelLua\n解决字体，图片都找不到的问题";
	string tip_atreset = "Altas重置\n 解决程序运行后看不见图片的问题。";
	string tip_atempty = "To EmptyAltas\n 将所有的UISprite 的图元集都指向 EmptyAltas。\n 用于规范化图片资源。";
	string tip_bnd = "重置UI部件尺寸\n 计算Widget尺寸, 会遍历所有的子UI部件.";
	string tip_dup = "复制列表子部件\n 以数字1为名称为模板，复制到其他的子部件中.";
	string tip_pos = "重置UI部件位置\n 整数化所有UI子部件的位置.";
	string tip_cpypth = "将选择的Assert文件的路径复制到剪贴板。";
	string tip_ico_gen = "自动生成军官方形头像图元\n 尺寸:110x110, 名称: hero_1b.png\n 增加军官头像时使用。";
	string tip_ico_resort = "将所有的图元按名称重新排序.";
	string tip_reset_depth = "重新设置UI部件的绘制顺序\n 会遍历所有的子部件，绘制的顺序依照排列的顺序\n 从指定的序号开始逐一增加。";
	string tip_null = "未实现功能，请等待";
	string tip_addtween1 = "添加休闲呼吸效果\n TweenPoition & TweenScale";
  	string tip_dup_to_neighbour = "复制到附近支线上";
	string tip_tweenop1 = "设置 Tween to值为当前值";
	string tip_tweenop2 = "交换 Tween to值 与 from值";
	string tip_tweenop3 = "重新设置  Tween from,to 值为当前值";
	string tip_spritesyn = "将子Sprite的图片设置为同第一个子Sprite一致。";
	string tip_synchildrenpos = "将同步所有子对象的位置(仅仅Position)";

	void OnGUI ()
	{
		if (mSerialObj == null) mSerialObj = new SerializedObject(this);

		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button (new GUIContent("Altas reset", tip_atreset), GUILayout.Width (80))) {
				TestFunc1 ();
			}

			if (GUILayout.Button (new GUIContent("EmptyAltas", tip_atempty) , GUILayout.Width (80))) {
				SetAllSpriteUseEmptyAltas(1);
			}

			// GUI.contentColor = new Color(0.00f, 0.83f, 1.0f, 1.0f);
			if (GUILayout.Button (new GUIContent("Copy path", tip_cpypth), GUILayout.Width (80))) {
				TestFunc2 ();
			}
			// GUI.contentColor = Color.white;
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button (new GUIContent("Panel reset", tip_pal_reset), GUILayout.Width (80))) {
				ResetPanelAtlasAndFonts();
			}
			if (GUILayout.Button (new GUIContent("atlasAllReal", tip_null) , GUILayout.Width (80))) {
				SetAllSpriteUseEmptyAltas(2);
			}
			if (GUILayout.Button (new GUIContent("null", tip_null), GUILayout.Width (80))) {
				Debug.Log("unsupport");
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button (new GUIContent("Add Tween1", tip_addtween1), GUILayout.Width (80))) {
				AddTweenIdleToAllChildren();
			}
			if (GUILayout.Button (new GUIContent("Dup.To", tip_dup_to_neighbour), GUILayout.Width (80))) {
				DuplicateToOthers();
			}
			if (GUILayout.Button (new GUIContent("Random Delay", tip_null), GUILayout.Width (80))) {
				ChangeTweenIdleRandomDelay();
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button (new GUIContent("[T]To>>Cur", tip_tweenop1), GUILayout.Width (80))) {
				TweenSetCurToEnd();
			}
			if (GUILayout.Button (new GUIContent("[T]F<<>>T", tip_tweenop2), GUILayout.Width (80))) {
				TweenSwapFromTo();
			}
			if (GUILayout.Button (new GUIContent("[T]Cur>>FT", tip_tweenop3), GUILayout.Width (80))) {
				TweenResetFromTo();
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		{
			if (GUILayout.Button (new GUIContent("null", tip_null), GUILayout.Width (80))) {
				Debug.Log("unsupport");
			}
			if (GUILayout.Button (new GUIContent("null", tip_null), GUILayout.Width (80))) {
				Debug.Log("unsupport");
			}
			if (GUILayout.Button (new GUIContent("Remove Comp.", tip_null), GUILayout.Width (80))) {
				RemoveLastComponent();
			}
		}
		GUILayout.EndHorizontal ();


		if (NGUIEditorTools.DrawHeader("Widget Tools"))
		{
			GUILayout.BeginHorizontal ();
			{
				if (GUILayout.Button (new GUIContent("Bound", tip_bnd), GUILayout.Width (80))) {
					TestWidgetFixByChild ();
				}
				if (GUILayout.Button (new GUIContent("Pos", tip_pos ), GUILayout.Width (80))) {
					TestWidgetFixPosition ();
				}
				if (GUILayout.Button (new GUIContent("Dup. List", tip_dup ), GUILayout.Width (80))) {
					TestWidgetDuplicate ();
				}
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			{
				GUILayout.Label ("Reset Depth, Star at:");
				mWidgetFixDepth = GUILayout.TextField(mWidgetFixDepth, GUILayout.MinWidth (32));
				// mWidgetFixDepth = EditorGUILayout.TextField("", mWidgetFixDepth, GUILayout.MinWidth (32));
				if (GUILayout.Button (new GUIContent("Reset", tip_reset_depth), GUILayout.Width (60))) {
					TestWidgetFixDepth();
				}
			}
			GUILayout.EndHorizontal ();

			NGUIEditorTools.BeginContents();
			// 对所选的UISprite图片修改为指定的名称
			// GUILayout.BeginHorizontal();
			// {
			// 	GUILayout.Label ("Sprite Name:");
			// 	mNewSpriteName = EditorGUILayout.TextField("", mNewSpriteName, GUILayout.MinWidth (50));
			// 	if (GUILayout.Button ("Change", GUILayout.MinWidth (48) )) {
			// 		ChangeSpriteName();
			// 	}
			// }
			// GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				// GUILayout.Label ("Hero icon box:");
				if (GUILayout.Button ( new GUIContent("Hero ico generate", tip_ico_gen), GUILayout.MinWidth (80) )) {
					GenerateAltasSprite();
				}

				if (GUILayout.Button (new GUIContent("Sort atlasAllReal", tip_ico_resort), GUILayout.MinWidth (80) )) {
					ResortAltasSprite();
				}

				if (GUILayout.Button (new GUIContent("Sprite syn.", tip_spritesyn ), GUILayout.MinWidth (80) )) {
					SynchChilrenSprite();
				}
			}
			GUILayout.EndHorizontal();

			NGUIEditorTools.EndContents();
		}

		if (NGUIEditorTools.DrawHeader("Debug Tools"))
		{
			GUI.color = Color.white;
			GUILayout.BeginHorizontal ();
			{
				GUILayout.BeginVertical();
				{
					//GUILayout.Space(5);
					// NGUIEditorTools.DrawProperty("Color", mSerialObj, "mTestColor", GUILayout.Width (240f));
					//GUILayout.Space(5);
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal ();

			Color buf = mTestColor;
			mTestColor = EditorGUILayout.ColorField("Color", mTestColor, GUILayout.Width (220f));
			if (buf != mTestColor) {
				Color clr = mTestColor;
				mTestColorCode = 
					string.Format (" new Color({0:F2}f,{1:F2}f,{2:F2}f,{3:F2}f); ",
					               clr.r, clr.g, clr.b, clr.a);
			}
			GUILayout.BeginHorizontal ();
			{
				mTestColorCode = EditorGUILayout.TextField("", mTestColorCode, GUILayout.MinWidth (80));
				if (GUILayout.Button ("Copy", GUILayout.MinWidth (80) )) {
					TestShowColorValue();
				}
			}
			GUILayout.EndHorizontal ();

		
			GUILayout.BeginHorizontal ();
			{
				GUILayout.BeginVertical();
				{
					
					m_dbgSource = EditorGUILayout.ObjectField("Source", m_dbgSource, typeof(GameObject), true) as GameObject;
					GUILayout.Space(5);
					string srcname = NGUITools.GetHierarchy(m_dbgSource);
					EditorGUILayout.LabelField(string.Format(" {0}", srcname) );

					m_dbgDestination = EditorGUILayout.ObjectField("Dest", m_dbgDestination, typeof(GameObject), true) as GameObject;
					string dstname = NGUITools.GetHierarchy(m_dbgDestination);
					EditorGUILayout.LabelField(string.Format(" {0}", dstname) );
					GUILayout.Space(5);
					if (GUILayout.Button (new GUIContent("Syn. Children pos", tip_synchildrenpos), GUILayout.MinWidth (110) )) {
						SynchChildrenTransformValue();
					}
					GUILayout.Space(5);
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal ();

			NGUIEditorTools.DrawHeader("Syn. Neighbour");
			GUILayout.BeginHorizontal ();
			{
				NGUIEditorTools.SetLabelWidth(100f);
				m_dbgSynTweenFromTo = EditorGUILayout.Toggle("Tween[From,To]", m_dbgSynTweenFromTo);
				m_dbgSynTweenDurDelay = EditorGUILayout.Toggle("Tween[Dur,Delay]", m_dbgSynTweenDurDelay);
			}
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			m_dbgSynSpriteName = EditorGUILayout.Toggle("Sprite[name]", m_dbgSynSpriteName);
			GUILayout.EndHorizontal ();

			if (GUILayout.Button (new GUIContent("Sprite syn.", tip_spritesyn ), GUILayout.MinWidth (80) )) {
				SynchChilrenSprite();
			}
		}

		EditorGUILayout.EndScrollView ();
	}


	void OnInspectorUpdate() {
		// Repaint();
	}
	void OnSelectionChange() { Repaint(); }

	enum AtlasType
	{
		Normal,
		Reference,
	}

	AtlasType GetAtlasType(UIAtlas atlas)
	{
		if (atlas.replacement != null)
		{
			return AtlasType.Reference;
		}
		return AtlasType.Normal;
	}

	void SetAllSpriteUseEmptyAltas(int type)
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		if (selectObj == null) {
			Debug.Log ("Not found Selection.activeObject.");
			return;
		}

		string emptypath = "Assets/new3g/Resources/Atlas/EmptyAltas.prefab";
		string replaceName = "EmptyAltas";
		if (type == 2) {
			emptypath = "Assets/new3g/upgradeResMedium/priority/atlas/atlasAllReal.prefab";
			replaceName = "atlasAllReal";
		}
		
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (emptypath, typeof(UnityEngine.Object));
		if(obj == null) {
			Debug.Log ("Not found " + replaceName + ".");
			return;
		}
	
		{
			UIAtlas emptyAtlas = ((GameObject)obj).GetComponent<UIAtlas>();

			string text = "SelectObj Type=" + selectObj.GetType().FullName;
			Debug.Log(text);

			if (selectObj.GetType() == typeof(GameObject) )
			{
				GameObject root = selectObj as GameObject;
				UISprite[] sprites = root.GetComponentsInChildren<UISprite>();
				int imax = sprites.Length;
				int changedCount = 0;
				for (int i = 0; i < imax; ++i)
				{
					UISprite s = sprites[i];
					if (s.atlas == null || s.atlas.name != replaceName)
					{
						s.atlas = emptyAtlas;
						changedCount++;
					}
				}
				text = "Got sprite count=" + imax + ", changed=" + changedCount;
				Debug.Log (text);
			}
			return;
		}
	}

	void TestFunc1 ()
	{
		string filepath = "Assets/new3g/Resources/Atlas/EmptyAltas.prefab";
		string refpath = "Assets/new3g/upgradeResMedium/priority/atlas/atlasAllReal.prefab";

		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath (filepath, typeof(UnityEngine.Object));
		if(obj != null) {
			UIAtlas emptyAtlas = ((GameObject)obj).GetComponent<UIAtlas>();

			AtlasType type = GetAtlasType(emptyAtlas);
			if (type == AtlasType.Reference)
			{
				Debug.Log ("EmptyAltas is Reference!");
			}
			else
			{
				UnityEngine.Object refobj = AssetDatabase.LoadAssetAtPath (refpath, typeof(UnityEngine.Object));
				if (refobj == null) {
					Debug.Log ("Not found altasAllReal.");
					return;
				}
				UIAtlas altasAllReal = ((GameObject)refobj).GetComponent<UIAtlas>();

				if (altasAllReal != null) {
					NGUIEditorTools.RegisterUndo("Altas Change", emptyAtlas);
					emptyAtlas.replacement = altasAllReal;
					NGUITools.SetDirty(emptyAtlas);
				}
				Debug.Log ("EmptyAltas reset!");
			}
		} else {
			Debug.Log ("Not found EmptyAltas.");
		}
	}
    
	void TestFunc2()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		if (selectObj == null) {
			Debug.Log ("Not found Selection.activeObject.");
		} else {
			string path = AssetDatabase.GetAssetPath(selectObj);
			CopyTextToClip(path);
		}
	}

	void CopyTextToClip(string text)
	{
		TextEditor te = new TextEditor();//很强大的文本工具
		te.text = text;
		te.OnFocus();
		te.Copy();
		
		Debug.Log ("Copyed:\n" + text);
	}

	void TestShowColorValue()
	{
		CopyTextToClip(mTestColorCode);
	}

	void SwitchChildren(Transform a, Transform b)
	{
		int cnt = a.childCount;
		if ( cnt > 0 ) {
			Transform[] cache = new Transform[cnt];
			for (int i = 0; i < cnt; i++)
			{
				cache[i] = a.GetChild(i);
			}
			for (int i = 0; i < cnt; i++)
			{
				Transform c = cache[i];
				c.parent = b;
			}
		}
	}

	void TestWidgetFixByChild()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		if (selectObj == null) {
			Debug.Log ("No selection!");
			return;
		}
		UIWidget rootWidget = ((GameObject)selectObj).GetComponent<UIWidget>();
		if (rootWidget == null) {
			Debug.Log ("Selection is not UIWidget!");
			return;
		}
		rootWidget.enabled = false;
		{
			Transform t = rootWidget.transform;

			Transform p = t.parent;
			GameObject tmp = NGUITools.AddChild(p.gameObject);

			SwitchChildren(t, tmp.transform);
			{
				Bounds b = NGUIMath.CalculateRelativeWidgetBounds(tmp.transform, false);
				Vector3 c = b.center;
				t.localPosition = new Vector3(Mathf.RoundToInt(c.x), 
				                              Mathf.RoundToInt(c.y), 
				                              Mathf.RoundToInt(c.z) );
				rootWidget.width = (int)b.size.x;
				rootWidget.height = (int)b.size.y;
				Debug.Log ("Widgets Fix => pos" + b.center + " size="+ rootWidget.width + "x" + rootWidget.height);
			}
			SwitchChildren(tmp.transform, t);
			NGUITools.DestroyImmediate(tmp);
			tmp = null;
		}
		rootWidget.enabled = true;
	}

	void TestWidgetFixPosition()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		if (selectObj == null) {
			Debug.Log ("No selection!");
			return;
		}

		UIWidget rootWidget = ((GameObject)selectObj).GetComponent<UIWidget>();
		if (rootWidget == null) {
			Debug.Log ("Selection is not UIWidget!");
			return;
		}
		Transform root = rootWidget.transform;
		UIWidget[] ws = root.GetComponentsInChildren<UIWidget>();
		int imax = ws.Length;
		int chng = 0;
		for (int i = 0; i< imax; ++i)
		{
			UIWidget w = ws[i];
			Transform t = w.transform;
			Vector3 c = w.transform.localPosition;
			Vector3 n = new Vector3(Mathf.RoundToInt(c.x), 
			                		Mathf.RoundToInt(c.y), 
			            		    Mathf.RoundToInt(c.z) );
			if (n != c ) {
				t.localPosition = n;
				chng++;
			}
		}
		Debug.Log ("Widget ["+root.name+"] position Fixed! changed cnt="+ chng);
	}

	void TestWidgetFixDepth()
	{
		int start_depth = 1;
		if (Int32.TryParse(mWidgetFixDepth, out start_depth))
		{
			UnityEngine.Object[] selections = Selection.objects;
			int imax = selections.Length;
			int chng = 0;
			for (int i = 0; i< imax; ++i)
			{
				GameObject o = selections[i] as GameObject;
				UIWidget[] wes = o.GetComponentsInChildren<UIWidget>();
				int jmax = wes.Length;
				for (int j = 0; j < jmax; ++j)
				{
					UIWidget w = wes[j];
					if ( w != null ) {
						w.depth = start_depth + chng;
						chng++;
					}
				}
			}
			Debug.Log ("Widgets depth Fixed! changed cnt="+ chng);
		}
		else
		{
			Debug.Log ("String could not be parsed.");
		}
	}

	void ChangeSpriteName()
	{
		UnityEngine.Object[] selections = Selection.objects;
		int imax = selections.Length;
		int chng = 0;
		for (int i = 0; i< imax; ++i)
		{
			GameObject o = selections[i] as GameObject;
			UISprite s = o.GetComponent<UISprite>();
			if ( s != null ) {
				s.spriteName = mNewSpriteName;
				chng++;
			}
		}
		Debug.Log ("Sprite change to ["+mNewSpriteName+"] !cnt="+ chng);
	}


	void GenerateAltasSprite()
	{
		string refpath = "Assets/new3g/upgradeResMedium/priority/atlas/atlasAllReal.prefab";
		UnityEngine.Object refobj = AssetDatabase.LoadAssetAtPath (refpath, typeof(UnityEngine.Object));
		if (refobj == null) {
			Debug.Log ("Not found altasAllReal.");
			return;
		}
		UIAtlas altasAllReal = ((GameObject)refobj).GetComponent<UIAtlas>();
		int chng = 0;
		if (altasAllReal != null) {
//			Hashtable spriteMap = altasAllReal.spriteMap;
//			List<string> duplist = new List<string>();
			int imax = 10;
			for (int i = 1; i < imax; i++) {
				string spname = string.Format("hero_{0}_b", i);
				string hdname = string.Format("hero_{0}_h", i);
				UISpriteData spd = altasAllReal.GetSprite(spname);
				UISpriteData head = altasAllReal.GetSprite(hdname);

//				string text = "Change:" + spname + "[" + (spd != null) + "]->" + hdname 
//					+ "[" + (head != null) + "]";
//				Debug.Log (text);

				if (spd == null )
					break;

				if ( head == null)
				{
					string dupname = UIAtlasMaker.DuplicateSprite(altasAllReal, spname); 
					if (dupname != null) {
						head = altasAllReal.GetSprite(dupname);
						head.name = hdname;
						head.x = 0;
						head.y = 69;
						head.width = 110;
						head.height = 110;
						chng++;
					}
				}
			}

			if (chng>0)
			{
				altasAllReal.spriteMap.Clear ();
				List<UISpriteData> spriteList = altasAllReal.spriteList;
				int imax2 = spriteList.Count;
				for (int i = 0; i < imax2; i++) {
					UISpriteData tmpsp = spriteList[i];
					altasAllReal.spriteMap [tmpsp.name] = i;
				}
				NGUITools.SetDirty(altasAllReal);
				UIAtlasMaker.instance.Repaint();
			}
		}
		Debug.Log ("altasAllReal changed!"+" cnt="+ chng);
	}

	void ResortAltasSprite()
	{
		string refpath = "Assets/new3g/upgradeResMedium/priority/atlas/atlasAllReal.prefab";
		UnityEngine.Object refobj = AssetDatabase.LoadAssetAtPath (refpath, typeof(UnityEngine.Object));
		if (refobj == null) {
			Debug.Log ("Not found altasAllReal.");
			return;
		}
		UIAtlas altasAllReal = ((GameObject)refobj).GetComponent<UIAtlas>();
		int chng = 0;
		if (altasAllReal != null) {

			List<UISpriteData> spriteList = altasAllReal.spriteList;
			spriteList.Sort(delegate(UISpriteData r1, UISpriteData r2) {
				return r2.name.CompareTo(r1.name) * -1; });

			altasAllReal.spriteMap.Clear ();
			int imax2 = spriteList.Count;
			for (int i = 0; i < imax2; i++) {
				UISpriteData tmpsp = spriteList[i];
				altasAllReal.spriteMap [tmpsp.name] = i;
			}
			NGUITools.SetDirty(altasAllReal);
			UIAtlasMaker.instance.Repaint();
		}
		Debug.Log ("altasAllReal resort!"+" cnt="+ chng);
	}

	void TestWidgetDuplicate()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		if (select.childCount <= 1) {
			Debug.Log ("Selection' children is not >1!");
			return;
		}

		Transform templa = select.GetChild(0);
		if (templa.childCount <= 0) {
			Debug.Log ("The template child is empty!");
			return;
		}

		if (templa.name != "1" ) {
			Debug.Log ("The select object's first child, the name is not '1'!");
			return;
		}

		NGUIEditorTools.RegisterUndo("Duplicate List", select);

		string CKey = "(Clone)";// (Clone)
		int CKeyLen = CKey.Length;
		int imax = select.childCount;
		int chng = 0;
		for (int i = 1; i< imax; ++i)
		{
			Transform t = select.GetChild(i);
			int jmax = t.childCount;

			BetterList<Transform> deleted = new BetterList<Transform>();
			for (int j = 0; j <jmax; j++) {
				deleted.Add (t.GetChild(j));
			}

			for (int j = 0; j <jmax; j++)
			{
				Transform c = deleted[j];
				NGUITools.Destroy(c.gameObject);
			}

			jmax = templa.childCount;
			for (int j = 0; j < jmax; j++)
			{
				Transform tc = templa.GetChild(j);
				GameObject n = NGUITools.AddChild(t.gameObject, templa.GetChild(j).gameObject );
				string nname = n.name;
				if ( nname.EndsWith(CKey) ) {
					Debug.Log ("Dup ["+j+"]  changed ="+ nname);
					n.name = nname.Substring(0, nname.Length - CKeyLen);

					n.transform.localPosition = tc.localPosition;
					n.transform.localScale = tc.localScale;
					//n.transform.localRotation = tc.localRotation;
				}
			}
			chng++;
		}
		NGUITools.SetDirty(select);

		Debug.Log ("List ["+select.name+"] children nodes updated! changed cnt="+ chng);
	}

	void ResetPanelAtlasAndFonts()
	{
		UnityEngine.Object[] selections = Selection.objects;
		int imax = selections.Length;
		int chng = 0;
		for (int i = 0; i< imax; ++i)
		{
			GameObject o = selections[i] as GameObject;
			CLPanelLua panel = o.GetComponent<CLPanelLua>();
			if ( panel != null ) {
				if (panel.isNeedResetAtlase) {
					CLPanelManager.resetAtlasAndFont(panel.transform, false);

					Debug.Log ("ResetAtlasAndFont Panel to ["+o.name+"] ! idx="+ chng);
					chng++;
				}
			}
		}
		if (chng > 0) {
			CLUIInit.self.clean();
		}
	}

	Transform FindChildByName(Transform t,string checkname)
	{
		if (t.name == checkname) return t;

		for (int i = 0, imax = t.childCount; i < imax; ++i)
		{
			Transform ch = FindChildByName(t.GetChild(i), checkname);
			if (ch != null) return ch;
		}
		return null;
	}

	void DuplicateToOthers()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}
		if (select.childCount <= 1) {
			Debug.Log ("Selection' children is not >1!");
			return;
		}
		//                   Hero   offset 1     offset
//		Transform tag = select.parent.parent.parent;
		Transform r = select.parent.parent.parent.parent; // offset/*/offset/Hero

		NGUIEditorTools.RegisterUndo("DuplicateToOthers", r);

		int imax = r.childCount;
		string dupname = select.name;
		string dupparent = select.parent.name;
		int changed = 0;
		for (int i = 0; i< imax; ++i)
		{
			Transform t = r.GetChild(i);
			Transform dp = FindChildByName(t, dupparent);
			Transform d = FindChildByName(dp, dupname);
//			GameObject o = t.gameObject;
			string state = d == null ? "Not found" : "Found";

			string paState = (dp == select.parent) ? " IS MINE" : "NOT MINE";
			if (dp != select.parent)
			{
				GameObject n = NGUITools.AddChild(dp.gameObject, select.gameObject);
				n.name = dupname;
				n.transform.localPosition = select.localPosition;
				n.transform.localScale = select.localScale;
				changed++;
			}
			Debug.Log (" name=["+t.name+"] - " + state + " parent[" + dupparent + "] --" + paState + "\n");
		}
		Debug.Log( "source name=" + dupname + "\n");

		NGUITools.SetDirty(r);
		Debug.Log ("DuplicateToOthers Done, child cnt="+changed+".");				
	}

	void TweenSetCurToEnd()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		UITweener[] tweers = select.GetComponents<UITweener>();
		if (tweers.Length <= 0)
		{
			Debug.Log("Error:No UITweener found.");
			return;
		}

		NGUIEditorTools.RegisterUndo("Tween value Setting", select);
		int changed = 0;
		GameObject tgo = select.gameObject;
		if(tgo != null) {
			
			TweenPosition tp = tgo.GetComponent<TweenPosition>();
			if (tp!= null && tp.enabled) {
				tp.to = tp.value;
				tp.value = tp.from;
				changed++;
			}

			TweenScale ts = tgo.GetComponent<TweenScale>();
			if (ts != null && ts.enabled) {
				ts.to = ts.value;
				ts.value = ts.from;
				changed++;
			}
			/*
			TweenAlpha ta = tgo.GetComponent<TweenAlpha>();
			if (ta != null) {
				ta.to = ta.value;
				ta.value = ta.from;
				changed++;
			}*/
			
		}
		NGUITools.SetDirty(select);
		Debug.Log ("Tween value Setting Done, cnt="+changed+".");				
	}

	void Swap<T>(ref T a, ref T b) {
		T t = a;
		a = b;
		b = t;
	}


	void TweenReset<T>(ref T a,ref T b,T v) {
		a = v;
		b = v;
	}

	void TweenSwapFromTo()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		UITweener[] tweers = select.GetComponents<UITweener>();
		if (tweers.Length <= 0)
		{
			Debug.Log("Error:No UITweener found.");
			return;
		}

		NGUIEditorTools.RegisterUndo("Tween value Swap", select);
		int changed = 0;
		GameObject tgo = select.gameObject;
		if(tgo != null) {

			TweenPosition tp = tgo.GetComponent<TweenPosition>();
			if (tp!= null && tp.enabled) {
				Swap(ref tp.to,ref tp.from);
				changed++;
			}
			TweenScale ts = tgo.GetComponent<TweenScale>();
			if (ts != null && tp.enabled) {
				Swap(ref ts.to,ref ts.from);
				changed++;
			}
			/*
			TweenAlpha ta = t.gameObject.GetComponent<TweenAlpha>();
			if (ta != null) {
				Swap(ref ta.to,ref ta.from);
				changed++;
			}*/

		}
		NGUITools.SetDirty(select);
		Debug.Log ("TweenSwapFromTo Done, cnt="+changed+".");				

	}

	void TweenResetFromTo()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		UITweener[] tweers = select.GetComponents<UITweener>();
		if (tweers.Length <= 0)
		{
			Debug.Log("Error:No UITweener found.");
			return;
		}

		NGUIEditorTools.RegisterUndo("Tween Reset", select);
		int changed = 0;
		GameObject tgo = select.gameObject;
		if(tgo != null) {

			TweenPosition tp = tgo.GetComponent<TweenPosition>();
			if (tp!= null && tp.enabled) {
				TweenReset(ref tp.to,ref tp.from,tp.value);
				changed++;
			}
			TweenScale ts = tgo.GetComponent<TweenScale>();
			if (ts != null && ts.enabled) {
				TweenReset(ref ts.to,ref ts.from,ts.value);
				changed++;
			}

		}
		NGUITools.SetDirty(select);
		Debug.Log ("Tween Reset Done, cnt="+changed+".");				

	}
	void ChangeTweenIdleRandomDelay()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		if (select.childCount <= 1) {
			Debug.Log ("Selection' children is not >1!");
			return;
		}

		NGUIEditorTools.RegisterUndo("Change Tween delay", select);
		int imax = select.childCount;
		int changed = 0;
		for (int i = 0; i< imax; ++i)
		{
			Transform t = select.GetChild(i);
			TweenScale ts = t.GetComponent<TweenScale>();
			TweenRotation tr = t.GetComponent<TweenRotation>();

			float delay = UnityEngine.Random.Range(0.0f, 0.3f);
			if (ts!= null) { ts.delay = delay; changed++; }
			if (tr!= null) { tr.delay = delay; changed++; }
		}
		NGUITools.SetDirty(select);
		Debug.Log ("Change Tween delay over, changed cnt="+changed+".");
	}

	void AddTweenIdleToAllChildren()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		if (select.childCount <= 1) {
			Debug.Log ("Selection' children is not >1!");
			return;
		}
		NGUIEditorTools.RegisterUndo("AddTweenIdle", select);
		select.localScale = new Vector3(0.85f, 0.85f, 0.85f);
		int imax = select.childCount;
		for (int i = 0; i< imax; ++i)
		{
			Transform t = select.GetChild(i);
			GameObject o = t.gameObject;

			UIWidget w = o.GetComponent<UIWidget>();
			if (w != null) {
				w.pivot = UIWidget.Pivot.Bottom;
				w.MakePixelPerfect();
			}

			TweenScale ts = o.GetComponent<TweenScale>();
			if (ts == null) {	ts = o.AddComponent<TweenScale>(); } 
			ts.from = Vector3.one;
			ts.to = new Vector3(1.0f, 0.97f, 1.0f);
			ts.duration = UnityEngine.Random.Range(2.2f, 1.8f);
			ts.style = UITweener.Style.PingPong;
			ts.method = UITweener.Method.EaseInOut;

			TweenRotation tr = o.GetComponent<TweenRotation>();
			if (tr == null)	tr = o.AddComponent<TweenRotation>();
			tr.from = new Vector3(0.0f, 0.0f, -1.0f);
			tr.to  = new Vector3(0.0f, 0.0f, 1.0f);
			tr.duration = UnityEngine.Random.Range(2.2f, 1.8f);
			tr.style = UITweener.Style.PingPong;
			tr.method = UITweener.Method.EaseInOut;
		}
		NGUITools.SetDirty(select);
		Debug.Log ("Add TweenIdle Done, child cnt="+imax+".");
	}

	void SynchChilrenSprite() {
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		Transform firstTrans = null;
		if (select.childCount <= 1) {
			Transform parent = select.parent;
			if (parent.childCount <= 1) {
				Debug.Log ("Selection' children is not >1!");
				return;
			}
			firstTrans = select;
			select = parent;
		}

		string srcSpriteName = "";
		int imax = select.childCount;

		if (firstTrans == null) {
			firstTrans = select.GetChild(0);
			UISprite s = firstTrans.GetComponent<UISprite>();
			if ( s != null && String.IsNullOrEmpty(s.spriteName)) {
				srcSpriteName = s.spriteName;
			} else {
				if (m_dbgSynSpriteName) {
					Debug.Log ("Selection not found UISprite!");
					return;
				}
			}
		}

		TweenPosition ftp =	firstTrans.GetComponent<TweenPosition>();
		TweenScale fts = firstTrans.GetComponent<TweenScale>();
		TweenRotation ftr = firstTrans.GetComponent<TweenRotation>();

		int changed = 0;
		NGUIEditorTools.RegisterUndo("Sprite Synch.", select);
		for (int i = 0; i< imax; ++i)
		{
			Transform t = select.GetChild(i);
			UISprite s = t.GetComponent<UISprite>();
			if ( t != firstTrans) {
				if (m_dbgSynSpriteName) {
					s.spriteName = srcSpriteName;
				}
				TweenPosition tp = t.GetComponent<TweenPosition>();
				TweenRotation tr = t.GetComponent<TweenRotation>();
				TweenScale ts = t.GetComponent<TweenScale>();
				if (m_dbgSynTweenFromTo) {
					if (tp != null) { tp.from = ftp.from; tp.to = ftp.to; }
					if (ts != null) { ts.from = fts.from; ts.to = fts.to; }
					if (tr != null) { tr.from = ftr.from; tr.to = ftr.to; }				
				}
				if (m_dbgSynTweenDurDelay) {
					if (tp != null) { tp.duration = ftp.duration; tp.delay = ftp.delay; }
					if (ts != null) { ts.duration = fts.duration; ts.delay = fts.delay; }
					if (tr != null) { tr.duration = ftr.duration; tr.delay = ftr.delay; }				
				}
				changed ++;
			}
		}
		NGUITools.SetDirty(select);
		NGUITools.SetActive(select.gameObject, false);
		NGUITools.SetActive(select.gameObject, true);

		Debug.Log ("Sprite synchronize successd, changed cnt="+changed+".");
	}

	void RemoveLastComponent()
	{
		UnityEngine.Object selectObj = Selection.activeObject;
		Transform select = ((GameObject)selectObj).transform;
		if (select == null) {
			Debug.Log ("Selection is not Transform!");
			return;
		}

		Component[] monos = select.GetComponents<Component>();
		if (monos.Length > 0) {
			Component m = monos[monos.Length-1];
			if ( m.GetType() != typeof(Transform) )
			{
				NGUIEditorTools.RegisterUndo("Remove Last Component", select);
				DestroyImmediate(m);
				NGUITools.SetDirty(select);
			}
		}
	}

	void SynchChildrenTransformValue()
	{
		if (m_dbgSource == null || m_dbgDestination == null) {
			Debug.Log ("No source or destination object be picked!");
			return;
		}

		Transform src = m_dbgSource.transform;
		Transform dest = m_dbgDestination.transform;
		if (src.childCount <= 0 || dest.childCount <= 0 || src.childCount != dest.childCount ) {
			Debug.Log ("Source or Destination's children count is wrong!");
			return;
		}

		int changed = 0;
		NGUIEditorTools.RegisterUndo("Synch. transform", m_dbgDestination);

		int imax = src.childCount;
		for (int i = 0; i< imax; ++i)
		{
			Transform s = src.GetChild(i);
			Transform d = dest.FindChild(s.name);
			if (d == null) {
				Debug.LogFormat("Destination not found [{0}] in children", s.name );
			}
			d.localPosition = s.localPosition;
			changed++;
		}
		NGUITools.SetDirty(m_dbgDestination);
		Debug.Log ("Synchronize transform value successd, changed cnt="+changed+".");
	}
}
