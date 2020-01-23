using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFollowPlayer : MonoBehaviour
{
	PlayerMovement script;
	Vector3 playerPosition;
	GameObject player;

	public Slider slider;
	float timer;
	bool timerActive;

	void Start()
    {
		player = GameObject.Find("BoneSheet");
		script = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		if (player.gameObject != null)
			playerPosition = player.transform.position + Vector3.up ;
		
		transform.position = playerPosition;

		

		if (script.IsDashOnCD())
		{
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(160,20);
			if (!timerActive)
			{
				timerActive = true;
				timer = 0f;
			}

		} else
		{
			timerActive = false;
			slider.value = 0;
			gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

		}

		timer += Time.deltaTime;
		slider.value = timer / script.dashCooldown;

	}
}
