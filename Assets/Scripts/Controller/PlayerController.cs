using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Giavapps.MIDI;

public class PlayerController : MonoBehaviour
{

    TimingManager theTimingManager;

    //private GameObject NoteCanvas;

    void Awake()
    {
        //Prints all available MIDI Input devices
        Debug.Log("MIDI INPUT DEVICES:");
        uint i;
        for (i = 0; i < Midi.Input.Device.Count(); i++)
        {
            Debug.Log(Midi.Input.Device.Name(i));
        }
        Midi.Input.Device.Open(0);
        Midi.Input.Message.ManualChecking(true);
        //theTimingManager = GetComponent<TimingManager>();
        //NoteCanvas = GameObject.Find("NateCavas");
    }


    void Start()
    {
        PlayerSingleton.GetInstance.CheckScene = 0;
        //theTimingManager = FindObjectOfType<TimingManager>();
        //theTimingManager = NoteCanvas.GetComponent<TimingManager>();
        theTimingManager = null;
        Debug.Log("TimingManager start : " + theTimingManager);
    }

    void Update()
    {
        if(PlayerSingleton.GetInstance.CheckScene == 1)
        {
            Debug.Log("CheckScene : " + PlayerSingleton.GetInstance.CheckScene);
            PlayerSingleton.GetInstance.NoteCanvas = GameObject.Find("NoteCanvas");
            Debug.Log("TimingManager Next: "+ PlayerSingleton.GetInstance.NoteCanvas);
            theTimingManager = PlayerSingleton.GetInstance.NoteCanvas.GetComponent<TimingManager>();
            PlayerSingleton.GetInstance.CheckScene += 1;
        }
        if (PlayerSingleton.GetInstance.CheckScene == 2)
        {
            Drum();
            KeyBoard();
        }
    }

    void Drum()
    {
        string[] notes = null;
        for (uint d = 0; d < Midi.Input.Device.Count(); d++)
        {
            ulong messages = Midi.Input.Message.Count(d);

            for (ulong m = 0; m < messages; m++)
            {
                ////ulong time = Midi.Input.Message.Time(d, m);
                ulong size = Midi.Input.Message.Size(d, m);
                string bytes = "";

                for (ulong b = 0; b < size; b++)
                {
                    bytes += Midi.Input.Message.Byte(d, m, b).ToString() + " ";
                }
                notes = bytes.Split(' ');
                //Debug.Log(bytes);
            }
        }
        if (notes != null && notes[2] != "64")
        {
            CheckTiming(notes[1]);
        }
    }

    void KeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            CheckTiming("49");
        if (Input.GetKeyDown(KeyCode.W))
            CheckTiming("46");
        if (Input.GetKeyDown(KeyCode.S))
            CheckTiming("42");
        if (Input.GetKeyDown(KeyCode.E))
            CheckTiming("38");
        if (Input.GetKeyDown(KeyCode.R))
            CheckTiming("36");
        if (Input.GetKeyDown(KeyCode.Space))
            CheckTiming("48");
        if (Input.GetKeyDown(KeyCode.U))
            CheckTiming("45");
        if (Input.GetKeyDown(KeyCode.I))
            CheckTiming("41");
        if (Input.GetKeyDown(KeyCode.O))
            CheckTiming("51");
        if (Input.GetKeyDown(KeyCode.P))
            CheckTiming("57");
    }

    void CheckTiming(string note)
    {
        midiDebug(note);
        switch (note)
        {
            //Crash1
            case "49":
            case "55":
                theTimingManager.CheckTiming(0);
                break;
            //Hi hat open
            case "46":
            case "26":
                theTimingManager.CheckTiming(1);
                break;
            //Hi hat pedal
            case "42":
            case "22":
            case "44":
                theTimingManager.CheckTiming(2);
                break;
            //Kick
            case "36":
                theTimingManager.CheckTiming(3);
                break;
            //Snare
            case "38":
            case "40":
                theTimingManager.CheckTiming(4);
                break;
            //Tom1
            case "48":
            case "50":
                theTimingManager.CheckTiming(5);
                break;
            //Tom2
            case "45":
            case "47":
                theTimingManager.CheckTiming(6);
                break;
            //Tom3
            case "41":
            case "39":
                theTimingManager.CheckTiming(7);
                break;
            //Ride
            case "51":
            case "59":
            case "53":
                theTimingManager.CheckTiming(8);
                break;
            //Crash2
            case "57":
            case "52":
                theTimingManager.CheckTiming(9);
                break;
            default:
                // theTimingManager.CheckTiming();
                break;
        }
    }


    void midiDebug(string note)
    {
        switch (note)
        {
            //Crash1
            case "49":
            case "55":
                Debug.Log("Crash1: " + note);
                break;
            //Hi hat open
            case "46":
            case "26":
                Debug.Log("Hi hat open: " + note);
                break;
            //Hi hat pedal
            case "42":
            case "22":
            case "44":
                Debug.Log("Hi hat pedal: " + note);
                break;
            //Kick
            case "36":
                Debug.Log("Kick: " + note);
                break;
            //Snare
            case "38":
            case "40":
                Debug.Log("Snare: " + note);
                break;
            //Tom1
            case "48":
            case "50":
                Debug.Log("Tom1: " + note);
                break;
            //Tom2
            case "45":
            case "47":
                Debug.Log("Tom2: " + note);
                break;
            //Tom3
            case "41":
            case "39":
                Debug.Log("Tom3: " + note);
                break;
            //Ride
            case "51":
            case "59":
            case "53":
                Debug.Log("Ride: " + note);
                break;
            //Crash2
            case "57":
            case "52":
                Debug.Log("Crash2: " + note);
                break;
            default:
                Debug.Log("Wrong Midi: " + note);
                break;
        }
    }

    void OnApplicationQuit()
    {
        Midi.Deinitialize();//Deinitializes Midi
    }
}
