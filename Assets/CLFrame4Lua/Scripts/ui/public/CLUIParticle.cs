using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent(typeof(UITexture))]

/// <summary>
/// CLUI particle.在ui中使用粒子时，需要绑定该脚本，主要功能是设置粒子的renderQueue,使粒子可以在两个页面之间显示，而不总是显示在最顶层
/// </summary>
public class CLUIParticle : MonoBehaviour
{
	public UIPanel _panel;
	UIWidget widget;
	public int depth = 0;
	public int currRenderQueue = 0;
	Renderer[] _renders;
	Renderer[] renders {
		get {
			if(_renders == null) {
				_renders = GetComponentsInChildren<Renderer>();
			}
			return _renders;
		}
	}
	public UIPanel panel {
		get {
			if (_panel == null) {
				_panel = GetComponentInParent<UIPanel>();
			}
			return _panel;
		}
	}
	// Use this for initialization
	public void Start()
	{
		mWidget.depth = depth;
		#if UNITY_EDITOR
		//因为是通过assetebundle加载的，在真机上不需要处理，只有在pc上需要重设置shader
		if(Application.isPlaying) {
			Utl.setBodyMatEdit(transform);
		}
		#endif

	}

	public int mDepth {
		get {
			return depth;
		}
		set {
			depth = value;
			mWidget.depth = depth;
		}
	}

	public UIWidget mWidget{
		get {
			if (widget == null) {
				// 建议使用 _empty 的 UITextures
				widget = GetComponent<UIWidget>();
				widget.height = 1;
				widget.width = 1;
				widget.alpha = 0.004f;
				widget.depth = depth;
			}
			return widget;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
		#if UNITY_EDITOR
		if(!Application.isPlaying) {
			mWidget.depth = depth;
		}
		#endif
		setRenderQueue();
	}

	[ContextMenu("setRenderQueue")]
	void setRenderQueue()
	{
		int newRenderQ = currRenderQueue;
		Material mat = null;
		if (mWidget != null && NGUITools.GetActive(mWidget) ) {
			if (mWidget.drawCall == null) {
				#if UNITY_EDITOR
				Debug.LogWarning("widget.drawCall == null");
				#endif
				mWidget.depth = depth;
			} else {
				newRenderQ = mWidget.drawCall.finalRenderQueue;
			}
		} else {
			newRenderQ = panel != null ? panel.startingRenderQueue + depth : depth;
		}

		if (newRenderQ != currRenderQueue) {
			for(int i=0; i < renders.Length; i++) {
				mat = renders[i].material;
				if(mat != null) {
					mat.renderQueue =  newRenderQ;
				}
			}
			currRenderQueue = newRenderQ;
		}
	}
}
