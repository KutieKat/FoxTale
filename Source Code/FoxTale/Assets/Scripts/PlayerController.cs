using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Direction { 
        Left, 
        Right 
    };
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
    public bool stopInput;
    public Direction direction;
    private int movingStatus; // -1: Moving Left; 0: Idling; 1: Moving right

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        direction = Direction.Right;
        movingStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (deflectCounter <= 0)
            {
                theRigidBody.velocity = new Vector2(moveSpeed * movingStatus, theRigidBody.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                if (direction == Direction.Right)
                {
                    theSpriteRenderer.flipX = false;
                }
                else if (direction == Direction.Left)
                {
                    theSpriteRenderer.flipX = true;
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

    public void Jump()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (deflectCounter <= 0)
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
        }
    }

    public void MoveRight()
    {
        direction = Direction.Right;
        movingStatus = 1;
    }

    public void MoveLeft()
    {
        direction = Direction.Left;
        movingStatus = -1;
    }

    public void Stop()
    {
        movingStatus = 0;
    }
}
