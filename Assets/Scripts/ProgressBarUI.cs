using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBarUI : MonoBehaviour
{
    private bool isFilling = false;
    private float fillAmmount = 0f;
    private bool isChestOpen = false;
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject bar;
    [SerializeField] private Sprite openChestSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public event EventHandler OnChestOpened;

    private void Update()
    {
        if (fillAmmount < 0)
        {
            fillAmmount = 0f;
        }
        if (fillAmmount > 1 && !isChestOpen)
        {
            fillAmmount = 0;
            isChestOpen = true;
            OnChestOpened?.Invoke(this, EventArgs.Empty);
            spriteRenderer.sprite = openChestSprite;
        }
        if (isFilling && !isChestOpen)
        {
            fillAmmount += Time.deltaTime / 2;
            fillImage.fillAmount = fillAmmount;
        }
        if (!isFilling && !isChestOpen)
        {
            fillAmmount -= Time.deltaTime / 2;
            fillImage.fillAmount = fillAmmount;
        }
        if (fillImage.fillAmount <= 0 || fillImage.fillAmount >= 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show()
    {
        bar.gameObject.SetActive(true);
    }
    private void Hide()
    {
        bar.gameObject.SetActive(false);
    }
    public void SetIsFilling(bool state)
    {
        isFilling = state;
    }
}