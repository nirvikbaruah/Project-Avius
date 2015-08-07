using UnityEngine;
using System.Collections;

public class TrojanBoss : MonoBehaviour {

	public GameObject player;
	public Animator playerAnim;
	private Vector3 distance;
	public Animator anim;
	public PlayerController health;
	private float clock = 3f;
	public GameObject target1;
	public Transform target2;
	public float healthLevel;

	// Use this for initialization
	void Start () {
	
	}

	void Update(){
		distance = player.transform.position - transform.position;
		
		if (distance.x > 0f) {
			transform.eulerAngles = new Vector2 (0, 180);
			
		} else {
			transform.eulerAngles = new Vector2 (0, 0);
		}

		clock -= Time.deltaTime;
		if (clock <= 0f) {
			if (Random.value < 0.2f){
				anim.SetBool("Charge", true);
				transform.position = Vector3.MoveTowards(transform.position, target1.transform.position, 30f * Time.deltaTime);
			}
			clock = 3f;
		}

	}

	
	// Update is called once per frame
	/*void Update () {
		clock -= Time.deltaTime;
		if (clock <= 0f) {
			if (Random.value < 0.2f){
				anim.SetBool("Charge", true);
				target = new Vector3(player.transform.position.x, transform.position.y, 0f);
				transform.position = Vector3.MoveTowards(transform.position, target, 30f * Time.deltaTime);
			}
			clock = 3f;
		}
		distance = transform.position - player.transform.position;
		anim.SetFloat ("Distance", distance.x);

	}*/

}
