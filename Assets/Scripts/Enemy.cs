using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.2f;
    private bool isKnockingBack = false;
    private float knockbackTimer;
    private static string DIED = "Died";
    private int currentHealth;
    [SerializeField] private int healthMax = 1;
    private Animator animator;
    private bool isAggroed;
    private PlayerController playerController;
    private Vector3 playerPosition;
    private Rigidbody2D rb;
    private float moveSpeed = 1f;
    [SerializeField] private float aggroRange = 3f;
    private int enemyDamage = 1;
    public event EventHandler OnHealthChanged;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = healthMax;
    }
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        HandleAggro();
        
        if (currentHealth <= 0)
        {
            enemyDamage = 0;
            PlayDieAnimation();
        }
        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                rb.velocity = Vector2.zero;
                isKnockingBack = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isAggroed && currentHealth > 0)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }
    private void HandleAggro()
    {
        playerPosition = playerController.GetPlayerTransform().position;
        if (Vector3.Distance(transform.position, playerPosition) < aggroRange)
        {
            isAggroed = true;
        }
        else
        {
            isAggroed = false;
        }
    }
    private void MoveTowardsPlayer()
    {
        if (!isKnockingBack)
        {
            Vector3 direction = playerPosition - transform.position;
            rb.velocity = direction.normalized * moveSpeed;
        }
    }
    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
    public void DealDamageToEnemy(int damageAmmount)
    {
        currentHealth -= damageAmmount;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if(currentHealth > 0)
        {
            Knockback();
        }
    }
    private void PlayDieAnimation()
    {
        animator.SetTrigger(DIED);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public int GetEnemyDamage()
    {
        return enemyDamage;
    }
    public float GetCurrentHealthNormalized()
    {
        return (float)currentHealth / healthMax;
    }
    private void Knockback()
    {
        isKnockingBack = true;
        Vector2 playerPosition = playerController.GetPlayerTransform().position;
        Vector2 direction = -(playerPosition - (Vector2)transform.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        knockbackTimer = knockbackDuration;
    }
}
