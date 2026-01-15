using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Movement Movement;
    void Start()
    {
        Movement = GetComponentInParent<Movement>();
    }
    void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Movement.onground = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Invoke("ongroundFalse", 0.1f);
        }
    }
    void ongroundFalse()
    {
        Movement.onground = false;
    }
}