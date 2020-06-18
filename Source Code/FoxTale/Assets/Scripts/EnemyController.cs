using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool movingRight;
    private Rigidbody2D theRigidBody;
    public SpriteRenderer theSpriteRenderer;
    public float moveTime, waitTime;
    private float moveCount, waitCount;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                theRigidBody.velocity = new Vector2(moveSpeed, theRigidBody.velocity.y);

                theSpriteRenderer.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                theRigidBody.velocity = new Vector2(-moveSpeed, theRigidBody.velocity.y);

                theSpriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);
        }  
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRigidBody.velocity = new Vector2(0f, theRigidBody.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(waitTime * 0.75f, waitTime * 0.75f);
            }

            anim.SetBool("isMoving", false);
        }      
    }
}
