using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public static BossTankController instance;
    public enum bossStates
    {
        shooting,
        hurt,
        moving,
        ended
    };
    public bossStates currentState;
    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool moveRight;
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]    
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public int healths = 5;
    public GameObject explosion, winPlatform;
    private bool isDefeated;
    public float shotSpeedUp, mineSpeedUp;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;

                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }

                break;

            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;

                        mineCounter = 0;

                        if (isDefeated)
                        {
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            winPlatform.SetActive(true);

                            AudioManager.instance.StopBossMusic();

                            currentState = bossStates.ended;
                        }
                    }
                }

                break;

            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        moveRight = true;
                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;

                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }

                break;
        }
    }

    public void TakeDamage()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;

        anim.SetTrigger("Hit");

        AudioManager.instance.PlaySFX(0);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();

        if (mines.Length > 0)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        healths--;

        if (healths <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
