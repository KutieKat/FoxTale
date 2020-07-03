using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public SpriteRenderer theSpriteRenderer;
    public Sprite checkpointOff, checkpointOn;

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
            CheckpointsController.instance.DeactivateCheckpoints();

            theSpriteRenderer.sprite = checkpointOn;

            Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
            CheckpointsController.instance.SetSpawnPoint(spawnPoint);
        }
    }

    public void ResetCheckpoint()
    {
        theSpriteRenderer.sprite = checkpointOff;
    }
}
