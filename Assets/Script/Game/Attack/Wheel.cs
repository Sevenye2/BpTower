using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;


public class Wheel : Object2D
{
    
    private static Wheel _prefab;
    private static BufferPool<Wheel> _pool;
    private static readonly List<Wheel> Wheels = new List<Wheel>();
    public static void Create(WheelData config, Action<RuntimeData> onHit = null)
    {
        _prefab ??= Resources.Load<Wheel>($"Prefabs/Wheel");
        _pool ??= new BufferPool<Wheel>(_prefab);
        _ = _pool.CreateAsync(config.Amount, (w, i) =>
        {
            w.Initialized(i, config, onHit);
            Wheels.Add(w);
            w.gameObject.SetActive(true);
        });
    }

    public static void Clear()
    {
        Wheels.ForEach(w=> Destroy(w.gameObject));
        Wheels.Clear();
        _pool.Clear();
    }
    
    
    private WheelData _data;
    private float _angle;
    private float _time;
    private Action<RuntimeData> _onHit;

    private void Initialized(int index,WheelData data, Action<RuntimeData> onHit)
    {
        _data = data;
        _onHit = onHit;
        _time = 0;
        _angle = 2 * Mathf.PI / _data.Amount * index;

        transform.localScale = Vector3.one * (_data.Radius * 0.1f);
        WorldPosition = _data.Origin;
    } 

    private void Update()
    {
        _time += Time.deltaTime;
        var distance = Mathf.Min(_time * 2, _data.Distance);

        _angle += Time.deltaTime * _data.Speed * Mathf.Deg2Rad;
        WorldPosition = _data.Origin + distance * new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle), 0);

        if (_time < _data.Duration) return;
        
        gameObject.SetActive(false);
        _pool.Destroy(this);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        var e = other.gameObject.GetComponent<EnemyViewer>();
        
        _onHit?.Invoke(new RuntimeData()
        {
            Position = e.WorldPosition,
            Enemy = e
        });
    }
}


public struct WheelData
{
    public Vector3 Origin;
    public int Amount;
    public float Duration;
    public float Radius;
    public float Distance;
    public float Speed;
}