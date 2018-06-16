using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {


	float h;

	public float speed;

	Rigidbody2D rB;

	// Use this for initialization
	void Start () {
		//rB.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hasAuthority)
		{
			Movement();
		}
	}



	void Movement(){
        h = Input.GetAxis("Horizontal");
        transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);

		//if(Input.GetButtonDown("Jump")){
		//	rB.AddForce(Vector2.up * 2);

		//}

	}
}
