using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Factory
{
    public static BlueprintBase CreateBlueprint(BpNodeSaveData data)
    {
        var config = ConfigHandler.NodeConfigs[data.id];

        var type = Type.GetType(config.className);
        if (type == null)
            throw new Exception("Cannot find type: " + config.className);

        var ctl = Activator.CreateInstance(type, data);

        return ctl as BlueprintBase;
    }

    public static BPViewerBase CreateNodeViewer(Transform parent, BpNodeSaveData data)
    {
        const string basePath = "Prefabs/BlueprintViewer/";

        var config = ConfigHandler.NodeConfigs[data.id];

        var path = config.style switch
        {
            NodeStyle.Root => basePath + "Root",
            NodeStyle.Node => basePath + "Node",
            NodeStyle.Extend => basePath + "Extend",
            _ => throw new Exception("Cannot find type: " + config.style)
        };

        var viewerPrefab = Resources.Load<BPViewerBase>(path);
        var script = Object.Instantiate(viewerPrefab, parent);
        script.transform.position = data.GetPosition();
        script.Initialize(data);
        return script;
    }

    public static PortViewer CreatePortView(int index, BPViewerBase bp, BpPortConfig config)
    {
        const string basePath = "Prefabs/BlueprintViewer/IOPort/";
        var ioPortPrefab = Resources.Load<PortViewer>(basePath + "PortView");
        var io = Object.Instantiate(ioPortPrefab);
        io.Initialize(index, bp, config);
        return io;
    }
}