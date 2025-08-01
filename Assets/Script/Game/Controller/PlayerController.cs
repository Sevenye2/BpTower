using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

public class PlayerController
{
    public PlayerViewer Viewer;
    private readonly List<IBpRoot> _executors = new();
    private int _hpMax;
    private static int Hp
    {
        get => SaveDataHandler.Data.hp;
        set => SaveDataHandler.Data.hp = value;
    }

    public void BeAttacked(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            ProcessController.Instance.Lose();
    }
    

    public void ReLoad(BpSaveData data, PlayerViewer viewer)
    {
        SaveDataHandler.PropertyRefresh();
        
        Viewer = viewer;
        viewer.Controller = this;

        var nodes = new List<BlueprintBase>();
        foreach (var n in data.nodes.Select(Factory.CreateBlueprint))
        {
            n.Controller = this;
            nodes.Add(n);

            if (n is IBpRoot executor)
                _executors.Add(executor);
        }

        foreach (var edge in data.edges)
        {
            var input = nodes.Find(n => n.Data.uid == edge.inputUid);
            var output = nodes.Find(n => n.Data.uid == edge.outputUid);
            var inputPort = edge.inputPort;
            var outputPort = edge.outputPort;

            input.Connect(inputPort, output);
            output.Connect(outputPort, input);
        }

        // update

        foreach (var node in nodes)
        {
            node.RefreshCollection();
        }
        foreach (var node in nodes)
        {
            node.RefreshValues();
        } 
        foreach (var executor in _executors)
        {
            executor.Recalculate();
        }
    }

    public void OnStart()
    {
        _hpMax = 20 + SaveDataHandler.Upgrades.ExtraHpMax;
        Hp = Mathf.Clamp(Hp + SaveDataHandler.Upgrades.RestoreHpOnStart, 0, _hpMax);
    }

    public void Run()
    {
        GlobalUI.Instance.battleUI.LogHp(Hp, _hpMax);
        
        _executors.OfType<IBlueprintUpdate>()
            .ToList()
            .ForEach(update => update.OnUpdate());
    }

}

