using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnLocation;
    [SerializeField] private float shootTimerMax = 5f;
    [SerializeField] private float shootTimerMaxDelay = 3f;

    private static string SHOOT = "Shoot";
    private Animator animator;
    private float shootTimer;
    private void Awake()
    {
        shootTimer = Random.Range(0, 3);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        shootTimer += Time.deltaTime;

        if(shootTimer > shootTimerMax)
        {
            shootTimer = Random.Range(0, shootTimerMaxDelay);

            animator.SetTrigger(SHOOT);
        }
    }
    public void ShootBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity, transform);
    }
}
