using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkStuff : NetworkBehaviour {

    
    public GameObject startScreen;
    

    private void Start()
    {

    }


    public void DisableCamera() {
        startScreen.SetActive(false);

    }

    

    public void SpawnPlayer() {
        Debug.Log("Spawn!");

    }
    
}
