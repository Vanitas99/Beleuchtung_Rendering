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
    public GameObject finishPanel;


	private void Awake()
	{
		if (instance == null)
			instance = this;
	
	}

	#region Timer Functionality 
		
	#endregion

	#region Loading Functionality

	public void FreshStart()
	{
        deathPanel.SetActive(false);
        pausePanel.SetActive(false);
        finishPanel.SetActive(false);
        dashPanel.SetActive(true);

        //GameManager.instance.player.transform.localScale = new Vector3(1, 1, 1);
        GameManager.instance.player.SetActive(true);
        //GameManager.instance.player.GetComponent<PlayerMovement>().ZeroVelocity();
        GameManager.instance.player.transform.position = GameManager.instance.playerPosition;
	}

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
		if (GameManager.instance.player)
			GameManager.instance.player.SetActive(true);
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
		pausePanel.SetActive(true);
		Cursor.visible = false;
		timerPanel.SetActive(false);
		dashPanel.SetActive(false);

	}

    public void LoadFinishMenu()
    {
        finishPanel.SetActive(true);
        Cursor.visible = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	#endregion
}
