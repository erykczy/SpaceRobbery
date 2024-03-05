using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public bool PlayOnEnable;
    public List<AudioClip> Clips;

    private void OnEnable()
    {
        if (PlayOnEnable)
            PlaySound();
    }

    public void PlaySound()
    {
        AudioPlayer.Instance.PlayAudio(Clips);
    }
}
