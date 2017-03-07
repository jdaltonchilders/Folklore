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
	private Collider2D col2d;
	private float minDistance = 1.0f;
	private float maxDistance = 2.0f;

	public float speed;
	public float health;

	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		col2d = GetComponent<Collider2D> ();
		health = 100;
	}

	void Update()
	{
		if (anim.GetBool ("isDead") == true)
		{
			return;
		}
		Player = GameObject.FindWithTag("Player").transform.position;
		xDiff = Player.x - transform.position.x;
		yDiff = Player.y - transform.position.y;

		Playerdirection = new Vector2 (xDiff, yDiff);
		if (((minDistance < xDiff) || (minDistance < -xDiff)) || ((minDistance < yDiff) || (minDistance < -yDiff)))
		{
			anim.SetBool ("isAttacking", false);
			myRigidbody.AddForce (Playerdirection.normalized * speed);
		}
		else
		{
			myRigidbody.velocity = Vector3.zero;
			anim.SetBool ("isAttacking", true);
			//myRigidbody.angularVelocity = Vector3.zero;
		}
		//Debug.Log (xDiff);
		//Debug.Log (yDiff);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Fireball") {
			Destroy (col.gameObject);
			health -= 50;
			if (health <= 0) {
				anim.SetBool ("isDead", true);
				Destroy (gameObject);
			}
		}

		if (col.gameObject.tag == "LightningBolt") {
			Destroy (col.gameObject);
			health -= 100;
			if (health <= 0) {
				anim.SetBool ("isDead", true);
				Destroy (gameObject);
			}
		}
	}
}