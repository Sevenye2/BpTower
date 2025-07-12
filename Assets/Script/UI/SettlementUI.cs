using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettlementUI : MonoBehaviour
{
    public Transform window;
    public Image panel;
    private const float DefaultAlpha = 145 / 255f;

    public TextMeshProUGUI title;
    
    public TextMeshProUGUI txtPrefab;
    public ValueUI valuePrefab;
    
    public Transform headLayout;
    public Transform valueLayout;
    
    public TextMeshProUGUI btnDescription;
    public Action OnConfirm;

    private readonly Dictionary<string, int> _display = new();
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public async UniTask Open(bool value)
    {
        // display
        title.text = value? "Win" : "Lose";
        while (headLayout.childCount > 0)
        {
            DestroyImmediate(headLayout.GetChild(0).gameObject);
        }

        while (valueLayout.childCount > 0)
        {
            DestroyImmediate(valueLayout.GetChild(0).gameObject);
        }
        
        
        // anim
        window.localPosition = Vector3.up * 900;
        gameObject.SetActive(true);
        
        panel.DOFade(DefaultAlpha, 0.5f);
        window.DOLocalMoveY(0, 1).SetEase(Ease.OutBounce);

        await UniTask.Delay(500);

        for (var i = 0; i < _display.Count; i++)
        {
            var headUI= Instantiate(txtPrefab, headLayout);
            var valueUI = Instantiate(valuePrefab, valueLayout);
            
            var head = _display.Keys.ToList()[i];
            var v = _display[head];
            
            headUI.text = head;
            valueUI.ShowContent(2, v);
            
            await UniTask.Delay(500);
        }
        
        _display.Clear();
    }

    public void AddDisplay(string head, int value)
    {
        _display.Add(head, value);
    }

    public void Close()
    {
        _ = CloseAsync();
    }


    private async UniTask CloseAsync()
    {
        OnConfirm?.Invoke();

        panel.DOFade(0, 0.5f);
        await UniTask.Delay(1000);
        gameObject.SetActive(false);
    }
}