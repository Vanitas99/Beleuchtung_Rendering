using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleButtons : MonoBehaviour
{
    public void OnTutorial()
	{
		SceneManager.LoadScene(1);
	}

	public void OnQuit()
	{
		Application.Quit();
	}
}
