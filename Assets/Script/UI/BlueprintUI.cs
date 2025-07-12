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
    private readonly List<BPViewerBase> _nodes = new();
    private readonly List<EdgeViewer> _edges = new();

    public void Start()
    {
    }

    public async UniTask LoadAsync()
    {
        SaveDataHandler.Load();

        // creat node
        foreach (var data in SaveDataHandler.Data.nodes)
        {
            var n = Factory.CreateNodeViewer(nodePanel, data);
            _nodes.Add(n);
        }

        // 等待 unity 将位置设置完毕
        await UniTask.Yield();

        // creat edge
        foreach (var data in SaveDataHandler.Data.edges)
        {
            var e = new EdgeViewer(linePanel, data);
            EdgeSet(e, data);
            _edges.Add(e);
        }

        Refresh();
    }

    public void CreateNode(BpNodeSaveData data)
    {
        var n = Factory.CreateNodeViewer(nodePanel, data);
        _nodes.Add(n);
        SaveDataHandler.Data.nodes.Add(data);
    }

    public void RemoveNode(BPViewerBase bp)
    {
        SaveDataHandler.Data.nodes.Remove(bp.data);
        _nodes.Remove(bp);
        bp.ports.Where(io => io && io.Edge != null).Select(io => io.Edge).ToList().ForEach(RemoveEdge);
        Destroy(bp.gameObject);
    }

    public EdgeViewer CreateEdge()
    {
        var data = new BpEdgeSaveData();
        var e = new EdgeViewer(linePanel, data);
        _edges.Add(e);
        SaveDataHandler.Data.edges.Add(data);
        return e;
    }

    private void EdgeSet(EdgeViewer edge, BpEdgeSaveData data)
    {
        var startNode = _nodes.Find(n => n.data.uid == data.outputUid);
        var endNode = _nodes.Find(n => n.data.uid == data.inputUid);

        var output = startNode.GetPort(data.outputPort);
        var input = endNode.GetPort(data.inputPort);

        edge.Confirm(output, input);
    }

    public void RemoveEdge(EdgeViewer edge)
    {
        SaveDataHandler.Data.edges.Remove(edge.Data);
        _edges.Remove(edge);
        edge.Discard();
    }

    public void Refresh()
    {
        _nodes.Where(n => n.config.style == 0)
            .ToList()
            .ForEach(n => n.Refresh());
    }

    public void OnBpViewPortChange()
    {
        foreach (var edge in _edges)
        {
            edge.UpdateLine();
        }
    }


    public void Clear()
    {
        _nodes.Clear();
        while (nodePanel.childCount > 0)
        {
            DestroyImmediate(nodePanel.GetChild(0).gameObject);
        }

        _edges.Clear();
        while (linePanel.childCount > 0)
        {
            DestroyImmediate(linePanel.GetChild(0).gameObject);
        }
    }
}