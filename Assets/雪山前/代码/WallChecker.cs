using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Movement Movement;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (Movement.FaceRight == true)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Movement.onwallR = true;
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Movement.onwallL = true;
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