using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

	public Transform target;

	//how many times each second we will update out path
	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;


	//the calculated path
	public Path path;

	//AI's speed per second
	public float speed  = 300f;
	public ForceMode2D fMode; 

	[HideInInspector]
	public bool pathIsEnded = false;

	//nax dust from AI to waypoint before it can continue to the next one
	public float nextWaypointDistance = 3;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;

	private bool searchingForPlayer = false;


	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

		//start a new path to target's position and return the result to the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}


	IEnumerator SearchForPlayer() {
		GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
		if (sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		} else {
			searchingForPlayer = false;
			target = sResult.transform;
			StartCoroutine (UpdatePath ());
			return false;
		}
	}

	IEnumerator UpdatePath() {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f/updateRate);
		StartCoroutine (UpdatePath());
	}

	public void OnPathComplete(Path p) {
		//Debug.Log ("EEE " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void Update() {
		
	}

	void FixedUpdate() {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}

			//Debug.Log ("end");
			pathIsEnded = true;
		}

		pathIsEnded = false;

		//Dirrection to next waypoint
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//move the enemy
		rb.AddForce(dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
		
}
