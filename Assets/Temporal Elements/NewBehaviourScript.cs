using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject canvas;
	public GameObject button;

	void Start () {
		GameObject newButton = Instantiate(button) as GameObject;
		newButton.transform.SetParent(canvas.transform, false);
	}
}
