using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuySetup : NetworkBehaviour {

	public GameObject[] thingstoDisable;

   

	// Use this for initialization
	void Start () {
		if(!hasAuthority){
			//Debug.Log("Turn off " + gameObject.name);
			foreach (GameObject g  in thingstoDisable){
				g.SetActive(false);
			}

		}

		
	}
}
