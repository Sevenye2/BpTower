using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;


public static class SaveDataHandler
{
    private static readonly BpSaveData Template = new BpSaveData()
    {
        hp = 20,
        point = 0,
        uidMax = 2,
        upgrades = new int[32],
        nodes = new List<BpNodeSaveData>()
        {
            new BpNodeSaveData()
            {
                id = 0,
                uid = 0,
                position = new List<float>() { 200, 600 }
            },
            new BpNodeSaveData()
            {
                id = 1,
                uid = 1,
                position = new List<float>() { 500, 600 }
            },
            new BpNodeSaveData()
            {
                id = 5,
                uid = 2,
                position = new List<float>() { 800, 400 }
            }
        },
        edges = new List<BpEdgeSaveData>()
        {
            new BpEdgeSaveData()
            {
                outputUid = 0,
                outputPort = 0,
                inputUid = 1,
                inputPort = 0,
            },
            new BpEdgeSaveData()
            {
                outputUid = 1,
                outputPort = 2,
                inputUid = 2,
                inputPort = 0,
            }
        },
        level = 1
    };

    private static BpSaveData _data;

    public static BpSaveData Data
    {
        get
        {
            if (_data == null)
                Load();
            return _data;
        }
    }

    private static string Folder => Application.dataPath + "/../Save";

    private static string Path => Application.dataPath + "/../Save/saveData.json";

    public static UpgradeProperty Upgrades { get; private set; }

    public static void PropertyRefresh()
    {
        Upgrades = new UpgradeProperty();
        for (var i = 0; i < ConfigHandler.UpgradeConfig.Length; i++)
        {
            var level = Data.upgrades[i];
            ConfigHandler.UpgradeConfig[i].OnLoad?.Invoke(Upgrades, level);
        } 
    }

    public static void Renew()
    {
        Random.InitState((int)DateTime.Now.Ticks); 
        
        _data = Template;
        _data.seed = Random.Range(0, 1 << 31);
        PropertyRefresh();
        Save();
    }

    public static void Delete()
    {
        if (!File.Exists(Path))
        {
            File.Delete(Path);
        } 
    }

    public static bool Exists()
    {
        return File.Exists(Path);
    }
    
    public static void Save()
    {
        // 指针
        var json = JsonConvert.SerializeObject(Data);
        Directory.CreateDirectory(Folder);
        File.WriteAllText(Path, json);
    }

    public static void Load()
    {
        Directory.CreateDirectory(Folder);
        if (!File.Exists(Path))
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(Template));
        }

        // var json = Resources.Load<TextAsset>("JSON/StartData").text;
        var json = File.ReadAllText(Path);
        _data = JsonConvert.DeserializeObject<BpSaveData>(json);
        // random
        Random.InitState(_data.seed);
        for (var i = 0; i < _data.count; i++)
            Random.Range(0, 0);
        // property
        PropertyRefresh();
    }
}

public class UpgradeProperty
{
    public int UpdateNodeCost;
    public int RestoreHpOnStart;
    public int MoreDamagePercent;
    public int ExtraHpMax;
    public int ExtraShopCount;
    public int ExtraRare;
}