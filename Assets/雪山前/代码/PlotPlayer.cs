using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryRow
{
    public int Chapter;   // 章节
    public int Scene;     // 场景
    public int Clip;      // 剧情片段
    public int Number;    // 序号
    public string Name;   // 说话人名字
    [TextArea(3, 10)]    // 加上这个特性，文本框会变高，方便编辑长文本
    public string Text;   // 剧情文本内容
    public string Re1;    // 选项/回复1
    public string Re2;    // 选项/回复2
    public string Re3;    // 选项/回复3
    public string Re4;    // 选项/回复4
}

public class PlotPlayer : MonoBehaviour
{
    public List<StoryRow> StoryDataList = new List<StoryRow>();
    public static PlotPlayer Instance;
    public TextAsset csvAsset;
    [ContextMenu("Load from CSV")]
    public void LoadFromCsv()
    {
        if (csvAsset == null)
        {
            Debug.LogError("Can't find CSV");
            return;
        }

        // 清空旧数据
        StoryDataList.Clear();

        // 读取文本（建议用 .text 而不是 .ToString()）
        string csvText = csvAsset.text;
        
        // 按行拆分 (同时处理 \r\n 和 \n)
        string[] lines = csvText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            if (line.Contains("Chapter")) 
            {
                continue;
            }
            string[] cols = line.Split(',');
            if (cols.Length < 10) continue;

            StoryRow row = new StoryRow();
            int.TryParse(cols[0].Trim(), out row.Chapter);
            int.TryParse(cols[1].Trim(), out row.Scene);
            int.TryParse(cols[2].Trim(), out row.Clip);
            int.TryParse(cols[3].Trim(), out row.Number);
            
            row.Name = cols[4].Trim();
            row.Text = cols[5].Trim();
            row.Re1  = cols[6].Trim();
            row.Re2  = cols[7].Trim();
            row.Re3  = cols[8].Trim();
            row.Re4  = cols[9].Trim();

            // 添加到列表中
            StoryDataList.Add(row);
        }

        Debug.Log($"CSV 加载完成，共 {StoryDataList.Count} 条数据。");
    }
    public StoryRow GetStoryRow(int chapter, int scene, int clip, int number)
    {
    // 使用 Find 方法查找第一个符合所有条件的数据
    // r 代表 List 里的每一行
    StoryRow row = StoryDataList.Find(r => 
        r.Chapter == chapter && 
        r.Scene   == scene   && 
        r.Clip    == clip    && 
        r.Number  == number
    );

    if (row == null)
    {
        Debug.LogWarning($"未找到剧情数据: Ch{chapter} Sc{scene} Cl{clip} No{number}");
    }

    return row;
}
    void Start()
    {
        csvAsset = Resources.Load<TextAsset>("TextAssets/CSV test");
        LoadFromCsv();
    }
    void Awake()
    {
        Instance = this;
    }
}
