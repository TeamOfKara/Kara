using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWait : PlayerStateConponent, IPlayerState
{
    public PlayerStateWait(Rigidbody2D rigid, CapsuleCollider2D col, PlayerController playerController) : base(rigid, col, playerController) { }
    
    public void OperateUpdate()
    {
        
    }

    public void OperateFixedUpdate() { }

    public void OperateLateUpdate() { }

    public void OperateEnter() { }

    public void OperateExit() { }
}
