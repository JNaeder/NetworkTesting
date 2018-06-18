using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GuySetup : NetworkBehaviour {

	public Behaviour[] thingstoDisable;

   

	// Use this for initialization
	void Start () {
		if(!hasAuthority){
			Debug.Log("Turn off " + gameObject.name);
			foreach (Behaviour b  in thingstoDisable){
				b.enabled = false;  
			}

		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
