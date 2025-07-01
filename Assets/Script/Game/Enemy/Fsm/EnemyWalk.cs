using UnityEngine;

public class EnemyWalk : EnemyState
{
    private const float WalkTime = 0.5f;
    private const float Height = 0.5f;

    private float _time;
    private Vector3 _direction;

    public EnemyState Idle;

    protected override void OnEnter()
    {
        _direction = Player.PlanePosition - Controller.Viewer.PlanePosition;
        _direction.Normalize();

        _time = 0f;
    }

    protected override void OnUpdate()
    {
        _time += Time.deltaTime;

        var xy = Controller.Viewer.PlanePosition + _direction * Time.deltaTime;
        var h = Mathf.Sin(_time / WalkTime * Mathf.PI) * Height;

        Controller.Viewer.SetPosition(new Vector3(xy.x, xy.y, h));

        if (_time > WalkTime)
            Idle.Jump();
    }
}