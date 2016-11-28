using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Toolkit;
using System.Collections.Generic;
using UnityEditorHelper;

#if UNITY_3_5
[CustomEditor(typeof(CLScene))]

#else
[CustomEditor (typeof(CLScene), true)]
#endif
public class CLSceneInspector : Editor
{
	CLScene scene;
	Object luaAsset = null;
	private static int seletectTerrainIndex = -1;
	private static CLTerrainInfor tmpTerrain = new CLTerrainInfor ();
	private static bool showNewTerrain = false;
	MapTileType tmpTileType = MapTileType.s_01;
	string tmpOrnament4Ground = "";
	bool isShow = false;
	public override void OnInspectorGUI ()
	{
		scene = target as CLScene;
		//		DrawDefaultInspector ();
		init ();
//		NGUIEditorTools.BeginContents ();
		using (new UnityEditorHelper.FoldableBlock (ref isShow, "List")) {
			if (isShow) {
				
				GUI.color = Color.white;

				if (scene.terrainInfor == null) {
					scene.terrainInfor = new List<CLTerrainInfor> ();
				}
				for (int i = 0; i < scene.terrainInfor.Count; i++) {
					CLTerrainInfor terrain = scene.terrainInfor [i];
					GUILayout.BeginHorizontal ();
					{
						if (GUILayout.Button (terrain.name)) {
							if (seletectTerrainIndex == i) {
								seletectTerrainIndex = -1;
							} else {
								seletectTerrainIndex = i;
							}
						}
						if (GUILayout.Button ("-", GUILayout.Width (100))) {
							if (EditorUtility.DisplayDialog ("Alert", "Really whant to delete it?", "yes", "no")) {
								scene.terrainInfor.RemoveAt (i);
								seletectTerrainIndex = -1;
								break;
							}
						}
					}
					GUILayout.EndHorizontal ();
					if (seletectTerrainIndex == i) {
						using (new HighlightBox (Color.black)) {
							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Tile Types", GUILayout.Width (100));
								if (terrain.tileTypes == null) {
									terrain.tileTypes = new List<MapTileType> ();
								}
								GUILayout.BeginVertical ();
								{
									for (int j = 0; j < terrain.tileTypes.Count; j++) {
								
										GUILayout.BeginHorizontal ();
										{
											terrain.tileTypes [j] = (MapTileType)EditorGUILayout.EnumPopup ("", terrain.tileTypes [j]);
											if (GUILayout.Button ("-", GUILayout.Width (50))) {
												terrain.tileTypes.RemoveAt (j);
												break;
											}
										}
										GUILayout.EndHorizontal ();
									}
							
									GUILayout.BeginHorizontal ();
									{
										tmpTileType = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTileType);
										if (GUILayout.Button ("+", GUILayout.Width (50))) {
											terrain.tileTypes.Add (tmpTileType);
										}
									}
									GUILayout.EndHorizontal ();
								}
								GUILayout.EndVertical ();
							}
							GUILayout.EndHorizontal ();
					
							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Oranment Types", GUILayout.Width (100));
								if (terrain.ornTypes == null) {
									terrain.ornTypes = new List<MapTileType> ();
								}
								GUILayout.BeginVertical ();
								{
									for (int j = 0; j < terrain.ornTypes.Count; j++) {
								
										GUILayout.BeginHorizontal ();
										{
											terrain.ornTypes [j] = (MapTileType)EditorGUILayout.EnumPopup ("", terrain.ornTypes [j]);
											if (GUILayout.Button ("-", GUILayout.Width (50))) {
												terrain.ornTypes.RemoveAt (j);
												break;
											}
										}
										GUILayout.EndHorizontal ();
									}
							
									GUILayout.BeginHorizontal ();
									{
										tmpTileType = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTileType);
										if (GUILayout.Button ("+", GUILayout.Width (50))) {
											terrain.ornTypes.Add (tmpTileType);
										}
									}
									GUILayout.EndHorizontal ();
								}
								GUILayout.EndVertical ();
							}
							GUILayout.EndHorizontal ();

							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Ground Name", GUILayout.Width (100));
								terrain.ground = EditorGUILayout.TextField (terrain.ground);

								Object obj = CLEditorTools.getObjectByPath (terrain.ground);
								obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
								if (obj != null) {
									terrain.ground = CLEditorTools.getPathByObject (obj);
								}
							}
							GUILayout.EndHorizontal ();

							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Ground High", GUILayout.Width (100));
								terrain.groundHigh = EditorGUILayout.FloatField (terrain.groundHigh);
							}
							GUILayout.EndHorizontal ();
							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Oranment 4 Ground", GUILayout.Width (100));
								if (terrain.ornament4Ground == null) {
									terrain.ornament4Ground = new List<string> ();
								}
								GUILayout.BeginVertical ();
								{
									for (int j = 0; j < terrain.ornament4Ground.Count; j++) {

										GUILayout.BeginHorizontal ();
										{
											terrain.ornament4Ground [j] = EditorGUILayout.TextField (terrain.ornament4Ground [j]);

											Object obj = CLEditorTools.getObjectByPath (terrain.ornament4Ground [j]);
											obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
											if (obj != null) {
												terrain.ornament4Ground [j] = CLEditorTools.getPathByObject (obj);
											}

											if (GUILayout.Button ("-", GUILayout.Width (50))) {
												terrain.ornament4Ground.RemoveAt (j);
												break;
											}
										}
										GUILayout.EndHorizontal ();
									}

									GUILayout.BeginHorizontal ();
									{
										tmpOrnament4Ground = EditorGUILayout.TextField (tmpOrnament4Ground);

										Object obj = CLEditorTools.getObjectByPath (tmpOrnament4Ground);
										obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
										if (obj != null) {
											tmpOrnament4Ground = CLEditorTools.getPathByObject (obj);
										}

										if (GUILayout.Button ("+", GUILayout.Width (50))) {
											terrain.ornament4Ground.Add (tmpOrnament4Ground);
										}
									}
									GUILayout.EndHorizontal ();
								}
								GUILayout.EndVertical ();
							}
							GUILayout.EndHorizontal ();

							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Sky Shader", GUILayout.Width (100));
								terrain.skyMaterial = EditorGUILayout.TextField (terrain.skyMaterial);

								Object obj = CLEditorTools.getObjectByPath (terrain.skyMaterial);
								obj = EditorGUILayout.ObjectField (obj, typeof(Material));
								if (obj != null) {
									terrain.skyMaterial = CLEditorTools.getPathByObject (obj);
								}
							}
							GUILayout.EndHorizontal ();

							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("Effect Name", GUILayout.Width (100));
								terrain.effect = EditorGUILayout.TextField (terrain.effect);
							}
							GUILayout.EndHorizontal ();

							GUILayout.BeginHorizontal ();
							{
								EditorGUILayout.LabelField ("use Fog", GUILayout.Width (100));
								terrain.fog = EditorGUILayout.Toggle (terrain.fog, "");
							}
							GUILayout.EndHorizontal ();
							if (terrain.fog) {
								GUILayout.BeginHorizontal ();
								{
									EditorGUILayout.LabelField ("fogColor", GUILayout.Width (100));
									terrain.fogColor = EditorGUILayout.ColorField ("", terrain.fogColor);
								}
								GUILayout.EndHorizontal ();
						
								GUILayout.BeginHorizontal ();
								{
									EditorGUILayout.LabelField ("fogDensity", GUILayout.Width (100));
									terrain.fogDensity = EditorGUILayout.FloatField (terrain.fogDensity, "");
								}
								GUILayout.EndHorizontal ();
								GUILayout.BeginHorizontal ();
								{
									EditorGUILayout.LabelField ("fogStartDis", GUILayout.Width (100));
									terrain.fogStartDis = EditorGUILayout.FloatField (terrain.fogStartDis, "");
								}
								GUILayout.EndHorizontal ();
								GUILayout.BeginHorizontal ();
								{
									EditorGUILayout.LabelField ("fogEndDis", GUILayout.Width (100));
									terrain.fogEndDis = EditorGUILayout.FloatField (terrain.fogEndDis, "");
								}
								GUILayout.EndHorizontal ();
							}
						}
					}
				}
			}
		}
