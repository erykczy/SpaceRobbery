using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public GameObject SoundPrefab;
    public static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudio(List<AudioClip> clips)
    {
        PlayAudio(clips[Random.Range(0, clips.Count)]);
    }

    public void PlayAudio(AudioClip clip)
    {
        var soundObj = Instantiate(SoundPrefab, transform);
        var soundSource = soundObj.GetComponent<AudioSource>();
        soundSource.clip = clip;
        soundSource.Play();

        StartCoroutine(DeleteSoundObject(soundSource));
    }

    private IEnumerator DeleteSoundObject(AudioSource source)
    {
        yield return new WaitForSecondsRealtime(source.clip.length);
        Destroy(source.gameObject);
    }
}
