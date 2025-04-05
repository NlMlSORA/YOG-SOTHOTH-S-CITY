using UnityEngine;

public class PetView : SingletonMonoBehavior<PetView>
{
    public Player Player;
    public Transform playerTransform;
    public float followDistance = 1.5f;
    public float moveSpeed;
    public float jumpForce;
    public float jumpHeightThreshold = 1.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;

    private float idleTimer = 0f;
    public float idleDelay = 5f; // 等待多少秒后触发站立动画

    private bool idleLocked = false; // 是否已经进入 Idle 最后一帧状态

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
        {
            Player = PlayerManager.instance.player;
            playerTransform = Player.transform;
            moveSpeed = Player.moveSpeed;
            jumpForce = Player.jumpForce;
        }

        TeleportBehindPlayer();
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return;

        float horizontalDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float directionX = Mathf.Sign(playerTransform.position.x - transform.position.x);

        // 控制朝向
        Vector3 scale = transform.localScale;
        scale.x = directionX > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        // 是否需要移动
        bool shouldMove = horizontalDistance > followDistance;

        // 控制动画状态（5秒后才 idle，播放完后锁定最后一帧）
        if (animator != null)
        {
            if (shouldMove)
            {
                animator.SetBool("isMoving", true);
                animator.speed = 1f;         // 恢复动画速度
                idleLocked = false;         // 解锁 idle 状态
                idleTimer = 0f;
            }
            else
            {
                idleTimer += Time.fixedDeltaTime;
                if (idleTimer >= idleDelay && !idleLocked)
                {
                    animator.SetBool("isMoving", false);
                    // 播放 idle 动画，播放完成后动画事件会锁定最后一帧
                }
            }
        }

        // 控制移动
        if (shouldMove)
        {
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        // 跳跃判断
        float verticalOffset = playerTransform.position.y - transform.position.y;
        if (IsGrounded() && verticalOffset > jumpHeightThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void TeleportBehindPlayer(float offset = 1.5f)
    {
        if (playerTransform == null) return;

        Vector3 playerPos = playerTransform.position;
        float playerScaleX = playerTransform.localScale.x;
        float direction = Mathf.Sign(playerScaleX);

        Vector3 newPos = playerPos - new Vector3(direction * offset, 0, 0);
        transform.position = newPos;

        Vector3 scale = transform.localScale;
        scale.x = direction > 0 ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
