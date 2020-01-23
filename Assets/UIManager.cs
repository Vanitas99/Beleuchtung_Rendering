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
		Cursor.visible = false;
		deathPanel.SetActive(true);
	}
	public void UnloadDeathScreen()
	{
		//UnloadPauseMenu();
		Cursor.visible = true;
		deathPanel.SetActive(false);
	}

	public void UnloadPauseMenu()
	{
		Cursor.visible = false;
		pausePanel.SetActive(false);
	}

	public void LoadPauseMenu()
	{
		Cursor.visible = true;
		pausePanel.SetActive(true);
	}


	#endregion

	#region Button Functionality

	public void OnResume()
	{
		UnloadPauseMenu();
		Time.timeScale = 1f;
		GameManager.gameIsPaused = false;
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
