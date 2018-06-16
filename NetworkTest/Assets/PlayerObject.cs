using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject playerUnit;

	// Use this for initialization
	void Start () {
		if(isLocalPlayer == false){
			return;
		}

		CmdSpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	[Command]
	void CmdSpawnPlayer(){
		GameObject gO = Instantiate(playerUnit);
		NetworkServer.SpawnWithClientAuthority(gO, connectionToClient);
	}
}
