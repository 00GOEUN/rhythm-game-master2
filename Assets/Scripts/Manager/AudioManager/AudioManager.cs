using System;
using System.Collections;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public bool isAudioManagerReady { get; set; }
    public static AudioManager instance;
    public double noteSpeed { get; set; }

    public Sound bgm { get; set; }
    public Sound midi { get; set; }
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource midiPlayer = null;

    

    void Awake()
    {
        isAudioManagerReady = false;
        bgmPlayer.clip = bgm.clip;
        midiPlayer.clip = midi.clip;
        isAudioManagerReady = true;
        Debug.Log("AudioManager Ready");
    }

    void Start()
    {
        float timeDelay = (float)((GameObject.Find("NoteAppearLine").transform.localPosition.y - GameObject.Find("BeatLine").transform.localPosition.y) / noteSpeed);
        bgmPlayer.PlayDelayed(timeDelay);
        midiPlayer.PlayDelayed(timeDelay);
    }

    bool timeNeg = false;
    float time = 0;

    void Update()
    {
        if (timeNeg)
        {
            Invoke("Wait", time / 2);
        }
    }

    void Wait() {
        timeNeg = false;
        time = 0;
        bgmPlayer.time = time;
        midiPlayer.time = time;
        Go();
    }

    public void Repeat(double startTime, double noteTime)
    {
        float time = (float)(startTime - noteTime);
        if(time < 0)
        {
            timeNeg = true;
            this.time = -time;
            return;
        }
        bgmPlayer.time = time;
        midiPlayer.time = time;
        Go();
    }


    public void Go()
    {
        bgmPlayer.Play();
        midiPlayer.Play();
    }

    public void Stop()
    {
        bgmPlayer.Pause();
        midiPlayer.Pause();
    }

    public void ChangeBPM(float pitch)
    {
        bgmPlayer.pitch = pitch;
        midiPlayer.pitch = pitch;
    }
}
