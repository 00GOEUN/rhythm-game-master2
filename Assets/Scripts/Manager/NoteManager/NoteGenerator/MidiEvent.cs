using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MidiEvent : MDEvent
{
    public byte Fdata
    {
        get;
    }
    public byte Sdata
    {
        get;
    }

    public MidiNoteInfo GenerateNote
    {
        get
        {
            if (EventType < 0x80)
            {
                return new MidiNoteInfo(0, 0, 0);
            }
            switch (EventType >> 4)
            {
                case 0x8:
                case 0x9:
                case 0xA: return new MidiNoteInfo(Delta, Fdata, Sdata);
            }
            return new MidiNoteInfo(0, 0, 0);
        }
    }

    public MidiEvent(byte eventType, int delta, byte fdata, byte sdata, byte[] orgbuffer) : base(eventType, delta, orgbuffer)
    {
        Fdata = fdata;
        Sdata = sdata;
    }

    public static MDEvent MakeEvent(byte eventType, int delta, byte[] buffer, ref int offset, int oldoffset, byte pre_eventType)
    {
        if (offset >= buffer.Length) return null;
        byte fdata;
        byte sdata = 0;
        if (eventType < 0x80)
        {
            fdata = eventType;
            eventType = pre_eventType;
        }
        else
        {
            fdata = buffer[offset++];
        }
        switch (eventType >> 4)
        {
            case 0x8: //Note Off
            case 0x9: //Note On
            case 0xA: //Note after touch
            case 0xB: //Controller
            case 0xE: //Pitch Bend
                sdata = buffer[offset++];
                break;
            case 0xC: //Change Instrument
            case 0xD: //Channel after touch
                break;
            default: return null;
        }
        byte[] orgbuffer = new byte[offset - oldoffset];
        Array.Copy(buffer, oldoffset, orgbuffer, 0, orgbuffer.Length);
        return new MidiEvent(eventType, delta, fdata, sdata, orgbuffer);
    }
}
