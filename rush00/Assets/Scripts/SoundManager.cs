using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void RandomSound (params AudioClip[] clips)
    {
        int randIndex = Random.Range(0, clips.Length);
        float randPitch = Random.Range(lowPitchRange, highPitchRange);

        effectSource.pitch = randPitch;
        effectSource.clip = clips[randIndex];
        effectSource.Play();
    }
}