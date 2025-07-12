using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BlueprintBase
{
    public PlayerController Controller;

    public BpNodeSaveData Data { get; }
    protected BpNodeConfig Config => ConfigHandler.NodeConfigs[Data.id];
    protected readonly BlueprintPort[] Ports;

    // 
    private List<BlueprintBase> _process;
    private List<BlueprintBase> _amplify;
    protected BlueprintBase OnHit;
    protected BlueprintBase OnKilled;

    protected BlueprintBase(BpNodeSaveData data)
    {
        Data = data;
        Ports = new BlueprintPort[Config.ports.Length];
        for (var i = 0; i < Ports.Length; i++)
        {
            var config = Config.ports[i];
            Ports[i] = new BlueprintPort(i, config);
        }
    }

    public void Connect(int port, BlueprintBase other)
    {
        Ports[port].Node = other;
    }

    public virtual void RefreshCollection()
    {
        _process = Ports
            .Where(p => p.Config.ioType == IOType.Output)
            .Where(p => p.Config.portType == PortType.Process)
            .Where(p => p.Config.flag == "Main")
            .Where(p => p.Node != null)
            .Select(p => p.Node)
            .ToList();

        _amplify = Ports
            .Where(p => p.Config.ioType == IOType.Input)
            .Where(p => p.Config.portType == PortType.Amplify)
            .Where(p => p.Node != null)
            .Select(p => p.Node)
            .ToList();

        OnHit = Ports
            .FirstOrDefault(p =>
            {
                var output = p.Config.ioType == IOType.Output;
                var type = p.Config.portType == PortType.Process;
                var flag = p.Config.flag == "OnHit";
                return output && type && flag && p.Node != null; 
            })?.Node;
        
        OnKilled = Ports
            .FirstOrDefault(p =>
            {
                var output = p.Config.ioType == IOType.Output;
                var type = p.Config.portType == PortType.Process;
                var flag = p.Config.flag == "OnKilled";
                return output && type && flag && p.Node != null; 
            })?.Node; 
    }

    public virtual void RefreshValues()
    {
    }

    protected virtual float CalculateCosts()
    {
        return Config.cost + _process.Sum(n => n.CalculateCosts());
    }

    public virtual void DoNext(RuntimeData data)
    {
        foreach (var next in _process)
        {
            next.DoNext(data);
        }
    }

    public virtual ValueProperty GetProperty(ValueProperty data)
    {
        return _amplify.Aggregate(data, (current, node) => node.GetProperty(current));
    }
}

public class BlueprintPort
{
    public readonly BpPortConfig Config;
    public BlueprintBase Node;
    public int Index;

    public BlueprintPort(int index, BpPortConfig config)
    {
        Index = index;
        Config = config;
    }
}

public class RuntimeData
{
    public Vector3 Position;
    public EnemyViewer Enemy;
}

public class ValueProperty
{
    public float Value;
    public float ExFix;
    public float ExPercent;

    public float Result => Value + ExFix + Value * ExPercent;

    public ValueProperty(float value)
    {
        Value = value;
    }
}

public interface IBpRoot
{
    public void Recalculate();
}

public interface IBlueprintUpdate : IBpRoot
{
    public void OnUpdate();
}