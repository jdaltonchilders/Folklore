using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadScene ("Village");
	}

	public void Controls()
	{
		SceneManager.LoadScene ("Controls");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
