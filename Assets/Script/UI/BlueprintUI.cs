using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintUI : MonoBehaviour
{

    public Transform nodePanel;
    public Transform linePanel;
    public readonly List<BPViewerBase> Nodes = new();
    
    public void Start()
    {
    }

    public async UniTask LoadAsync()
    {
        SaveDataHandler.Load();

        // creat node
        foreach (var data in SaveDataHandler.Temp.nodes)
        {
            var n = Factory.CreateNodeViewer(nodePanel, data);
            Nodes.Add(n);
        }

        // 等待 unity 将位置设置完毕
        await UniTask.Yield();

        // creat edge
        foreach (var data in SaveDataHandler.Temp.edges)
        {
            var e = new EdgeViewer(linePanel);
            EdgeSet(e, data);
        }

        Refresh();
    }

    public void CreateNode(BpNodeSaveData data)
    {
        var n = Factory.CreateNodeViewer(nodePanel, data);
        Nodes.Add(n);
        SaveDataHandler.Temp.nodes.Add(data);
    }

    public void RemoveNode(BPViewerBase bp)
    {
        SaveDataHandler.Temp.nodes.Remove(bp.data);
        Nodes.Remove(bp);
        bp.ports.Where(io => io && io.Edge != null).Select(io=>io.Edge).ToList().ForEach(RemoveEdge);
        Destroy(bp.gameObject);
    }

    public EdgeViewer CreateEdge()
    {
        var e = new EdgeViewer(linePanel);
        SaveDataHandler.Temp.edges.Add(e.Data);
        return e;
    }

    private void EdgeSet(EdgeViewer edge, BpEdgeSaveData data)
    {
        var startNode = Nodes.Find(n => n.data.uid == data.outputUid);
        var endNode = Nodes.Find(n => n.data.uid == data.inputUid);

        var output = startNode.GetPort(data.outputPort);
        var input = endNode.GetPort(data.inputPort);

        edge.Confirm(output, input);
    }

    public void RemoveEdge(EdgeViewer edge)
    {
        SaveDataHandler.Temp.edges.Remove(edge.Data);
        edge.Discard();
    }

    public void Refresh()
    {
        Nodes.Where(n => n.config.style == 0)
            .ToList()
            .ForEach(n => n.Refresh());
    }


    public void Clear()
    {
        Nodes.Clear();

        while (nodePanel.childCount > 0)
        {
            DestroyImmediate(nodePanel.GetChild(0).gameObject);
        }

        while (linePanel.childCount > 0)
        {
            DestroyImmediate(linePanel.GetChild(0).gameObject);
        }
    }
}