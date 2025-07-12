using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpRay : BlueprintBase
{
    private readonly Collider2D[] _results = new Collider2D[16];

    // Start is called before the first frame update
    public BpRay(BpNodeSaveData data) : base(data)
    {
    }

    public override void DoNext(RuntimeData data)
    {
        var size = Physics2D.OverlapCircleNonAlloc(data.Position, 5, _results,
            LayerMask.GetMask("Enemy"));

        float angle;
        if (size > 0)
        {
            var position = _results[Random.Range(0, size)].transform.position;
            var dot = Vector2.Dot((position - data.Position).normalized, Vector2.right);
            angle = Mathf.Asin(dot) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Random.Range(0, 360f);
        }

        RayLine.Create(new RayLineData()
        {
            Duration = 2,
            Length = 200,
            Width = 3,
            Origin = data.Position,
            Angle = angle,
            OnHitCallback = OnHitCallBack
        });
        
        
        base.DoNext(data);
    }

    private void OnHitCallBack(RuntimeData data)
    {
        OnHit?.DoNext(data);
    }
}
