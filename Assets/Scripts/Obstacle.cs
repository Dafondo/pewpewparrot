using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	private SpriteRenderer spr;
	public GameObject player;

	// Use this for initialization
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		//Invoke ("setInvis", 1f);
		//VisibilityController.toggleVis += ToggleVisible;
		InvokeRepeating("ToggleVisible", 2f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetKeyDown(KeyCode.Space)) {
			spr.enabled = spr.enabled ? false : true;
		}
		if (player.GetComponent<SpriteRenderer> ().enabled == false) {
			spr.enabled = true;
		} else if (player.GetComponent<SpriteRenderer> ().enabled == true) {
			spr.enabled = false;
		}
		*/
	}
		
	void setInvis() {
		spr.enabled = false;
	}

	void ToggleVisible() {
		spr.enabled = spr.enabled ? false : true;
	}
}
