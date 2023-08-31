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
    private float swordAttackOffset = 0.2f;
    private float swordAttackRadius = 0.8f;
    private int swordAttackDamage = 1;
    [SerializeField] private AudioClip swordAttackAudioClip;
    private float swordAttackCooldown = 0f;
    private float swordAttackCooldownMax = 0.5f;
    private bool canAttack = true;
    private bool isAttacking = false;
    private float attackMovementCooldown;
    private float attackMovementCooldownMax = 0.5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            attackMovementCooldown += Time.deltaTime;

            if (attackMovementCooldown > attackMovementCooldownMax) 
            {
                attackMovementCooldown = 0f;
                isAttacking = false;
            }
        }
        if (!canAttack)
        {
            swordAttackCooldown += Time.deltaTime;
            if(swordAttackCooldown > swordAttackCooldownMax)
            {
                swordAttackCooldown = 0f;
                canAttack = true;
            }
        }
        if (!isAttacking)
        {
            HandleSwordAttackColliderPosition();
        }
        if (Input.GetKeyDown(KeyCode.F) && canAttack)
        {
            PlayerSwordAttack();
            isAttacking = true;
            canAttack = false;
        }
    }
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Chest>(out Chest chest))
        {
            chest.SetIsInRange(true);
        }
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.DealDamageToEnemy(swordAttackDamage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Chest>(out Chest chest))
        {
            chest.SetIsInRange(false);
        }
    }
    private void HandleSwordAttackColliderPosition()
    {
        if (lastMoveDirection.x > 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(swordAttackOffset, 0);
            swordAttackCollider.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (lastMoveDirection.x < 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(-swordAttackOffset, 0);
            swordAttackCollider.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        }
        else if (lastMoveDirection.y > 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(0, swordAttackOffset);
            swordAttackCollider.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);


        }
        else if (lastMoveDirection.y < 0) 
        {
            swordAttackCollider.gameObject.transform.localPosition = new Vector3(0, -swordAttackOffset);
            swordAttackCollider.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
    }
    public Vector3 GetMovementVectorNomalized()
    {
        if (isAttacking)
        {
            return lastMoveDirection;
        }
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
    private void PlayerSwordAttack()
    {
        OnAttackActionPerformed?.Invoke(this, EventArgs.Empty);
        AudioSource.PlayClipAtPoint(swordAttackAudioClip, transform.position);
    }
    public void SwordAttack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(swordAttackCollider.transform.position, swordAttackRadius, Vector2.zero);
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