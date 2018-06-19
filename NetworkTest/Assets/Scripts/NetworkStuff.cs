using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkStuff : NetworkBehaviour {

    public Camera startCam;
    public GameObject startScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DisableCamera() {
        Debug.Log("DisableCamera");
        startCam.gameObject.SetActive(false);
        startScreen.SetActive(false);

    }

    public void EnableStartScreen() {
        startScreen.SetActive(true);

    }
    
}
