using UnityEngine;
using System.Collections;

public class CLUIPlaySound : UIPlaySound {
	[HideInInspector]
	public string soundFileName = "Tap.wav";
	public string soundName = "Tap";

	public override void Play ()
	{
		Toolkit.SoundEx.playSound(soundName, volume, 1);
	}
}
