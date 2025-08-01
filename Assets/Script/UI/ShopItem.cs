using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Button button;

    public BpNodeSaveData data;
    public BpNodeConfig config;

    public StoreUI storeUI;

    public void Display()
    {
        label.text = $"{config.name}\n\nCost : {config.prise} pt";
        button.interactable = SaveDataHandler.Data.point >= config.prise;
    }

    public void Buy()
    {
        SaveDataHandler.Data.point -= config.prise;
        storeUI.OnBuy(this);
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
        storeUI.OnItemDestroy(this);
    }
}