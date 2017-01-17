using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject ground;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float speed;
	public float propulsion;
	public float invulTime;
	public int health = 10;
	//public Text healthText;
	public int fallBoundary = -20;

	private bool invuln = false;

	private float groundedRadius = 0.2f;
	private bool grounded = false;

	private SpriteRenderer spriteRenderer;

	private float distToGround;

	private bool facingRight = true;

	private Rigidbody2D rb2d;

	private bool playerMove;
	private bool jumpSquat;
	private Animator anim;

	private Transform pirateGraphics;
	private Transform pirate;

    /*
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();

    [SerializeField]
    private StatusIndicator statusIndicator;

    */

	// Use this for initialization
	void Start () {
		pirate = transform.FindChild ("Pirate");
		pirateGraphics = pirate.transform.FindChild ("Graphics");
		anim = pirateGraphics.transform.FindChild("Pirate").GetComponent<Animator>();
		rb2d = pirate.GetComponent<Rigidbody2D> ();
		spriteRenderer = pirate.GetComponent<SpriteRenderer> ();
		distToGround = pirate.GetComponent<BoxCollider2D> ().bounds.extents.y;
        /*
        stats.Init();

        if(statusIndicator == null)
        {
            Debug.LogError("No SI");
        } else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        */
    }

    /*
    void Update() {
		if (pirate.transform.position.y <= fallBoundary)
			DamagePlayer (stats.maxHealth);
	}
    */

    // Update is called once per frame
    void FixedUpdate () {
		grounded = IsGrounded();//Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool ("Grounded", grounded);
	}

	public void Move(float move, bool jump) {
		if (move == 0) {
			playerMove = false;
			anim.SetBool ("playerMove", playerMove);
		} else {
			playerMove = true;
			anim.SetBool ("playerMove", playerMove);
		}

		if (grounded && jump && !jumpSquat) {
			grounded = false;
			anim.SetBool ("Grounded", grounded);
			rb2d.AddForce (Vector2.up * propulsion);
		}

		if (!jumpSquat) {
			rb2d.velocity = new Vector2 (move * speed, rb2d.velocity.y);
		}

		if (move > 0 && !facingRight) {
			Flip ();
		}
		else if(move < 0 && facingRight) {
			Flip();
		}
	}

	/*
	void Jump() {
		jumpSquat = false;
		anim.SetBool ("Squat", false);
		anim.SetBool ("Grounded", false);
		rb2d.AddForce (Vector2.up * propulsion);
	}
	*/

	bool IsGrounded() {
		return Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
	}

    /*
	void OnCollisionEnter2D(Collision2D other) {
		if(!invuln && other.gameObject.CompareTag("Enemy Attack")) {
			DamagePlayer (10);
		}
	}

    
	public void DamagePlayer(int damage) {
		stats.curHealth -= damage;
		if(stats.curHealth <= 0) {
			GameMaster.KillPlayer (this);
		}
		Invuln ();
		Invoke ("ClearInvuln", invulTime);
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

	void Die() {
		spriteRenderer.enabled = false;
		pirate.GetComponent<ParticleSystem>().Play();
		spriteRenderer.enabled = false;
		rb2d.isKinematic = true;
		pirate.GetComponent<BoxCollider2D> ().enabled = false;
	}

	void Invuln() {
		invuln = true;
	}

	void ClearInvuln () {
		invuln = false;
	}
    */

    void Flip () {
		facingRight = !facingRight;

		Vector3 flipVector = pirateGraphics.transform.localScale;

		flipVector.x *= -1;

		pirateGraphics.localScale = flipVector;
	}
}
