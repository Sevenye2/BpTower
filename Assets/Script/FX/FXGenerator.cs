using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class FXGenerator
{
    private const float Duration = 0.5f;
    private readonly BufferPool<Transform> _pool;
    public FXGenerator(string path)
    {
        var prefabs = Resources.Load<Transform>(path);
        _pool = new(prefabs);
    }


    public async UniTask CreateAsync(Vector3 position, float size = 1.0f)
    {
        var explosion = await _pool.CreateAsync();

        explosion.transform.position = position;
        explosion.transform.localScale = Vector3.one * size;
        explosion.gameObject.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(Duration));

        explosion.gameObject.SetActive(false);
        _pool.Destroy(explosion);
    }
}

public static class FXFactory
{

    public static readonly FXGenerator Explosion = new FXGenerator("Prefabs/FX/Explosion");
    public static readonly FXGenerator BigExplosion = new FXGenerator("Prefabs/FX/BigExplosion");


}