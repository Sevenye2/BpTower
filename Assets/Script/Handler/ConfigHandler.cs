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
            rare = 6,
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
            rare = 2,
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
        new() // dmg 5
        {
            id = 5,
            name = "Damage",
            description = "Deal 5 Damage",
            cost = 300,
            prise = 1000,
            rare = 3,
            className = "BpDamage",
            json = "{Value: 5}",
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
        new() // wheel
        {
            id = 6,
            name = "Wheel",
            description = "Generate Rotate Wheel",
            cost = 0,
            prise = 2000,
            rare = 4,
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
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Amount",
                    description = "Amount",
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Duration",
                    description = "Duration",
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Radius",
                    description = "Radius",
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Distance",
                    description = "Distance",
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Speed",
                    description = "Speed",
                },
            }
        },
        new() // doom
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
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Size",
                    description = "Size",
                },
            }
        },
        new()
        {
            id = 8,
            name = "Ray",
            description = "Generate Ray",
            cost = 0,
            prise = 5000,
            rare = 3,
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
                },
                new BpPortConfig()
                {
                    ioType = IOType.Input,
                    portType = PortType.Amplify,
                    flag = "Width",
                    description = "Width",
                },
            }
        },
        new()
        {
            id = 9,
            name = " +2",
            description = "+2",
            cost = 0,
            prise = 300,
            rare = 2,
            className = "BpAmplify",
            json = "{Fix: 2, Percent:0}",
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
            id = 10,
            name = " +5",
            description = "+5",
            cost = 0,
            prise = 800,
            rare = 3,
            className = "BpAmplify",
            json = "{Fix: 5, Percent:0}",
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
            id = 10,
            name = " +10",
            description = "+10",
            cost = 0,
            prise = 800,
            rare = 4,
            className = "BpAmplify",
            json = "{Fix: 10, Percent:0}",
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
            id = 11,
            name = " +20",
            description = "+20",
            cost = 0,
            prise = 2000,
            rare = 4,
            className = "BpAmplify",
            json = "{Fix: 20, Percent:0}",
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
            id = 11,
            name = " +50",
            description = "+50",
            cost = 0,
            prise = 6000,
            rare = 5,
            className = "BpAmplify",
            json = "{Fix: 50, Percent:0}",
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
            id = 10,
            name = " +100",
            description = "+100",
            cost = 0,
            prise = 10000,
            rare = 6,
            className = "BpAmplify",
            json = "{Fix: 100, Percent:0}",
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
    };

    public static readonly UpgradeConfig[] UpgradeConfig = new[]
    {
        new UpgradeConfig()
        {
            id = 0,
            name = "Optimize",
            max = 3,
            cost = new[] { 1000, 2000, 5000 },
            notice = new[]
            {
                "lv.1 Update node Reduce <color=blue>100ms</color>",
                "lv.2 Update node Reduce <color=blue>300ms</color>",
                "lv.max Update node Reduce <color=blue>500ms</color>"
            },
            OnLoad = (p, level) => { p.ReduceUpdate -= (100 + 200 * level); }
        },
        new UpgradeConfig()
        {
            id = 1,
            name = "Hp Restore",
            max = 3,
            cost = new[] { 1000, 2000, 5000 },
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
            name = "Damage%+",
            max = 10,
            cost = new[] { 1000, 2000, 5000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, },
            notice = new[]
            {
                "lv.1 Deal More <color=blue>5%</color> Damage",
                "lv.2 Deal More <color=blue>10%</color> Damage",
                "lv.3 Deal More <color=blue>20%</color> Damage",
                "lv.4 Deal More <color=blue>30%</color> Damage",
                "lv.5 Deal More <color=blue>40%</color> Damage",
                "lv.6 Deal More <color=blue>50%</color> Damage",
                "lv.7 Deal More <color=blue>100%</color> Damage",
                "lv.8 Deal More <color=blue>150%</color> Damage",
                "lv.9 Deal More <color=blue>200%</color> Damage",
                "lv.max Deal More <color=blue>300%</color> Damage",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 20,
                    4 => 30,
                    5 => 40,
                    6 => 50,
                    7 => 100,
                    8 => 150,
                    9 => 200,
                    10 => 300,
                    _ => 0
                };
                p.ExtraDamagePercent += value;
            }
        },
        new UpgradeConfig()
        {
            id = 3,
            name = "HP Max++",
            max = 3,
            cost = new[] { 1000, 2000, 5000 },
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
            name = "Store Rare++",
            max = 6,
            cost = new[] { 1000, 2000, 5000, 10000, 20000, 50000 },
            notice = new[]
            {
                "lv.1 Shop Rare <color=blue>2</color>",
                "lv.2 Shop Rare <color=blue>3</color>",
                "lv.3 Shop Rare <color=blue>4</color>",
                "lv.4 Shop Rare <color=blue>5</color>",
                "lv.5 Shop Rare <color=blue>6</color>",
                "lv.max Shop Rare <color=blue>7</color>",
            },
            OnLoad = (p, level) => { p.ExtraRare = level; }
        },
        new UpgradeConfig()
        {
            id = 5,
            name = "Store Count++",
            max = 4,
            cost = new[] { 1000, 2000, 5000, 5000},
            notice = new[]
            {
                "lv.1 Shop has <color=blue>4</color> Item ",
                "lv.2 Shop has <color=blue>5</color> Item ",
                "lv.3 Shop has <color=blue>6</color> Item ",
                "lv.4 Shop has <color=blue>7</color> Item ",
            },
            OnLoad = (p, level) => { p.ExtraShopCount += level; }
        },
        new UpgradeConfig()
        {
            id = 6,
            name = "Damage+",
            max = 10,
            cost = new[] { 100, 500, 1000, 2000, 4000, 8000, 10000, 15000, 20000, 30000 },
            notice = new[]
            {
                "lv.1 Deal <color=blue>1</color> Extra Damage ",
                "lv.2 Deal <color=blue>2</color> Extra Damage ",
                "lv.3 Deal <color=blue>3</color> Extra Damage ",
                "lv.4 Deal <color=blue>4</color> Extra Damage ",
                "lv.5 Deal <color=blue>5</color> Extra Damage ",
                "lv.6 Deal <color=blue>10</color> Extra Damage ",
                "lv.7 Deal <color=blue>20</color> Extra Damage ",
                "lv.8 Deal <color=blue>50</color> Extra Damage ",
                "lv.9 Deal <color=blue>100</color> Extra Damage ",
                "lv.max Deal <color=blue>200</color> Extra Damage ",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => 4,
                    5 => 5,
                    6 => 10,
                    7 => 20,
                    8 => 50,
                    9 => 100,
                    10 => 200,
                    _ => 0
                };
                p.ExtraDamageFix += value;
            }
        },
        new UpgradeConfig()
        {
            id = 7,
            name = "Award++",
            max = 10,
            cost = new[] { 100, 500, 1000, 2000, 4000, 8000, 10000, 15000, 20000, 30000 },
            notice = new[]
            {
                "lv.1 Get <color=blue>50</color> Extra Point On Win",
                "lv.2 Get <color=blue>100</color> Extra Point On Win",
                "lv.3 Get <color=blue>200</color> Extra Point On Win",
                "lv.4 Get <color=blue>500</color> Extra Point On Win",
                "lv.5 Get <color=blue>1000</color> Extra Point On Win",
                "lv.6 Get <color=blue>2000</color> Extra Point On Win",
                "lv.7 Get <color=blue>4000</color> Extra Point On Win",
                "lv.8 Get <color=blue>8000</color> Extra Point On Win",
                "lv.9 Get <color=blue>15000</color> Extra Point On Win",
                "lv.max Get <color=blue>20000</color> Extra Point On Win",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 50,
                    2 => 100,
                    3 => 200,
                    4 => 500,
                    5 => 1000,
                    6 => 2000,
                    7 => 4000,
                    8 => 8000,
                    9 => 15000,
                    10 => 20000,
                    _ => 0
                };
                p.ExtraAward += value;
            }
        },
        new UpgradeConfig()
        {
            id = 8,
            name = "Enemy Pt++",
            max = 10,
            cost = new[] { 100, 500, 1000, 2000, 4000, 8000, 10000, 15000, 20000, 30000 },
            notice = new[]
            {
                "lv.1 Get <color=blue>1</color> Extra Point On Kill",
                "lv.2 Get <color=blue>2</color> Extra Point On Kill",
                "lv.3 Get <color=blue>5</color> Extra Point On Kill",
                "lv.4 Get <color=blue>10</color> Extra Point On Kill",
                "lv.5 Get <color=blue>15</color> Extra Point On Kill",
                "lv.6 Get <color=blue>20</color> Extra Point On Kill",
                "lv.7 Get <color=blue>50</color> Extra Point On Kill",
                "lv.8 Get <color=blue>100</color> Extra Point On Kill",
                "lv.9 Get <color=blue>200</color> Extra Point On Kill",
                "lv.max Get <color=blue>500</color> Extra Point On Kill",
            },
            OnLoad = (p, level) =>
            {
                var value = level switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 5,
                    4 => 10,
                    5 => 15,
                    6 => 20,
                    7 => 50,
                    8 => 100,
                    9 => 200,
                    10 => 500,
                    _ => 0
                };
                p.ExtraEnemyAward += value;
            }
        },
    };
}