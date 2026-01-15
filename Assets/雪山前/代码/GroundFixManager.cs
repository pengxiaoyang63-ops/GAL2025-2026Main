using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFixManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Movement mainCharacterController;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            mainCharacterController.GroundFix();
        }
    }
}
