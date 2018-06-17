using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour {


	public PlayableDirector pD;

	public PlayableAsset cutscene;


	bool hasBeenTriggered;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (!hasBeenTriggered)
		{
			pD.playableAsset = cutscene;
			pD.Play();
			hasBeenTriggered = true;
		}
	}
}
