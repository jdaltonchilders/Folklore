using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathScreen : MonoBehaviour {

	public Button exitText;

	// Use this for initialization
	void Start () 
	{
		exitText = exitText.GetComponent<Button> ();
	}
	public void ExitGame()
	{
		Application.Quit ();
	}
}
