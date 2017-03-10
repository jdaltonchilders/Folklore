using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpellGO : MonoBehaviour 
{

	public GameObject rightFireBall;
	// Use this for initialization
	void Start () 
	{
		//Invoke ("FireSpell", 1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Invoke ("FireSpell", 2f);
		rightFireBall.transform.position = transform.position;
		//Debug.Log (rightFireBall.transform.position);
	}

	public void Shoot()
	{
		Invoke ("FireSpell", 0f);
	}

	public void FireSpell()
	{
		// get player position
		GameObject Player = GameObject.FindWithTag ("Player");

		// Instantiate a spell
		GameObject spell = (GameObject)Instantiate(rightFireBall);

		// Set the initial position
		rightFireBall.transform.position = this.transform.position;

		//Debug.Log (rightFireBall.transform.position);

		// Compute the spell's direction towards player
		Vector2 direction = Player.transform.position - rightFireBall.transform.position;

		// Set bullet's direction
		spell.GetComponent<enemySpell>().SetDirection(direction);
	}
}
