using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonAnimation : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("isIdle", true);
		anim.SetBool ("walkRight", false);
		anim.SetBool ("walkLeft", false);
		anim.SetBool ("walkDown", false);
		anim.SetBool ("walkUp", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((rb.velocity.x == 0) && (rb.velocity.y == 0) && (anim.GetBool("isAttacking") == false))
		{
			anim.SetBool ("isIdle", true);
			anim.SetBool ("walkRight", false);
			anim.SetBool ("walkLeft", false);
			anim.SetBool ("walkDown", false);
			anim.SetBool ("walkUp", false);
		}
		if ((rb.velocity.x < 0) && ((-rb.velocity.x > rb.velocity.y) || (-rb.velocity.x > -rb.velocity.y)))
		{
			anim.SetBool ("walkLeft", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("walkRight", false);
			anim.SetBool ("walkDown", false);
			anim.SetBool ("walkUp", false);
		}

		if (rb.velocity.x > 0 && ((rb.velocity.x > rb.velocity.y) || (rb.velocity.x > -rb.velocity.y)))
		{
			anim.SetBool ("walkLeft", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("walkRight", true);
			anim.SetBool ("walkDown", false);
			anim.SetBool ("walkUp", false);
		}

		if (rb.velocity.y > 0 && ((rb.velocity.x < rb.velocity.y) || (rb.velocity.x < -rb.velocity.y)))
		{
			anim.SetBool ("walkLeft", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("walkRight", false);
			anim.SetBool ("walkDown", false);
			anim.SetBool ("walkUp", true);
		}
		if (rb.velocity.y < 0 && ((rb.velocity.x < rb.velocity.y) || (rb.velocity.x < -rb.velocity.y)))
		{
			anim.SetBool ("walkLeft", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("walkRight", false);
			anim.SetBool ("walkDown", true);
			anim.SetBool ("walkUp", false);
		}
	}
}
