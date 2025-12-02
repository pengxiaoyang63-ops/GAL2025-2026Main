using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Movement Movement;
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
            Invoke("ongroundFalse", 0.1f);
        }
    }
    void ongroundFalse()
    {
        Movement.onground = false;
    }
}