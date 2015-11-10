using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float WalkSpeed = 5f;
    public float SprintSpeed = 10f;
    public float AirMoveSpeed = 1f;
    public float AirDashForce = 20f;

	public float DoubleTapSpeed = 0.5f;

	float SprintCooler;
	int SprintTapCount = 0;
	bool isSprinting = false;

    public LayerMask ground;

    public float GroundCheckDistance = 1.01f;
    public float JumpForce = 10f;

    Rigidbody2D RB;
    Animator anim;

    Vector3 StartingScale;


    public void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartingScale = transform.localScale;
		SprintCooler = DoubleTapSpeed;
    }

    bool grounded = false;

	float LastHorz = 0;

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, ground);

        grounded = hit;

        float Horizontal = Input.GetAxis("Horizontal");
		float HorzSpeed = Horizontal * (grounded ? WalkSpeed : AirMoveSpeed);

		//Double Tap For sprint
		if (Mathf.Abs(Horizontal) != 0 && Mathf.Abs(LastHorz) == 0) {
			Debug.Log ("Tapped");
			//Tap
			if (grounded && SprintCooler > 0 && SprintTapCount == 1){
				Debug.Log ("Started Sprinting");
				isSprinting = true;
			} else {
				SprintCooler = DoubleTapSpeed; 
				SprintTapCount += 1;
			}
		}

		if ( SprintCooler > 0 ) {
			SprintCooler -= 1 * Time.deltaTime ;
		} else{
			SprintTapCount = 0 ;
		}

		if (Horizontal == 0) {
			isSprinting = false;
		}

		if (isSprinting) {
			HorzSpeed = Horizontal * SprintSpeed;
		}

		RB.velocity = new Vector2(HorzSpeed, RB.velocity.y);

        if (Input.GetButtonDown("Jump") && hit)
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        if (anim)
        {
            if (Horizontal != 0)
            {
                transform.localScale = new Vector3((RB.velocity.x >= 0 ? StartingScale.x : -StartingScale.x), StartingScale.y);
                anim.SetBool("PlayRunAnim", true);
            }
            else
            {
                anim.SetBool("PlayRunAnim", false);
            }

            anim.SetBool("isMoving", RB.velocity.x != 0);
            anim.SetBool("Jumped", !grounded);
            anim.SetBool("Landed", grounded);
        }

		LastHorz = Horizontal;
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * GroundCheckDistance, grounded ? Color.green : Color.red);
    }

    public void OnDestroy()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
