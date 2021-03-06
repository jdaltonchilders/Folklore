using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SallyManager : MonoBehaviour {
	public float moveSpeed;
	public int health;
	public GameObject leftFireBall, rightFireBall, upFireBall, downFireBall;
	public GameObject lightningBolt;
    public GameObject explosion;
	public float fireBallDelay, lightningBoltDelay, lightningBoltCooldown, explosionDelay, explosionCooldown;
    public Image fireBallImage, lightningBoltImage, explosionImage;

	private Rigidbody2D myRigidbody;
    private Animator anim;
    private float lightningBoltCurrentCooldown, explosionCurrentCooldown;
	private bool fireBallWaitActive, lightningBoltWaitActive, explosionWaitActive;
	private bool castFireBall, castLightningBolt, castExplosion;
	private bool sallyMoving, sallyAttacking;
	public Vector2 lastMove;
	bool facingRight, facingLeft, facingUp, facingDown;
	Transform downPos, leftPos, rightPos, upPos;
	Vector3 lightningDownPosition, lightningLeftPosition, lightningRightPosition, lightningUpPosition;

	public AudioSource punchSound;
	public AudioSource soundFireBall;
	public AudioSource soundLightning;
	public AudioSource soundExplosion;
	private static bool playerExists;
	void Awake()
	{
		punchSound = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
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
        castExplosion = true;

		if (!playerExists)
		{
			playerExists = true;
			DontDestroyOnLoad (transform.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		sallyMoving = false;
		sallyAttacking = false;

		if ((!explosionWaitActive && !lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw ("Horizontal") > 0.5f) || (!explosionWaitActive && !lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw ("Horizontal") < -0.5f))
		{
			transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
			sallyMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			if (lastMove.x < 0)
			{
				facingLeft = true;
				facingDown = false;
				facingUp = false;
				facingRight = false;
			}
			if (lastMove.x > 0)
			{
				facingRight = true;
				facingDown = false;
				facingUp = false;
				facingLeft = false;
			}

		}
		else
		{
			myRigidbody.velocity = Vector3.zero;
		}

		if ((!explosionWaitActive && !lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw ("Vertical") > 0.5f) || (!explosionWaitActive && !lightningBoltWaitActive && !fireBallWaitActive && Input.GetAxisRaw ("Vertical") < -0.5f))
		{
			transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));
			sallyMoving = true;
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			if (lastMove.y < 0)
			{
				facingLeft = false;
				facingRight = false;
				facingDown = true;
				facingUp = false;

			}
			if (lastMove.y > 0)
			{
				facingLeft = false;
				facingRight = false;
				facingUp = true;
				facingDown = false;
			}

		}
		else
		{
			myRigidbody.velocity = Vector3.zero;
		}
        
		if(!fireBallWaitActive && Input.GetKeyDown(KeyCode.Q)){
		    FireBall ();
		}

		if (!lightningBoltWaitActive && Input.GetKeyDown (KeyCode.E)) {
			LightningBolt ();
		}
      
        if(!explosionWaitActive && Input.GetKeyDown(KeyCode.R))
        {
            Explosion();
        }
        if(!castLightningBolt && (lightningBoltCurrentCooldown < lightningBoltCooldown))
        {
            lightningBoltCurrentCooldown += Time.deltaTime;
            lightningBoltImage.fillAmount = lightningBoltCurrentCooldown / lightningBoltCooldown;
        }

        if (!castExplosion && (explosionCurrentCooldown < explosionCooldown))
        {
            explosionCurrentCooldown += Time.deltaTime;
            explosionImage.fillAmount = explosionCurrentCooldown / explosionCooldown;
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
		if (col.gameObject.tag == "zombie1") {
			health -= 10;
			Debug.Log ("Hit from normal zombie!");
			punchSound.Play ();
		}

		if (col.gameObject.tag == "zombie2") {
			health -= 15;
			Debug.Log ("Hit from fire zombie!");
			punchSound.Play ();
		}

		if (col.gameObject.tag == "Skeleton Spell") {
			health -= 20;
			Debug.Log ("Hit from spell!");
			punchSound.Play ();
		}

		if (col.gameObject.tag == "Boss Spell") {
			health -= 50;
			Debug.Log ("Hit from BOSS!");
			punchSound.Play ();
		}

        if (col.gameObject.tag == "Explosion")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }

		if (health <= 0) {
			Destroy (gameObject);
			SceneManager.LoadScene ("Dead");
		}
    }

	void OnGUI()
	{
		GUI.color = Color.red;
		GUI.Label (new Rect (20, 20, 200, 20), "Health = " + health);
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

    void Explosion()
    {
        if (castExplosion)
        {
            Instantiate(explosion, downPos.position, Quaternion.identity);
            if (!explosionWaitActive)
            {
                StartCoroutine(ExplosionWait());
                StartCoroutine(ExplosionCooldown());
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
        lightningBoltCurrentCooldown = 0;
        yield return new WaitForSeconds (lightningBoltCooldown);
		castLightningBolt = true;
	}

    IEnumerator ExplosionWait()
    {
        explosionWaitActive = true;
        yield return new WaitForSeconds(explosionDelay);
        explosionWaitActive = false;
    }

    IEnumerator ExplosionCooldown()
    {
        castExplosion = false;
        explosionCurrentCooldown = 0;
        yield return new WaitForSeconds(explosionCooldown);
        castExplosion = true;
    }

}


