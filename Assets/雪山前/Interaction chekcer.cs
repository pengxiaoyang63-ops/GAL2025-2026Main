using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactionchekcer : MonoBehaviour
{
    public int StoryNumber;
    public bool Active;
    public float Chapter;
    public float Scene;
    public float Clip;
    public InteractionManager interactionManager;
    // Start is called before the first frame update
    void Start()
    {
        interactionManager = GetComponent<InteractionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (StoryNumber != 0)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactionManager.Clip = Clip;
                    interactionManager.Scene = Scene;
                    interactionManager.Chapter = Chapter;
                    interactionManager.Active = true;
                }     
            } 
        }
    }

}
