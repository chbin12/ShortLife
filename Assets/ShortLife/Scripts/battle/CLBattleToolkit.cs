using UnityEngine;
using System.Collections;
using Toolkit;

// battle toolkit
public class CLBattleToolkit
{

	/// <summary>
	/// Gets the nearest target.
	/// 取得最可视范围内最近的单元，range为负数时代表全屏
	/// </summary>
	/// <returns>The nearest target.</returns>
	/// <param name="origin">Origin.</param>
	/// <param name="dis">Dis.</param>
	public static SUnit getNearestTarget (SUnit origin, float range, float minRange)
	{
		if (origin == null)
			return null;
		return getNearestTarget (origin.mbody.position, range, minRange, origin.isOffense);
	}
	public static SUnit getNearestTarget (Vector3 origin, float range, float minRange, bool isOffense)
	{
		ArrayList list = new ArrayList ();
		if (isOffense) {
			list.AddRange (CLBattle.self.defense);
		} else {
			list.AddRange (CLBattle.self.offense);
		}
		if (list.Count == 0) {
			return null;
		}
		list.Sort (new DistanceComp (origin));
		int count = list.Count;
		SUnit tmpUnit = null;
		SUnit ret = null;
		Vector3 pos = origin;
		float dis = 0;
		for (int i =0; i < count; i++) {
			tmpUnit = (SUnit)(list [i]);
			if (tmpUnit.isDead || tmpUnit.isCopyBody) {
				continue;
			}
			dis = Vector3.Distance (tmpUnit.mbody.position, pos);
			if (range < 0 || dis <= range) {
				if(minRange <=0 || dis >= minRange) {
					ret = tmpUnit;
					break;
				}
			}
		}
		list.Clear ();
		list = null;
		return ret;
	}

	public static SUnit getNearestTower (SUnit origin)
	{
		ArrayList list = new ArrayList ();
		if (origin.isOffense) {
			list.AddRange (CLBattle.self.defense);
		} else {
			list.AddRange (CLBattle.self.offense);
		}
		if (list.Count == 0) {
			return null;
		}
		list.Sort (new DistanceComp (origin.mbody.position));
		int count = list.Count;
		CLUnit4Lua tmpUnit = null;
		SUnit ret = null;
		Vector3 pos = origin.mbody.position;
		for (int i =0; i < count; i++) {
			tmpUnit = (CLUnit4Lua)(list [i]);
			if (tmpUnit.isDead || tmpUnit.isCopyBody || !tmpUnit.isTower) {
				continue;
			}
			ret = tmpUnit;
			break;
		}
		list.Clear ();
		list = null;
		return ret;
	}

	public static ArrayList getTargets (SUnit origin, float range)
	{
		return getTargets (origin.mbody, range, origin.isOffense);
	}
	
	public static ArrayList getTargets (Transform origin, float range, bool isOffense)
	{
		if (origin == null)
			return new ArrayList ();
		return getTargets (origin.position, range, isOffense);
	}
	public static ArrayList getTargets (Vector3 origin, float range, bool isOffense)
	{
		ArrayList list = new ArrayList ();
		ArrayList ret = new ArrayList ();
		if (isOffense) {
			list.AddRange (CLBattle.self.defense);
		} else {
			list.AddRange (CLBattle.self.offense);
		}
		if (list.Count == 0) {
			return ret;
		}
		list.Sort (new DistanceComp (origin));
		int count = list.Count;
		SUnit tmpUnit = null;
		Vector3 pos = origin;
		for (int i =0; i < count; i++) {
			tmpUnit = (SUnit)(list [i]);
			if (tmpUnit.isDead || tmpUnit.isCopyBody) {
				continue;
			}
			if (range < 0 || Vector3.Distance (tmpUnit.mbody.position, pos) <= range) {
				ret.Add (tmpUnit);
			} else {
				break;
			}
		}
		list.Clear ();
		list = null;
		return ret;
	}
}

public class DistanceComp : IComparer
{
	Vector3 target;
	
	public DistanceComp (Vector3 target)
	{
		this.target = target;
	}
	
	public int Compare (object x, object y)
	{
		Transform t1 = null;
		Transform t2 = null;
		if (x is SUnit && y is SUnit) {
			t1 = ((SUnit)x).mbody;
			t2 = ((SUnit)y).mbody;
		} else if (x is Transform && y is Transform) {
			t1 = (Transform)x;
			t2 = (Transform)y;
		} else {
			return 0;
		}
		Vector3 pos = target;
		
		float d2 = Vector3.Distance (t2.position, pos);
		float d1 = Vector3.Distance (t1.position, pos);
		float d = d2 - d1;
		return Mathf.Abs (d) < 0.0001f ? 0 : (d > 0 ? -1 : 1);
	}
}
