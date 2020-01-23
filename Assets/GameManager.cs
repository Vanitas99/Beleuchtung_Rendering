using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;


	public static bool gameIsPaused = false;
	public static bool dead = false;
	public static bool timerRunning = false;


	public float buildingTimer = 5f;
	private UIManager uiManager;
	private MoveManager moveManager;
	public GameObject player;


	public enum GameStage
	{
		Building,
		Playing
	}

	public GameStage currentStage = GameStage.Playing;

	void Awake()
	{
		if (instance == null)
			instance = this;
		uiManager = UIManager.instance;
		moveManager = MoveManager.Instance;
		currentStage = GameStage.Playing;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		if (dead)
		{
			uiManager.LoadDeathScreen();
		}
	}

	private void Start()
	{
		uiManager.UnloadPauseMenu();
		uiManager.UnloadDeathScreen();
	}


	public void StartTimer()
	{
		
		StartCoroutine(BuildingTimer(buildingTimer));
	}

	void Resume()
	{
		uiManager.UnloadPauseMenu();
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	void Pause()
	{
		uiManager.LoadPauseMenu();
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	private IEnumerator BuildingTimer(float time)
	{
		timerRunning = true;
		currentStage = GameStage.Building;
		player.GetComponent<PlayerMovement>().ZeroVelocity();

		yield return new WaitForSeconds(time);
		timerRunning = false;
		currentStage = GameStage.Playing;
	}
}
