using UnityEngine;

public class EnemyIdle : EnemyState
{
    private readonly float _waitTime = 1f;
    private float _time;
    public EnemyState Walk;
    public EnemyState Atk;
    protected override void OnEnter()
    {
        _time = 0;
    }

    protected override void OnUpdate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime) return;
        
        var distance = (Player.transform.position - Controller.Viewer.transform.position).magnitude;
        
        if(distance < 1f)
            Atk.Jump();
        else
            Walk.Jump();
    }
}