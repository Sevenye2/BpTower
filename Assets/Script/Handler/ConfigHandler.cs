using System;
using System.Collections.Generic;
using System.IO;
using Framework;
using UnityEngine;
using Newtonsoft.Json;

public static class ConfigHandler
{
    public static readonly List<BpNodeConfig> NodeConfigs = new List<BpNodeConfig>()
    {
        new()
        {
            id = 0,
            name = "Loop",
            description = "Execute Per Some Time",
            cost = 1000,
            prise = 10000,
            rare =  6,
            className = "BpUpdate",
            style = NodeStyle.Root,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "Main",
                    weight = 1
                }
            },
        },
        new()
        {
            id = 1,
            name = "Shoot Bullet",
            description = "Attack <3m enemy",
            cost = 0,
            prise = 1000,
            rare =  2,
            className = "BpBullet",
            style = NodeStyle.Node,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                    weight = 1
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Distance",
                    description = "Distance",
                    weight = 1
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "OnHit",
                    description = "On Hit",
                }
            },
        },
        new()
        {
            id = 2,
            name = " +1",
            description = "+1",
            cost = 0,
            prise = 100,
            rare = 1,
            className = "BpAmplify",
            json = "{Fix: 1, Percent:0}",
            style = NodeStyle.Extend,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Amplify,
                }
            }
        },
        new()
        {
            id = 3,
            name = "Parallel 2",
            description = "",
            cost = 0,
            prise = 500,
            rare = 2,
            className = "BpParallel",
            style = NodeStyle.Extend,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "Main",
                    weight = 1
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "Main",
                    weight = 1
                }
            }
        },
        new()
        {
            id = 4,
            name = "Merge 2",
            description = "",
            cost = 0,
            prise = 200,
            rare = 2,
            className = "BpMerge",
            style = NodeStyle.Extend,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Amplify,
                }
            }
        },
        new()
        {
            id = 5,
            name = "Damage",
            description = "Deal 5 Damage",
            cost = 300,
            prise = 1000,
            rare = 3,
            className = "BpDamage",
            json ="{Value: 5}",
            style = NodeStyle.Node,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Damage",
                    description = "Damage",
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "OnKilled",
                    description = "On Killed",
                    weight = 0.8f,
                }
            }
        },
        new()
        {
            id = 6,
            name = "Wheel",
            description = "Generate Rotate Wheel",
            cost = 0,
            prise = 2000,
            rare = 3,
            className = "BpWheel",
            json = null,
            style = NodeStyle.Node,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "OnHit",
                    description = "On Hit",
                }
            }
        },
        new()
        {
            id = 7,
            name = "Doom",
            description = "Generate Doom Atk Range",
            cost = 0,
            prise = 8000,
            rare = 5,
            className = "BpDoom",
            json = null,
            style = NodeStyle.Node,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "OnHit",
                    description = "On Hit",
                }
            }
        },
        new()
        {
            id = 8,
            name = "Ray",
            description = "Generate Ray",
            cost = 0,
            prise = 5000,
            rare = 4,
            className = "BpRay",
            json = null,
            style = NodeStyle.Node,
            ports = new[]
            {
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Process,
                },
                new BpPortConfig()
                {
                    ioType = IOType.Output,
                    portType = PortType.Process,
                    flag = "OnHit",
                    description = "On Hit",
                }
            }
        },
    };

    public static readonly UpgradeConfig[] UpgradeConfig = new[]
    {
        new UpgradeConfig()
        {
            id = 0,
            name = "Optimize",
            cost = 1000,
            max = 3,
            notice = new[]
            {
                "lv.1 Update node Reduce <color=blue>100ms</color>",
                "lv.2 Update node Reduce <color=blue>300ms</color>",
                "lv.max Update node Reduce <color=blue>500ms</color>"
            },
            OnLoad = (p, level) => { p.UpdateNodeCost -= (100 + 200 * level); }
        },
        new UpgradeConfig()
        {
            id = 1,
            name = "Fix",
            cost = 1000,
            max = 3,
            notice = new[]
            {
                "lv.1 Restore <color=blue>2</color> Hp On Turn Start ",
                "lv.2 Restore <color=blue>5</color> Hp On Turn Start ",
                "lv.max Restore <color=blue>10</color> Hp On Turn Start ",
            },
            OnLoad = (p, level) =>

            {
                var value = level switch
                {
                    1 => 2,
                    2 => 5,
                    3 => 10,
                    _ => 0
                };
                p.RestoreHpOnStart += value;
            }
        },
        new UpgradeConfig()
        {
            id = 2,
            name = "Power Enhance",
            cost = 1000,
            max = 3,
            notice = new[]
            {
                "lv.1 Deal More <color=blue>5%</color> Damage",
                "lv.2 Deal More <color=blue>10%</color> Damage",
                "lv.max Deal More <color=blue>20%</color> Damage",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 20,
                    _ => 0
                };
                p.MoreDamagePercent += value;
            }
        },
        new UpgradeConfig()
        {
            id = 3,
            name = "Expansion",
            cost = 1000,
            max = 3,
            notice = new[]
            {
                "lv.1 +<color=blue>5</color>HP Max",
                "lv.2 +<color=blue>10</color>HP Max",
                "lv.max +<color=blue>20</color>HP Max",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 20,
                    _ => 0
                };
                p.ExtraHpMax += value;
            }
        },
        new UpgradeConfig()
        {
            id = 4,
            name ="Enhance Store",
            cost = 1000,
            max = 6,
            notice = new[]
            {
                "lv.1 Shop Rare <color=blue>2</color>",
                "lv.2 Shop Rare <color=blue>3</color>",
                "lv.3 Shop Rare <color=blue>4</color>",
                "lv.4 Shop Rare <color=blue>5</color>",
                "lv.5 Shop Rare <color=blue>6</color>",
                "lv.max Shop Rare <color=blue>7</color>",
            },
            OnLoad = (p, level) =>
            {

                p.ExtraRare = level + 1;
            }     
            
        },        new UpgradeConfig()
        {
            id = 5,
            name ="Store Count++",
            cost = 1000,
            max = 5,
            notice = new[]
            {
                "lv.1 Shop has <color=blue>4</color> Item ",
                "lv.2 Shop has <color=blue>5</color> Item ",
                "lv.3 Shop has <color=blue>6</color> Item ",
                "lv.4 Shop has <color=blue>7</color> Item ",
                "lv.max Shop has <color=blue>8</color> Item ",
            },
            OnLoad = (p, level) =>
            {
                p.ExtraShopCount += level;
            }     
            
        }
    };
}