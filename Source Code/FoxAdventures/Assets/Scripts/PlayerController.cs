using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public Rigidbody2D theRigidBody;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool isGrounded;
    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        theRigidBody.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRigidBody.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, jumpSpeed);
            }
            else
            {
                if (canDoubleJump)
                {
                    theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, jumpSpeed);
                    canDoubleJump = false;
                }
            }
        }

        if (theRigidBody.velocity.x < 0)
        {
            theSpriteRenderer.flipX = true;
        }
        else if (theRigidBody.velocity.x > 0)
        {
            theSpriteRenderer.flipX = false;
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }
}
