using UnityEngine;
using System.Collections;

public class skeletonAI : MonoBehaviour
{
	private Vector3 Player;
	private Vector2 Playerdirection;
	private float xDiff;
	private float yDiff;
	private Animator anim;
	private Rigidbody2D myRigidbody;
	private float minDistance = 1.0f;
	private float maxDistance = 2.0f;
	public float speed;
	public float health;
	private bool spellWait;
	public float spellDelay = 5.0f;
	public GameObject rightFireBall;
	private Behaviour h;
	public Transform skeletonGO;
	public enemySpellGO _enemySpellGO;
	public spawner _spawner;
	private int skeletonCount = 0;
	public int skeletonMax;

	void Awake()
	{
		// Gets functions from the sript "enemySpellGO"
		_enemySpellGO = transform.FindChild ("skeletonGO").GetComponent<enemySpellGO>();
		_spawner = GameObject.Find ("Spawner").GetComponent<spawner>();
		skeletonGO = this.gameObject.transform.GetChild (0);
	}

	void Start()
	{
		
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		health = 100;
		spellWait = false;
		//skeletonGO = GameObject.FindGameObjectWithTag("skeletonGO").transform.GetChild (0);

		h = (Behaviour)GetComponent ("Halo");
		h.enabled = false;
	}


	void Update()
	{
		//skeletonGO = gameObject.transform.GetChild (0);
		//skeletonGO.position = myRigidbody.position;

		if (anim.GetBool ("isDead") == true)
		{
			return;
		}
			
		Player = GameObject.FindWithTag("Player").transform.position;
		xDiff = Player.x - transform.position.x;
		yDiff = Player.y - transform.position.y;

		Playerdirection = new Vector2 (xDiff, yDiff);
		if ((((minDistance < xDiff) || (minDistance < -xDiff)) || ((minDistance < yDiff) || (minDistance < -yDiff))) && spellWait == false)
		{
			h.enabled = false;
			anim.SetBool ("isAttacking", false);
			myRigidbody.AddForce (Playerdirection.normalized * speed);
		}
		else
		{
			if(!spellWait)
			{
				// Stop moving
				myRigidbody.velocity = Vector3.zero;

				anim.SetBool ("isAttacking", true);
				anim.SetBool ("walkRight", false);
				anim.SetBool ("walkLeft", false);
				anim.SetBool ("walkUp", false);
				anim.SetBool ("walkDown", false);

				// Enable glowing effect
				h.enabled = true;

				// Shoot spell
				Spell ();

				// Delay
				StartCoroutine (SpellWait ());
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Fireball") 
		{
			Destroy (col.gameObject);
			health -= 50;
			myRigidbody.velocity = Vector3.zero;
		}

		if (col.gameObject.tag == "LightningBolt")
		{
			Destroy (col.gameObject);
			health -= 100;
		}

		if (col.gameObject.tag == "Explosion")
		{
			Destroy (gameObject);
			health -= 10000;
		}

		if (health <= 0) 
		{
			// Sound Effect?
			anim.SetBool ("isDead", true);
			myRigidbody.velocity = Vector3.zero;
			Destroy (gameObject, 1);
			if (skeletonCount <= skeletonMax)
			{
				_spawner.Spawn ();
				skeletonCount++;
				//Debug.Log (skeletonCount);
			}
		}
	}

	void Spell()
	{
		_enemySpellGO.FireSpell ();
	}

	IEnumerator SpellWait()
	{
		spellWait = true;
		myRigidbody.velocity = Vector3.zero;
		yield return new WaitForSeconds (spellDelay);
		anim.SetBool ("isAttacking", false);
		spellWait = false;
	}
}