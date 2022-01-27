using System.Collections.Generic;

public class MidiNoteInfo
{
    public int Time
    {
        get;
        set;
    }
    public byte Sound
    {
        get;
    }
    public byte Power
    {
        get;
    }

    public MidiNoteInfo(int delta, byte sound, byte power)
    {
        Time = delta;
        if (!soundName.ContainsKey(sound))
        {
            sound = 255;
        }
        Sound = sound;
        Power = power;
    }
    public string showNode
    {
        get
        {
            string sound; soundName.TryGetValue(Sound, out sound);
            string power;
            power = (Power != 0) ? "ON" : "OFF";
            return string.Format("Time:{0}, Sound:{1}, Power:{2}", Time, sound, power);
        }
    }

    Dictionary<byte, string> soundName = new Dictionary<byte, string>()
        {
            {49, "Crash1" },
            {55, "Crash1" },
            {46, "Hi hat open" },
            {26, "Hi hat open" },
            {42, "Hi hat close" },
            {22, "Hi hat close" },
            {44, "Hi hat close" },
            {36, "Kick" },
            {38, "Snare" },
            {40, "Snare" },
            {48, "Tom1" },
            {50, "Tom1" },
            {45, "Tom2" },
            {47, "Tom2" },
            {41, "Tom3" },
            {39, "Tom3" },
            {51, "Ride" },
            {59, "Ride" },
            {53, "Ride" },
            {57, "Crash2" },
            {52, "Crash2" },
            {255, "Undifined" }
        };
}
