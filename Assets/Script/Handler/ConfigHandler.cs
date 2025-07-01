using System;
using System.Collections.Generic;
using System.IO;
using Framework;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigHandler : Singleton<ConfigHandler>
{
    // public readonly List<BpNodeConfig> NodeConfigs = new List<BpNodeConfig>()
    // {
    //     new()
    //     {
    //         id = 0,
    //         name = "Loop",
    //         description = "Execute Per Some Time",
    //         cost = 1000,
    //         className = "BpUpdate",
    //         style = NodeStyle.Root,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Process,
    //                 flag = "Main",
    //                 weight = 1
    //             }
    //         },
    //     },
    //     new()
    //     {
    //         id = 1,
    //         name = "Shoot Bullet",
    //         description = "Attack <3m enemy",
    //         cost = 1000,
    //         prise = 1000,
    //         className = "BpBulletAtk",
    //         style = NodeStyle.Node,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Process,
    //                 weight = 1
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Amplify,
    //                 flag = "Distance",
    //                 description = "Distance",
    //                 weight = 1
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Process,
    //                 flag = "OnHit",
    //                 description = "On Hit",
    //             }
    //         },
    //     },
    //     new()
    //     {
    //         id = 2,
    //         name = " +1",
    //         description = "+1",
    //         cost = 0,
    //         rare = 0,
    //         prise = 500,
    //         className = "BpDistanceAmplify",
    //         json = "{Fix: 1, Percent:0}",
    //         style = NodeStyle.Extend,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Amplify,
    //             }
    //         }
    //     },
    //     new()
    //     {
    //         id = 3,
    //         name = "Parallel 2",
    //         description = "",
    //         cost = 100,
    //         prise = 500,
    //         className = "BpParallel",
    //         style = NodeStyle.Extend,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Process,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Process,
    //                 flag = "Main",
    //                 weight = 1
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Process,
    //                 flag = "Main",
    //                 weight = 1
    //             }
    //         }
    //     },
    //     new()
    //     {
    //         id = 4,
    //         name = "Merge 2",
    //         description = "",
    //         cost = 0,
    //         prise = 500,
    //         className = "BpMerge",
    //         style = NodeStyle.Extend,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Amplify,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Amplify,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Amplify,
    //             }
    //         }
    //     },
    //     new()
    //     {
    //         id = 5,
    //         name = "Damage",
    //         description = "Deal 5 Damage",
    //         cost = 300,
    //         prise = 500,
    //         className = "BpDamage",
    //         style = NodeStyle.Node,
    //         ports = new[]
    //         {
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Process,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Process,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Process,
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Input,
    //                 portType = PortType.Amplify,
    //                 flag = "Damage",
    //                 description = "Damage",
    //             },
    //             new BpPortConfig()
    //             {
    //                 ioType = IOType.Output,
    //                 portType = PortType.Process,
    //                 flag = "OnKilled",
    //                 description = "On Killed",
    //                 weight = 0.8f,
    //             }
    //         }
    //     }
    // };

    
    public readonly List<BpNodeConfig> NodeConfigs = new List<BpNodeConfig>();
    public ConfigHandler()
    {
        var path = Application.dataPath + "/../Save/Config.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            NodeConfigs = JsonConvert.DeserializeObject<List<BpNodeConfig>>(json);
        }
        else
        {
            var json = JsonConvert.SerializeObject(NodeConfigs);
            Debug.Log(json);
            File.WriteAllText(path, json);
        }
    }
}