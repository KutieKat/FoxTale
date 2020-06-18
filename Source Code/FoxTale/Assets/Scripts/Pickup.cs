using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHealth;
    public GameObject pickupEffect;

    // Make sure everything is still correct upon collision with multiple colliders
    private bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                LevelManager.instance.gemsCollected++;
                isCollected = true;
                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCount();

                AudioManager.instance.PlaySFX(6);
            }

            if (isHealth)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();

                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);
                }

                AudioManager.instance.PlaySFX(7);
            }
        }
    }
}
