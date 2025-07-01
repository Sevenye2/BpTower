using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentMenuUI : MonoBehaviour, IPointerClickHandler
{
    public MenuItemUI prefab;
    public RectTransform layout;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Open(Vector3 position)
    {
        layout.position = position;
        gameObject.SetActive(true);
    }

    public void AddDisplay(string text)
    {
        var item = Instantiate(prefab, layout);
        item.Initialize(text);
    }


    public void AddBtn(string text, Action onclick)
    {
        var item = Instantiate(prefab, layout);
        onclick += Close;
        item.Initialize(text, onclick);
    }

    private void Close()
    {
        while (layout.childCount > 0)
        {
            DestroyImmediate(layout.GetChild(0).gameObject);
        }

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}