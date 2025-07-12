using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;

public class Doom : Object2D
{
    private static Doom _prefab;
    private static BufferPool<Doom> _pool;

    private static Transform _explosionPrefab;
    private static BufferPool<Transform> _explosionPool;

    public static async UniTask Create(DoomData data, Action<RuntimeData> onHit = null)
    {
        _prefab ??= Resources.Load<Doom>($"Prefabs/Doom");
        _pool ??= new BufferPool<Doom>(_prefab);

        var doom = await _pool.CreateAsync();
        doom.Initialized(data, onHit);
        doom.gameObject.SetActive(true);
    }


    private DoomData _data;
    private Action<RuntimeData> _onHit;
    private Vector3 _dir;
    private float _speed;

    private void Initialized(DoomData data, Action<RuntimeData> onHit)
    {
        _data = data;
        _onHit = onHit;

        _dir = new Vector3(-3, 0, 10).normalized;
        WorldPosition = data.Target + _dir * 10;

        _speed = 10;
    }


    private void Update()
    {
        _speed += Time.deltaTime * 20;
        WorldPosition -= _dir * (_speed * Time.deltaTime);

        if (Vector3.Magnitude(WorldPosition - _data.Target) > 0.1f) return;

        _pool.Destroy(this);
        gameObject.SetActive(false);

        _ = FXFactory.Explosion.CreateAsync(_data.Target);

        var results = new Collider2D[16];
        var size = Physics2D.OverlapCircleNonAlloc(WorldPosition, _data.Radius, results);

        Debug.DrawRay(transform.position, Vector3.right * _data.Radius, Color.red,2);
        
        for (var i = 0; i < size; i++)
        {
            var e = results[i].GetComponent<EnemyViewer>();

            if (!e) continue;
            
            _onHit.Invoke(new RuntimeData()
            {
                Position = _data.Target,
                Enemy = e
            });
        }
    }
}


public struct DoomData
{
    public Vector3 Target;
    public float Radius;
}