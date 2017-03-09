using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

	public Button startText;
	public Button exitText;
	public Button controlText;

	// Use this for initialization
	void Start () 
	{
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		controlText = controlText.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	public void StartLevel()
	{
		Application.LoadLevel (1);
	}

	public void Controls()
	{
		Application.LoadLevel (3);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
