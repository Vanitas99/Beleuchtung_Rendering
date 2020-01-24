using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public Vector3 offset;
	public float smoothSpeed = 0.125f;
	public Vector3 buildingCamPosition;
	// Start is called before the first frame update

	private void Awake()
	{
		ChangeToPlayingMode();
		buildingCamPosition = new Vector3(14.97f, 14.97f, -63.9f);
	}


	

	private void LateUpdate()
	{
		if (GameManager.instance.currentStage == GameManager.GameStage.Playing 
		|| GameManager.instance.currentStage == GameManager.GameStage.Dead) 
		{
			ChangeToPlayingMode();
			Vector3 targetVelocity = target.gameObject.GetComponent<PlayerMovement>().GetCurrentVelocity();
			float targetY = 0f;
			if (Mathf.Abs(targetVelocity.y) > 2f)
			{
				targetY = targetVelocity.y / 2;
			}
			Vector3 dirpPos = target.position + new Vector3(targetVelocity.x, targetY, 0f) / 4 + offset;
			Debug.DrawRay(target.position, new Vector3(targetVelocity.x, 0f, 0f));
			Vector3 smoothPos = Vector3.Lerp(transform.position, dirpPos, smoothSpeed);
			transform.position = smoothPos;
		} else
		{
			ChangeToBuildingMode();
		}
		
	}

	public void ChangeToPlayingMode()
	{
		transform.rotation = Quaternion.identity;
		if (Camera.main.orthographic == true)
			Camera.main.orthographic = false;
	}

	public void ChangeToBuildingMode()
	{
		if (Camera.main.orthographic == false)
			Camera.main.orthographic = true;
		
		transform.eulerAngles = new Vector3(35f, 0f, 0f);
		transform.position = buildingCamPosition;
	}
}
