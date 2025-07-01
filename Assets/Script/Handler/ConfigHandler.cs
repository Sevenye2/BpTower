using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigHandler : Singleton<ConfigHandler>
{
    public readonly List<BpPortConfig> PortConfigs = new List<BpPortConfig>()
    {
        new() // 0
        {
            type = IOType.Input,
            flag = PortFlag.Process,
        },
        new() // 1
        {
            type = IOType.Output,
            flag = PortFlag.Process,
            port = PortType.Main
        },
        new() // 2
        {
            type = IOType.Output,
            flag = PortFlag.Process,
            port = PortType.OnHit,
            description = "On Hit"
        },
        new() // 3
        {
            type = IOType.Output,
            flag = PortFlag.Process,
            port = PortType.OnKilled,
            description = "On Killed"
        },
        new() // 4
        {
            type = IOType.Output,
            flag = PortFlag.Amplify,
        },
        new() // 5
        {
            type = IOType.Input,
            flag = PortFlag.Amplify,
        },
        new() // 6
        {
            type = IOType.Input,
            flag = PortFlag.Amplify,
            port = PortType.Damage,
            description = "Damage"
        },
        new() // 7
        {
            type = IOType.Input,
            flag = PortFlag.Amplify,
            port = PortType.Distance,
            description = "Distance"
        },
        new() // 8
        {
            type = IOType.Input,
            flag = PortFlag.Amplify,
            port = PortType.Radius,
            description = "Radius"
        },
    };


    public readonly List<BpNodeConfig> NodeConfigs = new List<BpNodeConfig>()
    {
        new()
        {
            id = 0,
            name = "Loop",
            description = "Execute Per Some Time",
            cost = 1000,
            className = "BpUpdate",
            style = NodeStyle.Root,
            ports = new[] { 1 },
        },
        new()
        {
            id = 1,
            name = "Shoot Bullet",
            description = "Attack <3m enemy",
            cost = 1000,
            prise = 1000,
            className = "BpBulletAtk",
            style = NodeStyle.Node,
            ports = new[] { 0, 4, 2, 3 },
        },
        new()
        {
            id = 2,
            name = "Distance +1m",
            description = "distance +1m",
            cost = 0,
            rare = 0,
            prise = 500,
            className = "BpDistanceAmplify",
            json = "{Fix: 1, Percent:0}",
            style = NodeStyle.Extend,
            ports = new[] { 5 }
        },
        new()
        {
            id = 3,
            name = "Parallel 2",
            description = "",
            cost = 100,
            prise = 500,
            className = "BpParallel",
            style = NodeStyle.Extend,
            ports = new[] { 0, 1, 1 }
        },
        new()
        {
            id = 4,
            name = "Merge 2",
            description = "",
            cost = 0,
            prise = 500,
            className = "BpMerge",
            style = NodeStyle.Extend,
            ports = new[] { 4, 4, 5 }
        }
    };

    public ConfigHandler()
    {
        string json;
        var asset = Resources.Load<TextAsset>($"JSON/NodeConfig");
        if (!asset)
        {
            json = JsonConvert.SerializeObject(NodeConfigs);
            Debug.Log(json);
            return;
        }

        json = asset.text;
        NodeConfigs = JsonConvert.DeserializeObject<List<BpNodeConfig>>(json);
    }
}