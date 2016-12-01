using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;
using Toolkit;

/// <summary>
/// CL scene.战斗场景
/// </summary>
using System.Collections.Generic;


public class CLScene : CLBaseLua
{
	
	public bool fog;
	public float fogDensity;
	public Color fogColor = Color.white;
	public float fogStartDis;
	public float fogEndDis;
	//--------------------------------------
	[SerializeField]
	public List<CLTerrainInfor> terrainInfor;
	public CLTerrainInfor currTerrain= null;

	public static CLScene self;
	public CLScene() 
	{
		self = this;
	}

//	LuaFunction lfinit = null;
//	LuaFunction lfloadScene = null;
//	public override void setLua ()
//	{
//		base.setLua ();
//		if(luaTable != null) {
//			lfinit  = (LuaFunction)(luaTable ["init"]);
//			lfloadScene = (LuaFunction)(luaTable ["loadScene"]);
//			lfClean = (LuaFunction)(luaTable ["clean"]);
//		}
//	}

//	public void init(object onLoadProgressCallback, object onFinishCallback) {
//		if(lfinit != null)
//			lfinit.Call(onLoadProgressCallback, onFinishCallback);
//	}

//	public void loadScene (string dataPath, object onProgress, object onFinish)
//	{
//		StartCoroutine (doLoadScene (dataPath, onProgress, onFinish));
//	}

//	public IEnumerator doLoadScene (string dataPath, object onProgress, object onFinish)
//	{
//		yield return null;
//		if (lfloadScene != null) {
//			lfloadScene.Call (dataPath, onProgress, onFinish);
//		}
//	}
	
//	public void AddMapTileToMap2 (MapTile tile, int x, int y, int z = 0)
//	{
//		tile.map = this;
//		tiles [CLMap.getPosStr(x, y,z)] = tile;
//	}
	
//	LuaFunction lfClean = null;

//	public override void clean ()
//	{
//		CancelInvoke();
//		base.clean();
//		if (lfClean != null) {
//			lfClean.Call ();
//		}
//		tiles.Clear();
//	}

