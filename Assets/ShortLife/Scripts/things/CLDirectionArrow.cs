using UnityEngine;
using System.Collections;

public class CLDirectionArrow : MonoBehaviour {
	public LineRenderer line;
	public Transform arrow;
	public Renderer arrowRender;
	Vector3 startPos;
	Vector3 endPos;

	public void init(float startWidth, float endWidth, Color startColor, Color endColor, bool useWorldSpace) {
		line.SetWidth (startWidth, endWidth);
		line.SetColors (startColor, endColor);
		line.useWorldSpace = useWorldSpace;
		arrowRender.sharedMaterial.color = endColor;
	}

	public void SetPosition(Vector3 startPos, Vector3 endPos) {
		this.startPos = startPos;
		this.endPos = endPos;
		line.SetPosition (0, startPos);
		line.SetPosition (1, endPos);
		arrow.position = endPos;
		Utl.RotateTowards (arrow, startPos, endPos);
	}
	
	public void SetEndPosition( Vector3 endPos) {
		this.endPos = endPos;
		line.SetPosition (1, endPos);
		arrow.position = endPos;
		Utl.RotateTowards (arrow, startPos, endPos);
	}

	public void SetStartPosition(Vector3 startPos) {
		this.startPos = startPos;
		line.SetPosition (0, startPos);
		Utl.RotateTowards (arrow, startPos, endPos);
	}
}
