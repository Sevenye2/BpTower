using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 追踪形的攻击注意判断目标状态，因为缓存池的问题。容易错误追踪
/// </summary>
public class Bullet : Object2D
{
    private static BufferPool<Bullet> _pool;
    private static BufferPool<Transform> _shadowPool;

    public static async UniTaskVoid Create(Vector3 startPos, EnemyViewer target, Action<RuntimeData> onHitCallback,
        Vector3 pos = default)
    {
        _pool ??= new(ReferenceManager.Instance.bulletPrefab);
        _shadowPool ??= new(ReferenceManager.Instance.bulletShadowPrefab);

        var task1 = _pool.CreateAsync();
        var task2 = _shadowPool.CreateAsync();

        var bullet = await task1;
        bullet._onHitCallback = onHitCallback;
        bullet._target = target;
        bullet._pos = pos;
        bullet.WorldPosition = startPos;

        var shadow = await task2;
        bullet._shadow = shadow;
        shadow.position = bullet.PlanePosition;


        bullet.gameObject.SetActive(true);
        shadow.gameObject.SetActive(true);
    }

    private Action<RuntimeData> _onHitCallback;
    private EnemyViewer _target;
    private Transform _shadow;
    private Vector3 _pos;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (_target)
        {
            if (!_target.gameObject.activeSelf)
                _target = null;
        }

        Vector3 dir;
        if (_target)
        {
            _pos = _target.PlanePosition;

            var v = _target.WorldPosition - WorldPosition;
            dir = v.normalized;

            if (v.magnitude < 0.1f)
            {
                _onHitCallback?.Invoke(new()
                {
                    Position = transform.position,
                    Enemy = _target
                });
                DestroyObj();
                return;
            }
        }
        else
        {
            var v = _pos - WorldPosition;
            dir = v.normalized;

            if (v.magnitude < 0.1f)
            {
                DestroyObj();
                return;
            }
        }

        WorldPosition += dir * (Time.deltaTime * 5);
        _shadow.position = PlanePosition;
    }


    private void DestroyObj()
    {
        _target = null;

        gameObject.SetActive(false);
        _shadow.gameObject.SetActive(false);

        _pool.Destroy(this);
        _shadowPool.Destroy(_shadow);
    }
}