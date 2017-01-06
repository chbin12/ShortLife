using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// CL texture mgr.
/// 1.这个脚本可以取得物体及物体子对象上所有引用的Material及textures，并把这种引用关系保存起来
/// 2.在Start函数中，把引用关系中用到的Material及textures都取出来，并设置给对应的render
/// 3.当OnDestroy销毁该对象时，还回引用的资源
/// 4.当生成assetbundle时，要去掉引用
/// 5.该脚本只能挂在prefab的最外层
/// </summary>
public class CLTextureMgr : MonoBehaviour
{
	public static CLDelegate OnGetTextureCallbacks = new CLDelegate ();
	[SerializeField]
	public List<CLTexture>
		data = new List<CLTexture> ();
	bool isFinishInited = false;
	bool isAllTextureLoaded = false;
	// Use this for initialization
	public void Start ()
	{
		if (isFinishInited)
			return;
		isFinishInited = true;
		resetMat ();
	}

	public void init (object finishCallback, object orgs)
	{
		if (isAllTextureLoaded) {
			if (finishCallback != null) {
				if (finishCallback is LuaInterface.LuaFunction) {
					((LuaInterface.LuaFunction)finishCallback).Call (orgs);
				} else if (finishCallback is Callback) {
					((Callback)finishCallback) (orgs);
				}
			}
		} else {
			OnGetTextureCallbacks.add (gameObject.GetInstanceID().ToString(), finishCallback, orgs);
			Start ();
		}
	}

	public void OnDestroy ()
	{
		returnTextures ();
	}

	public void returnTextures() {
		CLTexture clMat;
		Texture tex = null;
		for (int i=0; i < data.Count; i++) {
			clMat = data [i];
			if (clMat.material.mainTexture != null) {
				CLTexturePool.returnTexture (clMat.texture);
			}
		}
	}
	
	/// <summary>
	/// Cleans the mat.清除对材质球的引用
	/// </summary>
	public void cleanMat ()
	{
		CLTexture clMat;
		for (int i=0; i < data.Count; i++) {
			clMat = data [i];
			clMat.material.mainTexture = null;
		}
	}
	
	public void resetMat ()
	{
		CLTexture clMat;
		Texture tex = null;
		for (int i=0; i < data.Count; i++) {
			clMat = data [i];
			if (clMat.render == null || clMat.material == null || string.IsNullOrEmpty (clMat.texture))
				continue;
#if UNITY_EDITOR
			string tmpPath = clMat.texture4Editor;
			if(Application.isPlaying) {
				if(SCfg.self.isEditMode) {
					tmpPath = clMat.texture.Replace("/upgradeRes/", "/upgradeRes4Publish/");
					CLTexturePool.borrowTextureAsyn (tmpPath, (Callback)onGetTexture, clMat);
				} else {
					CLTexturePool.borrowTextureAsyn (clMat.texture, (Callback)onGetTexture, clMat);
				}
			} else {
				if (!tmpPath.StartsWith ("Assets/")) {
					tmpPath = "Assets/" + tmpPath;
				}
				tex = AssetDatabase.LoadAssetAtPath (
					tmpPath, typeof(UnityEngine.Object)) as Texture;
				clMat.material.mainTexture = tex;
			}
#else
			CLTexturePool.borrowTextureAsyn (clMat.texture, (Callback)onGetTexture, clMat);
#endif
		}
	}
	
	public void resetMat2 ()
	{
		CLTexture clMat;
		Texture tex = null;
		for (int i=0; i < data.Count; i++) {
			clMat = data [i];
			clMat.render.sharedMaterial = clMat.material;
		}
	}

	int counter = 0;

	void onGetTexture (object[] paras)
	{
		if (paras != null && paras.Length > 2) {
			counter++;
			string path = paras [0].ToString ();
			Texture tex = paras [1] as Texture;
			CLTexture mat = paras [2] as CLTexture;
//			try {
				if (mat != null && mat.material != null) {
					mat.material.mainTexture = tex;
					#if UNITY_EDITOR
					//因为是通过assetebundle加载的，在真机上不需要处理，只有在pc上需要重设置shader
					mat.material.shader = Shader.Find(mat.material.shader.name);
					#endif
					mat.render.sharedMaterial = mat.material;
					CLMaterialCache.setMaterial (mat.material);
				}
				if (counter >= data.Count) {
					isAllTextureLoaded = true;
					ArrayList callbackList = OnGetTextureCallbacks.getDelegates (gameObject.GetInstanceID().ToString());
					int count = callbackList.Count;
					ArrayList cell = null;
					object cb = null;
					object orgs = null;
					for (int i=0; i< count; i++) {
						cell = callbackList [i] as ArrayList;
						if (cell != null && cell.Count > 1) {
							cb = cell [0];
							orgs = cell [1];
							if (cb != null) {
								if (cb is LuaInterface.LuaFunction) {
									((LuaInterface.LuaFunction)cb).Call (orgs);
								} else if (cb is Callback) {
									((Callback)cb) (orgs);
								}
							}
						}
					}
					callbackList.Clear ();
					OnGetTextureCallbacks.removeDelegates (gameObject.GetInstanceID().ToString());
				}
//			} catch (System.Exception e) {
//				Debug.LogError ("name===" + name + ",err:" + e);
//			}
		}
	}
}

[System.Serializable]
public class CLTexture
{
	public Renderer render;
	public Material  material;
	public string texture4Editor;
	[System.NonSerialized]
	static Hashtable
		texturePathCache = new Hashtable ();
	
	public string texture {
		get {
			string texPath = MapEx.getString (texturePathCache, texture4Editor);
			if (string.IsNullOrEmpty (texPath) && !string.IsNullOrEmpty (texture4Editor)) {
				texPath = PStr.b ().a (Path.GetDirectoryName (texture4Editor)).a ("/").a (PathCfg.self.platform).a ("/").a (Path.GetFileNameWithoutExtension (texture4Editor)).a (".unity3d").e ();
				texPath = texPath.Replace ("/upgradeResMedium/", "/upgradeRes/");
				texturePathCache [texture4Editor] = texPath;
			}
			return texPath;
		}
	}
}