using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour {

	public int currentDialogue = 0;
	public Dialogue[] dialogues;

	public void LaunchDialogue(){
		FindObjectOfType<DialogueManager> ().StartDialogue (this.dialogues[currentDialogue]);
	}
}
