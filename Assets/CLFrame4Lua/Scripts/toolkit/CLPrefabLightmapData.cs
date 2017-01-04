using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

/// <summary>
/// CL prefab lightmap data. for unity 5.x version
/// </summary>
public class CLPrefabLightmapData : MonoBehaviour
{
	[System.Serializable]
	public struct RendererInfo
	{
		public Renderer renderer;
		public int lightmapIndex;
		public Vector4 lightmapOffsetScale;
	}

	[SerializeField]
	public LightmapsMode lightmapsMode;

	[SerializeField]
	public LightProbes lightProbes;

	[SerializeField]
	public List<RendererInfo>	m_RendererInfo = new List<RendererInfo> ();

	[SerializeField]
	public Texture2D[] m_Lightmaps;


	void Awake ()
	{
		LoadLightmap ();
	}

	public void SaveLightmap ()
	{
		m_RendererInfo.Clear ();
		lightmapsMode = LightmapSettings.lightmapsMode;
		lightProbes = LightmapSettings.lightProbes;

		var renderers = GetComponentsInChildren<MeshRenderer> ();
		Texture2D lightmapTexture;
		LightmapData lightMapData;
		m_Lightmaps = new Texture2D[LightmapSettings.lightmaps.Length];
		foreach (MeshRenderer r in renderers) {
			if (r.lightmapIndex != -1) {
				RendererInfo info = new RendererInfo ();
				info.renderer = r;
				info.lightmapOffsetScale = r.lightmapScaleOffset;
				info.lightmapIndex = r.lightmapIndex;
				//=====================================
				Debug.Log(r.lightmapIndex);
				lightMapData = LightmapSettings.lightmaps [r.lightmapIndex];
				lightmapTexture = lightMapData.lightmapFar;
//				info.lightmapIndex = m_Lightmaps.IndexOf (lightmapTexture);
				if (m_Lightmaps[r.lightmapIndex] == null) {
					info.lightmapIndex = r.lightmapIndex;
//					m_Lightmaps.Add (lightmapTexture);
					m_Lightmaps[r.lightmapIndex] = lightmapTexture;
				}
				//=====================================

				m_RendererInfo.Add (info);
			}
		}
	}

	public void LoadLightmap ()
	{
		if (m_RendererInfo == null || m_RendererInfo.Count <= 0)
			return;

//		Debug.Log ("--------------------------------------------");
//		var renderers = GetComponentsInChildren<MeshRenderer> ();
//		foreach (var item in renderers) {
//			Debug.Log (item.lightmapIndex);
//		}

				
		var lightmaps = LightmapSettings.lightmaps;
		int preIndex = lightmaps.Length;
		var combinedLightmaps = new LightmapData[preIndex + m_Lightmaps.Length];
		lightmaps.CopyTo (combinedLightmaps, 0);
		LightmapData lightMapData;
		for (int i = 0; i < m_Lightmaps.Length; i++) {
			lightMapData = new LightmapData ();
			lightMapData.lightmapFar = m_Lightmaps [i];
			lightMapData.lightmapNear = m_Lightmaps [i];
			combinedLightmaps [preIndex + i] = lightMapData;
		}
//				ApplyRendererInfo(m_RendererInfo, lightmaps.Length);
		LightmapSettings.lightmapsMode = lightmapsMode;
		LightmapSettings.lightmaps = combinedLightmaps;

		foreach (var item in m_RendererInfo) {
			item.renderer.lightmapIndex = preIndex + item.lightmapIndex;
			item.renderer.lightmapScaleOffset = item.lightmapOffsetScale;
		}

	}
}