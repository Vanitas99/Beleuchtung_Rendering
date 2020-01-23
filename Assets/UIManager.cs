using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager instance = null;
	[Header("Panels for UI")]
	public GameObject deathPanel;
	public GameObject pausePanel;
	public GameObject timerPanel;
	public GameObject dashPanel;


	private void Awake()
	{
		if (instance == null)
			instance = this;
	
	}

	#region Timer Functionality 
		
	#endregion

	#region Loading Functionality

	public void LoadMenu()
	{
		SceneManager.LoadScene(0);
		Time.timeScale = 1f;
		GameManager.gameIsPaused = false;
	}

	

	public void LoadDeathScreen()
	{
		UnloadPauseMenu();
		Cursor.visible = true;
		deathPanel.SetActive(true);
		timerPanel.SetActive(false);
		dashPanel.SetActive(false);
	}
	public void UnloadDeathScreen()
	{
		//UnloadPauseMenu();
		Cursor.visible = false;
		timerPanel.SetActive(true);
		dashPanel.SetActive(true);
		deathPanel.SetActive(false);
	}

	public void UnloadPauseMenu()
	{
		Cursor.visible = false;
		pausePanel.SetActive(false);
		timerPanel.SetActive(true);
		dashPanel.SetActive(true);
	}

	public void LoadPauseMenu()
	{
		Cursor.visible = true;
		timerPanel.SetActive(false);
		pausePanel.SetActive(true);
		dashPanel.SetActive(false);
	}


	#endregion

	#region Button Functionality

	public void OnResume()
	{
		GameManager.instance.Resume();
	}

	public void OnQuit()
	{
		Application.Quit();
	}

	public void OnRetry()
	{
		UnloadDeathScreen();
		//SceneManager.LoadScene(1);
	}

	#endregion
}
