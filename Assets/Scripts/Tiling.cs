using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;

	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false;

	private float spriteWidth;
	private Camera cam;
	private Transform myTransform;

	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		// does it still need buddies? If not do nothing
		if (!hasALeftBuddy || !hasARightBuddy) {
			// calculates camera's extent, or half the width, of what the cam can see in world coordinates
			float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

			// calculates the x position where cam can see the edge of sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtent;

			// cehcks if we can see edge of the element and calling MakeNewBuddy if so
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy) {
				MakeNewBuddy (1);
				hasARightBuddy = true;
			} else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy) {
				MakeNewBuddy (-1);
				hasALeftBuddy = true;
			}
		}
	}

	// makes a buddy on side required
	void MakeNewBuddy (int rightOrLeft) {
		// calculates position of new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		// insantiating our new buddy and storing him in a variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		// flips the sprite if the ends don't match up
		if (reverseScale) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling> ().hasARightBuddy = true;
		}
	}
}
