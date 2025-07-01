using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public ProgrammingUI programmingUI;
    public Transform layout;
    public Button refreshButton;
    private readonly List<ShopItem> _shopItems = new List<ShopItem>();

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Update()
    {
        refreshButton.interactable = SaveDataHandler.Temp.point >= 100;
        _shopItems.ForEach(n=> n.Display());
    }

    public async UniTask CreateShopItem(int id)
    {
        var require = InstantiateAsync(ReferenceManager.Instance.shopItemPrefab, layout);
        await require;
        var item = require.Result[0];
        _shopItems.Add(item);

        var config = ConfigHandler.Instance.NodeConfigs[id];
        item.storeUI = this;
        item.config = config;
        item.data = new BpNodeSaveData()
        {
            id = id,
            position = new List<float>() { Screen.width / 2, Screen.height / 2 }
        };

        item.Display();
    }

    public void OnBuy(ShopItem item)
    {
        programmingUI.BuyNode(item);
    }
    public void OnItemDestroy(ShopItem shopItem)
    {
        _shopItems.Remove(shopItem);
    }

    public void Clear()
    {
        foreach (var item in _shopItems)
        {
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }

    public void Refresh()
    {
        programmingUI.OnRefresh();
    }


}