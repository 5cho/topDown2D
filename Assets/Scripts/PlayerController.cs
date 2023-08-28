using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject swordAttackCollider;

    public event EventHandler OnAttackActionPerformed;

    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    private float moveSpeed = 3f;
    private float swordAttackOffset = 0.3f;
    private Vector2 swordAttackBoxSize = new Vector2(0.3f, 0.3f);
    private int swordAttackDamage = 3;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandlePlayerSpriteRotation();
        HandleSwordAttackColliderPosition();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwordAttack();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
    private void HandleSwordAttackColliderPosition()
    {
        if (lastMoveDirection.x > 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(swordAttackOffset, 0);
        }
        else if (lastMoveDirection.x < 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(-swordAttackOffset, 0);
        }
        else if (lastMoveDirection.y > 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(0, swordAttackOffset);
        }
        else if (lastMoveDirection.y < 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(0, -swordAttackOffset);
        }
    }
    private void HandlePlayerSpriteRotation()
    {
        float moveX = GetMovementVectorNomalized().x;
        if (moveX == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        }
        else if (moveX == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
        }
    }
    public Vector3 GetMovementVectorNomalized()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }

        moveDirection = new Vector3(moveX, moveY).normalized;

        if(moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }

        return moveDirection;
    }
    public bool IsMoving()
    {
        return moveDirection != Vector3.zero;
    }
    private void SwordAttack()
    {
        OnAttackActionPerformed?.Invoke(this, EventArgs.Empty);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(swordAttackCollider.transform.position, swordAttackBoxSize, 0f, Vector2.zero);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.GetComponent<Enemy>())
            {
                Enemy EnemyToAttack = hits[i].collider.GetComponent<Enemy>();
                EnemyToAttack.DealDamageToEnemy(swordAttackDamage);
            }
        }
    }
    public Transform GetPlayerTransform()
    {
        return transform;
    }
}