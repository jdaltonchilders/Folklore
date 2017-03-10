using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour {

	private Vector3 Player;
	private Vector2 playerDirection;
	private float xDiff;
	private float yDiff;
	private Animator anim;
	private Rigidbody2D myRigidbody;
	public float speed;
	public float health;

	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//health = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Player = GameObject.FindWithTag("Player").transform.position;
		xDiff = Player.x - transform.position.x;
		yDiff = Player.y - transform.position.y;

		playerDirection = new Vector2 (xDiff, yDiff);
		myRigidbody.AddForce (playerDirection.normalized * speed);
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
			//anim.SetBool ("isDead", true);
			myRigidbody.velocity = Vector3.zero;
			Destroy (gameObject);
		}
	}
}
