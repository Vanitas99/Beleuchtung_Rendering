using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerPanelUpdate : MonoBehaviour
{
	public Image timerImg;
	public GameObject timerText;
	private bool timerActive;
	float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.timerRunning)
		{
			timerImg.gameObject.SetActive(true);
			timerText.gameObject.SetActive(true);
			if (!timerActive)
			{
				timerActive = true;
				timeElapsed = 0f;

			}
		} else
		{
			timerActive = false;
			timerImg.fillAmount = 0f;
			timerText.gameObject.SetActive(false);
			timerImg.gameObject.SetActive(false);
		}

		timeElapsed += Time.deltaTime;
		timerImg.fillAmount = timeElapsed / GameManager.instance.buildingTimer;
		timerText.GetComponent<TextMeshProUGUI>().text = (GameManager.instance.buildingTimer - timeElapsed).ToString("F0");
    }
}
