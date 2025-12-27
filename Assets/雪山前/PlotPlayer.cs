using System;
using System.Collections.Generic;
using UnityEngine;

// 1. 先定义一个类，代表 CSV 中的“一行”
// 必须加上 [System.Serializable]，否则 Unity 不会在 Inspector 里显示它
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
    // 2. 用我们定义的 StoryRow 类的列表来存储所有数据
    // 这样在 Inspector 里就会显示为一个可展开的列表，每一项都有 Chapter, Name 等字段
    public List<StoryRow> StoryDataList = new List<StoryRow>();
    public static PlotPlayer Instance;
    public TextAsset csvAsset;

    // 我加了一个右键菜单功能，方便你在编辑器里直接点击加载测试
    [ContextMenu("从 CSV 加载数据")]
    public void LoadFromCsv()
    {
        if (csvAsset == null)
        {
            Debug.LogError("请先把 CSV 文件拖到 Inspector 的 Csv Asset 槽里！");
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
        LoadFromCsv();
    }
    void Awake()
    {
        Instance = this;
    }
}
