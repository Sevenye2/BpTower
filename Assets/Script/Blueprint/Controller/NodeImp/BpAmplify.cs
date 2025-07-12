using Newtonsoft.Json;

public class BpAmplify : BlueprintBase
{
    private readonly ValueProperty _amp; 
    public BpAmplify(BpNodeSaveData data) : base(data)
    {
         _amp = JsonConvert.DeserializeObject<ValueProperty>(Config.json);
    }

    public override ValueProperty GetProperty(ValueProperty data)
    {
        data.ExFix += _amp.ExFix;
        data.ExPercent += _amp.ExPercent;
        return base.GetProperty(data);
    }
}

