using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealthMax = 3;
    private int playerHealthCurrent;

    private float invulnerableTimer = 0f;
    private float invulnerableTimerMax = 1f;
    private bool isInvulnerable = false;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnPlayerDied;

    [SerializeField] private AudioClip playerTakeDamageAudioClip;


    [SerializeField] private GameOverUI GameOverUI;
    private void Awake()
    {
        playerHealthCurrent = playerHealthMax;
    }
    private void Update()
    {
        if (isInvulnerable)
        {

            invulnerableTimer += Time.deltaTime;
            if(invulnerableTimer > invulnerableTimerMax)
            {
                invulnerableTimer = 0;
                isInvulnerable = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy) && !isInvulnerable)
        {
            int enemyDamageToTake = enemy.GetEnemyDamage();
            if(enemyDamageToTake != 0)
            {
                TakeDamage(enemyDamageToTake);
                AudioSource.PlayClipAtPoint(playerTakeDamageAudioClip, transform.position);
                OnHealthChanged?.Invoke(this, EventArgs.Empty);
                if (playerHealthCurrent <= 0)
                {
                    CallPlayerDiedEvent();
                }

                isInvulnerable = true;
            }
        }
        else if(collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet) && !isInvulnerable)
        {
            int bulletDamageToTake = bullet.GetBulletDamage();
            TakeDamage(bulletDamageToTake);
            bullet.DestroyBullet();

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
            if (playerHealthCurrent <= 0)
            {
                CallPlayerDiedEvent();
            }

            isInvulnerable = true;
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