//		NGUIEditorTools.EndContents ();

		NGUIEditorTools.BeginContents ();
		{
			using (new HighlightBox ()) {
				EditorGUILayout.LabelField ("新增一种地形");
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("Terrain Name", GUILayout.Width (100));
					tmpTerrain.name = EditorGUILayout.TextField (tmpTerrain.name);
					if (!showNewTerrain && GUILayout.Button ("+", GUILayout.Width (100))) {
						seletectTerrainIndex = -1;
						showNewTerrain = true;
					}
				
					if (showNewTerrain && GUILayout.Button ("-", GUILayout.Width (100))) {
						tmpTerrain = new CLTerrainInfor ();
						showNewTerrain = false;
					}
				}
				GUILayout.EndHorizontal ();

				if (showNewTerrain) {
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Tile Types", GUILayout.Width (100));
						if (tmpTerrain.tileTypes == null) {
							tmpTerrain.tileTypes = new List<MapTileType> ();
						}
						GUILayout.BeginVertical ();
						{
							for (int j = 0; j < tmpTerrain.tileTypes.Count; j++) {
							
								GUILayout.BeginHorizontal ();
								{
									tmpTerrain.tileTypes [j] = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTerrain.tileTypes [j]);
									if (GUILayout.Button ("-", GUILayout.Width (50))) {
										tmpTerrain.tileTypes.RemoveAt (j);
										break;
									}
								}
								GUILayout.EndHorizontal ();
							}
						
							GUILayout.BeginHorizontal ();
							{
								tmpTileType = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTileType);
								if (GUILayout.Button ("+", GUILayout.Width (50))) {
									tmpTerrain.tileTypes.Add (tmpTileType);
								}
							}
							GUILayout.EndHorizontal ();
						}
						GUILayout.EndVertical ();
					}
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Oranment Types", GUILayout.Width (100));
						if (tmpTerrain.ornTypes == null) {
							tmpTerrain.ornTypes = new List<MapTileType> ();
						}
						GUILayout.BeginVertical ();
						{
							for (int j = 0; j < tmpTerrain.ornTypes.Count; j++) {
							
								GUILayout.BeginHorizontal ();
								{
									tmpTerrain.ornTypes [j] = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTerrain.ornTypes [j]);
									if (GUILayout.Button ("-", GUILayout.Width (50))) {
										tmpTerrain.ornTypes.RemoveAt (j);
										break;
									}
								}
								GUILayout.EndHorizontal ();
							}
						
							GUILayout.BeginHorizontal ();
							{
								tmpTileType = (MapTileType)EditorGUILayout.EnumPopup ("", tmpTileType);
								if (GUILayout.Button ("+", GUILayout.Width (50))) {
									tmpTerrain.ornTypes.Add (tmpTileType);
								}
							}
							GUILayout.EndHorizontal ();
						}
						GUILayout.EndVertical ();
					}
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Ground Name", GUILayout.Width (100));
						tmpTerrain.ground = EditorGUILayout.TextField (tmpTerrain.ground);

						Object obj = CLEditorTools.getObjectByPath (tmpTerrain.ground);
						obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
						if (obj != null) {
							tmpTerrain.ground = CLEditorTools.getPathByObject (obj);
						}
					}
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Ground High", GUILayout.Width (100));
						tmpTerrain.groundHigh = EditorGUILayout.FloatField (tmpTerrain.groundHigh);
					}
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Oranment 4 Ground", GUILayout.Width (100));
						if (tmpTerrain.ornament4Ground == null) {
							tmpTerrain.ornament4Ground = new List<string> ();
						}
						GUILayout.BeginVertical ();
						{
							for (int j = 0; j < tmpTerrain.ornament4Ground.Count; j++) {

								GUILayout.BeginHorizontal ();
								{
									tmpTerrain.ornament4Ground [j] = EditorGUILayout.TextField (tmpTerrain.ornament4Ground [j]);

									Object obj = CLEditorTools.getObjectByPath (tmpTerrain.ornament4Ground [j]);
									obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
									if (obj != null) {
										tmpTerrain.ornament4Ground [j] = CLEditorTools.getPathByObject (obj);
									}

									if (GUILayout.Button ("-", GUILayout.Width (50))) {
										tmpTerrain.ornament4Ground.RemoveAt (j);
										break;
									}
								}
								GUILayout.EndHorizontal ();
							}

							GUILayout.BeginHorizontal ();
							{
								tmpOrnament4Ground = EditorGUILayout.TextField (tmpOrnament4Ground);

								Object obj = CLEditorTools.getObjectByPath (tmpOrnament4Ground);
								obj = EditorGUILayout.ObjectField (obj, typeof(GameObject));
								if (obj != null) {
									tmpOrnament4Ground = CLEditorTools.getPathByObject (obj);
								}

								if (GUILayout.Button ("+", GUILayout.Width (50))) {
									tmpTerrain.ornament4Ground.Add (tmpOrnament4Ground);
								}
							}
							GUILayout.EndHorizontal ();
						}
						GUILayout.EndVertical ();
					}
					GUILayout.EndHorizontal ();
					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Sky Shader", GUILayout.Width (100));
						tmpTerrain.skyMaterial = EditorGUILayout.TextField (tmpTerrain.skyMaterial);
						Object obj = CLEditorTools.getObjectByPath (tmpTerrain.skyMaterial);
						obj = EditorGUILayout.ObjectField (obj, typeof(Material));
						if (obj != null) {
							tmpTerrain.skyMaterial = CLEditorTools.getPathByObject (obj);
						}
					}
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Effect Name", GUILayout.Width (100));
						tmpTerrain.effect = EditorGUILayout.TextField (tmpTerrain.effect);
					}
					GUILayout.EndHorizontal ();

					GUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("use Fog", GUILayout.Width (100));
						tmpTerrain.fog = EditorGUILayout.Toggle (tmpTerrain.fog, "");
					}
					GUILayout.EndHorizontal ();
					if (tmpTerrain.fog) {
						GUILayout.BeginHorizontal ();
						{
							EditorGUILayout.LabelField ("fogColor", GUILayout.Width (100));
							tmpTerrain.fogColor = EditorGUILayout.ColorField ("", tmpTerrain.fogColor);
						}
						GUILayout.EndHorizontal ();
					
						GUILayout.BeginHorizontal ();
						{
							EditorGUILayout.LabelField ("fogDensity", GUILayout.Width (100));
							tmpTerrain.fogDensity = EditorGUILayout.FloatField (tmpTerrain.fogDensity, "");
						}
						GUILayout.EndHorizontal ();
						GUILayout.BeginHorizontal ();
						{
							EditorGUILayout.LabelField ("fogStartDis", GUILayout.Width (100));
							tmpTerrain.fogStartDis = EditorGUILayout.FloatField (tmpTerrain.fogStartDis, "");
						}
						GUILayout.EndHorizontal ();
						GUILayout.BeginHorizontal ();
						{
							EditorGUILayout.LabelField ("fogEndDis", GUILayout.Width (100));
							tmpTerrain.fogEndDis = EditorGUILayout.FloatField (tmpTerrain.fogEndDis, "");
						}
						GUILayout.EndHorizontal ();
					}
					if (GUILayout.Button ("Add")) {
						if (string.IsNullOrEmpty (tmpTerrain.name)) {
							EditorUtility.DisplayDialog ("Alert", "The name is null!!", "Okay");
							return;
						}
						scene.terrainInfor.Add (tmpTerrain);
						tmpTerrain = new CLTerrainInfor ();
						EditorUtility.SetDirty (scene);
					}
				}
			}
		}
		NGUIEditorTools.EndContents ();

