using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateNormal : PlayerStateComponent, IPlayerState
{
    // Movement
    private float baseSpeed = 5f;
    private float sprintMultiple = 1.5f;
    private float moveSpeed = 0f;

    // Jump
    private float jumpForce = 2f; // Jump 값
    private float jumpMultiple = 10f;
    private int jumpCount = 0;
    private int jumpMaxCount = 2;
    private bool isFirstJump = true;
    private bool isJumping = false;
    private float jumpTime = 0f;

    private void Sprint()
    {
        moveSpeed = playerInputSystem.isSprinting ? baseSpeed * sprintMultiple : baseSpeed;
    }
    
    private void Move()
    {
        rigid.velocity = new Vector2(playerInputSystem.moveVector.x * moveSpeed, rigid.velocity.y);
    }

    private void JumpAction()
    {
        if (jumpCount >= jumpMaxCount) return;
        
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumpTime = Time.time;
        isJumping = true;
        ++jumpCount;
    }

    private void Jump()
    {
        if (!playerInputSystem.isJumping || !isJumping) return;

        if (isFirstJump) {
            isFirstJump = false;
        }

        if (isJumping) {
            rigid.AddForce(new Vector2(0, jumpForce) * jumpMultiple, ForceMode2D.Force);
        }

        if (Time.time - jumpTime > 0.2f) {
            isJumping = false;
        }
    }

    public void OperateFixedUpdate()
    {
        Move();
        Jump();
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
        playerInputSystem.jumpAction += () => JumpAction();
        playerInputSystem.groundAction += () => jumpCount = 0;
    }

    public void OperateExit()
    {
        playerInputSystem.jumpAction -= () => JumpAction();
        playerInputSystem.groundAction -= () => jumpCount = 0;
    }
}