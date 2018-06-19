using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkStuff : NetworkBehaviour {

    
    public GameObject startScreen;

    PlayerObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerObject>();

    }


    public void DisableCamera() {
        startScreen.SetActive(false);

    }

    

    public void SpawnPlayer() {
        Debug.Log("Spawn!");
        player.CmdSpawnPlayer(player.playerName);

    }
    
}
