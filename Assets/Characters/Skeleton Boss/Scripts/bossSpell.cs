using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpell : MonoBehaviour {

	public float speed;
	Vector2 _direction;
	bool isReady;

	void Awake()
	{
		isReady = false;
	}

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 3);
	}

	public void SetDirection(Vector2 direction)
	{
		_direction = direction.normalized;
		isReady = true;
	}
	// Update is called once per frame
	void Update () 
	{
		if (isReady == true)
		{
			// Get the bullet's current position
			Vector2 position = transform.position;
			//Debug.Log (position);
			// Compute the bullet's new position
			position += _direction * speed * Time.deltaTime;

			// Update the bullet's current position
			transform.position = position;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "zombie1")
		{
			Destroy (gameObject);
		}
	}
}
