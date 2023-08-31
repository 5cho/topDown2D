using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ProgressBarUI progressBarUI;
    private bool isInRange;
    [SerializeField] private AudioClip chestOpenAudioClip;
    
    private void Start()
    {
        progressBarUI.OnChestOpened += ProgressBarUI_OnChestOpened;
    }

    private void ProgressBarUI_OnChestOpened(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(chestOpenAudioClip, transform.position);
    }

    private void Update()
    {
        if (isInRange && Input.GetKey(KeyCode.E))
        {
            progressBarUI.SetIsFilling(true);
        }
        else
        {
            progressBarUI.SetIsFilling(false);
        }
    }
    public void SetIsInRange(bool state)
    {
        isInRange = state;
    }
}
