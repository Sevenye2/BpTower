using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


public static class SaveDataHandler
{
    private static readonly BpSaveData SaveData = new BpSaveData()
    {
        hp = 20,
        point = 10000,
        uidMax = 5,
        nodes = new List<BpNodeSaveData>()
        {
            new BpNodeSaveData()
            {
                id = 0,
                uid = 0,
                position = new List<float>() { 400, 200 }
            },
            new BpNodeSaveData()
            {
                id = 1,
                uid = 1,
                position = new List<float>() { 700, 200 }
            },
            new BpNodeSaveData()
            {
                id = 2,
                uid = 2,
                position = new List<float>() { 400, 400 }
            },
            new BpNodeSaveData()
            {
                id = 3,
                uid = 3,
                position = new List<float>() { 700, 400 }
            },
            new BpNodeSaveData()
            {
                id = 4,
                uid = 4,
                position = new List<float>() { 400, 600 }
            },
            new BpNodeSaveData()
            {
                id = 1,
                uid = 5,
                position = new List<float>() { 700, 600 }
            }
            
        },
    };

    private static BpSaveData _temp;

    public static BpSaveData Temp
    {
        get
        {
            if (_temp == null)
                Load();
            return _temp;
        }
    }

    private static string Folder => Application.dataPath + "/../Save";

    private static string Path => Application.dataPath + "/../Save/saveData.json";

    public static void Save()
    {
        // 指针
        var json = JsonConvert.SerializeObject(Temp);
        Directory.CreateDirectory(Folder);
        File.WriteAllText(Path, json);
        
        Debug.Log(Temp);
    }

    public static void Load()
    {
        Directory.CreateDirectory(Folder);
        if (!File.Exists(Path))
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(SaveData));
        }

        // var json = Resources.Load<TextAsset>("JSON/StartData").text;
        var json = File.ReadAllText(Path);
        _temp = JsonConvert.DeserializeObject<BpSaveData>(json);
    }
}