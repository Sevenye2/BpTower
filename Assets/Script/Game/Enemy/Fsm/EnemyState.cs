using Framework;

public abstract class EnemyState : StateBase
{
    protected PlayerViewer Player => ProcessController.Instance.Player.Viewer;

    public EnemyController Controller;

    public void Init(EnemyController controller)
    {
        Controller = controller;
    }
}