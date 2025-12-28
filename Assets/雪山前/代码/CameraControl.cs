using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public StoryUI storyUI;
    public bool FollowPlayer;
    public Transform Playertransform;
    public float CamX;
    public float CamY;
    public float PlayerX;
    public float PlayerY;
    public float CamIndex;
    public bool fixY;
    public bool fixX;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Obj = GameObject.Find("Player");
        Playertransform = Obj.GetComponent<Transform>();
        Obj = GameObject.Find("StoryPlayTesting");
        storyUI = Obj.GetComponent<StoryUI>();
        FollowPlayer = true;
        CamIndex = 0;
        fixX = false;
        fixY = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition();
        StoryPlayCheck();
        SetPostion();
    }
    void StoryPlayCheck()
    {
        if (storyUI.StoryPlay)
            {
                FollowPlayer = false;
                CamIndex = 0.5f;
            }
            else
            {
                FollowPlayer = true;
            }
    }
    void SetPostion()
    {
        if (!fixX)
        {
            transform.position = new Vector2(PlayerX+(CamX-PlayerX)*CamIndex,transform.position.y);
        }
        if (!fixY)
        {
            transform.position = new Vector2(transform.position.x,PlayerY+(CamY-PlayerY)*CamIndex);
        }
    }
    void UpdatePosition()
    {
        PlayerX = Playertransform.position.x;
        PlayerY = Playertransform.position.y;
        if (FollowPlayer)
        {
            CamX = transform.position.x;
            CamY = transform.position.y;
        }
    }
}
