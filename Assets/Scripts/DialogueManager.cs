using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	private Queue<string> sentences;
	private Queue<string> names;
	public Text nameTextArea;
	public Text dialogueTextArea;
	public GameObject dialogueBoxPanel;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();	
		names = new Queue<string> ();
	}


	public void StartDialogue(Dialogue d){
		GameObject.Find ("Main Camera").GetComponent<MouseClick>().canIPut = false;
		sentences.Clear ();
		names.Clear ();

		foreach (string sentence in d.senteces) {
			sentences.Enqueue (sentence);
		}

		foreach (string name in d.names) {
			names.Enqueue (name);
		}

		dialogueBoxPanel.SetActive (true);
		DisplayNextSentence ();
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndOfDialogue ();
			return;
		}

		string tmpsent = sentences.Dequeue ();
		string tmpnam = names.Dequeue ();
		dialogueTextArea.text = tmpsent;
		nameTextArea.text = tmpnam;

	}

	private void EndOfDialogue(){
		dialogueBoxPanel.SetActive (false);
		dialogueTextArea.text = "";
		nameTextArea.text = "";
		GameObject.Find ("Main Camera").GetComponent<MouseClick>().canIPut = true;
	}
}
