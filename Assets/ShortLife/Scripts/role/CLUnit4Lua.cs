using UnityEngine;
using System.Collections;
using LuaInterface;

public class CLUnit4Lua : SUnit
{
	public bool isFinishSetLua = false;
	public bool isTower = false;
	public SRoleAction action;	//动作
	public MyTween tween;		//位移
	public CLAIPath aiPath;		//a*寻路
	public CLEjector ejector; 	//发射器
	public SRoleAvata avata;	//换装用
	public GameObject shadow;
	public GameObject shadow2;
	public LuaFunction lfinit;
	public LuaFunction lfonPathComplete;
	public LuaFunction lfonMoving;
	public LuaFunction lfonArrived;
	public LuaFunction lfdoSearchTarget;
	public LuaFunction lfonBeTarget;
	public LuaFunction lfonRelaseTarget;
	public LuaFunction lfonHurt;
	public LuaFunction lfonHurtHP;
	public LuaFunction lfonHurtFinish;
	public LuaFunction lfdoAttack;
	public LuaFunction lfonDead;
	public LuaFunction lfpause;
	public LuaFunction lfregain;

	public override void init (int id, int star, int lev, bool isOffense, object other)
	{
		Start ();
		base.init (id, star, lev, isOffense, other);
		if (!isFinishSetLua) {
			isFinishSetLua = true;
			setLua ();
		}
		if (aiPath != null) {
			aiPath.init (onPathComplete, (Callback)onMoving, (Callback)onArrived);
		}
		if (lfinit != null) {
			lfinit.Call (this, id, star, lev, isOffense, other);
		}
	}

	public override void setLua ()
	{
		if(luaTable != null) {
			destoryLua();
		}
		base.setLua ();
	}

	public override void initGetLuaFunc ()
	{
		base.initGetLuaFunc ();
		if (luaTable != null) {
			lfinit = getLuaFunction ("init");
			lfonPathComplete = getLuaFunction ("onPathComplete");
			lfonMoving = getLuaFunction ("onMoving");
			lfonArrived = getLuaFunction ("onArrived");
			lfdoSearchTarget = getLuaFunction ("doSearchTarget");
			lfonBeTarget = getLuaFunction ("onBeTarget");
			lfonRelaseTarget = getLuaFunction ("onRelaseTarget");
			lfonHurt = getLuaFunction ("onHurt");
			lfonHurtHP = getLuaFunction ("onHurtHP");
			lfonHurtFinish = getLuaFunction ("onHurtFinish");
			lfdoAttack = getLuaFunction ("doAttack");
			lfonDead = getLuaFunction ("onDead");
			lfpause = getLuaFunction ("pause");
			lfregain = getLuaFunction ("regain");
		}
	}
	bool isStarted = false;
	public override void Start ()
	{
		if (isStarted)
			return;
		isStarted = true;
		base.Start ();
		CLTextureMgr tm = GetComponent<CLTextureMgr> ();
		if (tm != null) {
			tm.Start();
		}
	}
	public void onPathComplete (Pathfinding.Path _p)
	{
		if (lfonPathComplete != null) {
			lfonPathComplete.Call (_p);
		}
	}

	public void onMoving (object[] paras)
	{
		if (lfonMoving != null)
			lfonMoving.Call ();
	}

	public void onArrived (object[] paras)
	{
		if (lfonArrived != null)
			lfonArrived.Call ();
	}

	public override SUnit doSearchTarget ()
	{
		if (lfdoSearchTarget != null) {
			object[] ret = lfdoSearchTarget.Call ();
			if (ret != null && ret.Length > 0) {
				return (SUnit)(ret [0]);
			}
		}
		return null;
	}

	public override void onBeTarget (SUnit attacker)
	{
		if (lfonBeTarget != null) {
			lfonBeTarget.Call (attacker);
		}
	}

	public override void onRelaseTarget (SUnit attacker)
	{
		if (lfonRelaseTarget != null) {
			lfonRelaseTarget.Call (attacker);
		}
	}

	public override bool onHurt (int hurt, object skillAttr, SUnit attacker)
	{
		if (lfonHurt != null) {
			object[] ret = lfonHurt.Call (hurt, skillAttr, attacker);
			if (ret != null && ret.Length > 0) {
				return (bool)(ret [0]);
			}
		}
		return false;
	}

	public override void onHurtHP (int hurt, object skillAttr)
	{
		if (lfonHurtHP != null) {
			lfonHurtHP.Call (hurt, skillAttr);
		}
	}

