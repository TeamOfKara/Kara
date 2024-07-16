using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Normal, // Movement, Jump, Object_Interaction
    Wait, // Not_Movementm Not_Jump, UI_Interaction
    Dead // Not_Movement, Not_Jump
}

public class PlayerStateMachine : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CapsuleCollider2D col;
    private PlayerController playerController;

    public IPlayerState CurrentState { get; private set; }
    private Dictionary<PlayerState, IPlayerState> stateDictionary = new Dictionary<PlayerState, IPlayerState>();

    public void Awake()
    {
        this.TryGetComponent<Rigidbody2D>(out rigid);
        this.TryGetComponent<CapsuleCollider2D>(out col);
        this.TryGetComponent<PlayerController>(out playerController);
        
        IPlayerState normal = new PlayerStateNormal(rigid, col, playerController);
        IPlayerState wait = new PlayerStateWait(rigid, col, playerController);
        IPlayerState dead = new PlayerStateDead(rigid, col, playerController);

        stateDictionary.Add(PlayerState.Normal, normal);
        stateDictionary.Add(PlayerState.Wait, wait);
        stateDictionary.Add(PlayerState.Dead, dead);

        CurrentState = normal;
        CurrentState.OperateEnter();
    }

    public void SetState(IPlayerState state)
    {
        if(CurrentState == state) return;

        CurrentState.OperateExit();

        CurrentState = state;
        
        CurrentState.OperateEnter();
    }

    public void FixedUpdate() => CurrentState.OperateFixedUpdate();
    public void Update() => CurrentState.OperateUpdate();
    public void LateUpdate() => CurrentState.OperateLateUpdate();
}