using System;
using Framework;
using UnityEngine;

public class EnemyViewer : Object2D
{
    public SpriteRenderer body;
    private Transform _shadow;
    private EnemyController _controller;
    public HpBar hpBar;
    public void Init(Transform shadow, EnemyController controller)
    {
        _controller = controller;
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
        if (_controller != null)
        {
            hpBar.SetValue(_controller.Hp/ EnemyController.HpMax);
        }
        
    }


    public void BeAttacked(float damage, Action onKilled)
    {
        _controller.OnBeAttacked(damage, onKilled);
    }
}