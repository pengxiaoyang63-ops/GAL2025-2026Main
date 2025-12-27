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
    private int NumberIndex;    // 序号索引
    public bool StoryPlay;
    public int CurrentNumberIndex;
    // Start is called before the first frame update
    void Start()
    {
        NumberIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        NextPage();
        if (StoryPlay)
        {
            StoryRow currentRow = PlotPlayer.Instance.GetStoryRow(
            ChapterIndex, 
            SceneIndex, 
            ClipIndex, 
            NumberIndex
        );
        if (currentRow != null)
        {
            if (CurrentNumberIndex != NumberIndex)
                {
                    Debug.Log("=== Play ===");
                    Debug.Log($"Name:{currentRow.Name}");
                    Debug.Log($"Text {currentRow.Text}");
                    Debug.Log($"Re1 {currentRow.Re1}");
                    Debug.Log($"Re2 {currentRow.Re2}");
                    Debug.Log($"Re3 {currentRow.Re3}");
                    Debug.Log($"Re4 {currentRow.Re4}"); 
                }
        }
        else
        {
            StoryPlay=false;
        }
        }
        CurrentNumberIndex = NumberIndex;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLayer Enter. Press J to continue.");
            StoryPlay=true;
        }
    }
    void NextPage()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            NumberIndex++;
        }
    }
}

