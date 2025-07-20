using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BpDoom : BlueprintBase
{
    private readonly Collider2D[] _results = new Collider2D[16];

    // Start is called before the first frame update
    public BpDoom(BpNodeSaveData data) : base(data)
    {
    }

    private BlueprintBase _sizeNode;
    
    private readonly ValueProperty _size = new ValueProperty(50f);

    public override void DoNext(RuntimeData data)
    {
        var size = Physics2D.OverlapCircleNonAlloc(data.Position, 5, _results,
            LayerMask.GetMask("Enemy"));

        Vector3 position;
        if (size == 0)
        {
            position = data.Position + new Vector3(Random.Range(3, 3), Random.Range(3, 3), 0);
        }
        else
        {
            position = _results[Random.Range(0, size)].transform.position;
        }


        _ = Doom.Create(new DoomData()
        {
            Target = position,
            Size = _size.Result / 100f
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
        _sizeNode = Ports.ToList().Find(p=>p.Config.flag == "Size").Node;
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
        _sizeNode?.GetProperty(_size);
    }
}