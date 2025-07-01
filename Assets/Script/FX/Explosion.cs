using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class Explosion : Object2D
{
    private const float Duration = 0.5f;
    private static BufferPool<Explosion> _pool;

    public static async UniTask CreateAsync(Vector3 position)
    {
        _pool ??= new(ReferenceManager.Instance.explosionPrefab);

        var explosion = await _pool.CreateAsync();
        
        explosion.WorldPosition = position;
        explosion.gameObject.SetActive(true);
        
        await UniTask.Delay(TimeSpan.FromSeconds(Duration));
        
        explosion.gameObject.SetActive(false);
        _pool.Destroy(explosion);
    }

}
