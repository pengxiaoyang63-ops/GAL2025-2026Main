using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    ParticleSystem PS;
    Rigidbody2D RD2;
    public float speed;
    public float jump;
    public bool onground;
    public bool onwallL;
    public bool onwallR;
    public bool WallJumpBool;
    public int WallJumpCounter;
    public float maxspeed;
    public float Dashcounter;
    public float DashWait;
    public float Timer;
    public bool Dashing;
    public bool FaceRight;
    public int faceCoefficient;
    void Start()
    {
        Time.fixedDeltaTime = 1/500f;
        // Get the Rigidbody2D first before modifying it
        RD2 = GetComponent<Rigidbody2D>();
        if (RD2 != null)
        {
            RD2.bodyType = RigidbodyType2D.Dynamic;
            RD2.isKinematic = false;
            RD2.gravityScale = 5f;
        }
        speed = 0.5f;
        jump = 26f;
        onground = false;
        maxspeed = 12;
        RD2.gravityScale = 6f;
    }
    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        DashWait += Time.deltaTime;
        if (Dashing == false)
        {
            locomotion();
            faceupdate();
            Jumping();
            Wallmotion();
        }
        Reset();
        Dash();
        ResetY();
    }
    void faceupdate()
    {
        if (FaceRight == true)
        {
            faceCoefficient = 1;
        }
        else
        {
            faceCoefficient = -1;
        }
    }
    void locomotion()
    {
        if (Input.GetKey(KeyCode.A))
        {  
            FaceRight = false;
            if (RD2.velocity.x > -maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x-1f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(-maxspeed, RD2.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            FaceRight = true;
            if (RD2.velocity.x < maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x+1f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(maxspeed, RD2.velocity.y);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            RD2.velocity = new Vector2(0, RD2.velocity.y);
        }
        else if (!Input.GetKeyUp(KeyCode.A) && !Input.GetKeyUp(KeyCode.D))
        {
            if (Dashing == false)
            {
                RD2.velocity = new Vector2(0, RD2.velocity.y);   
            }
        }
    }
    void Wallmotion()
    {
        if (onwallR == true)
        {
            if (Input.GetKey(KeyCode.D)&&Input.GetKeyDown(KeyCode.K))
            {
                WallJumpBool = !WallJumpBool;
                WallJumpCounter = 40;
                RD2.velocity = new Vector2(-WallJumpCounter,20);
            }
        }
        if (onwallL == true)
        {
            if (Input.GetKey(KeyCode.A)&&Input.GetKeyDown(KeyCode.K))
            {
                WallJumpBool = !WallJumpBool;
                WallJumpCounter = 40;
                RD2.velocity = new Vector2(WallJumpCounter,20);
            }
        }
        if (WallJumpBool == true&&WallJumpCounter>=-9&&onwallL == false&&onwallR == false)
        {
            if (WallJumpBool&&Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.K))
            {
                RD2.velocity = new Vector2(WallJumpCounter,20);
                WallJumpCounter-=2;
            }
            else if (WallJumpBool&&Input.GetKey(KeyCode.D)&&Input.GetKey(KeyCode.K))
            {
                RD2.velocity = new Vector2(-WallJumpCounter,20);
                WallJumpCounter-=2;
            }
        }
        else if (WallJumpCounter < 2)
        {
            WallJumpBool = false;
        }
    }
    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.K) && onground == true)
        {
            if (Dashing == false)
            {
                RD2.velocity = new Vector2(RD2.velocity.x, jump);
            }
        }
    }
    void ResetY()
    {
        if (onground==false&&!Input.GetKey(KeyCode.K))
        {
            if (RD2.velocity.y > 0)
            {
                RD2.velocity = new Vector2(RD2.velocity.x,RD2.velocity.y/5);   
            }
        }
    }
    void Dash()
    {
        if (Input.GetKey(KeyCode.Space)&&DashWait>0.5f)
        {
            RD2.velocity = new Vector2(0,0);
            Dashing = true;
            DashWait = 0f;
            RD2.velocity = new Vector2(RD2.velocity.x,0);
            RD2.gravityScale = 0;
            Dashcounter = 0f;
        }
        if(Dashing == true)
        {
            RD2.velocity = new Vector2(35*faceCoefficient,0);
            Dashcounter += Time.deltaTime;
            if (Dashcounter >= 0.2f)
            {
                DashWait = 0f;
                Dashing = false;
                RD2.gravityScale = 5f;
            }
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
    public void GroundFix()
    {
        RD2.velocity = new Vector2(RD2.velocity.x, 1);
    }
}