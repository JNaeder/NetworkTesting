using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject playerUnit;

    [SyncVar]
    public string playerName = "Blank";

    CameraMovement camMove;
    GameObject gO;

    Vector2 spawnPos;

	// Use this for initialization
	void Start () {
        
        if (isLocalPlayer == false){
			return;
		}
        camMove = Camera.main.GetComponent<CameraMovement>();
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



        
		


	}
    


	[Command]
	void CmdSpawnPlayer(string n){
        playerName = n;
        Debug.Log(playerName + " has entered Server");
        gO = Instantiate(playerUnit, spawnPos, Quaternion.identity);
        GuyController guyCont = gO.GetComponent<GuyController>();
        guyCont.playerName = playerName;
        NetworkServer.SpawnWithClientAuthority(gO, connectionToClient);
        RpcAssignObject(gO);
    }

    [ClientRpc]
    void RpcAssignObject(GameObject newPlayerObject) {
        Debug.Log(gameObject.name + " added camera to this object");
        if (hasAuthority)
        {
            camMove.SetPlayer(newPlayerObject.transform);
        }
    }


    


    
}
