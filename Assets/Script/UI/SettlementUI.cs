using System;
using System.Collections;
using System.Collections.Generic;
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
    public TextMeshProUGUI content;
    public TextMeshProUGUI btnDescription;
    public Action OnConfirm;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Open(bool value)
    {
        // display
        title.text = value? "Win" : "Lose"; 
        
        // anim
        window.localPosition = Vector3.up * 900;
        gameObject.SetActive(true);
        
        panel.DOFade(DefaultAlpha, 0.5f);
        window.DOLocalMoveY(0, 1).SetEase(Ease.OutBounce);
    }

    public void Close()
    {
        _ = CloseAsync();
    }


    public async UniTask CloseAsync()
    {
        OnConfirm?.Invoke();

        panel.DOFade(0, 0.5f);
        await UniTask.Delay(1000);
        gameObject.SetActive(false);
    }
}