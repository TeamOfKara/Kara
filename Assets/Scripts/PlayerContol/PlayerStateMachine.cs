using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    Normal, // Movement, Jump, Object_Interaction
    Wait, // Not_Movementm Not_Jump, UI_Interaction
    Dead // Not_Movement, Not_Jump
}

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState CurrentState;

    public void Awake()
    {
        CurrentState = this.AddComponent<PlayerStateNormal>();
        CurrentState.OperateEnter();
    }

    public void Start()
    {
        Invoke(nameof(TEST), 2f);

        Invoke(nameof(TEST1), 4f);
    }
    
    public void TEST()
    {
        SetState<PlayerStateDead>();
    }

    public void TEST1()
    {
        SetState<PlayerStateNormal>();
    }

    public void SetState<T>() where T : IPlayerState
    {
        if(CurrentState.ThisState().GetType().Name == typeof(T).Name) return;

        CurrentState.OperateExit();

        Destroy(CurrentState.ThisComponent());

        CurrentState = this.gameObject.AddComponent(typeof(T)) as IPlayerState;
        
        CurrentState.OperateEnter();
    }

    public void FixedUpdate() => CurrentState.OperateFixedUpdate();
    public void Update() => CurrentState.OperateUpdate();
    public void LateUpdate() => CurrentState.OperateLateUpdate();
}