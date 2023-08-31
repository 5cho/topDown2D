using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float lifetime;
    private float lifetimeMax = 10f;
    private int bulletDamage = 1;

    private void Awake()
    {
        lifetime = 0f;
    }
    private void Update()
    {
        lifetime += Time.deltaTime;

        if(lifetime > lifetimeMax)
        {
            DestroyBullet();
        }

        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
    public int GetBulletDamage()
    {
        return bulletDamage;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
