using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleButtonPause : MonoBehaviour
{
	public void PlayHighlightSound()
	{
		SoundManager.instance.PlaySound("ButtonHighlight");
	}

	public void PlayClickSound()
	{
		SoundManager.instance.PlaySound("ButtonClick");
	}
}
