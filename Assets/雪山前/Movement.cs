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
    public bool Walljumping;
    public int WallJumpingCounter;
    public float deceleration;
    public int test;
    public bool IsparticleOn = false;
    Vector2 velocities;
    public float maxspeed;
    public float PreviousY;
    public float Dashcounter;
    public float DashWait;
    public float Timer;
    public bool Dashing;
    public bool FaceRight;
    // Start is called before the first frame update
    void Start()
    {

        // Get the Rigidbody2D first before modifying it
        RD2 = GetComponent<Rigidbody2D>();
        if (RD2 != null)
        {
            RD2.bodyType = RigidbodyType2D.Dynamic;
            RD2.isKinematic = false;
            RD2.gravityScale = 5f;
        }
        speed = 0.5f;
        jump = 15f;
        onground = false;
        deceleration = 1.5f;
        test = 2;
        maxspeed = 10;
    }
    void Awake()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        DashWait += Time.deltaTime;
        if (Dashing == false)
        {
            locomotion();
            Jumping();
        }
        Reset();
        Dash();
        //Isonground();
    }
    void locomotion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
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
        else if (Input.GetKey(KeyCode.RightArrow))
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
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            RD2.velocity = new Vector2(0, RD2.velocity.y);
        }
        else if (!Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Dashing == false)
            {
                RD2.velocity = new Vector2(0, RD2.velocity.y);   
            }
        }
        if (onwallL == true)
        {
            Debug.Log("WJ1");
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("WJ2");
                if (Input.GetKeyDown(KeyCode.UpArrow)&&Walljumping==false)
                {
                    Debug.Log("WJ3");
                    WallJumpingCounter = 20;
                    Walljumping = true;
                }
            }
        }
        if (Walljumping == true && Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RD2.velocity = new Vector2(-WallJumpingCounter, 20);
                WallJumpingCounter-=1;
                Debug.Log($"Counter={WallJumpingCounter}");
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Debug.Log("UpArrow UP");
                Walljumping = false;
                RD2.velocity = new Vector2(RD2.velocity.x, 0);
            }
        }
        if (onwallR == true)
        {
            Debug.Log("WJ1");
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("WJ2");
                if (Input.GetKeyDown(KeyCode.UpArrow)&&Walljumping==false)
                {
                    Debug.Log("WJ3");
                    WallJumpingCounter = 20;
                    Walljumping = true;
                }
            }
        }
        if (WallJumpingCounter <-9)
        {
            Debug.Log("WJsetfalse");    
            Walljumping = false;
        }
        if (Walljumping == true && Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                RD2.velocity = new Vector2(WallJumpingCounter, 15);
                WallJumpingCounter-=1;
                Debug.Log($"Counter={WallJumpingCounter}");
            }
            else
            {
                Debug.Log("UpArrow UP");
                Walljumping = false;
                RD2.velocity = new Vector2(RD2.velocity.x, 0);
            }
        }
    }
    void Jumping()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onground == true)
        {
            if (Dashing == false&&Walljumping == false)
            {
                RD2.velocity = new Vector2(RD2.velocity.x, jump);
            }
        }
    }
    void Dash()
    {
        if (Input.GetKey(KeyCode.Space)&&DashWait>0.7f)
        {
            RD2.velocity = new Vector2(0,0);
            PreviousY = RD2.position.y;
            Debug.Log("Dash");
            Dashing = true;
            DashWait = 0f;
            RD2.velocity = new Vector2(RD2.velocity.x,0);
            RD2.gravityScale = 0;
            Dashcounter = 0f;
        }
        if(Dashing == true)
        {
            if (FaceRight)
                {
                    RD2.velocity = new Vector2(30,0);
                }
                else
                {
                    RD2.velocity = new Vector2(-30,0);
                }
            Dashcounter += Time.deltaTime;
            if (Dashcounter >= 0.25f)
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
}