using UnityEngine;

public class PlayerStateComponent : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected CapsuleCollider2D col;
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected PlayerInputSystem playerInputSystem;

    protected virtual void Awake()
    {
        player = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        playerController  = GetComponent<PlayerController>();
        playerInputSystem = GetComponent<PlayerInputSystem>();
    }

    public IPlayerState ThisState() { return this.GetComponent<IPlayerState>(); }
    public PlayerStateComponent ThisComponent() { return this; }
}