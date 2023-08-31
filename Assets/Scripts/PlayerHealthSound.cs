using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipArray;
    private AudioSource audioSource;
    private PlayerHealth playerHealth;
    private int audioClipIndex;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();

        audioSource.clip = audioClipArray[audioClipArray.Length - 1];
        audioSource.Play();
    }
    private void Start()
    {
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        audioClipIndex = playerHealth.GetPlayerHealthCurrent();
        if(audioClipIndex > 0)
        {
            audioSource.clip = audioClipArray[audioClipIndex - 1];
            audioSource.Play();
        }
    }
}
