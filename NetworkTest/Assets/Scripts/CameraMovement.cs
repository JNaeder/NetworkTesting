using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float speed;
    Vector3 diff;

	// Use this for initialization
	void Start () {
        //diff = transform.position - player.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (player != null)
        {
            
            Vector3 transPos = transform.position;
            transPos = Vector3.Lerp(transPos, player.position + diff, speed * Time.deltaTime);
            transform.position = transPos;
        }
	}

    public void SetPlayer(Transform trans) {
        diff = new Vector3(0,0,-10) - trans.position;
        player = trans;
    }
}
