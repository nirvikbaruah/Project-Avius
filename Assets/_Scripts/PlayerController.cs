﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;
	public static bool isRight = true;

	public float jumpHeight = 7.5f;
	private int jumpCount = 0;

	private Animator anim;
	private Rigidbody2D body;

	public GameObject bullet;
	private float shootTimer = 5f;
	private bool shoot = false;
	public GameObject shootPoint;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		TakeDamage ();

		if (Input.GetKeyDown("x")) {
			anim.SetBool ("Shooting", true);
			shoot = true;
			shootTimer = 5f;

			Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);

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
	}

	void Movement(){

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 0);
			isRight = true;
			anim.SetBool ("isMoving", true);
		}

		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0, 180);
			isRight = false;
			anim.SetBool ("isMoving", true);
		}
		if(Input.GetKeyDown("z") && jumpCount == 0) { 
			body.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
			jumpCount = 1; 
			anim.SetBool ("Jumped", true);
			anim.SetBool ("Landed", false);
		}
	}

	void TakeDamage(){
		if(Input.GetKeyDown("space")) { 
			Debug.Log ("Testing death anim");
			anim.SetBool ("Dead", true);
		}
	}

	void OnCollisionEnter2D (Collision2D hit) { 
		if(hit.gameObject.tag == "Floor") { 
			jumpCount = 0; 
			anim.SetBool ("Jumped", false);
			anim.SetBool ("Landed", true);
		} 
	}

}
