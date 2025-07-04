using UnityEngine;

public class EnemyAtk : EnemyState
{
    private const float AtkTime = 2f;
    private float _time;
    private Vector3 _o;
    public EnemyState Idle;
    

    protected override void OnEnter()
    {
        _time = 0f;
        _o = Controller.Viewer.PlanePosition;
    }

    protected override void OnUpdate()
    {
        _time += Time.deltaTime;

        var x = Mathf.Cos(_time * Mathf.PI * 5) * 0.02f;
        Controller.Viewer.SetPosition(_o + Vector3.right * x);
        
        if (_time < AtkTime) return;
        Idle.Jump();
        Player.Controller.BeAttacked(5);

        _ = FXFactory.BigExplosion.CreateAsync(_o);
        Controller.Destroy();
    }

    protected override void OnExit()
    {
        Controller.Viewer.SetPosition(Vector3.zero);
    }
}