using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundLoader
{
    public string Background;    
    public string Backgroundpath;
}

public class BackgroundLocations : MonoBehaviour
{
    public List<BackgroundLoader> BackgroundLoadingList = new List<BackgroundLoader>();
    public static BackgroundLocations Instance;
    public TextAsset BackgroundLocation;
    [ContextMenu("Load from CSV")]
    public void LoadFromCsv()
    {
        if (BackgroundLocation == null)
        {
            Debug.LogError("Can't find BackgroundCSV");
            return;
        }

        // 清空旧数据
        BackgroundLoadingList.Clear();

        // 读取文本（建议用 .text 而不是 .ToString()）
        string csvText = BackgroundLocation.text;
        
        // 按行拆分 (同时处理 \r\n 和 \n)
        string[] lines = csvText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            if (line.Contains("Illustration")) 
            {
                continue;
            }
            string[] cols = line.Split(',');

            BackgroundLoader row = new BackgroundLoader();
            row.Background = cols[0].Trim();
            row.Backgroundpath = cols[1].Trim();

            // 添加到列表中
            BackgroundLoadingList.Add(row);
        }

        Debug.Log($"CSV 加载完成，共 {BackgroundLoadingList.Count} 条数据。");
    }
    public BackgroundLoader GetBackgroundLoadingList(string background)
    {
    // 使用 Find 方法查找第一个符合所有条件的数据
    // r 代表 List 里的每一行
        BackgroundLoader row = BackgroundLoadingList.Find(r => 
        r.Background ==  background
        );

        if (row == null)
        {
            Debug.LogWarning($"Asset Not Found! Name:{background}");
        }

        return row;
    }
    void Start()
    {
        BackgroundLocation = Resources.Load<TextAsset>("TextAssets/CSV Backgrounds");
        LoadFromCsv();
    }
    void Awake()
    {
        Instance = this;
    }
}
