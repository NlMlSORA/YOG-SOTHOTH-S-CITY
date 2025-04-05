using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D cd { get; private set; }
    //public EntityFX fX { get; private set; }
    public CharacterStats stats { get; private set; }

    #region Collision
    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    public Transform attackCheck;
    public float attackCheckRadius;

    #endregion

    #region Filp
    public float facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;
    #endregion

    #region Knocked
    [SerializeField] protected Vector2 knockedDirection;
    protected bool isKnocked;
    [SerializeField] protected float knockedDuration;


    #endregion


    public System.Action OnFlipped;

    public bool canHurt = true;





    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        //fX = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }


    protected virtual void Update()
    {

    }


    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(OnFlipped != null)
            OnFlipped();
    }

    public virtual void FlipController(float _xVelocity)
    {
        if (_xVelocity < 0 && facingRight)
        {
            Flip();
        }
        else if (_xVelocity > 0 && !facingRight)
        {
            Flip();
        }
    }

    #endregion

    #region Velocity
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
        {
            return;
        }

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        if (isKnocked)
        {
            return;
        }

        rb.velocity = Vector2.zero;
    }

    #endregion

    #region Collision
    public virtual bool IsGroundDetected()
    {
        bool flag1 = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        bool flag2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, whatIsGround);
        if (flag1 || flag2)
        {
            return true;
        }
        return false;
    }

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck2.position, new Vector2(groundCheck2.position.x, groundCheck2.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }


    #endregion

    public virtual void Damage(float distance)
    {
        if (canHurt)
        {
            //fX.StartCoroutine("FlashFX");
            if (distance < 0 && facingDir == 1)
            {
                Flip();
            }
            else if (distance > 0 && facingDir == -1)
            {
                Flip();
            }



            StartCoroutine("Knocked");
        }
    }

    protected virtual IEnumerator Knocked()
    {
       
        isKnocked = true;
        rb.velocity = new Vector2(knockedDirection.x * -facingDir, knockedDirection.y);

        yield return new WaitForSeconds(knockedDuration);
        isKnocked = false;
    }

    public virtual void Die()
    {

    }

    

}
