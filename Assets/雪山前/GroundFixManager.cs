using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFixManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Movement mainCharacterController;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            mainCharacterController.GroundFix();

        }
    }
}
