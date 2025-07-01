using Newtonsoft.Json;

public class BpAmplify : BlueprintBase
{
    private readonly Property _amp; 
    public BpAmplify(BpNodeSaveData data) : base(data)
    {
         _amp = JsonConvert.DeserializeObject<Property>(Config.json);
    }

    public override Property GetProperty(Property data)
    {
        data.ExFix += _amp.ExFix;
        data.ExPercent += _amp.ExPercent;
        return base.GetProperty(data);
    }
}

