using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Jump Movement;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Movement.onground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Movement.onground = false;
        }
    }
}