using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BpBullet : BlueprintBase
{
    private List<BlueprintBase> _ampDistance;

    private readonly ValueProperty _distance;

    public BpBullet(BpNodeSaveData data) : base(data)
    {
        _distance = new ValueProperty(3);
    }

    public override void DoNext(RuntimeData data)
    {
        // -----------------------------

        var results = new Collider2D[16];
        var size = Physics2D.OverlapCircleNonAlloc(data.Position, _distance.Result, results,
            LayerMask.GetMask("Enemy"));

        var result = results[Random.Range(0, size)];
        var angle = Random.Range(0f, 360f);
        var randDis = Random.Range(1, _distance.Result);
        var randPos = new Vector3(randDis * Mathf.Cos(angle), randDis * Mathf.Sin(angle), 0f);

        var o = data.Position;
        o.z = 0;
        randPos += o;

        var enemy = result?.GetComponent<EnemyViewer>();

        _ = Bullet.Create(data.Position, enemy, OnHitCallback, randPos);

        base.DoNext(data);
    }

    private void OnHitCallback(RuntimeData data)
    {
        OnHit?.DoNext(data);
    }


    public override void RefreshCollection()
    {
        base.RefreshCollection();



        _ampDistance = Ports
            .Where(p => p.Config.ioType == IOType.Input)
            .Where(p => p.Config.portType == PortType.Amplify)
            .Where(p => p.Config.flag == "Distance")
            .Where(p => p.Node != null)
            .Select(p => p.Node).ToList();
    }

    public override void RefreshValues()
    {
        _ampDistance.Aggregate(_distance, (current, node) => node.GetProperty(current));
    }
}