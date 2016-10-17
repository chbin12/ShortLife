using UnityEngine;
using System.Collections;

/// <summary>
/// CL scene light mapping.
/// </summary>
public class CLSceneLightMapping : MonoBehaviour
{
    public bool isNeedLightmapping = true;
    public Texture2D[] lightMaps;
    LightmapData[] _lightmapDatas = null;

    public LightmapData[] lightmapDatas
    {
        get
        {
            if (lightMaps == null)
            {
                return null;
            }
            if (_lightmapDatas == null)
            {
                _lightmapDatas = new LightmapData[lightMaps.Length];
                for (int i = 0; i < lightMaps.Length; i++)
                {
                    LightmapData ld = new LightmapData();
					ld.lightmapNear = lightMaps[i];
                    ld.lightmapFar = lightMaps[i];
                    _lightmapDatas[i] = ld;
                }
            }
            return _lightmapDatas;
        }
    }

    public void setLightmapping()
    {
#if UNITY_5
        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
#else
        LightmapSettings.lightmapsMode = LightmapsMode.Single;
#endif

        if (isNeedLightmapping)
        {
            LightmapSettings.lightmaps = lightmapDatas;
        }
        else
        {
            LightmapSettings.lightmaps = new LightmapData[0];
        }
    }
}