//		NGUIEditorTools.BeginContents ();
//		{
			using (new HighlightBox (Color.black)) {
			GUILayout.BeginHorizontal ();
			{
				EditorGUILayout.LabelField ("RefreshTiles", GUILayout.Width (100));
//				scene.isRefreshTiles = GUILayout.Toggle (scene.isRefreshTiles, "");
			}
			GUILayout.EndHorizontal ();
			
			
			GUILayout.BeginHorizontal ();
			{
				EditorGUILayout.LabelField ("use Fog", GUILayout.Width (100));
				scene.fog = GUILayout.Toggle (scene.fog, "");
			}
			GUILayout.EndHorizontal ();
			if (scene.fog) {
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("fogColor", GUILayout.Width (100));
					scene.fogColor = EditorGUILayout.ColorField ("", scene.fogColor);
				}
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("fogDensity", GUILayout.Width (100));
					scene.fogDensity = EditorGUILayout.FloatField (scene.fogDensity, "");
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("fogStartDis", GUILayout.Width (100));
					scene.fogStartDis = EditorGUILayout.FloatField (scene.fogStartDis, "");
				}
				GUILayout.EndHorizontal ();
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("fogEndDis", GUILayout.Width (100));
					scene.fogEndDis = EditorGUILayout.FloatField (scene.fogEndDis, "");
				}
				GUILayout.EndHorizontal ();
			}
			
			if (scene != null) {
				GUILayout.BeginHorizontal ();
				{
					EditorGUILayout.LabelField ("Lua Text", GUILayout.Width (100));
					luaAsset = EditorGUILayout.ObjectField (luaAsset, typeof(UnityEngine.Object), GUILayout.Width (125));
				}
				GUILayout.EndHorizontal ();
				string luaPath = AssetDatabase.GetAssetPath (luaAsset);
				scene.luaPath = Utl.filterPath (luaPath);
				EditorUtility.SetDirty (scene);
			}
			
			GUILayout.Space (3);
