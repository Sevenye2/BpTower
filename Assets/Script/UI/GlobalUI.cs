using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoSingleton<GlobalUI>
{
    // Start is called before the first frame update
    public Canvas canvas;
    public MaskUI mask;
    
    [Header("UI")]
    public StartUI startUI;
    public ProgrammingUI programmingUI;
    public BattleUI battleUI;
    public SettlementUI settlementUI;
    public ContentMenuUI menuUI;

    void Start()
    {
    }


}