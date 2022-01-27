using Giavapps.MIDI;
using System.Collections.Generic;
using UnityEngine;

public class MidiInputManager : MonoBehaviour
{
    void Awake()
    {
        //Prints all available MIDI Input devices
        Debug.Log("MIDI INPUT DEVICES:");
        uint i;
        for (i = 0; i < Midi.Input.Device.Count(); i++)
        {
            Debug.Log(Midi.Input.Device.Name(i));
        }
        Midi.Input.Device.Open(0);//Opens the third MIDI Input Device ("Oxygen 49" MIDI Keyboard in my case)
        Midi.Input.Message.ManualChecking(true);
    }

    //void Start() { }

    void Update()
    {
        string[] notes = null;
        for (uint d = 0; d < Midi.Input.Device.Count(); d++)
        {
            ulong messages = Midi.Input.Message.Count(d);

            for (ulong m = 0; m < messages; m++)
            {
                ulong time = Midi.Input.Message.Time(d, m);
                ulong size = Midi.Input.Message.Size(d, m);
                string bytes = "";

                for (ulong b = 0; b < size; b++)
                {
                    bytes += Midi.Input.Message.Byte(d, m, b).ToString() + " ";
                }
                notes = bytes.Split(' ');
            }
        }
        if(notes != null && notes[2] != "64")
            Debug.Log(soundName[notes[1]]);
    }

    Dictionary<string, string> soundName = new Dictionary<string, string>()
        {
            {"49", "Crash1" },
            {"55", "Crash1" },
            {"46", "Hi hat open" },
            {"26", "Hi hat open" },
            {"42", "Hi hat close" },
            {"22", "Hi hat close" },
            {"44", "Hi hat close" },
            {"36", "Kick" },
            {"38", "Snare" },
            {"40", "Snare" },
            {"48", "Tom1" },
            {"50", "Tom1" },
            {"45", "Tom2" },
            {"47", "Tom2" },
            {"41", "Tom3" },
            {"39", "Tom3" },
            {"51", "Ride" },
            {"59", "Ride" },
            {"53", "Ride" },
            {"57", "Crash2" },
            {"52", "Crash2" },
        };

    void OnApplicationQuit()
    {
        Midi.Deinitialize();//Deinitializes Midi
    }
}
