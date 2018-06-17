using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour {

    public GameObject ballGO;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

     public void SpawnRandomBall() {
        
            Vector2 randomSpawnPos = new Vector2(Random.Range(-8, 8), Random.Range(0, 3));
            GameObject gO = Instantiate(ballGO, randomSpawnPos, Quaternion.identity);
            NetworkServer.Spawn(gO);
        
    }
    


    
}
