using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BpWheel : BlueprintBase
{
    
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
        OnHit?.DoNext(data);
    }

    public override void RefreshCollection()
    {
        base.RefreshCollection();
    }
}
