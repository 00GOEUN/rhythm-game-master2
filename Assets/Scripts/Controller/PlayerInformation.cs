using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public float NoteSpeed { get; set; }
    public int score { get; set; }
    public bool LED { get; set; }
    public float LEDTime { get; set; }
    public double LEDDuration { get; set; }

    void Awake()
    {
        var obj = FindObjectsOfType<PlayerInformation>();
        if (obj.Length == 1) { DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        NoteSpeed = 200;
        score = 0;
        LED = true;
        LEDTime = 0.3f;
        LEDDuration = 0.2;
    }
}
