using UnityEngine;
using System.Collections;

public class PlayerMechanics : MonoBehaviour
{

	private Rigidbody2D    body2d;
	public bool            canMove;
	public bool            up;
	public bool            down;
	public bool            right;
	public bool            left;
	public bool            suicide;
//	private bool rD;
//	private bool lD;
	private bool upD;
	private bool downD;
	public bool            deathWheel;

//	public float time;

	public RectTransform[] dWDeaths;

	public GameObject      deathPanel;
	public RectTransform   cursor;
	public Animator        cursorAnimator;

	public bool            speedBuff;
	public bool            speedBuff2Right;
	public bool            speedBuff2Left;
	public float           speedBuffValue;
	public float           speedBuffTimer = 10;
	public float           speedBuffTimerReset;

	public Transform       checkpoint;
	public GameObject      deathResource0; 

	public float           speed = 10f;
	public float           tmpSpeed;
	public float           velX;
	public float           jumpSpeed = 10;
	public bool            canJump;
	public float           jumpDelay = 0.1f;
	private float          lastJumpTime = 0;
	public int             jumpCount = 1;
	public int             jumpReset;
	public bool            standing;
	public Vector2         bottomPosition;
	public float           collisionRadius;
	public LayerMask       collisionLayer;


	public void Start()
	{
		body2d = GetComponent<Rigidbody2D>();
		jumpReset = jumpCount;
		speedBuffTimerReset = speedBuffTimer;
	}

	public void Update()
	{
		up         = Input.GetKeyDown(KeyCode.W);
		down       = Input.GetKeyDown(KeyCode.S);
		right      = Input.GetKey(KeyCode.D);
		left       = Input.GetKey(KeyCode.A);
		deathWheel = Input.GetKey(KeyCode.Space);
		suicide    = Input.GetKeyUp(KeyCode.Space);
//		rD = Input.GetKeyDown (KeyCode.D);
//		lD = Input.GetKeyDown (KeyCode.A);
		upD        = Input.GetKey(KeyCode.W);
		downD      = Input.GetKey(KeyCode.S);

		if (canMove)
		{
			Time.timeScale = 1;
			if (right)
			{
				tmpSpeed = speed;
				velX = tmpSpeed * 1;

				if (speedBuff)
				{
//					velX *= Mathf.Lerp (1, speedBuffValue, Time.deltaTime * speedBuffTimer);
					speedBuff2Right = true;
				}
				transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z); 
				body2d.velocity = new Vector2 (velX, body2d.velocity.y);
			}
			if (left)
			{
				tmpSpeed = speed;
				velX = tmpSpeed * -1;

				if (speedBuff)
				{
//					velX *= Mathf.Lerp (1, speedBuffValue, Time.deltaTime * speedBuffTimer);
//					velX = -8 * Mathf.Lerp (1, speedBuffValue, Time.deltaTime * speedBuffTimer);
					speedBuff2Left = true;
				}
				transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
				body2d.velocity = new Vector2 (velX, body2d.velocity.y);
			}
			if (!right && !left)
			{
				tmpSpeed = 0;
				body2d.velocity = new Vector2(0, body2d.velocity.y);
			}
			if (standing)
			{
//				if (suicide)
//				{
//					SuicideMechanics();
//				}
				if (!speedBuff)
				{
					jumpCount = jumpReset;
				}
			}

			if (jumpCount > 0)
			{
				canJump = true;
			}
			else if (jumpCount == 0)
			{
				canJump = false;
			}

			if (canJump)
			{
				if (up && Time.time - lastJumpTime > jumpDelay)
				{
					jumpCount --;
					lastJumpTime = Time.time;
					body2d.velocity = new Vector2(body2d.velocity.x, jumpSpeed);
				}
			}
		}

		if (deathWheel)
		{
			canMove = false;
			tmpSpeed = velX = 0;
			body2d.velocity = new Vector2(0, body2d.velocity.y);
//			velX *= Mathf.Lerp (1, 0, Time.deltaTime * 2);
//			body2d.velocity = new Vector2 (velX, body2d.velocity.y);
			Time.timeScale = .15f;
//			body2d.isKinematic = true;
			deathPanel.SetActive (true);

			if (upD)
			{
				// morte plataforma
//				cursorAnimator.
				cursor.position = dWDeaths[1].position;
			}
			else if (downD)
			{
				// morte peso
				cursor.position = dWDeaths[2].position;
			}
			else if (left)
			{
				// morte grude
				cursor.position = dWDeaths[3].position;
			}
			else if (right)
			{
				// morte impulso diagonal
				cursor.position = dWDeaths[4].position;
			}
			else
			{
				// morte defô
				cursor.position = dWDeaths[0].position;
//				Debug.Log(Time.deltaTime * 2);
//				if (suicide)
//					SuicideMechanics();
			}
		}
		else
		{
			body2d.isKinematic = false;
			canMove = true;
			deathPanel.SetActive (false);

			if (suicide)
			{
				if (standing && cursor.position == dWDeaths[0].position)
				{
					SuicideMechanics(0);
				}
				else
				{
					DeathMechanics();
				}
			}
		}

		Vector2 pos = bottomPosition;
		pos.x      += transform.position.x;
		pos.y      += transform.position.y;

		standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

	}

	public void FixedUpdate()
	{
		if (speedBuff)
		{
			jumpCount = 0;
			canJump = false;
			speedBuffTimer --;
		}
		if (speedBuff2Right)
		{
			speedBuff2Left = false;
			velX = speed * Mathf.Lerp (1, speedBuffValue, Time.deltaTime * speedBuffTimer);
//			velX = speedBuffValue;
			body2d.velocity = new Vector2 (velX, body2d.velocity.y);
		}
		if (speedBuff2Left)
		{
			speedBuff2Right = false;
			velX = -speed * Mathf.Lerp (1, speedBuffValue, Time.deltaTime * speedBuffTimer);
			body2d.velocity = new Vector2 (velX, body2d.velocity.y);
		}
		if (speedBuffTimer <= 0)
		{
			speedBuff = speedBuff2Right = speedBuff2Left =  false;
			speedBuffTimer = speedBuffTimerReset;
		}
	}

	public void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "FallZone")
		{
			DeathMechanics();
		}
	}

	public void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.tag == "SpeedBuff")
		{
			speedBuff = true;
			speedBuffTimer = speedBuffTimerReset;
		}
	}

	public void SuicideMechanics(int death)
	{
		if (death == 0)
		{
			var deathStuff = Instantiate(deathResource0);
			deathStuff.transform.position = new Vector2(transform.position.x, transform.position.y - .5f);
			transform.position = checkpoint.position;
		}
	}

	public void DeathMechanics()
	{
		transform.position = checkpoint.position;
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Vector2 pos = bottomPosition;
		pos.x  += transform.position.x;
		pos.y  += transform.position.y;

		Gizmos.DrawWireSphere(pos, collisionRadius);
	}
}