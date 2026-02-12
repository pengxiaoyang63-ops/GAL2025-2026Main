using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter != null)
        {
            Transform destination = currentTeleporter.GetComponent<Teleporter>().GetDestination();
            FadeTransition(destination);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    async void FadeTransition(Transform destination)
    {
        await ScreenFader.Instance.FadeOut();
        transform.position = destination.position;
        await ScreenFader.Instance.FadeIn();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentTeleporter)
        {
            currentTeleporter = null;
        }
    }


}
