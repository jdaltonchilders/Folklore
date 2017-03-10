using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class deathScreen : MonoBehaviour {

	public Button exitText;

	// Use this for initialization
	void Start () 
	{
		exitText = exitText.GetComponent<Button> ();
	}
	public void ExitGame()
	{
		SceneManager.LoadScene ("Title Screen");
	}
}
