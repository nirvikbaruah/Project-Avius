using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

	public GameObject player;
	private float health;
	public GameObject checkpointSpawn;
	public Animator anim;
	private int checkpointCount = 0;
	public int currentCheckpoint;
	public GameObject healthBar;

	// Use this for initialization
	void Start () {
		health = player.GetComponent<Health> ().GetHealth();
	}

	// Update is called once per frame
	void Update () {
		health = healthBar.transform.localScale.x * 100f;
		if (health < 0f) {
			anim.SetBool("Dead", true);
			Respawn();
		}
	}

	void Respawn(){
		player.transform.position = checkpointSpawn.transform.position;
	}

	void OnCollisionEnter2D (Collision2D hit) { 
		if(hit.gameObject.tag == "Player" && currentCheckpoint > checkpointCount) { 
			checkpointSpawn.transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);
			checkpointCount = currentCheckpoint;
			Debug.Log (checkpointCount);
			Debug.Log(currentCheckpoint);
		} 
	}
}
