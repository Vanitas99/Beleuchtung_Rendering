using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
	public Vector3 playerPosition;
	Vector3 leftWallCollision, rightWallCollision, headCollision, headRightWall, headLeftWall;

	public Vector3 lightPosition;
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
		playerPosition = gameObject.transform.position + Vector3.down * 0.6f;
		leftWallCollision = gameObject.transform.position + Vector3.left * 0.1f + Vector3.down * 0.5f;
		rightWallCollision = gameObject.transform.position + Vector3.right * 0.1f + Vector3.down * 0.5f;
		headCollision = gameObject.transform.position + Vector3.up * 0.6f;
		headRightWall = headCollision + Vector3.right * 0.2f + Vector3.down * 0.1f;
		headLeftWall = headCollision + Vector3.left * 0.1f + Vector3.down * 0.1f;


		if (isPlayerGrounded())
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
			Debug.DrawRay(headLeftWall, lightPosition - headLeftWall, Color.red);
		}
		else
		{
			leftBlocked = false;
			//Debug.Log("Player in Air");
			Debug.DrawRay(leftWallCollision, lightPosition - leftWallCollision);
			Debug.DrawRay(headLeftWall, lightPosition - headLeftWall);
		}
		if (HitRightWall())
		{
			rightBlocked = true;
			Debug.DrawRay(rightWallCollision, lightPosition - rightWallCollision, Color.red);
			Debug.DrawRay(headRightWall, lightPosition - headRightWall, Color.red);
		}
		else
		{
			rightBlocked = false;
			Debug.DrawRay(rightWallCollision, lightPosition - rightWallCollision);
			Debug.DrawRay(headRightWall, lightPosition - headRightWall);
		}


		if (HitHead())
		{
			upBlocked = true;
			Debug.DrawRay(headCollision, lightPosition - headCollision, Color.red);
		} else
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
		return (Physics.Raycast(leftWallCollision, lightPosition - leftWallCollision)
			|| Physics.Raycast(headLeftWall, lightPosition - headLeftWall));
	}

	private bool HitRightWall()
	{
		return (Physics.Raycast(rightWallCollision, lightPosition - rightWallCollision)
			|| Physics.Raycast(headRightWall, lightPosition - headRightWall));
	}

	private bool isPlayerGrounded() {
        return (Physics.Raycast(playerPosition, lightPosition - playerPosition));
    }
}
