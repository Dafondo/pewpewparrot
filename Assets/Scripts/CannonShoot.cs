using UnityEngine;
using System.Collections;

public class CannonShoot : MonoBehaviour {

	public GameObject parrotPrefab;
	public GameObject bulletTrailPrefab;
	public Transform muzzleFlashPrefab;

	public float power;
	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public float effectSpawnRate = 10;

	private float timeToFire = 0;
	private float timeToSpawnEffect = 0;

	private Transform cannonEnd;

    //Handle camera shaking
    public float camShakeAmt = 0.1f;
    CameraShake camShake;

	// Use this for initialization
	void Awake () {
		cannonEnd = transform.FindChild ("cannonEnd");
		if (cannonEnd == null) {
			Debug.LogError ("No cannonEnd");
		}
	}
	
    void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
    }

	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
			}
		}
	}

	void Shoot () {
		//Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		//Vector2 firePointPos = new Vector2 (cannonEnd.position.x, cannonEnd.position.y);
		//RaycastHit2D hit = Physics2D.Raycast (firePointPos, mousePos - firePointPos, 100, whatToHit);

		/*
		if(Time.time >= timeToSpawnEffect) {	
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}
		*/
			

		if (transform.eulerAngles.z > 90f && transform.eulerAngles.z <= 270f) {
			//Debug.Log ("yes!");
			GameObject parrotInstance = Instantiate (parrotPrefab, cannonEnd.position, cannonEnd.rotation) as GameObject;
			Vector3 flipVector = parrotInstance.transform.localScale;
			flipVector.y *= -1;
			parrotInstance.transform.localScale = flipVector;
			//parrotInstance.GetComponent<Rigidbody2D>().AddForce (cannonEnd.forward * power, ForceMode2D.Impulse);
		} 
		else {
			//Debug.Log ("no");
			GameObject parrotInstance = Instantiate (parrotPrefab, cannonEnd.position, cannonEnd.rotation) as GameObject;
			//parrotInstance.GetComponent<Rigidbody2D> ().AddForce (cannonEnd.forward * power, ForceMode2D.Impulse);
		}


        //parrotInstance.GetComponent<Transform>().parent = cannonEnd;
        //parrotInstance.GetComponent<Rigidbody2D> ().AddForce (cannonEnd.forward * power);
        camShake.Shake(camShakeAmt, 0.2f);

	}

	void Effect () {
		//Instantiate (bulletTrailPrefab, cannonEnd.position, cannonEnd.rotation);
		Transform muzzleFlashInstance = Instantiate (muzzleFlashPrefab, cannonEnd.position, cannonEnd.rotation) as Transform;
		muzzleFlashInstance.parent = cannonEnd;
		//float size = Random.Range (0.6f, 0.9f);
		//muzzleFlashInstance.localScale = new Vector3 (size, size, size);
		Destroy (muzzleFlashInstance.gameObject, 0.1f);
	}
}
