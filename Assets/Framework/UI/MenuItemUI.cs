using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemUI : MonoBehaviour
{
    public TextMeshProUGUI label;

    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(string text, Action onClick)
    {
        label.text = text;
        btn.onClick.AddListener(()=>onClick?.Invoke());
    }

    public void Initialize(string text)
    {
        label.text = text;
        btn.interactable = false;
    }
}