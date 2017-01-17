using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;

	Vector3 offset;
	float nextTimeToSearch = 0;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	void Update() {
		if (player == null) {
			FindPlayer ();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (player != null) {
			transform.position = player.transform.position + offset;
		}
	}

	void FindPlayer() {
		//if (nextTimeToSearch <= Time.time) {
			player = GameObject.FindGameObjectWithTag ("Player");
			if (player != null) {
				Vector3 newPosition = player.transform.position;
				newPosition.z = -25f;
				transform.position = newPosition;
				offset = transform.position - player.transform.position;
			}
			// nextTimeToSearch = Time.time + 0.5f;
		//}
	}
}
