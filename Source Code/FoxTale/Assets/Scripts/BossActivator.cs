using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public static BossActivator instance;

    public GameObject theBossBattle;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theBossBattle.SetActive(true);

            gameObject.SetActive(false);

            AudioManager.instance.PlayBossMusic();
        }
    }
}
