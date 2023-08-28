using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static string DIED = "Died";
    [SerializeField] private int health;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = 1;
    }
    private void Update()
    {
        if (health <= 0)
        {
            PlayDieAnimation();
        }
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
}
