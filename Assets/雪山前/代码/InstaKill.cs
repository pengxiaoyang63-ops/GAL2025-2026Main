using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    public GameObject player;
    public GameObject defaultRespawnPoint;
    
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
        if (other.gameObject.CompareTag("Player"))
        {
            // Use current checkpoint if set, otherwise default respawn point
            Vector3 respawnPos = defaultRespawnPoint.transform.position;

            if (Checkpoint.currentCheckpoint != null)
            {
                respawnPos = Checkpoint.currentCheckpoint.transform.position;
            }
    
            // Call the async fade transition method
            FadeTransition(other.gameObject, respawnPos);
        }
    }
    
    async void FadeTransition(GameObject playerObj, Vector3 respawnPos)
    {
        await ScreenFader.Instance.FadeOut();
        playerObj.transform.position = respawnPos;
        Debug.Log("Player killed and respawned at: " + respawnPos);
        await ScreenFader.Instance.FadeIn();
    }
}