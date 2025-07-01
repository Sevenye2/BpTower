using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private static BufferPool<HpBar> _pool;

    public static UniTask<HpBar> CreateAsync()
    {
        _pool ??= new BufferPool<HpBar>(ReferenceManager.Instance.hpBarPrefab);


        return _pool.CreateAsync();
    }

    public Slider slider;


    public void SetValue(float value)
    {
        slider.value = value;
    }
}