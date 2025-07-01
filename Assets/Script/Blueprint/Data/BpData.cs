using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum IOType
{
    Input,
    Output
}

public enum PortFlag
{
    Process,
    Amplify
}

public enum PortType
{
    Main,
    OnKilled,
    OnHit,
    Damage,
    Distance,
    Radius
}

public enum NodeStyle
{
    Root,
    Node,
    Extend
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
    public int[] ports;
}

[Serializable]
public struct BpPortConfig
{
    public IOType type;
    public PortFlag flag;
    public PortType port;
    public string description;
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
    public List<BpNodeSaveData> nodes = new List<BpNodeSaveData>();
    public List<BpEdgeSaveData> edges = new List<BpEdgeSaveData>();

    public int GetUid()
    {
        uidMax += 1;
        return uidMax;
    }
}