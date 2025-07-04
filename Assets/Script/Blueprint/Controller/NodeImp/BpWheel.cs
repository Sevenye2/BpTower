using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BpWheel : BlueprintBase
{
    private List<BlueprintBase> _onHit;
    
    // Start is called before the first frame update
    public BpWheel(BpNodeSaveData data) : base(data)
    {
    }

    public override void DoNext(RuntimeData data)
    {
        Wheel.Create(new WheelData()
        {
            Origin = data.Position,
            Amount = 3,
            Duration = 1.5f,
            Damage = 3,
            Radius = 2f,
            Distance = 0.5f,
            Speed = 270 
        }, OnHitCallback);
       
        base.DoNext(data);
    }
    private void OnHitCallback(RuntimeData data)
    {
        _onHit.ForEach(n => n.DoNext(data));
    }
    public override void RefreshCollection()
    {
        base.RefreshCollection();
        _onHit = Ports
            .Where(p => p.Config.ioType == IOType.Output)
            .Where(p => p.Config.portType == PortType.Process)
            .Where(p => p.Config.flag == "OnHit")
            .Where(p => p.Node != null)
            .Select(p => p.Node).ToList();
    }
}
