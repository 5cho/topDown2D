using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    [SerializeField] private Enemy enemy;

    private void Start()
    {
        enemy.OnHealthChanged += Enemy_OnHealthChanged;
    }

    private void Enemy_OnHealthChanged(object sender, System.EventArgs e)
    {
        fillImage.fillAmount = enemy.GetCurrentHealthNormalized();
    }
}
