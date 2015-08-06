using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 6f;
	public static bool isRight = true;
	public float tapSpeed = 0.5f; //in seconds
	private float lastTapTime = 0;
	public float sprintSpeed = 12f;
	private float stamina = 20f;
	private bool sprinting = false;
	public float staminaDecrease = 5f;
	
	public float jumpHeight = 7.5f;
	private int jumpCount = 0;
	
	private Animator anim;
	private Rigidbody2D body;
	
	public GameObject bullet;
	private float shootTimer = 2f;
	private bool shoot = false;
	public GameObject shootPoint;
	
	private bool gunEquip; 
	private bool isDead;
	private bool jumped;
	public static bool onLand = false;

	public float playerHealth = 100f;
	public float waterHealthDecrease = 10f;
	private bool enterWater = false;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
		gunEquip = true;
		isDead = anim.GetBool ("Dead");
		jumped = anim.GetBool ("Jumped");
		
		lastTapTime = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		DoubleTapSprint ();
		Movement ();
		TakeDamage ();
		
		if (Input.GetKeyDown("x") && gunEquip) {
			anim.SetBool ("Shooting", true);
			shoot = true;
			shootTimer = 2f;
			
			Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
			
		}
		
		if(Input.GetKey(KeyCode.DownArrow)){
			if (speed == 12f){
				anim.SetBool("Slide", true);
			}
			else{
				anim.SetBool("ShootDown", true);
			}
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if (anim.GetBool("Slide") == true){
				anim.SetBool("Slide", false);
			}
			else{
				anim.SetBool("ShootDown", false);
			}
		}
		
		if (shoot) {
			if (shootTimer < 0f){
				anim.SetBool ("Shooting", false);
				shoot = false;
			}
			else{
				shootTimer -= Time.deltaTime;
			}
		}
		
		if (Input.GetKeyDown("c")) {
			gunEquip = !gunEquip;
			anim.SetBool ("gunOut", gunEquip);
		}


		if (enterWater) {
			speed = 3f;
			playerHealth -= waterHealthDecrease * Time.deltaTime;
			Debug.Log (playerHealth);
		}
	}
	
	void DoubleTapSprint(){
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
			if ((Time.time - lastTapTime) < tapSpeed && stamina > 0f) {
				speed = sprintSpeed;
				sprinting = true;
			}
			lastTapTime = Time.time;
		}
	}
	
	void Movement(){
		
		if (Input.GetAxisRaw("Horizontal") > 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);
			isRight = true;
			anim.SetBool ("isMoving", true);
		}
		
		if (Input.GetAxisRaw("Horizontal") < 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180);
			isRight = false;
			anim.SetBool ("isMoving", true);
		}
		if(Input.GetKeyDown("z") && jumpCount == 0 && onLand) { 
			body.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
			jumpCount = 1; 
			onLand = false;
			anim.SetBool ("Jumped", true);
			anim.SetBool ("Landed", false);
		}
		
		if (sprinting && speed == sprintSpeed) {
			stamina -= staminaDecrease * Time.fixedDeltaTime;
			anim.SetBool("isSprinting", true);
		}
		else{
			sprinting = false;
			stamina += (staminaDecrease / 2) * Time.deltaTime;
			anim.SetBool("isSprinting", false);
		}
		
		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D) || stamina <= 0f) {
			speed = 6f;
		}
		
	}
	
	void TakeDamage(){
		if(Input.GetKeyDown("space")) { 
			anim.SetBool ("Dead", true);
		}

	}

	void OnTriggerEnter2D(Collider2D hit) {
		if(hit.gameObject.tag == "Water") { 
			enterWater = true;
			jumpHeight *= 0.75f;
		} 
	}

	void OnTriggerExit2D(Collider2D hit){
		if(hit.gameObject.tag == "Water") { 
			enterWater = false;
			speed = 6f;
			jumpHeight *= 1.25f;
		} 
	}
	
	void OnCollisionEnter2D (Collision2D hit) { 
		if(hit.gameObject.tag == "Floor") { 
			jumpCount = 0; 
			onLand = true;
			anim.SetBool ("Jumped", false);
			anim.SetBool ("Landed", true);
		} 
	}


}
