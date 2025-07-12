using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class BpDamage : BlueprintBase
{
    private readonly ValueProperty _damage;
    private List<BlueprintBase> _damagePorts;

    // Start is called before the first frame update
    public BpDamage(BpNodeSaveData data) : base(data)
    {
        _damage = JsonConvert.DeserializeObject<ValueProperty>(Config.json);
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
                OnKilledCallback(d);
            });
        }

        base.DoNext(data);
    }

    private void OnKilledCallback(RuntimeData data)
    {
        OnKilled?.DoNext(data);
    }

    public override void RefreshCollection()
    {
        base.RefreshCollection();
        
        _damagePorts = Ports
            .Where(p => p.Config.ioType == IOType.Input)
            .Where(p => p.Config.portType == PortType.Amplify)
            .Where(p => p.Config.flag == "Damage")
            .Where(p => p.Node != null)
            .Select(p => p.Node).ToList();
    }

    public override void RefreshValues()
    {
        _damagePorts.Aggregate(_damage, (current, node) => node.GetProperty(current));
    }
}