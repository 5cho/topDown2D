using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image playerHealthFillImage;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        playerHealthFillImage.fillAmount = (float)playerHealth.GetPlayerHealthCurrent() / playerHealth.GetPlayerHealthMax();
    }
}
