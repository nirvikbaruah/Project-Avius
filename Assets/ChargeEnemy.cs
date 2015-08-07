using UnityEngine;
using System.Collections;

public class ChargeEnemy : MonoBehaviour {

	public Transform player;
	public Rigidbody2D playerBody;
	public Animator anim;
	private Vector3 distance;
	private Rigidbody2D rigidbody;
	private float startTime; 
	private bool attack = false;
	public float speed = 100f;
	private bool rush = false;
	private float clock = 2f;

	void Start(){
		rigidbody = GetComponent<Rigidbody2D> ();
		startTime = Time.time;
	}


	void Update(){

		if (attack) {
			distance = player.position - transform.position;

			if (distance.x > 0f) {
				transform.eulerAngles = new Vector2 (0, 180);

			} else {
				transform.eulerAngles = new Vector2 (0, 0);
			}

			transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

		}
		if (Random.value > 0.99){
			attack = false;
			rush = true;
		}
		if (rush) {
			clock -= Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
			anim.SetBool("Rush", true);
			if(clock <= 0f){
				rush = false;
				clock = 2f;
				attack = true;
			}
		}



	}

	void OnTriggerEnter2D(Collider2D hit) {
		if (hit.gameObject.tag == "Player") {
			attack = true;
		}
	}




}
