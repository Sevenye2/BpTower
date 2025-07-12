using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ValueUI : MonoBehaviour
{
    public TextMeshProUGUI content;

    private int _value;
    private float _duration;
    private float _t;

    public void ShowContent(float duration, int value)
    {
        _value = value;
        _duration = duration;
        _t = 0;

        _ = DisplayAsync();
    }


    private async UniTask DisplayAsync()
    {
        while (_t < _duration)
        {
            _t += Time.deltaTime;

            var v = Mathf.Lerp(0, _value, _t / _duration);

            content.text = $"{v:0.##}";
            await UniTask.Yield();
            
        }

        content.text = _value.ToString();
    }
 
}
