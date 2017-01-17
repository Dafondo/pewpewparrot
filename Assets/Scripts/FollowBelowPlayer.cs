using UnityEngine;
using System.Collections;

public class FollowBelowPlayer : MonoBehaviour {

	public GameObject player;
	private Vector3 displace;

	// Use this for initialization
	void Start () {
		//displace = transform.position - player.transform.position;
		//displace.y = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position + displace;	
		transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
