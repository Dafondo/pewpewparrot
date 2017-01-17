using UnityEngine;
using System.Collections;

public class ParrotDestruction : MonoBehaviour {

	public float moveSpeed;
	public int damage;
	public Transform hitPrefab;
	public Transform nose;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 1.5f);
	}

	void FixedUpdate() {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		//GetComponent<Rigidbody2D>().AddForce(transform.forward * moveSpeed);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Enemy")) {
			EnemyScript enemy = other.GetComponent<EnemyScript> ();
			enemy.DamageEnemy (damage);
			Destroy (gameObject);
			Transform clone = Instantiate (hitPrefab, nose.position, Quaternion.identity) as Transform;
		}
	}
}
