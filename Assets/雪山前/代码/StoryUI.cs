using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryUI : MonoBehaviour
{
    public int ChapterIndex;   // 章节索引
    public int SceneIndex;     // 场景索引
    public int ClipIndex;      // 剧情片段索引
    public CameraControl cameraControl;
    public StoryUILogOut storyUILogOut;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Obj = GameObject.Find("CameraController");
        cameraControl = Obj.GetComponent<CameraControl>();
        Obj = GameObject.Find("StoryUILogOuter");
        storyUILogOut = Obj.GetComponent<StoryUILogOut>();
    }

    // Update is called once per frame
    void Update()
    {
        if (storyUILogOut.StoryPlay == true)
        {
            if (storyUILogOut.ChapterIndex == ChapterIndex)
            {
                if (storyUILogOut.SceneIndex == SceneIndex)
                {
                    if (storyUILogOut.ClipIndex == ClipIndex)
                    {
                        cameraControl.CamX = transform.position.x;
                        cameraControl.CamY = transform.position.y;
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLayer Enter. Press J to continue.");
            cameraControl.CamX = transform.position.x;
            cameraControl.CamY = transform.position.y;
            cameraControl.CamIndex = 0.5f;
            storyUILogOut.StoryPlay = true;
            storyUILogOut.NumberIndex = 1;
            storyUILogOut.CurrentNumberIndex = 1;
            SetIndex();
        }
    }
    void SetIndex()
    {
        if (storyUILogOut.ChapterIndex == 0 & storyUILogOut.SceneIndex == 0 & storyUILogOut.ClipIndex == 0)
        {
            storyUILogOut.ChapterIndex = ChapterIndex;
            storyUILogOut.SceneIndex = SceneIndex;
            storyUILogOut.ClipIndex = ClipIndex;
        }
    }
}

