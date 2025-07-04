using System;
using System.Collections;
using System.Collections.Generic;
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
        point.text = $"Point: {SaveDataHandler.Temp.point}";
    }


    public void OnRefresh()
    {
        SaveDataHandler.Temp.point -= 100;
        storeUI.Clear();

        _ = storeUI.CreateShopItem(7);
        _ = storeUI.CreateShopItem(2);
        _ = storeUI.CreateShopItem(3);
        _ = storeUI.CreateShopItem(4);
        _ = storeUI.CreateShopItem(5);
    }

    public void BuyNode(ShopItem item)
    {
        item.data.uid = SaveDataHandler.Temp.GetUid();
        blueprintUI.CreateNode(item.data);
        SaveDataHandler.Temp.point -= item.config.cost;
        item.DestroyItem();
    }

    public async UniTask OpenAsync()
    {
        await GlobalUI.Instance.mask.FadeAsync(1, 500);

        gameObject.SetActive(true);
        storeUI.Refresh();
        await blueprintUI.LoadAsync();

        await UniTask.DelayFrame(10);
        _ = GlobalUI.Instance.mask.FadeAsync(1);
    }

    private async UniTask CloseAsync()
    {
        await GlobalUI.Instance.mask.FadeAsync(1, 500);
        gameObject.SetActive(false);
        Clear();

        _ = GlobalUI.Instance.mask.FadeAsync(1);
    }

    private void Clear()
    {
        blueprintUI.Clear();
    }

    public void Restore()
    {
        Clear();
        _ = blueprintUI.LoadAsync();
    }

    public void SaveAndClose()
    {
        SaveDataHandler.Save();
        _ = CloseAsync();
    }
}