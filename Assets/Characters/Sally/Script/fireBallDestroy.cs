using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallDestroy : MonoBehaviour {

	public float spellDelay;
	GameObject gobject;
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, spellDelay);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy (gobject);
	}
}
