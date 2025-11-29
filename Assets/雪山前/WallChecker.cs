using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Jump Movement;
    public bool Right;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (Right == true)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Movement.onwallR = true;
                Debug.Log("True R");
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Movement.onwallL = true;
                Debug.Log("True L");
            }
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Movement.onwallL = false;
            Movement.onwallR = false;
        }
    }
}