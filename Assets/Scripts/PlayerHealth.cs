using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealthMax = 3;
    private int playerHealthCurrent;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnPlayerDied;


    [SerializeField] private GameOverUI GameOverUI;
    private void Awake()
    {
        playerHealthCurrent = playerHealthMax;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            int enemyDamageToTake = enemy.GetEnemyDamage();
            TakeDamage(enemyDamageToTake);

            OnHealthChanged?.Invoke(this, EventArgs.Empty);    
            if (playerHealthCurrent <= 0)
            {
                CallPlayerDiedEvent();
            }
        }
        else if(collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            int bulletDamageToTake = bullet.GetBulletDamage();
            TakeDamage(bulletDamageToTake);
            bullet.DestroyBullet();

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
            if (playerHealthCurrent <= 0)
            {
                CallPlayerDiedEvent();
            }
        }
    }
    private void CallPlayerDiedEvent()
    {
        OnPlayerDied?.Invoke(this, EventArgs.Empty);
    }

    private void TakeDamage(int damageAmmount)
    {
        playerHealthCurrent -= damageAmmount;
    }
    public int GetPlayerHealthMax()
    {
        return playerHealthMax;
    }
    public int GetPlayerHealthCurrent()
    {
        return playerHealthCurrent;
    }
}
