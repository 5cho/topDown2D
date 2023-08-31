using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollider : MonoBehaviour
{
    public int audioClipIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MusicManager.Instance.SetAudioClip(audioClipIndex);
    }
}
