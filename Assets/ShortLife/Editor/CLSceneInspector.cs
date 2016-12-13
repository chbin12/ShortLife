using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Toolkit;
using System.Collections.Generic;
using UnityEditorHelper;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(CLScene), true)]
public class CLSceneInspector : Editor
{
	CLScene scene;
	Object luaAsset = null;
	private static int seletectTerrainIndex = -1;
	private static CLTerrainInfor tmpTerrain = new CLTerrainInfor();
	private static bool showNewTerrain = false;
	MapTileType tmpTileType = MapTileType.s_01;
	string tmpOrnament4Ground = "";
	string tmpTileMaterial = "";
	static bool isShow = false;
	static bool isShow2 = false;
	CLTerrainInfor terrain;

	static int x, y;
	static Hashtable tileMap = new Hashtable();

	public override void OnInspectorGUI()
	{
		scene = target as CLScene;
		//		DrawDefaultInspector ();
		init();

		if (scene != null) {
			GUILayout.BeginHorizontal();
			{
				EditorGUILayout.LabelField("Lua Text", GUILayout.Width(100));
				luaAsset = EditorGUILayout.ObjectField(luaAsset, typeof(UnityEngine.Object));
			}
			GUILayout.EndHorizontal();
			string luaPath = AssetDatabase.GetAssetPath(luaAsset);
			scene.luaPath = Utl.filterPath(luaPath);
			EditorUtility.SetDirty(scene);
		}

//		NGUIEditorTools.BeginContents ();
		using (new UnityEditorHelper.FoldableBlock(ref isShow, "Terrain List (Click to show)")) {
			if (isShow) {
				GUI.color = Color.white;

				if (scene.terrainInfor == null) {
					scene.terrainInfor = new List<CLTerrainInfor>();
				}
				for (int i = 0; i < scene.terrainInfor.Count; i++) {
					terrain = scene.terrainInfor [i];
					GUILayout.BeginHorizontal();
					{
						if (GUILayout.Button(terrain.name)) {
							if (seletectTerrainIndex == i) {
								seletectTerrainIndex = -1;
							} else {
								seletectTerrainIndex = i;
							}
						}
						if (GUILayout.Button("-", GUILayout.Width(100))) {
							if (EditorUtility.DisplayDialog("Alert", "Really whant to delete it?", "yes", "no")) {
								scene.terrainInfor.RemoveAt(i);
								seletectTerrainIndex = -1;
								break;
							}
						}
					}
					GUILayout.EndHorizontal();
					if (seletectTerrainIndex == i) {
						using (new HighlightBox(Color.black)) {
							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Tile Types", GUILayout.Width(100));
								if (terrain.tileTypes == null) {
									terrain.tileTypes = new List<MapTileType>();
								}
								GUILayout.BeginVertical();
								{
									for (int j = 0; j < terrain.tileTypes.Count; j++) {
								
										GUILayout.BeginHorizontal();
										{
											terrain.tileTypes [j] = (MapTileType)EditorGUILayout.EnumPopup("", terrain.tileTypes [j]);
											if (GUILayout.Button("-", GUILayout.Width(50))) {
												terrain.tileTypes.RemoveAt(j);
												break;
											}
										}
										GUILayout.EndHorizontal();
									}
							
									GUILayout.BeginHorizontal();
									{
										tmpTileType = (MapTileType)EditorGUILayout.EnumPopup("", tmpTileType);
										if (GUILayout.Button("+", GUILayout.Width(50))) {
											terrain.tileTypes.Add(tmpTileType);
										}
									}
									GUILayout.EndHorizontal();
								}
								GUILayout.EndVertical();
							}
							GUILayout.EndHorizontal();
					
							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Oranment Types", GUILayout.Width(100));
								if (terrain.ornTypes == null) {
									terrain.ornTypes = new List<MapTileType>();
								}
								GUILayout.BeginVertical();
								{
									for (int j = 0; j < terrain.ornTypes.Count; j++) {
								
										GUILayout.BeginHorizontal();
										{
											terrain.ornTypes [j] = (MapTileType)EditorGUILayout.EnumPopup("", terrain.ornTypes [j]);
											if (GUILayout.Button("-", GUILayout.Width(50))) {
												terrain.ornTypes.RemoveAt(j);
												break;
											}
										}
										GUILayout.EndHorizontal();
									}
							
									GUILayout.BeginHorizontal();
									{
										tmpTileType = (MapTileType)EditorGUILayout.EnumPopup("", tmpTileType);
										if (GUILayout.Button("+", GUILayout.Width(50))) {
											terrain.ornTypes.Add(tmpTileType);
										}
									}
									GUILayout.EndHorizontal();
								}
								GUILayout.EndVertical();
							}
							GUILayout.EndHorizontal();


							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Tile Materials", GUILayout.Width(100));
								if (terrain.tileMaterials == null) {
									terrain.tileMaterials = new List<string>();
								}
								GUILayout.BeginVertical();
								{
									for (int j = 0; j < terrain.tileMaterials.Count; j++) {

										GUILayout.BeginHorizontal();
										{
											terrain.tileMaterials [j] = EditorGUILayout.TextField(terrain.tileMaterials [j]);
											string path = PStr.b().a(PathCfg.self.basePath).a("/upgradeResMedium/other/Materials/").a(terrain.tileMaterials [j]).a(".mat").e();
											Object obj = CLEditorTools.getObjectByPath(path);
											obj = EditorGUILayout.ObjectField(obj, typeof(Material));
											if (obj != null) {
												terrain.tileMaterials [j] = Path.GetFileNameWithoutExtension(CLEditorTools.getPathByObject(obj));
											}

											if (GUILayout.Button("-", GUILayout.Width(50))) {
												terrain.tileMaterials.RemoveAt(j);
												break;
											}
										}
										GUILayout.EndHorizontal();
									}

									GUILayout.BeginHorizontal();
									{
										tmpTileMaterial = EditorGUILayout.TextField(tmpTileMaterial);

										Object obj = CLEditorTools.getObjectByPath(tmpTileMaterial);
										obj = EditorGUILayout.ObjectField(obj, typeof(Material));
										if (obj != null) {
											tmpTileMaterial = CLEditorTools.getPathByObject(obj);
										}

										if (GUILayout.Button("+", GUILayout.Width(50))) {
											Path.GetFileNameWithoutExtension(CLEditorTools.getPathByObject(obj));
											terrain.tileMaterials.Add(Path.GetFileNameWithoutExtension(tmpTileMaterial));
											tmpTileMaterial = "";
										}
									}
									GUILayout.EndHorizontal();
								}
								GUILayout.EndVertical();
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Ground Name", GUILayout.Width(100));
								terrain.ground = EditorGUILayout.TextField(terrain.ground);

								Object obj = CLEditorTools.getObjectByPath(terrain.ground);
								obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
								if (obj != null) {
									terrain.ground = CLEditorTools.getPathByObject(obj);
								}
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Ground High", GUILayout.Width(100));
								terrain.groundHigh = EditorGUILayout.FloatField(terrain.groundHigh);
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Oranment 4 Ground High", GUILayout.Width(100));
								terrain.ornament4GroundHigh = EditorGUILayout.FloatField(terrain.ornament4GroundHigh);
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Oranment 4 Ground", GUILayout.Width(100));
								if (terrain.ornament4Ground == null) {
									terrain.ornament4Ground = new List<string>();
								}
								GUILayout.BeginVertical();
								{
									for (int j = 0; j < terrain.ornament4Ground.Count; j++) {

										GUILayout.BeginHorizontal();
										{
											terrain.ornament4Ground [j] = EditorGUILayout.TextField(terrain.ornament4Ground [j]);

											Object obj = CLEditorTools.getObjectByPath(terrain.ornament4Ground [j]);
											obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
											if (obj != null) {
												terrain.ornament4Ground [j] = CLEditorTools.getPathByObject(obj);
											}

											if (GUILayout.Button("-", GUILayout.Width(50))) {
												terrain.ornament4Ground.RemoveAt(j);
												break;
											}
										}
										GUILayout.EndHorizontal();
									}

									GUILayout.BeginHorizontal();
									{
										tmpOrnament4Ground = EditorGUILayout.TextField(tmpOrnament4Ground);

										Object obj = CLEditorTools.getObjectByPath(tmpOrnament4Ground);
										obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
										if (obj != null) {
											tmpOrnament4Ground = CLEditorTools.getPathByObject(obj);
										}

										if (GUILayout.Button("+", GUILayout.Width(50))) {
											terrain.ornament4Ground.Add(tmpOrnament4Ground);
										}
									}
									GUILayout.EndHorizontal();
								}
								GUILayout.EndVertical();
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Sky Shader", GUILayout.Width(100));
								terrain.skyMaterial = EditorGUILayout.TextField(terrain.skyMaterial);
								Object obj = CLEditorTools.getObjectByPath(terrain.skyMaterial);
								obj = EditorGUILayout.ObjectField(obj, typeof(Material));
								if (obj != null) {
									terrain.skyMaterial = CLEditorTools.getPathByObject(obj);
								}
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Sky Oranment", GUILayout.Width(100));
								terrain.skyOranment = EditorGUILayout.TextField(terrain.skyOranment);
								Object obj = CLEditorTools.getObjectByPath(terrain.skyOranment);
								obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
								if (obj != null) {
									terrain.skyOranment = CLEditorTools.getPathByObject(obj);
								}
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Lookat Angle", GUILayout.Width(100));
								terrain.lookatAngle = EditorGUILayout.FloatField(terrain.lookatAngle);
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("Effect Name", GUILayout.Width(100));
								terrain.effect = EditorGUILayout.TextField(terrain.effect);
							}
							GUILayout.EndHorizontal();

							GUILayout.BeginHorizontal();
							{
								EditorGUILayout.LabelField("use Fog", GUILayout.Width(100));
								terrain.fog = GUILayout.Toggle(terrain.fog, "");
							}
							GUILayout.EndHorizontal();
							if (terrain.fog) {
								GUILayout.BeginHorizontal();
								{
									EditorGUILayout.LabelField("fogColor", GUILayout.Width(100));
									terrain.fogColor = EditorGUILayout.ColorField("", terrain.fogColor);
								}
								GUILayout.EndHorizontal();
						
								GUILayout.BeginHorizontal();
								{
									EditorGUILayout.LabelField("fogDensity", GUILayout.Width(100));
									terrain.fogDensity = EditorGUILayout.FloatField(terrain.fogDensity);
								}
								GUILayout.EndHorizontal();
								GUILayout.BeginHorizontal();
								{
									EditorGUILayout.LabelField("fogStartDis", GUILayout.Width(100));
									terrain.fogStartDis = EditorGUILayout.FloatField(terrain.fogStartDis);
								}
								GUILayout.EndHorizontal();
								GUILayout.BeginHorizontal();
								{
									EditorGUILayout.LabelField("fogEndDis", GUILayout.Width(100));
									terrain.fogEndDis = EditorGUILayout.FloatField(terrain.fogEndDis);
								}
								GUILayout.EndHorizontal();
							}
						}
						scene.terrainInfor [i] = terrain;
					}
				}
				EditorUtility.SetDirty(scene);
			}
		}
