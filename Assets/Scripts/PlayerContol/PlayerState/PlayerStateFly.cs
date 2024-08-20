using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFly : PlayerStateComponent, IPlayerState
{
    // Movement
    private float baseSpeed = 5f;
    private float sprintMultiple = 1.5f;
    private float moveSpeed = 0f;

    // Jump
    private float jumpForce = 2f; // Jump 값
    private float jumpMultiple = 10f;
    private bool isJumping = false;
    private float jumpTime = 0f;

    // Fly
    private bool isFly = false;
    private float gravity;

    protected override void Awake()
    {
        base.Awake();
        gravity = rigid.gravityScale;
    }

    private void Sprint()
    {
        moveSpeed = playerInputSystem.isSprinting ? baseSpeed * sprintMultiple : baseSpeed;
    }
    
    private void Move()
    {
        rigid.velocity = new Vector2(playerInputSystem.moveVector.x * moveSpeed, rigid.velocity.y);
    }

    private void FlyMove()
    {
        rigid.velocity = new Vector2(playerInputSystem.moveVector.x * moveSpeed, playerInputSystem.moveVector.y * moveSpeed);
    }

    private void JumpAction()
    {
        if (playerInputSystem.IsGrounded && !isFly) {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpTime = Time.time;
            isJumping = true;
        }
        else {
            if (!isFly) {
                isFly = true;
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = 0f;
            }
            else {
                isFly = false;
                rigid.velocity = Vector2.zero;
                rigid.gravityScale = gravity;
            }
        }
    }

    private void JumpAndFly()
    {
        if (!playerInputSystem.isJumping) { 
            isJumping = false; 
            return; 
        }

        if (isJumping && Time.time - jumpTime > 0.2f) {
            isJumping = false;
        }
        else if (isJumping) {
            rigid.AddForce(new Vector2(0, jumpForce) * jumpMultiple, ForceMode2D.Force);
        }
    }

    public void OperateFixedUpdate()
    {
        if (!isFly) {
            Move();
        }
        else {
            FlyMove();
        }
        
        JumpAndFly();
    }
    
    public void OperateUpdate()
    {
        Sprint();
    }

    public void OperateLateUpdate()
    {
        
    }

    public void OperateEnter()
    {
        playerInputSystem.jumpAction += JumpAction;
    }

    public void OperateExit()
    {
        playerInputSystem.jumpAction -= JumpAction;
    }
}