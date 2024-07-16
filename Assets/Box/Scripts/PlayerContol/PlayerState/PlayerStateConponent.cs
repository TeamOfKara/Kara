using UnityEngine;

public class PlayerStateConponent
{
    protected Transform player;
    protected Rigidbody2D rigid;
    protected CapsuleCollider2D col;
    protected PlayerController playerController;
    protected PlayerInputSystem playerInputSystem;

    public PlayerStateConponent(Rigidbody2D rigid, CapsuleCollider2D col, PlayerController playerController)
    {
        this.rigid = rigid;
        this.col = col;
        this.playerController = playerController;
        
        playerInputSystem = playerController.playerInputSystem;
        player = playerController.transform;
    }
}
