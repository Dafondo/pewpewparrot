using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float propulsion;
	/*
	public GameObject background;
	public GameObject farback;
	*/
	public int health;
	public GameObject ground;
	public Transform groundCheck;
	private float groundedRadius = 0.2f;
	private bool grounded = false;
	private bool jump = false;
	private Rigidbody2D rb2d;
	private SpriteRenderer spriteRenderer;
	private float distToGround;
	public LayerMask whatIsGround;
	private bool invuln = false;
	public Text healthText;
	public Sprite regSprite;
	public Sprite invulnSprite;
	public float invulTime;
	private bool facingRight = true;
	private bool playerMove = false;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = regSprite;
		distToGround = GetComponent<BoxCollider2D> ().bounds.extents.y;
		//groundCheck = transform.Find("GroundCheck");
		SetHealth();
	}

	void Update() {
		if (!jump) {
			// Read the jump input in Update so button presses aren't missed.
			jump = Input.GetButtonDown ("Jump");
		}
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
	}

	// Update is called once per frame
	void FixedUpdate () {
		/*
		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded()) {
			rb2d.AddForce (Vector2.up * propulsion);
			//spriteRenderer.enabled = spriteRenderer.enabled ? false : true;
		}
		*/

		float moveHorizontal = Input.GetAxis("Horizontal");
		//float moveVertical = Input.GetAxis("Vertical");

		if (moveHorizontal == 0) {
			playerMove = false;
		} else {
			playerMove = true;
		}

		if (grounded && jump) {
			rb2d.AddForce (Vector2.up * propulsion);
			grounded = false;
		}

		jump = false;

		//Vector2 movement = new Vector2 (moveHorizontal, 0.0f);//moveVertical);
		//rb2d.AddForce(movement * speed);
		rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
		if (moveHorizontal > 0 && !facingRight) {
			Flip ();
		}
		else if(moveHorizontal < 0 && facingRight) {
			Flip();
		}
		//transform.LookAt (movement);
	}



	bool IsGrounded() {
		return Physics2D.Raycast (transform.position, Vector2.down, distToGround + 0.1f);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(!invuln && other.gameObject.CompareTag("Enemy")) {
			health--;
			SetHealth ();
			if(health <= 0) {
				Die ();
			}
			Invuln ();
			Invoke ("ClearInvuln", invulTime);
		}
	}

	void Die() {
		spriteRenderer.enabled = false;
		GetComponent<ParticleSystem>().Play();
		spriteRenderer.enabled = false;
		rb2d.isKinematic = true;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

	void Invuln() {
		invuln = true;
		spriteRenderer.sprite = invulnSprite;
	}

	void ClearInvuln () {
		invuln = false;
		spriteRenderer.sprite = regSprite;
	}

	void SetHealth() {
		healthText.text = "Health: " + health;
	}

	void Flip () {
		facingRight = !facingRight;

		Vector3 flipVector = transform.localScale;

		flipVector.x *= -1;

		transform.localScale = flipVector;
	}
}