using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtendViewer : BPViewerBase
{
    public Transform inputLayout;
    public Transform outputLayout;

    public TextMeshProUGUI description;

    public override void Initialize(BpNodeSaveData saveData)
    {
        data = saveData;
        config = ConfigHandler.NodeConfigs[data.id];

        description.text = config.description;

        for (var i = 0; i < config.ports.Length; i++)
        {
            var pConfig = config.ports[i];
            var io = Factory.CreatePortView(i, this, pConfig);
            io.transform.SetParent(pConfig.ioType == IOType.Input ? inputLayout : outputLayout, false);
            ports.Add(io);
        }

        (transform as RectTransform)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            description.preferredWidth + 60);
    }

    public override void Refresh()
    {
    }
}