//			if (GUILayout.Button ("LoadPrefab")) {
//				loadPrefabTiles ();
//			}
			
			if (GUILayout.Button ("Save")) {
				save ();
			}
			
			if (GUILayout.Button ("load data")) {
				loadData ();
			}
		}
//		NGUIEditorTools.EndContents ();
	}

	bool isFinishInit = false;

	void init ()
	{
		if (!isFinishInit || luaAsset == null) {
			isFinishInit = true;
			
			if (!string.IsNullOrEmpty (scene.luaPath)) {
				string tmpPath = scene.luaPath.Replace ("/upgradeRes", "/upgradeResMedium");
				luaAsset = AssetDatabase.LoadMainAssetAtPath ("Assets/" + tmpPath);
			}
		}
	}

	void loadPrefabTiles ()
	{
//		string path = "Assets/" + PathCfg.self.basePath + "/upgradeResMedium/other/map/tiles";
//		MapTile tile = null;
//		string[] files = Directory.GetFiles (path);
//		for (int i =0; i < files.Length; i++) {
//			if (Path.GetExtension (files [i]) .Equals (".meta"))
//				continue;
//			Debug.Log (files [i]);
//			tile = (MapTile)(AssetDatabase.LoadAssetAtPath (
//				files [i], 
//				typeof(MapTile)));
//			if (tile != null) {
//				CLMap.addPrefabTile (tile);
//			}
//		}
	}

	void save ()
	{
//		string dir = Application.dataPath + "/" + PathCfg.self.basePath + "/upgradeResMedium/other/map/data";
//		Directory.CreateDirectory(dir);
//		string path = EditorUtility.SaveFilePanel ("Save scene to data", dir, "New scene", "");
//		if (string.IsNullOrEmpty (path))
//			return;
//		
//		Hashtable tmpTilesPos = new Hashtable();	
//		ArrayList buildingList = new ArrayList ();
//		ArrayList tilesList = new ArrayList ();
//		ArrayList ornList = new ArrayList ();
//		ArrayList effectList = new ArrayList ();
//		SceneData mapdata = new SceneData ();
//		mapdata.fog = scene.fog;
//		mapdata.fogColor = scene.fogColor;
//		mapdata.fogDensity = scene.fogDensity;
//		mapdata.fogStartDis = scene.fogStartDis;
//		mapdata.fogEndDis = scene.fogEndDis;
//		
//		int count = scene.transform.childCount;
//		MapTileData md = null;
//		Transform child = null;
//		MapOrnament mo = null;
//		CLMapTile mt = null;
//		SEffect eff = null;
//		for (int i = 0; i < count; i++) {
//			child = scene.transform.GetChild (i);
//			if (!child.gameObject.activeSelf)
//				continue;
//				mo = child.GetComponent<MapOrnament> ();
//				if (mo != null) {
//					md = new MapTileData ();
//					md.name = child.name;
//					md.x = mo.mapX;
//					md.y = mo.mapY;
//					md.z = mo.mapZ;
//					md.activeSelf = child.gameObject.activeSelf;
//					md.layer = child.gameObject.layer;
//					md.localEulerAngles = child.localEulerAngles;
//					md.localPosition = child.localPosition;
//					md.localScale = child.localScale;
//					md.type = mo.tileType.ToString ();
//					ornList.Add (md);
//				} else {
//					mt = child.GetComponent<CLMapTile> ();
//					if (mt != null) {
//						md = new MapTileData ();
//						md.name = child.name;
//						md.x = mt.mapX;
//						md.y = mt.mapY;
//						md.z = mt.mapZ;
//						md.activeSelf = child.gameObject.activeSelf;
//						md.layer = child.gameObject.layer;
//						md.localEulerAngles = child.localEulerAngles;
//						md.localPosition = child.localPosition;
//						md.localScale = child.localScale;
//						md.type = mt.tileType.ToString ();
//						tilesList.Add (md);
//						tmpTilesPos[mt.posStr] = md;
//					} else {
//						eff = child.GetComponent<SEffect> ();
//						if (eff != null) {
//							md = new MapTileData ();
//							md.name = child.name;
//							md.x = -1;
//							md.y = -1;
//							md.z = -1;
//							md.activeSelf = child.gameObject.activeSelf;
//							md.layer = child.gameObject.layer;
//							md.localEulerAngles = child.localEulerAngles;
//							md.localPosition = child.localPosition;
//							md.localScale = child.localScale;
//							effectList.Add (md);
//						}
//					}
//			}
//		}
//		// 标识不能移动的tile
//		for(int i =0; i < tilesList.Count; i++) {
//			md = (MapTileData)(tilesList[i]);
//			string posStr = Toolkit.PStr.begin (md.x, "_", md.y, "_", (md.z + 1)).end ();
//			//取和否有其它tile在上面
//			if(tmpTilesPos[posStr] != null) {
//				md.onTopObjPosStr = posStr;
//			}
//		}
//		
//		mapdata.tiles = tilesList;
//		mapdata.ornaments = ornList;
//		mapdata.effects = effectList;
//		mapdata.buildings = buildingList;
//		MemoryStream ms = new MemoryStream ();
//		B2OutputStream.writeObject (ms, mapdata.toMap ());
//		File.WriteAllBytes (path, ms.ToArray ());
	}

	void loadData ()
	{
//		string dir = Application.dataPath + "/"+PathCfg.self.basePath+"/upgradeResMedium/other/map/data";
//		string path = EditorUtility.OpenFilePanel ("Load scene to data", dir, "");
//		if (string.IsNullOrEmpty (path))
//			return;
//		
//		byte[] buffer = File.ReadAllBytes (path);
//		if (buffer == null || buffer.Length <= 0) {
//			Debug.LogError ("buffer is null");
//			return;
//		}
//		MemoryStream ms = new MemoryStream ();
//		ms.Write (buffer, 0, buffer.Length);
//		ms.Position = 0;
//		object obj = B2InputStream.readObject (ms);
//		if (obj == null) {
//			Debug.LogError ("The file is not bio data");
//			return ;
//		}
//		SceneData data = SceneData.parse ((Hashtable)obj);
//		if (data == null) {
//			Debug.LogError ("The scene data is null");
//			return;
//		}
//		clean ();
//		if (CLMap.mapTilePrefabs.Count <= 0) {
//			loadPrefabTiles ();
//		}
//		MapTileData md = null;
//		for (int i = 0; i < data.tiles.Count; i++) {
//			md = (MapTileData)(data.tiles [i]);
//			MapTile tile = CLMap.CreateMapTile (md.type);
//			if (tile == null)
//				continue;
//			tile.transform.parent = scene.transform;
//			tile.transform.localPosition = CLMap.getPos (md.x, md.y, md.z);// md.localPosition;
//			tile.transform.localEulerAngles = md.localEulerAngles;
////			tile.transform.localScale = md.localScale;
//		}
//		
//		for (int i = 0; i < data.ornaments.Count; i++) {
//			md = (MapTileData)(data.ornaments [i]);
//			MapTile tile = CLMap.CreateMapTile (md.name);
//			if (tile == null)
//				continue;
//			tile.transform.parent = scene.transform;
//			tile.transform.localPosition = CLMap.getPos (md.x, md.y, md.z);// md.localPosition;
//			tile.transform.localEulerAngles = md.localEulerAngles;
////			tile.transform.localScale = md.localScale;
//		}
//		scene.isRefreshTiles = true;
	}

	void clean ()
	{
		if (Application.isPlaying) {
//			scene.clean ();
		} else {
			int i = 0;
			int count = scene.transform.childCount;
			while (i < count) {
				NGUITools.DestroyImmediate (scene.transform.GetChild (0).gameObject);
				i++;
			}
		}
	}
}

