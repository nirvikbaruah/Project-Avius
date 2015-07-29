using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;

	public float jumpHeight = 7.5f;
	private int jumpCount = 0;

	private Animator anim;
	private Rigidbody2D body;
	private SpriteRenderer sprite;

	public Sprite shootingSprite;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();

		if(Input.GetButtonDown("Fire1")) { 
			Shooting ();
		}
	}

	void Movement(){

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);
		}

		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180);
		}
		if(Input.GetButtonDown("Jump") && jumpCount == 0) { 
			body.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
			jumpCount = 1; 
		}
	}

	void Shooting(){

	}

	void OnCollisionEnter2D (Collision2D hit) { 
		if(hit.gameObject.tag == "Floor") { 
			jumpCount = 0; 
		} 
	}

}
