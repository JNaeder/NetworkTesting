using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject playerUnit;

    [SyncVar]
    public string playerName = "Blank";


    Vector2 spawnPos;
    BallSpawner ballSpawner;

	// Use this for initialization
	void Start () {
        ballSpawner = FindObjectOfType<BallSpawner>();
        if (isLocalPlayer == false){
			return;
		}
        
        string n = "Dr_Monkfish" + Random.Range(1, 100);
        playerName = n;
        Debug.Log("I am " + playerName);
        CmdSpawnPlayer(playerName);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer){
            return;
        }



        if (Input.GetKeyDown(KeyCode.P)) {

            CmdSpawnBalls();
            

        }
		


	}
    


	[Command]
	void CmdSpawnPlayer(string n){
        playerName = n;
        Debug.Log(playerName + " has entered Server");
        spawnPos = new Vector2(Random.Range(-7,7), 0);
        Color randColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GameObject gO = Instantiate(playerUnit, spawnPos, Quaternion.identity);
        PlayerControl pC = gO.GetComponent<PlayerControl>();
        pC.startColor = randColor;
        pC.playerName = playerName;
        NetworkServer.SpawnWithClientAuthority(gO, connectionToClient);
        
    }


    [Command]
    void CmdSpawnBalls() {

        ballSpawner.SpawnRandomBall();
        print("SpawnBalls");
    }


    
}
