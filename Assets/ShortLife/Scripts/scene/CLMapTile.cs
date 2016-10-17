using UnityEngine;
using System.Collections;

public class CLMapTile : CLBaseLua
{
	public const float OffsetX = 0.8660254f * 2;
	public const float OffsetY = 0.75f * 2;
	public const float RowOffsetX = 0.4330127f * 2;
	public const float OffsetZ = 0.84f * 2;
	public MapTileType tileType;
	public float height = 0;
	//	[HideInInspector()]
	public int mapX;
	//	[HideInInspector()]
	public int mapY;
	//	[HideInInspector()]
	public int mapZ;
	//	[HideInInspector()]
	public CLMapTile ornamentOnTop; 	//有装饰物在上面
	public CLMapTile mapTileBelow;		//属于哪个tile
	
	TweenPosition _tweenPosition;

	public TweenPosition tweenPosition {
		get {
			if (_tweenPosition == null) {
				_tweenPosition = GetComponent<TweenPosition> ();
				if (_tweenPosition == null) {
					_tweenPosition = gameObject.AddComponent<TweenPosition> ();
				}
			}
			return _tweenPosition;
		}
	}
	//	Keyframe kf = new Keyframe(
	public AnimationCurve shakeCurve = new AnimationCurve (new Keyframe (0, 0, 4.7f, 4.7f),
	                                                      new Keyframe (0.043f, 0.2f, -0.223f, -0.223f),
	                                                      new Keyframe (0.118f, -0.185f, -1.185f, -1.185f),
	                                                      new Keyframe (0.297f, 0.312f, -0.1057f, -0.1057f),
	                                                      new Keyframe (0.5f, -0.295f, 0.319f, 0.319f),
	                                                      new Keyframe (0.69f, 0.4f, -0.116f, -0.116f),
	                                                      new Keyframe (0.89f, -0.395f, 0.0158f, 0.0158f),
	                                                      new Keyframe (1f, 0, 3.9f, 3.9f)
	);
	public AnimationCurve fallCurve = new AnimationCurve (new Keyframe (0, 0), new Keyframe (0.915f, 1.1f), new Keyframe (1, 1));
	
	public bool IsEmpty {
		get { return ornamentOnTop == null;}
	}
	
	public bool _canMoveTo = true;
	
	public bool CanMoveTo {
		get {
			return ornamentOnTop == null 
				&& _canMoveTo
				&& mapZ == 0;
		}
	}
	
	public string posStr {
		get {
			return Toolkit.PStr.begin (mapX, "_", mapY, "_", mapZ).end ();
		}
	}
	
	public Vector3 pos {
		get {
			Vector3 p = transform.position;
			p.y += height;
			return p;
		}
	}
}
public enum MapTileType
{
	s_01,
	s_02,
	s_03,
	s_04,
	s_05,
	s_06,
	s_07,
	s_08,
	s_09,
	s_10,
	s_11,
	s_12,
	s_13,
	s_14,
	s_15,
	s_16,
	s_17,
	s_18,
	s_19,
	s_20,
	s_21,
	s_22,
	s_23,
	s_24,
	stone_01,
	stone_02,
	stone_03,
	stone_04,
	stone_05,
	stone_06,
	stone_07,
	stone_08,
	tree_01,
	tree_02,
	tree_03,
	tree_04,
	tree_05,
	tree_06,
	tree_07,
	tree_08,
	tree_09,
	tree_10,
	tree_11,
	tree_12,
	tree_13,
	tree_14,
	tree_15,
}