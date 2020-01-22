using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private UIManager uiManager;

	private void Awake()
	{
		uiManager = UIManager.instance;
	}
}
