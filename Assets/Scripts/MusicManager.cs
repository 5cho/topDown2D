using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipArray;
    private AudioSource audioSource;

    public static MusicManager Instance;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void SetAudioClip(int audioClipIndex)
    {
        if(audioClipArray[audioClipIndex] != audioSource.clip)
        {
            audioSource.clip = audioClipArray[audioClipIndex];
            audioSource.Play();
        }
    }
}
