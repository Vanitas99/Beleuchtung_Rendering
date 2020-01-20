using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    HitDetection hitDetection;
    Rigidbody rigidbody;
	Animation animation;
	public AnimationClip walk, idle, jump;

    Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private float m_SmoothStep = .5f;
    [SerializeField] private float jumpForce = 100f;

    [SerializeField] private bool jump;
    [SerializeField] private bool doubleJump;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private bool grounded;
	[SerializeField] private bool returning;

	float dir;

	enum MovementStates
	{
		Grounded,
		Falling,
		Jumping
	};

	MovementStates currentMovState = MovementStates.Grounded;
	MovementStates prevMovState = MovementStates.Grounded;

    private void Awake() {
        //Application.targetFrameRate = 60; 
        //QualitySettings.vSyncCount = 1;   
    }

    void Start()
    {
        Physics.gravity = new Vector3(0,-10,0);
        hitDetection = GetComponent<HitDetection>();
        rigidbody = GetComponent<Rigidbody>();
		animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

		handleInput();
		returnToSurface();
	}

    private void FixedUpdate() 
    {
        checkIfGrounded();
		
		move(dir, false);
        
    }

    private void returnToSurface() 
    {
		
        if(returning)
        {
			float step = 0.02f;
			Vector3 stepVec = new Vector3(hitDetection.playerPosition.x, hitDetection.playerPosition.y + step, hitDetection.playerPosition.z) ;
			if (!Physics.Raycast(stepVec, hitDetection.lightPosition - stepVec))
			{
				Debug.DrawRay(stepVec, hitDetection.lightPosition - stepVec);
			} else
			{
				Debug.DrawRay(stepVec, hitDetection.lightPosition - stepVec,Color.red);
				transform.position += new Vector3(0f, step, 0f);
			}

            //transform.position += new Vector3(0f,0.1f,0f);
		}
    }

	private void changeMovState(MovementStates state)
	{
		if (currentMovState == state)
			return;
		prevMovState = currentMovState;
		currentMovState = state;

		Debug.Log("Previous State:" + prevMovState);
		Debug.Log("Current State:" + currentMovState);
	}


    private void checkIfGrounded() {
        if(hitDetection.isGrounded) {
			changeMovState(MovementStates.Grounded);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
            Physics.gravity = Vector3.zero;
			doubleJump = false;
        } else {
			if (rigidbody.velocity.y > 0)
				changeMovState(MovementStates.Jumping);
			else
				changeMovState(MovementStates.Falling);

			Physics.gravity = new Vector3(0,-10f,0);
        }
    }

    private void move(float dir, bool crouch)
    {
		Vector3 targetVelocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
		// if we move left
		if (dir < 0)
		{
			// if there is no wall we can move 
			if (!hitDetection.leftBlocked)
			{
				targetVelocity = new Vector3(dir * moveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
			} else
			{
				rigidbody.velocity = new Vector3(0f,rigidbody.velocity.y, rigidbody.velocity.z);
			}
		} else if (dir > 0)
		{
			if (!hitDetection.rightBlocked)
			{
				targetVelocity = new Vector3(dir * moveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
			} else
			{
			 	rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);

			}

		}

		if (hitDetection.upBlocked)
		{
			Debug.Log("TEST");
			// TODO GOTTA FIX'EM
			//	rigidbody.velocity = new Vector3(0f, 0f, 0f);
			targetVelocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
		}

		rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_SmoothStep);
	}

	void Jump()
	{
		if (currentMovState == MovementStates.Grounded)
			rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		else
		{
			//if (!doubleJump)
			//{
			//	doubleJump = true;
			//	// second jump is slightly more powerfull
			//	rigidbody.AddForce(new Vector3(0, jumpForce*1.5f, 0));
			//}
		}
	}

    private void handleInput() 
    {
        dir = Input.GetAxis("Horizontal");
		
        if (Input.GetKeyDown(KeyCode.Space)) {
			Jump();
        } else {
        }
    }

	void changeAnimation() {
		if (dir > 0) {
			animation.clip = walk;
			animation.Play();
		}
	}

	private void OnTriggerEnter(Collider other)
	{

		switch(other.tag)
		{
			case "TrapStar":
				Destroy(gameObject);
				break;
			case "Finish":
				Debug.Log("Reached Finish");
				break;
		}
	}
}
