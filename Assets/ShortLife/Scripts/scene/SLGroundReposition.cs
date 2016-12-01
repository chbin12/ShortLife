using UnityEngine;
using System.Collections;

public class SLGroundReposition : MonoBehaviour {
	public float high = 0;
	public Vector3 offset = Vector3.zero;
	public Transform target;
	Vector3 pos = Vector3.zero;
	Vector3 newPos = Vector3.zero;

	Transform _transform;
	public Transform transform 
	{
		get {
			if (_transform == null) {
				_transform = gameObject.transform;
			}
			return _transform;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if (target == null)
			return;
		pos = target.position;
		newPos = new Vector3 (pos.x, high, pos.z) + offset;
		transform.position = newPos;
	}
}
