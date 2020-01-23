using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region Variables
	HitDetection hitDetection;
    Rigidbody rigidbody;
	Animator animator;

	public ParticleSystem shadowDust;
	private SoundManager soundManager;

    Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private float m_SmoothStep = .01f;
    [SerializeField] private float jumpForce = 150f;
    [SerializeField] private float dashForce = 300f;

    [SerializeField] private bool jump;
    [SerializeField] private bool doubleJump;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float dashSpeed = 10f;
    public float dashCooldown = 3f;
    [SerializeField] private float dashTime = 0.5f;

    [SerializeField] private bool grounded;
	[SerializeField] private bool returning;
	[SerializeField] private bool canDash = true;
	
	[SerializeField] private bool facingRight;
	float dir;

	enum MovementStates
	{
		Grounded,
		Falling,
		Jumping,
		Dashing
	};

	MovementStates currentMovState = MovementStates.Grounded;
	MovementStates prevMovState = MovementStates.Grounded;
	#endregion

    void Start()
    {
		soundManager = SoundManager.instance;
        Physics.gravity = new Vector3(0,-10,0);
        hitDetection = GetComponent<HitDetection>();
        rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		facingRight = true;
		canDash = true;
    }

    // Update is called once per frame
    void Update()
    {

		HandleInput();
		ReturnToSurface();
		
	}

    private void FixedUpdate() 
    {
		checkIfGrounded();
		
		Move(dir, false);
		
	}

	#region CustomFunctions
	private void ReturnToSurface() 
    {
		
        if(returning)
        {
			float step = 0.045f;
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
		if (currentMovState != state)
		{
			prevMovState = currentMovState;
			currentMovState = state;

		}


		//Debug.Log("Current State:" + currentMovState);
		//Debug.Log("PRev State:" + prevMovState);
	}


    private void checkIfGrounded() {
        if(hitDetection.isGrounded) {
		
			if (currentMovState == MovementStates.Falling)
			{
				animator.SetBool("Jump", false);
				
			}
			if (currentMovState != MovementStates.Dashing)
				changeMovState(MovementStates.Grounded);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,0f,rigidbody.velocity.z);
            Physics.gravity = Vector3.zero;
			doubleJump = false;
			
		} else {
			if (rigidbody.velocity.y > 0)
			{
				if (currentMovState != MovementStates.Dashing)
					changeMovState(MovementStates.Jumping);
			}
			else if (rigidbody.velocity.y < 0f /*&& prevMovState == MovementStates.Jumping*/)
			{
				//Debug.Log("lol");
				if (currentMovState != MovementStates.Dashing )
				{
					changeMovState(MovementStates.Falling);
					//Debug.Log("Test");
				}
			}
			else
			{
				if (currentMovState != MovementStates.Dashing)
					changeMovState(MovementStates.Grounded);
			}

			Physics.gravity = new Vector3(0,-10f,0);
        }
    }

    private void Move(float dir, bool crouch)
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

		animator.SetFloat("Speed", Mathf.Abs(dir));

		if (dir > 0 && !facingRight)
		{
			FlipPlayerModel();
		}  else if (dir < 0 && facingRight)
		{
			FlipPlayerModel();
		}


		if (hitDetection.upBlocked)
		{
			
			// TODO GOTTA FIX'EM
			//	rigidbody.velocity = new Vector3(0f, 0f, 0f);
			targetVelocity = new Vector3(rigidbody.velocity.x, -rigidbody.velocity.y, rigidbody.velocity.z);
		}



		rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref m_Velocity, m_SmoothStep);
	}

	void FlipPlayerModel()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
		CreateDust();
	}

	private void CreateDust()
	{
		shadowDust.Play();
	}
	void Jump()
	{
		if ((currentMovState == MovementStates.Grounded && prevMovState == MovementStates.Falling)
			|| (currentMovState == MovementStates.Falling && prevMovState == MovementStates.Grounded) )
		{
			CreateDust();
			animator.SetBool("Jump", true);
			rigidbody.AddForce(new Vector3(0, jumpForce, 0));
			
		}
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

	void DashAbility()
	{
		if (canDash)
		{
			Debug.Log("Dash");
			StartCoroutine(Dash());
		}
	}

    private void HandleInput() 
    {
		if (GameManager.instance.currentStage == GameManager.GameStage.Playing)
		{
			dir = Input.GetAxis("Horizontal");

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
			else if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				DashAbility();

			}
		}
        

    }


	private IEnumerator Dash()
	{
		canDash = false;
		changeMovState(MovementStates.Dashing);
		if (animator.GetBool("Jump"))
			animator.SetBool("Jump", false);
		animator.SetBool("Dash", true);
		moveSpeed = dashSpeed;
		yield return new WaitForSeconds(dashTime);
		changeMovState(prevMovState);
		moveSpeed = 3f;
		animator.SetBool("Dash", false);
		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
	}
	
	public void ZeroVelocity()
	{
		rigidbody.velocity = Vector3.zero;
		dir = 0;
	}

	public Vector3 GetCurrentVelocity()
	{
		return rigidbody.velocity;
	}
	public bool IsDashOnCD()
	{
		
		return !canDash;
	}
	#endregion

	private void OnTriggerEnter(Collider other)
	{

		switch(other.tag)
		{
			case "TimerTrigger":
				GameManager.instance.StartTimer();
				
				Debug.Log("Entered Timer Area");
				break;
			case "TrapStar":
				Destroy(gameObject);
				GameManager.dead = true;
				break;
			case "Finish":
				Debug.Log("Reached Finish");
				break;
		}
	}

	#region AnimationEventCallbacks

	public void PlayStepSound()
	{
		soundManager.PlaySound("Steps");
	}

	public void PlayDashSound()
	{
		soundManager.PlaySound("Dash");
	}

	#endregion

}
