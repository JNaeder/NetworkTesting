using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    private Queue<string> sentences;

    public Text nameText;
    public Text sentenceText;

    public Animator anim;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog) {

        anim.SetBool("isOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();

        foreach (string sentence in dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }


    IEnumerator TypeSentence (string sentence) {
        sentenceText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            sentenceText.text += letter;
            yield return null;
        }
    }


    public void EndDialog() {
        anim.SetBool("isOpen", false);
        print("End Conversation");
    }
	
}