	public override void onHurtFinish (object skillAttr, SUnit attacker)
	{
		if (lfonHurtFinish != null) {
			lfonHurtFinish.Call (skillAttr, attacker);
		}
	}

	public override void doAttack ()
	{
		if (lfdoAttack != null) {
			lfdoAttack.Call ();
		}
	}

	public override void onDead ()
	{
		if (lfonDead != null)
			lfonDead.Call ();
	}

	public override void moveTo (Vector3 toPos)
	{
		if (aiPath != null) {
			aiPath.moveTo (toPos);
		}
	}
	
	public void moveTo (Vector3 toPos, float speed)
	{
		if (aiPath == null) {
			return;
		}
		moveTo (toPos, speed, aiPath.endReachedDistance);
	}

	public void moveTo (Vector3 toPos, float speed, float endReachedDistance)
	{
		if (aiPath == null) {
			return;
		}
		aiPath.speed = speed;
		aiPath.endReachedDistance = endReachedDistance;
		moveTo (toPos);
	}

	public void moveToDelay (Vector3 toPos,  float speed, float endReachedDistance,  float delaySec)
	{
		if (aiPath == null) {
			return;
		}
		aiPath.speed = speed;
		aiPath.endReachedDistance = endReachedDistance;
		aiPath.moveToDelay (toPos, delaySec);
	}

	public override void moveToTarget (Transform target)
	{
		if (aiPath == null)
			return;
		aiPath.startRepeatSearchPath (target, aiPath.repathRate);
	}

	public void moveToTarget (Transform target, float speed)
	{
		moveToTarget (target, speed, aiPath.endReachedDistance, aiPath.repathRate);
	}
	
	public void moveToTarget (Transform target, float speed, float endReachedDistance)
	{
		moveToTarget (target, speed, endReachedDistance, aiPath.repathRate);
	}

	public void moveToTarget (Transform target, float speed, float endReachedDistance, float repathRate)
	{
		if (aiPath == null)
			return;
		aiPath.speed = speed;
		aiPath.endReachedDistance = endReachedDistance;
		aiPath.startRepeatSearchPath (target, repathRate);
	}

	public override void pause ()
	{
		base.pause ();
		if (aiPath != null) {
			aiPath.pause ();
		}
		if (tween != null) {
			tween.enabled = false;
		}
		if (action != null) {
			action.pause();
		}
		if (lfpause != null) {
			lfpause.Call ();
		}
	}

	public override void regain ()
	{
		base.regain ();
		if (aiPath != null) {
			aiPath.regain ();
		}
		if (tween != null) {
			tween.enabled = true;
		}
		if (action != null) {
			action.regain();
		}
		if (lfregain != null) {
			lfregain.Call ();
		}
	}

	
	public void setUnitTrans (Color mainColor)
	{
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Unlit/Transparent Colored");
		mat.SetColor ("_Color", mainColor);
		setBodyMat (mbody, mat);
	}
	
	public void setOutlineShader (Color mainColor, Color outLineColor)
	{
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Outlined/Silhouetted Diffuse");
//		materials.shader = Shader.Find ("Outlined/Silhouetted Diffuse");
		mat.SetColor ("_Color", mainColor);
		mat .SetColor ("_OutlineColor", outLineColor);
		setBodyMat (mbody, mat);
	}
	
	public void setToonShader (Color color)
	{
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Toon/Basic");
		mat.SetColor ("_Color", color);
		setBodyMat (mbody, mat);
	}
	
	public void setGrayShader ()
	{
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Unlit/Transparent Colore Gray");
		setBodyMat (mbody, mat);
	}
	
	public void setToonShaderColor (Color mainColor, Color outLineColor)
	{
		setToonShaderColor (mainColor, outLineColor, 0.002f);
	}

	public void setToonShaderColor (Color mainColor, Color outLineColor, float outLineSize)
	{
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Toon/Basic Outline");
		mat.SetColor ("_Color", mainColor);
		mat.SetColor ("_OutlineColor", outLineColor);
		mat.SetFloat ("_Outline", outLineSize);
		setBodyMat (mbody, mat);
	}
	
	public void setRimLight(Color mainColor, Color rimColor, float rimWidth) {
		if (materials == null) {
			return;
		}
		Material mat = CLMaterialCache.getMaterial (materials.mainTexture, "Mobhero/RimLight");
		mat.SetColor ("_Color", mainColor);
		mat.SetColor ("_RimColor", rimColor);
		mat.SetFloat ("_RimWidth", rimWidth);
		setBodyMat (mbody, mat);
	}

}
