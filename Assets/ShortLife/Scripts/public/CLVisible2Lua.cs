using UnityEngine;
using System.Collections;

public class CLVisible2Lua : MonoBehaviour {

	public EventDelegate flOnBecameInvisible;
	public EventDelegate flOnBecameVisible;
	public virtual  void OnBecameInvisible ()
	{
		if (flOnBecameInvisible != null) {
			if(Application.isPlaying) {
				flOnBecameInvisible.Execute (gameObject);
			}
		}
	}
	void OnApplicationQuit() {
		flOnBecameInvisible = null;
	}

	public virtual  void OnBecameVisible ()
	{
		if (flOnBecameVisible != null) {
			flOnBecameVisible.Execute (gameObject);
		}
	}

}
