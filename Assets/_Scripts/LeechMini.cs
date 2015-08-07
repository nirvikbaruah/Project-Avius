using UnityEngine;
using System.Collections;

public class LeechMini : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	private Vector3 distance;
	public GameObject spawnPoint;
	public GameObject leech;
	public Rigidbody2D leechBody;
	private float clock = 1f;

	// Update is called once per frame
	void Update () {
		distance = player.transform.position - transform.position;
		if (distance.x > -15f && distance.x < 15f) {
			if (Random.value > 0.5f) {
				anim.SetBool ("Shoot", true);
			} else {
				spawnLeeches ();
			}
		}
	}

	void spawnLeeches(){
		anim.SetBool ("Leach", true);
		leechBody.velocity = new Vector2 (2f, 0f);
		clock -= Time.deltaTime;

		if (clock <= 0f) {
			Instantiate (leech, spawnPoint.transform.position, Quaternion.identity);
			clock = 3f;
		}

		leech.transform.LookAt(player.transform.position);
		leech.transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation
		
		
		leech.transform.Translate(new Vector2(3f* Time.deltaTime,0) );
	}
}
