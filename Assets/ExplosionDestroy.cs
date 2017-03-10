using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

    public float spellDelay;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, spellDelay);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
