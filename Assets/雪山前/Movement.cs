using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;

public class Jump : MonoBehaviour
{
    ParticleSystem PS;
    Rigidbody2D RD2;
    public float speed;
    public float jump;
    public bool onground;
    public bool onwallL;
    public bool onwallR;
    public float deceleration;
    public int test;
    public bool IsparticleOn = false;
    Vector2 velocities;
    public float maxspeed;
    public float onwallspeed = 3f;
    public float walljump = 8f;
    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D first before modifying it
        RD2 = GetComponent<Rigidbody2D>();
        if (RD2 != null)
        {
            RD2.bodyType = RigidbodyType2D.Dynamic;
            RD2.isKinematic = false;
            RD2.gravityScale = 3f;
        }
        speed = 0.5f;
        jump = 13f;
        onground = false;
        deceleration = 1.5f;
        test = 2;
        maxspeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        locomotion();
        Jumping();
        Reset();
        FreezeZRotation();
        //Isonground();
    }
    void FreezeZRotation()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    void locomotion()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (RD2.velocity.x > -maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x-0.5f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(-15, RD2.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (RD2.velocity.x < maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x+0.5f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(15, RD2.velocity.y);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            RD2.velocity = new Vector2(0, RD2.velocity.y);
        }
        if (onwallL == true)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RD2.velocity = new Vector2(-12, 20);
                }
            }
        }
        if (onwallR == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    RD2.velocity = new Vector2(12, 20);
                }
            }
        }
        //deceleration function, need to work on it more
        /*else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (abs(RD2.velocity.x) > 0)
            {
                float MainY;
                MainY = RD2.velocity.y;
                float MainX;
                MainX = RD2.velocity.x;
                MainX = MainX / deceleration;
                MainY = MainY / deceleration;
                Vector2 Main = new Vector2(MainX, MainY);
                RD2.velocity = Main;
                deceleration += 1.5f;
                if (abs(RD2.velocity.x) <= 0.3f)
                {
                    MainX = 0;
                    RD2.velocity = new Vector2(MainX, RD2.velocity.y);
                }

            }
        }*/

    }
    void Jumping()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onground == true)
        {
            RD2.velocity = new Vector2(RD2.velocity.x, jump);
        }
    }
    void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RD2.bodyType = RigidbodyType2D.Static;
            RD2.position = new Vector2(0, 0);
            RD2.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public float abs(float a)
    {
        if (a > 0)
        {
            return a;
        }
        else
        {
            return -a;
        }
    }
    public void GroundFix()
    {
        RD2.velocity = new Vector2(RD2.velocity.x, 1);
    }
    /*void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onground = false;
        }
    }*/

    // Use 2D collision callbacks because this script uses Rigidbody2D
   /*void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Draw the contact point normal for debugging
            Debug.DrawRay(contact.point, contact.normal, Color.red, 10f);
            // Output the normal direction
            Debug.Log($"Normal Direction: {contact.normal}");
            if (contact.normal.x == 0f)
            {
                contactground = true;
            }
            else
            {
                contactground = false;
            }
        }
        if (collision.gameObject.CompareTag("Ground") && contactground == true)
        {
            Debug.Log("Landed on the ground");
            onground = true;
        }

        // Evaluate wall contacts when a collision begins
        Walljump(collision);

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Draw the contact point normal for debugging
            Debug.DrawRay(contact.point, contact.normal, Color.red, 10f);
            // Output the normal direction
            Debug.Log($"Normal Direction: {contact.normal}");
            if (contact.normal.x == 0f)
            {
                contactground = true;
            }
            else
            {
                contactground = false;
            }
        }
        if (collision.gameObject.CompareTag("Ground") && contactground == true)
        {
            Debug.Log("Landed on the ground");
            onground = true;
        }

        // Keep evaluating wall contacts while staying in collision
        Walljump(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Left the ground");
            onground = false;
      
        }
    }

    // Walljump inspects contact normals from a Collision2D passed in.
    // Call this from a collision callback if you want wall detection.
    void Walljump(Collision2D collision)
    {
        if (collision == null)
            return;
        if (RD2 == null)
            return;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Draw the contact point normal for debugging
            Debug.DrawRay(contact.point, contact.normal, Color.red, 10f);
            // Output the normal direction
            Debug.Log($"Normal Direction: {contact.normal}");
            // If normal.x is near +/-1, that's a wall contact
            if (Mathf.Abs(contact.normal.x) == 1f)
            {
                onwall = true;
            }
            else
            {
                onwall = false;
            }
        }
        // Example usage: if not on wall and not on ground, you could apply air logic here.
        if (onwall == true && onground == false)
        {
            RD2.velocity = new Vector2(0, RD2.velocity.y);
            if (RD2.velocity.y < 0)
            {
                // Apply an upward bump when appropriate (set y to onwallspeed)
                RD2.velocity = new Vector2(RD2.velocity.x, -onwallspeed);
                // placeholder for air behavior
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Apply an upward bump when appropriate (set y to onwallspeed)
                RD2.velocity += new Vector2(walljump, walljump);
                // placeholder for air behavior
            }
            
        }
    }*/


}
    

