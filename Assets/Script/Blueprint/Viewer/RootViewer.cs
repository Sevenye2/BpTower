using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RootViewer : BPViewerBase
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI cost;
    
    public PortViewer outputPort;
    void Start()
    {
        
    }

    public override void Initialize(BpNodeSaveData saveData)
    {
        data = saveData;
        config = ConfigHandler.NodeConfigs[data.id]; 
        
        title.text = config.name;
        description.text = config.description;
        cost.text = $"{config.cost} ms";
        
        var pConfig = config.ports[0];
        outputPort.index = 0;
        outputPort.bp = this;
        outputPort.config = pConfig;
        ports.Add(outputPort);
    }

    public override void Refresh()
    {
    }
}