//		NGUIEditorTools.EndContents ();

		NGUIEditorTools.BeginContents();
		{
			using (new HighlightBox()) {
				EditorGUILayout.LabelField("新增一种地形");
				GUILayout.BeginHorizontal();
				{
					EditorGUILayout.LabelField("Terrain Name", GUILayout.Width(100));
					tmpTerrain.name = EditorGUILayout.TextField(tmpTerrain.name);
					if (!showNewTerrain && GUILayout.Button("+", GUILayout.Width(100))) {
						seletectTerrainIndex = -1;
						showNewTerrain = true;
					}
				
					if (showNewTerrain && GUILayout.Button("-", GUILayout.Width(100))) {
						tmpTerrain = new CLTerrainInfor();
						showNewTerrain = false;
					}
				}
				GUILayout.EndHorizontal();

				if (showNewTerrain) {
					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Tile Types", GUILayout.Width(100));
						if (tmpTerrain.tileTypes == null) {
							tmpTerrain.tileTypes = new List<MapTileType>();
						}
						GUILayout.BeginVertical();
						{
							for (int j = 0; j < tmpTerrain.tileTypes.Count; j++) {
							
								GUILayout.BeginHorizontal();
								{
									tmpTerrain.tileTypes [j] = (MapTileType)EditorGUILayout.EnumPopup("", tmpTerrain.tileTypes [j]);
									if (GUILayout.Button("-", GUILayout.Width(50))) {
										tmpTerrain.tileTypes.RemoveAt(j);
										break;
									}
								}
								GUILayout.EndHorizontal();
							}
						
							GUILayout.BeginHorizontal();
							{
								tmpTileType = (MapTileType)EditorGUILayout.EnumPopup("", tmpTileType);
								if (GUILayout.Button("+", GUILayout.Width(50))) {
									tmpTerrain.tileTypes.Add(tmpTileType);
								}
							}
							GUILayout.EndHorizontal();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Oranment Types", GUILayout.Width(100));
						if (tmpTerrain.ornTypes == null) {
							tmpTerrain.ornTypes = new List<MapTileType>();
						}
						GUILayout.BeginVertical();
						{
							for (int j = 0; j < tmpTerrain.ornTypes.Count; j++) {
							
								GUILayout.BeginHorizontal();
								{
									tmpTerrain.ornTypes [j] = (MapTileType)EditorGUILayout.EnumPopup("", tmpTerrain.ornTypes [j]);
									if (GUILayout.Button("-", GUILayout.Width(50))) {
										tmpTerrain.ornTypes.RemoveAt(j);
										break;
									}
								}
								GUILayout.EndHorizontal();
							}
						
							GUILayout.BeginHorizontal();
							{
								tmpTileType = (MapTileType)EditorGUILayout.EnumPopup("", tmpTileType);
								if (GUILayout.Button("+", GUILayout.Width(50))) {
									tmpTerrain.ornTypes.Add(tmpTileType);
								}
							}
							GUILayout.EndHorizontal();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();


					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Tile Materials", GUILayout.Width(100));
						if (tmpTerrain.tileMaterials == null) {
							tmpTerrain.tileMaterials = new List<string>();
						}
						GUILayout.BeginVertical();
						{
							for (int j = 0; j < tmpTerrain.tileMaterials.Count; j++) {

								GUILayout.BeginHorizontal();
								{
									tmpTerrain.tileMaterials [j] = EditorGUILayout.TextField(tmpTerrain.tileMaterials [j]);
									string path = PStr.b().a(PathCfg.self.basePath).a("/upgradeResMedium/other/Materials/").a(tmpTerrain.tileMaterials [j]).a(".mat").e();
									Object obj = CLEditorTools.getObjectByPath(path);
									obj = EditorGUILayout.ObjectField(obj, typeof(Material));
									if (obj != null) {
										tmpTerrain.tileMaterials [j] = Path.GetFileNameWithoutExtension(CLEditorTools.getPathByObject(obj));
									}

									if (GUILayout.Button("-", GUILayout.Width(50))) {
										tmpTerrain.tileMaterials.RemoveAt(j);
										break;
									}
								}
								GUILayout.EndHorizontal();
							}

							GUILayout.BeginHorizontal();
							{
								tmpTileMaterial = EditorGUILayout.TextField(tmpTileMaterial);

								Object obj = CLEditorTools.getObjectByPath(tmpTileMaterial);
								obj = EditorGUILayout.ObjectField(obj, typeof(Material));
								if (obj != null) {
									tmpTileMaterial = CLEditorTools.getPathByObject(obj);
								}

								if (GUILayout.Button("+", GUILayout.Width(50))) {
									Path.GetFileNameWithoutExtension(CLEditorTools.getPathByObject(obj));
									tmpTerrain.tileMaterials.Add(Path.GetFileNameWithoutExtension(tmpTileMaterial));
									tmpTileMaterial = "";
								}
							}
							GUILayout.EndHorizontal();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Ground Name", GUILayout.Width(100));
						tmpTerrain.ground = EditorGUILayout.TextField(tmpTerrain.ground);

						Object obj = CLEditorTools.getObjectByPath(tmpTerrain.ground);
						obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
						if (obj != null) {
							tmpTerrain.ground = CLEditorTools.getPathByObject(obj);
						}
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Ground High", GUILayout.Width(100));
						tmpTerrain.groundHigh = EditorGUILayout.FloatField(tmpTerrain.groundHigh);
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Oranment 4 Ground High", GUILayout.Width(100));
						tmpTerrain.ornament4GroundHigh = EditorGUILayout.FloatField(tmpTerrain.ornament4GroundHigh);
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Oranment 4 Ground", GUILayout.Width(100));
						if (tmpTerrain.ornament4Ground == null) {
							tmpTerrain.ornament4Ground = new List<string>();
						}
						GUILayout.BeginVertical();
						{
							for (int j = 0; j < tmpTerrain.ornament4Ground.Count; j++) {

								GUILayout.BeginHorizontal();
								{
									tmpTerrain.ornament4Ground [j] = EditorGUILayout.TextField(tmpTerrain.ornament4Ground [j]);

									Object obj = CLEditorTools.getObjectByPath(tmpTerrain.ornament4Ground [j]);
									obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
									if (obj != null) {
										tmpTerrain.ornament4Ground [j] = CLEditorTools.getPathByObject(obj);
									}

									if (GUILayout.Button("-", GUILayout.Width(50))) {
										tmpTerrain.ornament4Ground.RemoveAt(j);
										break;
									}
								}
								GUILayout.EndHorizontal();
							}

							GUILayout.BeginHorizontal();
							{
								tmpOrnament4Ground = EditorGUILayout.TextField(tmpOrnament4Ground);

								Object obj = CLEditorTools.getObjectByPath(tmpOrnament4Ground);
								obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
								if (obj != null) {
									tmpOrnament4Ground = CLEditorTools.getPathByObject(obj);
								}

								if (GUILayout.Button("+", GUILayout.Width(50))) {
									tmpTerrain.ornament4Ground.Add(tmpOrnament4Ground);
								}
							}
							GUILayout.EndHorizontal();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Sky Shader", GUILayout.Width(100));
						tmpTerrain.skyMaterial = EditorGUILayout.TextField(tmpTerrain.skyMaterial);
						Object obj = CLEditorTools.getObjectByPath(tmpTerrain.skyMaterial);
						obj = EditorGUILayout.ObjectField(obj, typeof(Material));
						if (obj != null) {
							tmpTerrain.skyMaterial = CLEditorTools.getPathByObject(obj);
						}
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Sky Oranment", GUILayout.Width(100));
						tmpTerrain.skyOranment = EditorGUILayout.TextField(tmpTerrain.skyOranment);
						Object obj = CLEditorTools.getObjectByPath(tmpTerrain.skyOranment);
						obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));
						if (obj != null) {
							tmpTerrain.skyOranment = CLEditorTools.getPathByObject(obj);
						}
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Lookat Angle", GUILayout.Width(100));
						tmpTerrain.lookatAngle = EditorGUILayout.FloatField(tmpTerrain.lookatAngle);
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Effect Name", GUILayout.Width(100));
						tmpTerrain.effect = EditorGUILayout.TextField(tmpTerrain.effect);
					}
					GUILayout.EndHorizontal();

					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("use Fog", GUILayout.Width(100));
						tmpTerrain.fog = GUILayout.Toggle(tmpTerrain.fog, "");
					}
					GUILayout.EndHorizontal();
					if (tmpTerrain.fog) {
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("fogColor", GUILayout.Width(100));
							tmpTerrain.fogColor = EditorGUILayout.ColorField("", tmpTerrain.fogColor);
						}
						GUILayout.EndHorizontal();
					
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("fogDensity", GUILayout.Width(100));
							tmpTerrain.fogDensity = EditorGUILayout.FloatField(tmpTerrain.fogDensity);
						}
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("fogStartDis", GUILayout.Width(100));
							tmpTerrain.fogStartDis = EditorGUILayout.FloatField(tmpTerrain.fogStartDis);
						}
						GUILayout.EndHorizontal();
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("fogEndDis", GUILayout.Width(100));
							tmpTerrain.fogEndDis = EditorGUILayout.FloatField(tmpTerrain.fogEndDis);
						}
						GUILayout.EndHorizontal();
					}
					if (GUILayout.Button("Add")) {
						if (string.IsNullOrEmpty(tmpTerrain.name)) {
							EditorUtility.DisplayDialog("Alert", "The name is null!!", "Okay");
							return;
						}
						scene.terrainInfor.Add(tmpTerrain);
						tmpTerrain = new CLTerrainInfor();
						if (!Application.isPlaying) {
							EditorUtility.SetDirty(scene);
							EditorSceneManager.MarkAllScenesDirty();
						}
					}
				}
			}
			if (GUILayout.Button("Save Terrain Cfg")) {
				save2();
			}
		}
		NGUIEditorTools.EndContents();

