using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("===Reference===")]
    public PlayerAction playerAction;
    public PlayerInputSystem playerInputSystem;
    public PlayerStateMachine playerStateMachine;
     
    private void Awake()
    {
        if (this.TryGetComponent<PlayerInputSystem>(out playerInputSystem) == false) {
            playerInputSystem = this.AddComponent<PlayerInputSystem>();
        }

        if (this.TryGetComponent<PlayerStateMachine>(out playerStateMachine) == false) {
            playerStateMachine = this.AddComponent<PlayerStateMachine>();
        }
    }

    public void ChangePlayerState(PlayerState state)
    {
        switch (state) {
            case PlayerState.Normal :
                playerStateMachine.SetState<PlayerStateNormal>();
                break;
            case PlayerState.Fly :
                playerStateMachine.SetState<PlayerStateFly>();
                break;
            case PlayerState.Wait :
                playerStateMachine.SetState<PlayerStateWait>();
                break;
            case PlayerState.Dead :
                playerStateMachine.SetState<PlayerStateDead>();
                break;
        }
    }
}
