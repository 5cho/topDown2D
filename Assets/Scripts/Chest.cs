using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ProgressBarUI progressBarUI;
    private bool isInRange;

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
