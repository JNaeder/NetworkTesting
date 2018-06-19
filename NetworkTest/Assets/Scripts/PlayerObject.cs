using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour {

	public GameObject playerUnit;
	public GameObject startScreen;

	[SyncVar]
    public string playerName = "Blank";

    CameraMovement camMove;
    GameObject gO;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer == false){
			return;
		}

        camMove = Camera.main.GetComponent<CameraMovement>();


		string n = "Dr_Monkfish" + Random.Range(1, 100);
		playerName = n;
		gameObject.name = playerName;
		CmdChangeName(playerName);
        
        
    }

    
	public void SpawnPlayerGuy(){
		if (isLocalPlayer)
		{
			CmdSpawnPlayer(playerName);
			CmdSayStartScreen(false, gameObject.name);
			startScreen.SetActive(false);
		}

	}

	[Command]
	void CmdSayStartScreen(bool status, string newName){
		Debug.Log(newName + " Start Screen is " + status);

	}


	[Command]
	public void CmdSpawnPlayer(string n){
        playerName = n;

		gO = Instantiate(playerUnit, transform.position, Quaternion.identity);

		NetworkServer.SpawnWithClientAuthority(gO, connectionToClient);


        GuyController guyCont = gO.GetComponent<GuyController>();

		//Debug.Log("Test");
		gO.name = playerName + "'s Guy";
		guyCont.playerName = playerName;
		guyCont.playerObject = this;
		RpcAssignPlayerObjects(gO);
        RpcAssignObject(gO);
		RpcAssignPlayerNames(playerName + "'s Guy", gO);


    }

	[Command]
	public void CmdChangeName(string name){

		gameObject.name = name;
		RpcChangeName(name);
	}

	[ClientRpc]
	public void RpcChangeName(string name){
		gameObject.name = name;

	}

	[ClientRpc]
	void RpcAssignPlayerObjects(GameObject newGo){
		GuyController guyCont = newGo.GetComponent<GuyController>();
		guyCont.playerObject = this;
	}

	[ClientRpc]
	void RpcAssignPlayerNames(string newName, GameObject newGO){
		newGO.name = newName;

	}

    [ClientRpc]
    void RpcAssignObject(GameObject newPlayerObject) {
        if (hasAuthority)
		{
			Vector3 camPos = new Vector3(newPlayerObject.transform.position.x, newPlayerObject.transform.position.y, -10);
			    camMove.SetPlayer(newPlayerObject.transform,camPos);
        }
    }
    


	public void PlayerGuyDeath(){
		Debug.Log("Restart Start Screen!");
		startScreen.SetActive(true);
		CmdSayStartScreen(true, gameObject.name);


	}


    


    
}
