using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {

	private SallyManager theSally;


	public Vector2 startDirection;

	// Use this for initialization
	void Start () {
		theSally = FindObjectOfType<SallyManager> ();
		theSally.transform.position = transform.position;
		theSally.lastMove = startDirection;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
