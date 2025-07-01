using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour
{
    public Image mask;
    private bool IsMasked => gameObject.activeSelf;
    private float EndValue => IsMasked ? 0 : 1;

    private void Awake()
    {
    }

    public async UniTask FadeAsync(float duration, int delay = 0)
    {
        var eColor = new Color(0, 0, 0, EndValue);
        var sColor = mask.color = new Color(0, 0, 0, 1 - EndValue);

        var isOut = IsMasked;
        mask.gameObject.SetActive(true);
        for (var t = 0f; t < duration; t += Time.deltaTime)
        {
            mask.color = Color.Lerp(sColor, eColor, t);
            await UniTask.Yield();
        }

        mask.color = eColor;

        await UniTask.Delay(delay);

        if (isOut)
            mask.gameObject.SetActive(false);
    }
}