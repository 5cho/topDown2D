using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static string DIED = "Died";
    [SerializeField] private int health;
    private Animator animator;
    private bool isAggroed;
    private PlayerController playerController;
    private Vector3 playerPosition;
    private Rigidbody2D rb;
    private float movesSpeed = 1f;
    private float aggroRange = 3f;
    private int enemyDamage = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = 1;
    }
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        HandleAggro();
        
        if (health <= 0)
        {
            PlayDieAnimation();
        }
    }
    private void FixedUpdate()
    {
        if (isAggroed)
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
        Vector3 direction = playerPosition - transform.position;
        rb.velocity = direction.normalized * movesSpeed;

    }
    private void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
    public void DealDamageToEnemy(int damageAmmount)
    {
        health -= damageAmmount;
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
}
