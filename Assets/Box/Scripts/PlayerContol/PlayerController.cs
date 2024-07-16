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
}
