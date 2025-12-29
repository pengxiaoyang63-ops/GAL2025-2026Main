using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

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
            SetIndex();
        }
    }
    void SetIndex()
    {
        storyUILogOut.ChapterIndex = ChapterIndex;
        storyUILogOut.SceneIndex = SceneIndex;
        storyUILogOut.ClipIndex = ClipIndex;
    }
}

