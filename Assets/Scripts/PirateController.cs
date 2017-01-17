using UnityEngine;
using System.Collections;

public class PirateController : MonoBehaviour {

	public PlayerScript playerScript;
	private Animator anim;
	private bool jump = false;

	void Start () {
		anim = GetComponent<Animator> ();
		playerScript = GetComponent<PlayerScript> ();
	}

	void Update() {
		if (!jump) {
			jump = Input.GetButtonDown ("Jump");
		}
	}
		
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		playerScript.Move (moveHorizontal, jump);
		jump = false;
	}
}