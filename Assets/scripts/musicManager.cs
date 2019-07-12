using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    private static musicManager _instance;

    public static musicManager instance {
        get { return _instance; }
    }


    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public AudioSource efxSource;
    public AudioSource backgroundSource;

    private void Awake()
    {
        _instance = this;
    }

    public void RandomPlay(params AudioClip[] clips) {
        float pitch = Random.Range(minPitch,maxPitch);
        int index = Random.Range(0,clips.Length);
        AudioClip clip = clips[index];
        efxSource.clip = clip;
        efxSource.pitch = pitch;
        efxSource.Play();
    }

    public void Stopmusic() {
        backgroundSource.Stop();
        efxSource.Stop();
     }
}
