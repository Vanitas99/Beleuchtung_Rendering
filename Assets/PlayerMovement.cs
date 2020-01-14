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
		
		move(dir, false, jump);
        returnToSurface();
    }

    private void returnToSurface() 
    {
        if(returning)
        {
            transform.position += Vector3.up * 0.05f;
		}
    }


    private void checkIfGrounded() {
        if(hitDetection.isGrounded) {
            grounded = true;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
            Physics.gravity = Vector3.zero;
			doubleJump = false;
        } else {
            grounded = false;
            Physics.gravity = new Vector3(0,-10f,0);
        }
    }

    private void move(float dir, bool crouch, bool jump)
    {
		Debug.Log(dir);
		// if we move left
		if (dir < 0)
		{
			// if there is no wall we can move 
			if (!hitDetection.leftBlocked)
			{
				Vector3 targetVelocity = new Vector3(dir * moveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
				rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_SmoothStep);
			} else
			{
				//rigidbody.velocity = new Vector3(0f,rigidbody.velocity.y, rigidbody.velocity.z);
			}
		} else if (dir > 0)
		{
			if (!hitDetection.rightBlocked)
			{
				Vector3 targetVelocity = new Vector3(dir * moveSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
				rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_SmoothStep);
			} else
			{
				//rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);

			}

		}


   //     if (jump) {
			//if (grounded)
			//	rigidbody.AddForce(new Vector3(0,jumpForce,0));
   //     }
    }

	void Jump()
	{
		if (grounded)
			rigidbody.AddForce(new Vector3(0, jumpForce, 0));
		else
		{
			if (!doubleJump)
			{
				doubleJump = true;
				rigidbody.AddForce(new Vector3(0, jumpForce*1.5f, 0));
			}
		}
	}

    private void handleInput() 
    {
        dir = Input.GetAxis("Horizontal");
		
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
			Jump();
			Debug.Log("Jump pressed");
        } else {
            jump = false;
        }
    }
}
