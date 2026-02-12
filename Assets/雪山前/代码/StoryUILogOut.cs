using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class StoryUILogOut : MonoBehaviour
{
    // Start is called before the first frame update
    public int ChapterIndex;   // 章节索引
    public int SceneIndex;     // 场景索引
    public int ClipIndex;      // 剧情片段索引
    public int NumberIndex;    // 序号索引
    public bool StoryPlay;
    public int CurrentNumberIndex;
    public bool Nextpage;
    
    void Start()
    {
        StoryPlay = false;
        NumberIndex = 1;
        ChapterIndex = SceneIndex = ClipIndex = 0;
    }
    void NextPage()
    {
        StoryRow currentRow = PlotPlayer.Instance.GetStoryRow(
            ChapterIndex, 
            SceneIndex, 
            ClipIndex, 
            CurrentNumberIndex
            );
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto1 != 0)
            {
                CurrentNumberIndex = 1;
                NumberIndex = 2;
                ClipIndex = currentRow.Goto1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto2 != 0)
            {
                CurrentNumberIndex = 1;
                NumberIndex = 2;
                ClipIndex = currentRow.Goto2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto3 != 0)
            {
                CurrentNumberIndex = 1;
                NumberIndex = 2;
                ClipIndex = currentRow.Goto3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto4 != 0)
            {
                CurrentNumberIndex = 1;
                NumberIndex = 2;
                ClipIndex = currentRow.Goto4;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (StoryPlay)
        {
            NextPage();
            ArtLoader currentIllustration = ArtLoaderBehaviors.Instance.GetArtLoadingList("AXY");
            StoryRow currentRow = PlotPlayer.Instance.GetStoryRow(
            ChapterIndex, 
            SceneIndex, 
            ClipIndex, 
            CurrentNumberIndex
            );
            if (currentRow != null)
            {
                if (Nextpage)
                    {
                        Nextpage = !Nextpage;
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
                ChapterIndex = SceneIndex = ClipIndex = 0;
                NumberIndex = 1;
                StoryPlay = false;
            }
        }
        CurrentNumberIndex = NumberIndex;
    }
}
