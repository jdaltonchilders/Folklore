using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallController : MonoBehaviour {

	public Vector2 fireballSpeed;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = fireballSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = fireballSpeed;
	}



}