	/// <summary>
	/// Gets the pve map by center.
	/// </summary>
	/// <param name='center'>
	/// Center. 12_1_0
	/// </param>
	/// <param name='range'>
	/// Range.
	/// </param>
//	public Hashtable getPveMapByCenter(Hashtable data, string center, int range) {
//		string[] strs = center.Split('_');
//		int x = NumEx.stringToInt(strs[0]);
//		int y = NumEx.stringToInt(strs[1]);
//		int z = NumEx.stringToInt(strs[2]);
//		Vector2[] v2s = GetTilesAroundVectors(x, y, range);
//		Hashtable vsMap = new Hashtable();
//		int count = v2s.Length;
//		string key = "";
//		for(int i =0; i < count; i++) {
//			key = v2s[i].x + "_" + v2s[i].y;
//			vsMap[key] = true;
//		}
//		vsMap[x + "_" + y] = true;
//		Hashtable ret = (Hashtable)(data.Clone());
//		ArrayList list = (ArrayList)(data["tiles"]);
//		count = list.Count;
//		Hashtable tile = null;
//		ArrayList tmpList = new ArrayList();
//		for(int i =0; i < count; i++) {
//			tile  = (Hashtable)(list[i]);
//			key = tile["x"] + "_" + tile["y"];
//			if(vsMap[key] != null)  {
//				tmpList.Add(tile);
//			}
//		}
//		ret["tiles"] = tmpList;
//		//////////////////
//		list = (ArrayList)(data["ornaments"]);
//		count = list.Count;
//		tmpList = new ArrayList();
//		for(int i =0; i < count; i++) {
//			tile  = (Hashtable)(list[i]);
//			key = tile["x"] + "_" + tile["y"];
//			if(vsMap[key] != null)  {
//				tmpList.Add(tile);
//			}
//		}
//		ret["ornaments"] = tmpList;
//		//////////////////
//		list = (ArrayList)(data["buildings"]);
//		count = list.Count;
//		tmpList = new ArrayList();
//		for(int i =0; i < count; i++) {
//			tile  = (Hashtable)(list[i]);
//			key = tile["x"] + "_" + tile["y"];
//			if(vsMap[key] != null)  {
//				tmpList.Add(tile);
//			}
//		}
//		ret["buildings"] = tmpList;
//		//////////////////////
//		list = (ArrayList)(data["effects"]);
//		count = list.Count;
//		tmpList = new ArrayList();
//		Vector3 mapPos = Vector3.zero;
//		for(int i =0; i < count; i++) {
//			tile  = (Hashtable)(list[i]);
//			mapPos = getMapPos(Utl.mapToVector3((Hashtable)(tile["localPosition"])));
//			key = (int)(mapPos.x) + "_" + (int)(mapPos.y);
//			if(vsMap[key] != null)  {
//				tmpList.Add(tile);
//			}
//		}
//		ret["effects"] = tmpList;
//		/////////////////////
//		return ret;
//	}
//	
//	int sideLeft = 0;
//	int sideRight = 0;
//	int MAX_Length = 30;
//	/// <summary>
//	/// Loads the infinite map.加载无限地图
//	/// </summary>
//	public void loadInfiniteMap(int x, int y, float tileTimeout,  int defalutTerrainIndex = -1) {
//		mapSizeX = x;
//		mapSizeY = y;
//		sideLeft = 0;
//		if(defalutTerrainIndex < 0) {
//			currTerrain = terrainInfor[NumEx.NextInt(0, terrainInfor.Count)];
//		} else {
//			currTerrain = terrainInfor[defalutTerrainIndex];
//		}
//		sideRight = mapSizeX - 1;
//		
//		int index = NumEx.NextInt(0, currTerrain.tileTypes.Count);
//		newDefaultMap(currTerrain.tileTypes[index].ToString());
////		InvokeRepeating("addRightSideTiles", 4,2.5f);
//		InvokeRepeating ("checkLeftSideTilesTimeout", tileTimeout, tileTimeout);
//	}
//
//	/// <summary>
//	/// Adds the right side tiles.
//	/// </summary>
//	public void addRightSideTiles() {
//		sideRight++;
//		int index = 0;
//		for(int i=0; i< mapSizeY; i++) {
//			index = NumEx.NextInt(0, currTerrain.tileTypes.Count);
//			AddMapTileToMap(currTerrain.tileTypes[index].ToString(), sideRight, i);
//		}
//	}
//	
//	public override void AddMapTileToMap (MapTile tile, int x, int y, int z = 0)
//	{
//		tile.GetComponent<Rigidbody>().isKinematic = true;
//		Vector3 topLeftPosition = new Vector3 (-0.5f * MapSizeX * MapTile.OffsetX, 0, 0.5f * MapSizeY * MapTile.OffsetY);
////		Vector3 topLeftPosition = new Vector3 (-1f * MapSizeX * MapTile.OffsetX, 0, 1f * MapSizeY * MapTile.OffsetY);
//		Vector3 tilePos = topLeftPosition + getPos (x, -y, z);
//		tile.mapX = x; 
//		tile.mapY = y;
//		tile.mapZ = z;
//		tile.name = string.Format ("({0},{1})", x, y);
//		tile.map = this;
//		tiles [tile.posStr] = tile;
//		tile.transform.parent = transform;
//		tile.transform.localEulerAngles =  new Vector3(-90, 90, 0);
//		tile.transform.localScale = Vector3.one*2;
//		tile.effectNew(tilePos, (Callback)onFinishLoadOneTile);
//		NGUITools.SetActive (tile.gameObject, true);
//	}
//
//	public void onFinishLoadOneTile(params object[] args) {
//		MapTile tile = (MapTile)(args[0]);
//		if(tile.mapY == 0 || tile.mapY == 1) {
//			int index = NumEx.NextInt(0, currTerrain.ornTypes.Count);
//			addOrnament(currTerrain.ornTypes[index], tile);
//		} else {
////			CLBattle.self.onNewTileAdded(tile);
//		}
//	}
//
//	public void addOrnament(MapTileType type, MapTile  tile) {
//		MapTile orn = CreateMapTile(type.ToString());
//		if(orn != null) {
//			orn.mapX = tile.mapX;
//			orn.mapY = tile.mapY;
//			orn.mapZ = tile.mapZ;
//			orn.transform.position = tile.transform.position;
//			orn.transform.parent = transform;
//			orn.transform.localScale = Vector3.one*2;
//			orn.transform.localEulerAngles =  new Vector3(-90, 90, 0);
//			tile.ornamentOnTop = (MapOrnament)orn;
//			NGUITools.SetActive(orn.gameObject, true);
//		}
//	}
//
//	/// <summary>
//	/// Checks the left side tiles timeout.
//	/// </summary>
//	public void checkLeftSideTilesTimeout() {
//		MapTile tile = null;
//		MapTile tile2 = null;
//		
//		for(int i=0; i< mapSizeY; i++) {
//			tile = GetTileAt(sideLeft, i);
//			if(tile == null) continue;
//			tile.effectFall((Callback)onFinishFall);
//		}
//		sideLeft++;
//
//		while(true && sideLeft < sideRight) {
//			tile = GetTileAt(sideLeft, 0);
//			tile2 = GetTileAt(sideRight, 0);
//			if(tile2 != null && Vector3.Distance(tile.transform.position, tile2.transform.position) > MAX_Length) {
//				for(int i=0; i< mapSizeY; i++) {
//					tile = GetTileAt(sideLeft, i);
//					if(tile == null) continue;
//					tile.effectFall((Callback)onFinishFall);
//				}
//				sideLeft++;
//			} else {
//				break;
//			}
//		}
//	}
//
//	void onFinishFall(params object[] args) {
//		MapTile tile = (MapTile)(args[0]);
//		tiles.Remove(tile);
//		NGUITools.SetActive(tile.gameObject, false);
//		tile.clean();
//		MapTilePool.returnTile(tile);
//	}
		
#if UNITY_EDITOR
//	public bool isRefreshTiles = false;
//	void Update() {
//		if(!isRefreshTiles) return;
//		isRefreshTiles = false;
//		Transform tr = null;
//		MapTile mt = null;
//		MapElement me = null;
////		MapTile.tmpMap.Clear();
//		for(int i =0; i < transform.childCount; i++) {
//			tr = transform.GetChild(i);
//			mt = tr.GetComponent<MapTile>();
//			if(mt != null) {
//				mt.resetPos();
//			} else {
//				me = tr.GetComponent<MapElement>();
//				if(me != null) {
//					me.resetPos();
//				}
//			}
//		}
//	}
#endif

}

/// <summary>
/// Terrain infor.地形信息
/// </summary>
[System.Serializable]
public class CLTerrainInfor {
	public string name = "";
	public List<MapTileType> tileTypes;
	public List<MapTileType> ornTypes;
	public string ground = "";
	public float groundHigh = 0;
	public List<string> ornament4Ground;
	public float ornament4GroundHigh = 0;
	public string skyMaterial = "";
	public string effect = "";
	public bool fog;
	public float fogDensity;
	public Color fogColor = Color.white;
	public float fogStartDis;
	public float fogEndDis;
}
