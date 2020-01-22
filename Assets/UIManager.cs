using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance = null;
	[Header("Panels for UI")]
	public GameObject deathPanel;
	public GameObject pausePanel;



	private void Awake()
	{
		if (instance == null)
			instance = this;
	
	}

	#region Loading Functionality
	
	public void LoadDeathScreen()
	{

	}

	#endregion

	#region Button Functionality

	public void OnQuit()
	{
		Application.Quit();
	}

	public void OnRetry()
	{
		deathPanel.SetActive(false);
	}

	#endregion
}
