    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;

	private Transform cam;
	private Vector3 previousCamPos;


	private void Awake()
	{
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++){
			parallaxScales[i] = backgrounds[i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < backgrounds.Length; i++){
			float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;

			float parallaxY = ((previousCamPos.y - cam.position.y)) * parallaxScales[i];

			float backGroundTargetPosY = backgrounds[i].position.y + parallaxY;

			Vector3 targetPos = new Vector3(backgroundTargetPosX, backGroundTargetPosY, backgrounds[i].position.z);


			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smoothing * Time.deltaTime);
            
		}

		previousCamPos = cam.position;



	}
}
