using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BpNodeViewer : BPViewerBase
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI cost;

    public Transform inputLayout;
    public Transform amplifyLayout;
    public Transform outputLayout;

    public override void Initialize(BpNodeSaveData saveData)
    {
        data = saveData;
        config = ConfigHandler.Instance.NodeConfigs[data.id];

        title.text = config.name;
        description.text = config.description;
        cost.text = $"{config.cost} ms";

        for (int i = 0; i < config.ports.Length; i++)
        {
            var id = config.ports[i];
            var pConfig = ConfigHandler.Instance.PortConfigs[id];
            var io = Factory.CreatePortView(i, this, pConfig);
            Transform parent;
            if (pConfig.type == IOType.Input)
            {
                parent = pConfig.flag == PortFlag.Process ? inputLayout : amplifyLayout;
            }
            else
            {
                parent = outputLayout;
            }

            io.transform.SetParent(parent, false);

            ports.Add(io);
        }
    }


    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
}