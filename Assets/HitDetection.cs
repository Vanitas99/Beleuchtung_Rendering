﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    Vector3 playerPosition;
	Vector3 leftWallCollision, rightWallCollision, headCollision;

    Vector3 lightPosition;
    public bool isGrounded;
	public bool leftBlocked;
	public bool rightBlocked;
	public bool upBlocked;
    // Start is called before the first frame update
    void Start()
    {
        lightPosition = GameObject.Find("MainLightSource").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		playerPosition = gameObject.transform.position + Vector3.down * 0.4f ;
		leftWallCollision = gameObject.transform.position + Vector3.left * 0.1f + Vector3.down*0.3f;
		rightWallCollision = gameObject.transform.position + Vector3.right * 0.1f + Vector3.down * 0.3f;
		headCollision = gameObject.transform.position + Vector3.up * 0.3f;


        if(isPlayerGrounded()) 
        {
            isGrounded = true;
            //Debug.Log("Player is grounded");
            Debug.DrawRay(playerPosition, lightPosition - playerPosition, Color.red);
        } else {
            isGrounded = false;
            //Debug.Log("Player in Air");
            Debug.DrawRay(playerPosition, lightPosition - playerPosition);
        }

		if (HitLeftWall())
		{
			leftBlocked = true;
			Debug.DrawRay(leftWallCollision, lightPosition - leftWallCollision, Color.red);
		}
		else
		{
			leftBlocked = false;
			//Debug.Log("Player in Air");
			Debug.DrawRay(leftWallCollision, lightPosition - leftWallCollision);
		}
		if (HitRightWall())
		{
			rightBlocked = true;
			Debug.DrawRay(rightWallCollision, lightPosition - rightWallCollision, Color.red);
		}
		else
		{
			rightBlocked = false;
			Debug.DrawRay(rightWallCollision, lightPosition - rightWallCollision);
		}


		if (HitHead())
		{
			upBlocked = true;
			Debug.DrawRay(headCollision, lightPosition - headCollision, Color.red);
		}else
		{
			upBlocked = false;
			Debug.DrawRay(headCollision, lightPosition - headCollision);
		}

	}


	private bool HitHead()
	{
		return (Physics.Raycast(headCollision, lightPosition - headCollision));
	}

	private bool HitLeftWall()
	{
		return (Physics.Raycast(leftWallCollision, lightPosition - leftWallCollision));
	}

	private bool HitRightWall()
	{
		return (Physics.Raycast(rightWallCollision, lightPosition - rightWallCollision));
	}

	private bool isPlayerGrounded() {
        return (Physics.Raycast(playerPosition, lightPosition - playerPosition));
    }
}