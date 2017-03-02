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

	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		health = 100;
	}

	void Update()
	{
		Player = GameObject.Find ("Player").transform.position;
		if (anim.GetBool ("isDead") == true)
		{
			return;
		}
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
		if (col.gameObject.tag == "Player")
		{
			health -= 20;
			Debug.Log ("Hit");
			if (health <= 0)
			{
				anim.SetBool ("isDead", true);
			}
		}
	}
}