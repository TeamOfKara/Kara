using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public PlayerAction playerAction;

    // ===Movement===
    public Vector2 inputVector { get; private set; }
    public Vector2 moveVector { get; private set; }

    // ===Ground===
    public LayerMask groundLayer { get; private set; }
    private bool isGrounded;
    public bool IsGrounded
    { 
        get { return isGrounded; }
        set 
        { 
            if (isGrounded != value) {
                isGrounded = value;
                if (value == true) {
                    groundAction?.Invoke();
                }
            }
        } 
    }
    public Action groundAction;
    private float offset = .7f;
    private float radius = .2f;

    // ===Jump===
    public Action jumpAction;
    public bool isJumping { get; private set; }

    // ===Sprint===
    public bool isSprinting { get; private set; }

    private void Awake()
    {
        playerAction = new PlayerAction();

        // LayerMask
        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        // Movement
        playerAction.Player.Movement.started += OnMovement;
        playerAction.Player.Movement.performed += OnMovement;
        playerAction.Player.Movement.canceled += OnMovement;

        // Jump
        playerAction.Player.Jump.started += val => jumpAction?.Invoke();
        playerAction.Player.Jump.started += val => isJumping = true;
        playerAction.Player.Jump.canceled += val => isJumping = false;

        // Jump
        playerAction.Player.Sprint.started += val => isSprinting = true;
        playerAction.Player.Sprint.canceled  += val => isSprinting = false;
    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector2>();
        moveVector = new Vector2(inputVector.x, 0f);
    }

    private void Update()
    {
        IsGrounded = GroundedCheck();
    }

    public bool GroundedCheck()
    {
        if (Physics2D.CircleCast(this.transform.position, radius, -this.transform.up, offset, groundLayer)) {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position  -this.transform.up * offset, radius);
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
    }
}
