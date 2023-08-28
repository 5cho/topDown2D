using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject swordAttackCollider;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private float moveSpeed = 5f;
    private bool isFacingRight;

    [SerializeField] private Vector2 swordAttackBoxSize = new Vector2(0.3f, 0.5f);


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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

        if(moveX == 1)
        {
            isFacingRight = true;
        }
        else if(moveX == -1)
        {
            isFacingRight = false;
        }

        if (isFacingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
        }

        HandleAnimations();

        if (Input.GetKeyDown(KeyCode.F))
        {
            SwordAttack();
        }
        
    }


    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
    private void HandleAnimations()
    {
        if (moveDirection == Vector3.zero)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }

    private void SwordAttack()
    {
        Vector2 swordAttackDirection;
        if (isFacingRight)
        {
            swordAttackDirection = new Vector2(1, 0);
        }
        else
        {
            swordAttackDirection = new Vector2(-1, 0);
        }
        RaycastHit2D[] hits = Physics2D.BoxCastAll(swordAttackCollider.transform.position, swordAttackBoxSize, 0f, Vector2.zero);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.GetComponent<Enemy>())
            {
                Enemy enemyToKill = hits[i].collider.GetComponent<Enemy>();
                enemyToKill.Die();
            }
        }
    }
}