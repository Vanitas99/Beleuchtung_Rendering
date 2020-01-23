using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleButtons : MonoBehaviour
{
    public void OnTutorial()
	{
		OnClick();
		SceneManager.LoadScene(1);
	}

	public void OnQuit()
	{
		OnClick();
		Application.Quit();
	}

	public void OnHighLight()
	{
		SoundManager.instance.PlaySound("Highlight");
	}

	public void OnClick()
	{
		SoundManager.instance.PlaySound("Click");
	}
}
