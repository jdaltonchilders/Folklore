using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SallyManager : MonoBehaviour {
	public float moveSpeed;
	public int health;
	public GameObject leftFireBall, rightFireBall, upFireBall, downFireBall;
	public GameObject lightningBolt;
	public float fireBallDelay, lightningBoltDelay, lightningBoltCooldown;
	private Animator anim;

	public bool isDead;
	private bool fireBallWaitActive, lightningBoltWaitActive;
	private bool castFireBall;
	private bool castLightningBolt;
	private bool sallyMoving, sallyAttacking;
	private Vector2 lastMove;
	bool facingRight, facingLeft, facingUp, facingDown;
	Transform downPos, leftPos, rightPos, upPos;
	Vector3 lightningDownPosition, lightningLeftPosition, lightningRightPosition, lightningUpPosition;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		downPos = transform.FindChild ("downPos");
		upPos = transform.FindChild ("upPos");
		leftPos = transform.FindChild ("leftPos");
		rightPos = transform.FindChild ("rightPos");
		lightningDownPosition = new Vector3 (0, -1, 0);
		lightningLeftPosition = new Vector3 (-1, 0, 0);
		lightningRightPosition = new Vector3 (1, 0, 0);
		lightningUpPosition = new Vector3 (0, 1, 0);
		facingDown = true;
		castFireBall = true;
		castLightningBolt = true;
		isDead = false;
	}

	// Update is called once per frame
	void Update () {
		sallyMoving = false;
		sallyAttacking = false;

		if ((!lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw("Horizontal") > 0.5f) || (!lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw("Horizontal") < -0.5f ))
		{
			transform.Translate (new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f ));
			sallyMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			if (lastMove.x < 0) {
				facingLeft = true;
				facingDown = false;
				facingUp = false;
				facingRight = false;
			}
			if (lastMove.x > 0) {
				facingRight = true;
				facingDown = false;
				facingUp = false;
				facingLeft = false;
			}

		}
		if ((!lightningBoltWaitActive && !fireBallWaitActive &&  Input.GetAxisRaw("Vertical") > 0.5f) || (!lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw("Vertical") < -0.5f))
		{
			transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
			sallyMoving = true;
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			if (lastMove.y < 0) {
				facingLeft = false;
				facingRight = false;
				facingDown = true;
				facingUp = false;

			}
			if (lastMove.y > 0) {
				facingLeft = false;
				facingRight = false;
				facingUp = true;
				facingDown = false;
			}

		}

		if(!fireBallWaitActive && Input.GetKeyDown(KeyCode.Q)){
		    FireBall ();
		}

		if (!lightningBoltWaitActive && Input.GetKeyDown (KeyCode.E)) {
			LightningBolt ();
		}
			

		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("SallyMoving", sallyMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
		anim.SetBool ("SallyAttacking", sallyAttacking);
	
	}


	//Collisions
	void OnCollisionEnter2D (Collision2D col)
	{
		// Damage from the skeleton spell
		if (col.gameObject.tag == "Skeleton Spell")
		{
			Destroy (col.gameObject);
			health -= 20;
			//Debug.Log ("Hit from spell!");
		}

		if (col.gameObject.tag == "zombie1")
		{
			health -= 10;
			Debug.Log ("Hit from zombie!");
		}

		if (health <= 0)
		{
			Destroy (gameObject);
			isDead = true;
			Application.LoadLevel (2);
		}

	}







	//Spells
	void FireBall(){
		if (castFireBall && facingRight) {
			Instantiate (rightFireBall, rightPos.position, Quaternion.identity);
			if (!fireBallWaitActive) {
				StartCoroutine (FireBallWait ());
			}
		}
		if (castFireBall && facingLeft) {
			Instantiate (leftFireBall, leftPos.position, Quaternion.identity);
			if (!fireBallWaitActive) {
				StartCoroutine (FireBallWait ());
			}
		}
		if (castFireBall && facingUp) {
			Instantiate (upFireBall, upPos.position, Quaternion.identity);
			if (!fireBallWaitActive) {
				StartCoroutine (FireBallWait ());
			}
		}if (castFireBall && facingDown) {
			Instantiate (downFireBall, downPos.position, Quaternion.identity);
			if (!fireBallWaitActive) {
				StartCoroutine (FireBallWait ());
			}
		}
	}

	void LightningBolt(){
		if (castLightningBolt && facingRight) {
			Instantiate (lightningBolt, rightPos.position + lightningRightPosition, Quaternion.identity);
			if (!lightningBoltWaitActive) {
				StartCoroutine (LightningBoltWait ());
				StartCoroutine (LightningBoltCooldown ());
			}
		}
		if (castLightningBolt && facingLeft) {
			Instantiate (lightningBolt, leftPos.position + lightningLeftPosition, Quaternion.identity);
			if (!lightningBoltWaitActive) {
				StartCoroutine (LightningBoltWait ());
				StartCoroutine (LightningBoltCooldown ());
			}
		}
		if (castLightningBolt && facingUp) {
			Instantiate (lightningBolt, upPos.position + lightningUpPosition, Quaternion.identity);
			if (!lightningBoltWaitActive) {
				StartCoroutine (LightningBoltWait ());
				StartCoroutine (LightningBoltCooldown ());
			}
		}if (castLightningBolt && facingDown) {
			Instantiate (lightningBolt, downPos.position + lightningDownPosition, Quaternion.identity);
			if (!lightningBoltWaitActive) {
				StartCoroutine (LightningBoltWait ());
				StartCoroutine (LightningBoltCooldown ());
			}
		}
	}

	//Delay between spell cast. Also, stops movement to allow vulrenability for the mage. She cannot move and cast.
	IEnumerator FireBallWait(){
		fireBallWaitActive = true;
		yield return new WaitForSeconds (fireBallDelay);
		fireBallWaitActive = false;
    }

	IEnumerator LightningBoltWait(){
		lightningBoltWaitActive = true;
		yield return new WaitForSeconds (lightningBoltDelay);
		lightningBoltWaitActive = false;
	}

	IEnumerator LightningBoltCooldown(){
		castLightningBolt = false;
		yield return new WaitForSeconds (lightningBoltCooldown);
		castLightningBolt = true;
	}
}


