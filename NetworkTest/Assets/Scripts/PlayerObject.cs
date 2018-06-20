using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour {
    
	public GameObject playerUnit;
	public GameObject startScreen;
	public Image guyPreview;

	Color playerColor = Color.white;

	[SyncVar]
    public string playerName = "Blank";

    CameraMovement camMove;
    GameObject gO;
	SpawnPoints spawnP;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer == false){
			return;
		}

        camMove = Camera.main.GetComponent<CameraMovement>();
		spawnP = FindObjectOfType<SpawnPoints>();

		//string n = "Dr_Monkfish" + Random.Range(1, 100);
		//playerName = n;
		gameObject.name = playerName;
		CmdChangeName(playerName);
        
        
    }

    
	public void SpawnPlayerGuy(){
		if (isLocalPlayer)
		{
			CmdSpawnPlayer(playerName);
			startScreen.SetActive(false);
		}

	}



	[Command]
	public void CmdSpawnPlayer(string n){
        playerName = n;
		Vector3 spawnPos = spawnP.GenerateRandomSpawnPoint();
		gO = Instantiate(playerUnit, spawnPos, Quaternion.identity);

		NetworkServer.SpawnWithClientAuthority(gO, connectionToClient);


        GuyController guyCont = gO.GetComponent<GuyController>();

		//Debug.Log("Test");
		gO.name = gameObject.name + "'s Guy";
		guyCont.playerName = playerName;
		guyCont.playerObject = this;
		guyCont.startColor = playerColor;


		RpcAssignPlayerObjects(gO);
		RpcAssignPlayerNames(gameObject.name + "'s Guy", gO);
		RpcSetCamToObject(gO);


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


    //I Need This
    [ClientRpc]
    void RpcSetCamToObject(GameObject newPlayerObject) {
        if (hasAuthority)
		{
			Vector3 camPos = new Vector3(newPlayerObject.transform.position.x, newPlayerObject.transform.position.y, -10);
			    camMove.SetPlayer(newPlayerObject.transform,camPos);
        }
    }
    


	public void PlayerGuyDeath(){
		//Debug.Log("Restart Start Screen!");
		startScreen.SetActive(true);


	}

	public void UpdateName(string newName){
		playerName = newName;
		gameObject.name = newName;
	}



	public void UpdateColor(int num){
		if (num == 0)
		{
			playerColor = new Color(1, 1, 1, 1);
		}
		else if (num == 1)
		{
			playerColor = Color.red;
		}
		else if (num == 2)
		{
			playerColor = Color.blue;
		}
		else if (num == 3)
		{
			playerColor = Color.yellow;
		}
		else if (num == 4)
		{
			playerColor = Color.green;
		}
		else if (num == 5)
		{
			playerColor = Color.magenta;
		}



		guyPreview.color = playerColor;
	}


    


    
}
