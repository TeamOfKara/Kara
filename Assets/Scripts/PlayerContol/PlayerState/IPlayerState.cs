public interface IPlayerState
{
    void OperateEnter();
    void OperateFixedUpdate();
    void OperateUpdate();
    void OperateLateUpdate();
    void OperateExit();
    IPlayerState ThisState();
    PlayerStateComponent ThisComponent();
}