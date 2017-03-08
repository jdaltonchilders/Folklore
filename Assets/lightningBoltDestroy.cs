using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBoltDestroy : MonoBehaviour {

	public float spellDelay;
	public float hitDelay;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, spellDelay);
	}

	void OnCollisionEnter2D (GameObject gobject, Collision2D col){
		Destroy (gobject, hitDelay);
	}
}
