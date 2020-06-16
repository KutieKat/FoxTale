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
        if (other.CompareTag("Player"))
        {
            theSpriteRenderer.sprite = checkpointOn;
            
            CheckpointsController.instance.DeactivateCheckpoints();
            CheckpointsController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        theSpriteRenderer.sprite = checkpointOff;
    }
}
