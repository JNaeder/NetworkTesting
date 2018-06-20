using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour {

	Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		spawnPoints = GetComponentsInChildren<Transform>();
	}
	

	public Vector3 GenerateRandomSpawnPoint(){

		int randNum = Random.Range(1, spawnPoints.Length);
		return spawnPoints[randNum].position;

	}
    
}
