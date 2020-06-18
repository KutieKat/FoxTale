using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth; 
    public float invincibleLength;
    public GameObject deathEffect;

    private float invincibleCounter;
    private SpriteRenderer theSpriteRenderer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                theSpriteRenderer.color = new Color(theSpriteRenderer.color.r, theSpriteRenderer.color.g, theSpriteRenderer.color.b, 1);
            }
        }
    }

    public void TakeDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSpriteRenderer.color = new Color(theSpriteRenderer.color.r, theSpriteRenderer.color.g, theSpriteRenderer.color.b, 0.5f);
            
                PlayerController.instance.Deflect();
                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();
    }
}
