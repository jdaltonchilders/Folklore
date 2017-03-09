using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBoltDestroy : MonoBehaviour {

	public float spellDelay;
	public float hitDelay;
	GameObject gobject;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, spellDelay);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		Destroy (gobject, hitDelay);
	}
}