//		NGUIEditorTools.BeginContents ();
//		{
		using (new UnityEditorHelper.FoldableBlock(ref isShow2, "Set Tile Map (Click to show)")) {
			if (isShow2) {
				GUILayout.BeginHorizontal();
				{
					EditorGUILayout.LabelField("X", GUILayout.Width(100));
					x = EditorGUILayout.IntField(x);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					EditorGUILayout.LabelField("Y", GUILayout.Width(100));
					y = EditorGUILayout.IntField(y);
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				{
					if (GUILayout.Button("Create Map")) {
						createMap();
					}

					if (GUILayout.Button("Clean")) {
						clean();
					}
				}
				GUILayout.EndHorizontal();

				GUILayout.Space(3);
				if (GUILayout.Button("Save Map to Json")) {
					save();
				}
			}
		}
	}

	bool isFinishInit = false;

	void init()
	{
		if (!isFinishInit || luaAsset == null) {
			isFinishInit = true;
			
			if (!string.IsNullOrEmpty(scene.luaPath)) {
				string tmpPath = scene.luaPath.Replace("/upgradeRes", "/upgradeResMedium");
				luaAsset = AssetDatabase.LoadMainAssetAtPath("Assets/" + tmpPath);
			}
		}
	}

	//	void loadPrefabTiles ()
	//	{
	//		string path = "Assets/" + PathCfg.self.basePath + "/upgradeResMedium/other/map/tiles";
	//		string[] files = Directory.GetFiles (path);
	//		for (int i =0; i < files.Length; i++) {
	//			if (Path.GetExtension (files [i]) .Equals (".meta"))
	//				continue;
	//			loadPrefabTile(files [i]);
	//		}
	//	}

	CLMapTile loadPrefabTile(string name)
	{
		string path = "Assets/" + PathCfg.self.basePath + "/upgradeResMedium/other/things/tiles/";
		CLMapTile tile = null;
		Debug.Log(path + name);
		tile = (CLMapTile)(AssetDatabase.LoadAssetAtPath(
			path + name, 
			typeof(CLMapTile)));
		return tile;
	}

	Vector3 getPos(int x, int y, int z)
	{
		y = -y;
		Vector3 pos = new Vector3(x * CLMapTile.OffsetX, z * CLMapTile.OffsetZ, y * CLMapTile.OffsetY);
		float off = y % 2;
		bool rowIndexIsUneven = (off == 1 || off == -1);
		if (rowIndexIsUneven) {
			pos.x = pos.x + CLMapTile.RowOffsetX;
		}
		return pos;
	}

	Vector3 getMapPos(Vector3 pos)
	{
		int flagX = 1;
		int flagY = 1;
		int flagZ = 1;
		int x = 0;
		int y = 0;
		if (pos.x >= 0) {
			flagX = 1;
		} else {
			flagX = -1;
		}

		if (pos.z >= 0) {
			flagY = 1;
		} else {
			flagY = -1;
		}
		if (pos.y >= 0) {
			flagZ = 1;
		} else {
			flagZ = -1;
		}

		pos = pos;// - topLeftPosition;

		y = (int)((pos.z - (CLMapTile.OffsetY / 2)) / CLMapTile.OffsetY);
		y = (int)y;

		int off = (int)(y % 2);
		bool rowIndexIsUneven = (off == 1 || off == -1);
		if (rowIndexIsUneven) {
			x = (int)((pos.x + flagX * (-CLMapTile.RowOffsetX + CLMapTile.OffsetX / 2)) / CLMapTile.OffsetX);
		} else {
			x = (int)((pos.x + (CLMapTile.OffsetX / 2)) / CLMapTile.OffsetX);
		}

		int z = (int)((pos.y + flagZ * (CLMapTile.OffsetZ / 2)) / CLMapTile.OffsetZ);
		return new Vector3(x, -y, z);
	}

	void createMap()
	{
		CLMapTile prefabTile = loadPrefabTile("s_01.prefab");
		CLMapTile tile = null;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				tile = GameObject.Instantiate(prefabTile);
				tile.transform.parent = scene.transform;
				tile.transform.localPosition = getPos(i, j, 0);
				tileMap [i + "_" + j] = tile.gameObject;
			}
		}	
	}

	void save2()
	{
		string dir = Application.dataPath + "/" + PathCfg.self.basePath + "/upgradeRes4Publish/priority/cfg/";
		Directory.CreateDirectory(dir);
		string path = EditorUtility.SaveFilePanel("Save scene to data", dir, "New scene", "cfg");
		if (string.IsNullOrEmpty(path))
			return;
		
		File.WriteAllText(path, scene.getJson());
	}

	void save()
	{
		int count = scene.transform.childCount;
		Transform tr = null;
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < count; i++) {
			tr = scene.transform.GetChild(i);
			pos = getMapPos (tr.localPosition);
			tileMap [pos.x + "_" + pos.y] = tr.gameObject;
		}


		string json = "[";
		GameObject tile = null;
		for (int i = 0; i < x; i++) {
			json = PStr.b().a(json).a("[").e();
			for (int j = 0; j < y; j++) {
				tile = (GameObject)(tileMap [i + "_" + j]);
				if (tile != null && tile.activeSelf) {
					json = PStr.b().a(json).a(1).e();
				} else {
					json = PStr.b().a(json).a(0).e();
				}
				if (j < y - 1) {
					json = PStr.b().a(json).a(",").e();
				}
			}
			json = PStr.b().a(json).a("]").e();

			if (i < x - 1) {
				json = PStr.b().a(json).a(",").e();
			}
		}
		json = PStr.b().a(json).a("]").e();

		string dir = Application.dataPath + "/" + PathCfg.self.basePath + "/upgradeRes4Publish/priority/cfg/";
		Directory.CreateDirectory(dir);
		string path = EditorUtility.SaveFilePanel("Save map to data", dir, "New map", "json");
		if (string.IsNullOrEmpty(path))
			return;
		
		File.WriteAllText(path, json);	
	}

	void loadData()
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

	void clean()
	{
		if (Application.isPlaying) {
//			scene.clean ();
		} else {
			int i = 0;
			int count = scene.transform.childCount;
			while (i < count) {
				NGUITools.DestroyImmediate(scene.transform.GetChild(0).gameObject);
				i++;
			}
			tileMap.Clear();
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

	public Hashtable toMap()
	{
		Hashtable r = new Hashtable();
		r ["fog"] = fog;
		r ["fogDensity"] = (double)fogDensity;
		r ["fogColor"] = Utl.colorToMap(fogColor);
		r ["fogStartDis"] = (double)fogStartDis;
		r ["fogEndDis"] = (double)fogEndDis;
		
		ArrayList _list = new ArrayList();
		int count = tiles.Count;
		MapTileData mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(tiles [i]);
			_list.Add(mtd.toMap());
		}
		r ["tiles"] = _list;
		
		
		_list = new ArrayList();
		count = ornaments.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(ornaments [i]);
			_list.Add(mtd.toMap());
		}
		r ["ornaments"] = _list;
		
		_list = new ArrayList();
		count = effects.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(effects [i]);
			_list.Add(mtd.toMap());
		}
		r ["effects"] = _list;
		
		_list = new ArrayList();
		count = buildings.Count;
		mtd = null;
		for (int i = 0; i < count; i++) {
			mtd = (MapTileData)(buildings [i]);
			_list.Add(mtd.toMap());
		}
		r ["buildings"] = _list;
		
		return r;
	}

	public static SceneData parse(Hashtable map)
	{
		SceneData r = new SceneData();
		
		r.fog = MapEx.getBool(map, "fog");
		r.fogDensity = MapEx.getDouble(map, "fogDensity");
		r.fogColor = Utl.mapToColor(MapEx.getMap(map, "fogColor"));
		r.fogStartDis = MapEx.getDouble(map, "fogStartDis");
		r.fogEndDis = MapEx.getDouble(map, "fogEndDis");
		
		r.tiles = new ArrayList();
		ArrayList list = MapEx.getList(map, "tiles");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.tiles.Add(MapTileData.parse((Hashtable)(list [i])));
			}
		}
		
		
		r.ornaments = new ArrayList();
		list = MapEx.getList(map, "ornaments");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.ornaments.Add(MapTileData.parse((Hashtable)(list [i])));
			}
		}
		
		r.effects = new ArrayList();
		list = MapEx.getList(map, "effects");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.effects.Add(MapTileData.parse((Hashtable)(list [i])));
			}
		}
		
		r.buildings = new ArrayList();
		list = MapEx.getList(map, "buildings");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.buildings.Add(MapTileData.parse((Hashtable)(list [i])));
			}
		}
		return r;
	}
}

