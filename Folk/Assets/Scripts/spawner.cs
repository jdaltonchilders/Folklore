using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour 
{
	public GameObject[] enemies;
	public Vector3 spawnValues;

	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;
	public int startWait;
	public bool stop;
	private int currentEnemies = 0;
	public int maxEnemies;
	private int currentSkeletons = 1;
	public int maxSkeletons;
	private int randEnemy;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (waitSpawner());
	}
	
	// Update is called once per frame
	void Update () 
	{
		spawnWait = Random.Range (spawnLeastWait, spawnMostWait);
		if (currentEnemies >= maxEnemies)
		{
			stop = true;
		}
	}

	public void Spawn ()
	{
		if (currentSkeletons <= maxSkeletons)
		{
			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), 0);
			Instantiate (enemies [2], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);
			currentSkeletons++;
		}
	}

	IEnumerator waitSpawner()
	{
		yield return new WaitForSeconds (startWait);
		while (!stop)
		{
			randEnemy = Random.Range (0, 2);

			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 0);

			Instantiate (enemies [randEnemy], spawnPosition + transform.TransformPoint (0, 0, 0), gameObject.transform.rotation);

			currentEnemies++;

			yield return new WaitForSeconds (spawnWait);
		}
	}
}
