using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieAnimation : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("idle", true);
		anim.SetBool ("WalkRight", false);
		anim.SetBool ("WalkLeft", false);
		anim.SetBool ("WalkDown", false);
		anim.SetBool ("WalkUp", false);
	}

	// Update is called once per frame
	void Update () 
	{
		if ((rb.velocity.x == 0) && (rb.velocity.y == 0))
		{
			anim.SetBool ("idle", true);
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("WalkUp", false);
		}
		if ((rb.velocity.x < 0) && ((rb.velocity.x > rb.velocity.y) || (-rb.velocity.x > -rb.velocity.y)))
		{
			anim.SetBool ("WalkLeft", true);
			anim.SetBool ("idle", false);
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("WalkUp", false);
		}

		if (rb.velocity.x > 0 && ((rb.velocity.x > rb.velocity.y) || (rb.velocity.x > -rb.velocity.y)))
		{
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("idle", false);
			anim.SetBool ("WalkRight", true);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("WalkUp", false);
		}

		if (rb.velocity.y > 0 && ((rb.velocity.x < rb.velocity.y) || (rb.velocity.x < -rb.velocity.y)))
		{
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("idle", false);
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkDown", false);
			anim.SetBool ("WalkUp", true);
		}
		if (rb.velocity.y < 0 && ((rb.velocity.x < rb.velocity.y) || (rb.velocity.x < -rb.velocity.y)))
		{
			anim.SetBool ("WalkLeft", false);
			anim.SetBool ("idle", false);
			anim.SetBool ("WalkRight", false);
			anim.SetBool ("WalkDown", true);
			anim.SetBool ("WalkUp", false);
		}
	}
}