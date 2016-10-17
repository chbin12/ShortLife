using UnityEngine;
using System.Collections;
using Toolkit;

public class MapData
{
	public int sizeX;
	public int sizeY;
	public ArrayList tiles;
	
	public Hashtable toMap ()
	{
		Hashtable r = new Hashtable ();
		r ["sizeX"] = sizeX;
		r ["sizeY"] = sizeY;
		
		ArrayList _list = new ArrayList ();
		int count = tiles.Count;
		MapTileData mtd = null;
		for (int i =0; i < count; i++) {
			mtd = (MapTileData)(tiles [i]);
			_list.Add (mtd.toMap ());
		}
		r ["tiles"] = _list;
		return r;
	}
	
	public static MapData parse (Hashtable map)
	{
		MapData r = new MapData ();
		r.sizeX = MapEx.getInt (map, "sizeX");
		r.sizeY = MapEx.getInt (map, "sizeY");
		r.tiles = new ArrayList ();
		ArrayList list = MapEx.getList (map, "tiles");
		if (list != null) {
			int count = list.Count;
			for (int i = 0; i < count; i++) {
				r.tiles.Add (MapTileData.parse ((Hashtable)(list [i])));
			}
		}
		return r;
	}
}

public class MapTileData
{
	public string name;
	public int id;
	public int layer;
	public int x;
	public int y;
	public int z;
	public Vector3 localPosition;
	public Vector3 localEulerAngles;
	public Vector3 localScale;
	public bool activeSelf;
	public string type;
	public string onTopObjPosStr;
	
	public Hashtable toMap ()
	{
		Hashtable r = new Hashtable ();
		r ["name"] = name;
		r["id"] = id;
		r ["layer"] = layer;
		r ["x"] = x;
		r ["y"] = y;
		r ["z"] =z;
		r ["localPosition"] = Utl.vector3ToMap (localPosition);
		r ["localEulerAngles"] = Utl.vector3ToMap (localEulerAngles);
		r ["localScale"] = Utl.vector3ToMap (localScale);
		r ["activeSelf"] = activeSelf;
		r ["type"] = type;
		r["onTopObjPosStr"] = onTopObjPosStr;
		return r;
	}
	
	public static MapTileData parse (Hashtable map)
	{
		MapTileData r = new MapTileData ();
		r .name = MapEx.getString (map, "name");
		r.id = MapEx.getInt(map, "id");
		r .layer = MapEx.getInt (map, "layer");
		r .x = MapEx.getInt (map, "x");
		r .y = MapEx.getInt (map, "y");
		r .z = MapEx.getInt (map, "z");
		r .localPosition = Utl.mapToVector3 (MapEx.getMap (map, "localPosition"));
		r .localEulerAngles = Utl.mapToVector3 (MapEx.getMap (map, "localEulerAngles"));
		r .localScale = Utl.mapToVector3 (MapEx.getMap (map, "localScale"));
		r.activeSelf = MapEx.getBool (map, "activeSelf");
		r .type = MapEx.getString (map, "type");
		r.onTopObjPosStr = MapEx.getString(map, "onTopObjPosStr");
		return r;
	}
}
