using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    HitDetection hitDetection;
    Rigidbody rigidbody;

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
    }

    // Update is called once per frame
    void Update()
    {

		handleInput();
		
	}

    private void FixedUpdate() 
    {
        checkIfGrounded();
		
		move(dir, false);
        returnToSurface();
    }

    private void returnToSurface() 
    {
        if(returning && currentMovState != MovementStates.Falling)
        {
            transform.position += Vector3.up * 0.02f;
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
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
		}

		rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_SmoothStep);
	}

	void Jump()
	{
		if (currentMovState == MovementStates.Grounded)
			rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		else
		{
			if (!doubleJump)
			{
				doubleJump = true;
				// second jump is slightly more powerfull
				rigidbody.AddForce(new Vector3(0, jumpForce*1.5f, 0));
			}
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "TrapStar")
		{
			Destroy(gameObject);
		}
	}
}
