using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private bool isRight;
	private Rigidbody2D body;
	public float speed = 15f;
	private bool isLand;


	void Start(){
		isRight = PlayerController.isRight;
		body = GetComponent<Rigidbody2D> ();
		isLand = PlayerController.onLand;


		if(Input.GetKey(KeyCode.DownArrow) && !isLand){
			body.velocity = new Vector2(0, -1f * speed);
		}
		else{
			if (isRight){
				body.velocity = new Vector2(speed, 0);
			} 
			else{
				body.velocity = new Vector2(speed * -1, 0f);
			}
		}
	}

	void Update(){
		if (Mathf.Abs (body.velocity.x) != speed && Mathf.Abs (body.velocity.y) != speed) {
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

}
