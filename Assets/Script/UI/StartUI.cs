using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public GameObject loadBtn;
    private void OnEnable()
    {
        loadBtn.SetActive(SaveDataHandler.Exists());
    }


    public void Open()
    {
        gameObject.SetActive(true);
    }

    public async UniTask OpenAsync()
    {
        await GlobalUI.Instance.mask.FadeAsync(0.5f);
        gameObject.SetActive(true);

        await UniTask.Delay(100);
        _ = GlobalUI.Instance.mask.FadeAsync(0.5f);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    public void NewBtn()
    {
        SaveDataHandler.Renew();
        _ = GlobalUI.Instance.programmingUI.OpenAsync(Close);
    }

    public void LoadBtn()
    {
        _ = GlobalUI.Instance.programmingUI.OpenAsync(Close);
    }
    
    
    public void ExitBtn()
    {
        Application.Quit();
    }
    
}