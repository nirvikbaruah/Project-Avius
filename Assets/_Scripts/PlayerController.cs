using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
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
	}
}
