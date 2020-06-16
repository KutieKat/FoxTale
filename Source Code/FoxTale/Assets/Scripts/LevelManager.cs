using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float waitToRespawn;
    public int gemsCollected;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointsController.instance.spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UIController.instance.UpdateHealthDisplay();
    }
}
