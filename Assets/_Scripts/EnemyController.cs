using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health;
	public bool witch;
	public Animator anim;
	private float clock = 1f;

	public GameObject healthBar;
	public bool trojan;

	// Use this for initialization
	void Start () {
		anim.GetComponent<Animator> ();
	}

	void Update(){
		if (witch) {
			if(health <= 50f){
				anim.SetBool("Heal", true);
			}else{
				anim.SetBool("Heal", false);
			}
		}

		if (trojan) {
			healthBar.transform.localScale = new Vector3 ((health / 1000f) / 10f, 0.2441701f, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D hit) {
		if(hit.gameObject.tag == "Enemy") { 
			clock -= Time.deltaTime;
			if (clock <= 0f){
				health += 5f;
				clock = 1f;
			}
		} 
	}
	
	void OnCollisionEnter2D (Collision2D hit) { 
		if(hit.gameObject.tag == "Bullet") { 
			Destroy(hit.gameObject);
			health -= 40f;
		} 

		if (health <= 0f){
			Destroy(gameObject);
		}
	}
}
