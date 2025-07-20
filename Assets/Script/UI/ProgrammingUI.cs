using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ProgrammingUI : MonoBehaviour
{
    public TextMeshProUGUI point;
    public BlueprintUI blueprintUI;
    public StoreUI storeUI;

    // Start is called before the first frame update
    void Start()
    {
    }


    public void Update()
    {
        point.text = $"Point: {SaveDataHandler.Data.point}";
    }

    public void BuyNode(ShopItem item)
    {
        item.data.uid = SaveDataHandler.Data.GetUid();
        blueprintUI.CreateNode(item.data);
        SaveDataHandler.Data.point -= item.config.cost;
        item.DestroyItem();
    }

    public async UniTask OpenAsync(Action onMasked = null)
    {
        await GlobalUI.Instance.mask.FadeAsync(1, 500);
        onMasked?.Invoke();
        gameObject.SetActive(true);
        RefreshShop();
        await blueprintUI.LoadAsync();

        await UniTask.DelayFrame(10);
        _ = GlobalUI.Instance.mask.FadeAsync(1);
    }

    private async UniTask CloseAsync(Action onMasked = null)
    {
        await GlobalUI.Instance.mask.FadeAsync(1, 500);
        onMasked?.Invoke();
        gameObject.SetActive(false);
        Clear();

        _ = GlobalUI.Instance.mask.FadeAsync(1);
    }
    
    public void RefreshShop()
    {
        storeUI.Clear();

        var configs = ConfigHandler.NodeConfigs;
        var list = configs.Where(c => c.rare <= 1 + SaveDataHandler.Upgrades.ExtraRare).ToList();

        for (var i = 0; i < 3 + SaveDataHandler.Upgrades.ExtraShopCount; i++)
        {
            var index = SaveDataHandler.Data.Random(0, list.Count);
            _ = storeUI.CreateShopItem(list[index].id);
        }
    }
    

    private void Clear()
    {
        blueprintUI.Clear();
        storeUI.Clear();
    }

    public void Restore()
    {
        SaveDataHandler.Load();

        Clear();
        _ = blueprintUI.LoadAsync();
        RefreshShop();
    }

    public void Confirm()
    {
        _ = CloseAsync();
        ProcessController.Instance.BattleStart();
    }
}