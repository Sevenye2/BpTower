using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum IOType
{
    Input,
    Output
}

public enum PortType
{
    Process,
    Amplify
}


public enum NodeStyle
{
    Root,
    Node,
    Extend
}


[Serializable]
public struct UpgradeConfig
{
    public int id;
    public string name;
    public int max;
    public int[] cost;
    public string[] notice;
    public Action<UpgradeProperty,int> OnLoad;
}

[Serializable]
public struct BpNodeConfig
{
    public int id;
    public string name;
    public string description;
    public int cost;
    public int rare;
    public int prise;
    public string className;
    public string json;
    public NodeStyle style;
    public BpPortConfig[] ports;
}

[Serializable]
public struct BpPortConfig
{
    public IOType ioType;
    public PortType portType;
    public string flag;
    public string description;
    public float weight;
}


[Serializable]
public class BpNodeSaveData
{
    public int id;
    public int uid;
    public List<float> position;

    public Vector2 GetPosition() => new Vector2(position[0], position[1]);
}

[Serializable]
public class BpEdgeSaveData
{
    public int outputUid;
    public int outputPort;
    public int inputUid;
    public int inputPort;
}

[Serializable]
public class BpSaveData
{
    public int hp;
    public int point;
    public int uidMax;
    public int[] upgrades;
    public List<BpNodeSaveData> nodes = new List<BpNodeSaveData>();
    public List<BpEdgeSaveData> edges = new List<BpEdgeSaveData>();

    public int level;
    public int seed;
    public int count;

    public int Random(int min, int max)
    {
        count++;
        return UnityEngine.Random.Range(min, max);
    }
    
    public int GetUid()
    {
        uidMax += 1;
        return uidMax;
    }
}