using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BpWheel : BlueprintBase
{

    private BlueprintBase _amountNode;
    private BlueprintBase _durationNode;
    private BlueprintBase _radiusNode;
    private BlueprintBase _distanceNode;
    private BlueprintBase _speedNode;

    private readonly ValueProperty _amount = new(1);
    private readonly ValueProperty _duration = new(100f);
    private readonly ValueProperty _radius = new(200f);
    private readonly ValueProperty _distance = new(50f);
    private readonly ValueProperty _speed = new(180f);
    
    // Start is called before the first frame update
    public BpWheel(BpNodeSaveData data) : base(data)
    {
        
    }

    public override void DoNext(RuntimeData data)
    {
        Wheel.Create(new WheelData()
        {
            Origin = data.Position,
            Amount = (int)_amount.Result,
            Duration = _duration.Result / 100,
            Radius = _radius.Result / 100,
            Distance = _distance.Result / 100f,
            Speed = _speed.Result, 
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
        var list = Ports.ToList();
        
        _amountNode = list.Find(p=>p.Config.flag == "Amount").Node;
        _durationNode = list.Find(p=>p.Config.flag == "Duration").Node;
        _radiusNode = list.Find(p=>p.Config.flag == "Radius").Node;
        _distanceNode = list.Find(p=>p.Config.flag == "Distance").Node;
        _speedNode = list.Find(p=>p.Config.flag == "Speed").Node;
    }

    
    public override void RefreshValues()
    {
        _amountNode?.GetProperty(_amount);
        _durationNode?.GetProperty(_duration);
        _radiusNode?.GetProperty(_radius);
        _distanceNode?.GetProperty(_distance);
        _speedNode?.GetProperty(_speed);
    }
}
