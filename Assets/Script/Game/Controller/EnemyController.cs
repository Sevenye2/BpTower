using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyController
{
    private static BufferPool<EnemyViewer> _pool;
    private static BufferPool<Transform> _shadowPool;

    public static void Clear()
    {
        _pool?.Clear();
        _shadowPool?.Clear();
    }


    public float Hp { get; private set; }
    public const float HpMax = 10;
    public EnemyViewer Viewer;
    private Transform _shadow;
    private readonly FsmAdvance _fsm;
    public bool _isDead = false;

    public EnemyController()
    {
        _pool ??= new(ReferenceManager.Instance.enemyPrefab);
        _shadowPool ??= new(ReferenceManager.Instance.enemyShadowPrefab);

        Hp = HpMax;

        _fsm = new FsmAdvance();

        var idle = _fsm.Get<EnemyIdle>("idle");
        var walk = _fsm.Get<EnemyWalk>("walk");
        var atk = _fsm.Get<EnemyAtk>("atk");

        idle.Init(this);
        idle.Walk = walk;
        idle.Atk = atk;

        walk.Init(this);
        walk.Idle = idle;

        atk.Init(this);
        atk.Idle = idle;

        _fsm.Start("idle");
    }

    public async UniTask Link()
    {
        var angle = Random.Range(0f, 360f);
        var distance = Random.Range(9.8f, 12.2f);

        var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

        Viewer = await _pool.CreateAsync();
        _shadow = await _shadowPool.CreateAsync();

        Viewer.Init(_shadow, this);
        Viewer.SetPosition(position);

        Viewer.gameObject.SetActive(true);
        _shadow.gameObject.SetActive(true);
    }


    public void Run()
    {
        _fsm.Run();
    }

    public void OnBeAttacked(float damage, Action onKilled)
    {
        Hp -= damage;
        if (Hp > 0) return;
        onKilled?.Invoke();
        OnDead();
    }

    private void Dead()
    {
    }


    public void OnDead()
    {
        _isDead = true;
        Viewer.gameObject.SetActive(false);
        _shadow.gameObject.SetActive(false);

        _pool.Destroy(Viewer);
        _shadowPool.Destroy(_shadow);
        
        ProcessController.Instance.EnemyDead(this);
    }
}