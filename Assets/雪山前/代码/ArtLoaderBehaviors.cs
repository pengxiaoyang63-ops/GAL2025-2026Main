using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArtLoader
{
    public string Illustration;    
    public string IlluLocation;
}

public class ArtLoaderBehaviors : MonoBehaviour
{
    public List<ArtLoader> ArtLoadingList = new List<ArtLoader>();
    public static ArtLoaderBehaviors Instance;
    public TextAsset ArtLocations;
    [ContextMenu("Load from CSV")]
    public void LoadFromCsv()
    {
        if (ArtLocations == null)
        {
            Debug.LogError("Can't find CSV");
            return;
        }

        // 清空旧数据
        ArtLoadingList.Clear();

        // 读取文本（建议用 .text 而不是 .ToString()）
        string csvText = ArtLocations.text;
        
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

            ArtLoader row = new ArtLoader();
            row.Illustration = cols[0].Trim();
            row.IlluLocation = cols[1].Trim();

            // 添加到列表中
            ArtLoadingList.Add(row);
        }

        Debug.Log($"CSV 加载完成，共 {ArtLoadingList.Count} 条数据。");
    }
    public ArtLoader GetArtLoadingList(string illustration)
    {
    // 使用 Find 方法查找第一个符合所有条件的数据
    // r 代表 List 里的每一行
        ArtLoader row = ArtLoadingList.Find(r => 
        r.Illustration ==  illustration
        );

        if (row == null)
        {
            Debug.LogWarning($"Asset Not Found! Name:{illustration}");
        }

        return row;
    }
    void Start()
    {
        ArtLocations = Resources.Load<TextAsset>("TextAssets/CSV ArtLocations");
        LoadFromCsv();
    }
    void Awake()
    {
        Instance = this;
    }
}
