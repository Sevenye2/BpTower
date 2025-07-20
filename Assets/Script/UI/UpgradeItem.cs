using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI costLabel;
    public Button upBtn;
    public Button downBtn;
    public Image bg;

    public int id;
    public List<UpgradeItem> pre;
    private readonly List<UpgradeItem> _back = new List<UpgradeItem>();
    public Color normalColor;
    public Color unusedColor;


    private int Level => SaveDataHandler.Data.upgrades[id];
    private UpgradeConfig Config => ConfigHandler.UpgradeConfig[id];
    private int Prise => Config.cost[Level];

    // Start is called before the first frame update
    void OnEnable()
    {
        pre.ForEach(item => item._back.Add(this));
        // display

        levelLabel.text = $"lv.{Level}/{Config.max}";
        nameLabel.text = Config.name;
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        levelLabel.text = $"lv.{Level}/{Config.max}";
        costLabel.text = Level < Config.max ? $"{Prise} pt" : $"MAX";

        var self = pre.Aggregate(true, (current, item) => current && item.Level > 0);
        var canMin = _back.Aggregate(false, (current, item) => current || item.Level > 0);

        bg.color = self ? normalColor : unusedColor;

        if (Level < Config.max)
        {
            upBtn.gameObject.SetActive(Level < Config.max && self && SaveDataHandler.Data.point >= Prise);
        }
        else
        {
            upBtn.gameObject.SetActive(false);
        }

        downBtn.gameObject.SetActive(Level > 0 && self && !canMin);
    }


    private void DisplayNotice()
    {
        GlobalUI.Instance.noticeUI.transform.position = transform.position + Vector3.right * 200;
        // hint
        if (Level == 0)
        {
            GlobalUI.Instance.noticeUI.ShowNext(Config.notice[0]);
        }
        else if (Level == Config.max)
        {
            GlobalUI.Instance.noticeUI.ShowCurrent(Config.notice[Level - 1]);
        }
        else
        {
            GlobalUI.Instance.noticeUI.ShowDouble(Config.notice[Level - 1], Config.notice[Level]);
        }
    }

    public void LevelUp()
    {
        SaveDataHandler.Data.point -= Config.cost[Level];
        SaveDataHandler.Data.upgrades[id]++;
        DisplayNotice();
        SaveDataHandler.PropertyRefresh();
    }

    public void LevelDown()
    {
        SaveDataHandler.Data.upgrades[id]--;
        SaveDataHandler.Data.point += Config.cost[Level];
        DisplayNotice();
        SaveDataHandler.PropertyRefresh();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayNotice();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GlobalUI.Instance.noticeUI.Close();
    }
}