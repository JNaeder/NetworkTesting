using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]


public class Tiling : MonoBehaviour {

	public int offsetX = 2;

	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	private void Awake()
	{
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRend = GetComponent<SpriteRenderer>();
		spriteWidth = sRend.sprite.bounds.size.x;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(hasALeftBuddy == false || hasARightBuddy == false){
			float camHorExtent = cam.orthographicSize * Screen.width / Screen.height;

			float edgeVisRight = (myTransform.position.x + spriteWidth / 2) - camHorExtent;
			float edgeVisLeft = (myTransform.position.x - spriteWidth / 2) + camHorExtent;

			if(cam.transform.position.x >= edgeVisRight - offsetX && hasARightBuddy == false){
				MakeNewBuddy(1);
				hasARightBuddy = true;

			} else if(cam.transform.position.x <= edgeVisLeft + offsetX && hasALeftBuddy == false){
				MakeNewBuddy(-1);
				hasALeftBuddy = true;

			}
            
		}
	}



	void MakeNewBuddy(int rightOfLeft){

		Vector3 newPos = new Vector3(myTransform.position.x + spriteWidth * rightOfLeft, myTransform.position.y, myTransform.position.z);

		Transform newBuddy = Instantiate(myTransform, newPos, myTransform.rotation) as Transform;

		if(reverseScale){
			newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);         
		}

		newBuddy.parent = myTransform.parent;

		if(rightOfLeft > 0){
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;

		} else if(rightOfLeft < 0){
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
