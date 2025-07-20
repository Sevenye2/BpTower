using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BpUpdate : BlueprintBase, IBlueprintUpdate
{
    private float _sumCosts = 0;

    private float _time = 0;

    public BpUpdate(BpNodeSaveData data) : base(data)
    {
        _sumCosts = Config.cost - SaveDataHandler.Upgrades.ReduceUpdate;
        _sumCosts /= 1000f;
    }

    public void OnUpdate()
    {
        _time += Time.deltaTime;
        if (_time < _sumCosts) return;

        _time -= _sumCosts;

        var runtime = new RuntimeData
        {
            Position = Controller.Viewer.transform.position + Vector3.forward,
        };
        DoNext(runtime);
    }

    public void Recalculate()
    {
    }
}