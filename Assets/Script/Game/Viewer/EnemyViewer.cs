using System;
using Framework;
using UnityEngine;

public class EnemyViewer : Object2D
{
    private Transform _shadow;
    public EnemyController Controller;
    public HpBar hpBar;
    public void Init(Transform shadow, EnemyController controller)
    {
        Controller = controller;
        _shadow = shadow;
    }


    public void SetPosition(Vector3 position)
    {
        WorldPosition = position;
        _shadow.position = PlanePosition;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Controller != null)
        {
            hpBar.SetValue(Controller.Hp/ EnemyController.HpMax);
        }
        
    }


    public void BeAttacked(float damage, Action onKilled)
    {
        Controller?.OnBeAttacked(damage, onKilled);
    }
}