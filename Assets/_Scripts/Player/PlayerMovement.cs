using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float WalkSpeed = 5f;
    public float SprintSpeed = 10f;
    public float AirMoveSpeed = 1f;
    public float AirDashForce = 20f;

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

    }

    bool grounded = false;

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, ground);

        grounded = hit;

        float Horizontal = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2(Horizontal * (grounded ? WalkSpeed : AirMoveSpeed), RB.velocity.y);

        if (Input.GetButtonDown("Jump") && hit)
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        if (anim)
        {
            if (Horizontal != 0)
            {
                transform.localScale = new Vector3((RB.velocity.x >= 0 ? StartingScale.x : -StartingScale.x), StartingScale.y);
            }
        }
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
