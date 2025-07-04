using System;
using Framework;
using UnityEngine;
using UnityEngine.Serialization;

public class ReferenceManager : MonoSingleton<ReferenceManager>
{
    public Canvas mapCanvas;
    [FormerlySerializedAs("shopBtnPrefab")] [Header("Prefabs")]
    public ShopItem shopItemPrefab;
    
    
    public EnemyViewer enemyPrefab;
    public Transform enemyShadowPrefab;
    public Bullet bulletPrefab;
    public Transform bulletShadowPrefab;
    public HpBar hpBarPrefab;
    [Header("References")] 
    public PlayerViewer player;

    public Theme blue;
    public Theme red;
}

[Serializable]
public struct Theme
{
    public Color lightColor;
    public Color darkColor;
}