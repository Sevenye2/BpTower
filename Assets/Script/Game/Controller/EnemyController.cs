using System;
using System.Collections.Generic;
using System.Linq;
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
    private static List<EnemyController> _enemies = new();

    public static void ClearAll()
    {
        _enemies.ForEach(e =>
        {
            Object.Destroy(e.Viewer.gameObject);
            Object.Destroy(e._shadow.gameObject);
        });
        _enemies.Clear();
        _pool?.Clear();
        _shadowPool?.Clear();
    }

    public static void RunAll()
    {
        _enemies = _enemies.Where(c => !c._isDead).ToList();
        _enemies.ForEach(e => { e.Run(); });
    }


    public float Hp { get; private set; }
    public float HpMax { get; private set; }
    public EnemyViewer Viewer;
    private Transform _shadow;
    private readonly FsmAdvance _fsm;
    private bool _isDead;

    public EnemyController(int hp)
    {
        _pool ??= new(ReferenceManager.Instance.enemyPrefab);
        _shadowPool ??= new(ReferenceManager.Instance.enemyShadowPrefab);

        Hp = hp;
        HpMax = hp;

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
        var distance = 0.5f * Random.Range(9.8f, 12.2f);

        var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

        Viewer = await _pool.CreateAsync();
        _shadow = await _shadowPool.CreateAsync();

        Viewer.Init(_shadow, this);
        Viewer.SetPosition(position);

        Viewer.gameObject.SetActive(true);
        _shadow.gameObject.SetActive(true);

        _enemies.Add(this);
    }


    private void Run()
    {
        _fsm.Run();
    }

    public void OnBeAttacked(float damage, Action onKilled)
    {
        Hp -= damage * (1 + SaveDataHandler.Upgrades.ExtraDamagePercent / 100f) 
              + SaveDataHandler.Upgrades.ExtraDamageFix;
        if (Hp > 0) return;
        onKilled?.Invoke();
        OnDead();
    }

    public void Destroy()
    {
        _isDead = true;
        Viewer.Controller = null;

        Viewer.gameObject.SetActive(false);
        _pool.Destroy(Viewer);

        _shadow.gameObject.SetActive(false);
        _shadowPool.Destroy(_shadow);
    }

    private void OnDead()
    {
        Destroy();
        ProcessController.Instance.EnemyDead(this);
    }
}