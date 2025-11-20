using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuanToday : MonoBehaviour
{
    private int Boyfriend;
    private int Girlfriend;
    public float Timer;
    private float Health;
    public MainCharacterController shaunbehavior;
    public int sleepingtime;
    private int i;
    void Awake()
    {
        Health -= 7.5f - sleepingtime;
        shaunbehavior = GetComponent<MainCharacterController>();
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Girlfriend < 1)
        {
            Debug.LogWarning("Girlfriend notfound!");
        }
        if (Boyfriend > 0)
        {
        while(i < 10)
            {
                Debug.LogWarning("Gay????");
                i++;
                if (i > 10)
                {
                    break;
                }
            }    
        }
        if (Health <= 60)
        {
            Debug.LogWarning("Health is low!");
            Debug.LogWarning($"Health={Health}");
        }
    }
}
