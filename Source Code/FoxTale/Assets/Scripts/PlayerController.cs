using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float jumpSpeed;
    public Rigidbody2D theRigidBody;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float deflectLength, deflectSpeed;

    private bool isGrounded;
    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSpriteRenderer;
    private float deflectCounter;

    public float bounceSpeed;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deflectCounter <= 0)
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
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, jumpSpeed);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
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
        }
        else
        {
            deflectCounter -= Time.deltaTime;

            if (!theSpriteRenderer.flipX)
            {
                theRigidBody.velocity = new Vector2(-deflectSpeed, theRigidBody.velocity.y);
            }
            else
            {
                theRigidBody.velocity = new Vector2(deflectSpeed, theRigidBody.velocity.y);
            }
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Deflect()
    {
        deflectCounter = deflectLength;
        theRigidBody.velocity = new Vector2(0f, theRigidBody.velocity.y);

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRigidBody.velocity = new Vector2(theRigidBody.velocity.x, bounceSpeed);
        AudioManager.instance.PlaySFX(10);
    }
}