public class SceneData
{
	public bool fog;
	public double fogDensity;
	public Color fogColor = Color.white;
	public double fogStartDis;
	public double fogEndDis;
	public ArrayList tiles;
	//地表块
	public ArrayList ornaments;
	//装饰物
	public ArrayList effects;
	//特效
	public ArrayList buildings;

	public Hashtable toMap ()
	{
		Hashtable r = new Hashtable ();
		r ["fog"] = fog;
		r ["fogDensity"] = (double)fogDensity;
		r ["fogColor"] = Utl.colorToMap (fogColor);
		r ["fogStartDis"] = (double)fogStartDis;
		r ["fogEndDis"] = (double)fogEndDis;
		
		ArrayList _list = new ArrayList ();
		int count = tiles.Count;
		MapTileData mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(tiles [i]);
			_list.Add (mtd.toMap ());
		}
		r ["tiles"] = _list;
		
		
		_list = new ArrayList ();
		count = ornaments.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(ornaments [i]);
			_list.Add (mtd.toMap ());
		}
		r ["ornaments"] = _list;
		
		_list = new ArrayList ();
		count = effects.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(effects [i]);
			_list.Add (mtd.toMap ());
		}
		r ["effects"] = _list;
		
		_list = new ArrayList ();
		count = buildings.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(buildings [i]);
			_list.Add (mtd.toMap ());
		}
		r ["buildings"] = _list;
		
		return r;
	}

	public static SceneData parse (Hashtable map)
	{
		SceneData r = new SceneData ();
		
		r.fog = MapEx.getBool (map, "fog");
		r.fogDensity = MapEx.getDouble (map, "fogDensity");
		r.fogColor = Utl.mapToColor (MapEx.getMap (map, "fogColor"));
		r.fogStartDis = MapEx.getDouble (map, "fogStartDis");
		r.fogEndDis = MapEx.getDouble (map, "fogEndDis");
		
		r.tiles = new ArrayList ();
		ArrayList list = MapEx.getList (map, "tiles");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.tiles.Add (MapTileData.parse ((Hashtable)(list [i])));
			}
		}
		
		
		r.ornaments = new ArrayList ();
		list = MapEx.getList (map, "ornaments");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.ornaments.Add (MapTileData.parse ((Hashtable)(list [i])));
			}
		}
		
		r.effects = new ArrayList ();
		list = MapEx.getList (map, "effects");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.effects.Add (MapTileData.parse ((Hashtable)(list [i])));
			}
		}
		
		r.buildings = new ArrayList ();
		list = MapEx.getList (map, "buildings");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.buildings.Add (MapTileData.parse ((Hashtable)(list [i])));
			}
		}
		return r;
	}
}

