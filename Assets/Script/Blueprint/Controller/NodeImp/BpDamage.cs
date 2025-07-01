using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class BpDamage : BlueprintBase
{
    private Property _damage;
    private List<BlueprintBase> _onKilled;
    private List<BlueprintBase> _damagePorts;

    // Start is called before the first frame update
    public BpDamage(BpNodeSaveData data) : base(data)
    {
        _damage = JsonConvert.DeserializeObject<Property>(Config.json);
    }

    public override void DoNext(RuntimeData data)
    {
        if (data.Enemy)
        {
            GlobalUI.Instance.battleUI.console.Log($"<size=10>hit ,dmg = {_damage.Result}</size>");

            data.Enemy.BeAttacked(_damage.Result, () =>
            {
                var d = new RuntimeData()
                {
                    Position = data.Enemy.WorldPosition,
                    Enemy = data.Enemy
                };
                OnKilled(d);
            });
        }

        base.DoNext(data);
    }

    private void OnKilled(RuntimeData data)
    {
        foreach (var node in _onKilled)
        {
            node.DoNext(data);
        }
    }

    public override void RefreshCollection()
    {
        base.RefreshCollection();

        _onKilled = Ports
            .Where(p => p.Config.type == IOType.Output)
            .Where(p => p.Config.flag == PortFlag.Process)
            .Where(p => p.Config.port == PortType.OnKilled)
            .Where(p => p.Node != null)
            .Select(p => p.Node).ToList();

        _damagePorts = Ports
            .Where(p => p.Config.type == IOType.Input)
            .Where(p => p.Config.flag == PortFlag.Amplify)
            .Where(p => p.Config.port == PortType.Damage)
            .Where(p => p.Node != null)
            .Select(p => p.Node).ToList();
    }

    public override void RefreshValues()
    {
        _damagePorts.Aggregate(_damage, (current, node) => node.GetProperty(current));
    }
}