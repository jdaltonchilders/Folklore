using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controls : MonoBehaviour {

	public Button returnText;

	// Use this for initialization
	void Start () 
	{
		returnText = returnText.GetComponent<Button> ();
	}

	public void Return()
	{
		Application.LoadLevel (0);
	}
}
