using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };

        for (int i = 0; i < numbers.Count; i++)
        {
            int element = numbers[i];
            Debug.Log($"元素: {element}");
        }
    }
}
