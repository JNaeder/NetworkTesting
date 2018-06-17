using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public Dialog dialog;
    DialogManager dM;

    // Use this for initialization
    void Start () {
        dM = FindObjectOfType<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        dM.StartDialog(dialog);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        dM.EndDialog();
    }


}
