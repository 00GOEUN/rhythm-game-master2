using System;
using UnityEngine;

public class MDEvent
{
    public int Delta
    {
        get;
    }
    public byte EventType
    {
        get;
    }
    public byte[] Buffer
    {
        get;
    }
    public MDEvent(byte evtype, int delta, byte[] buffer)
    {
        EventType = evtype;
        Delta = delta;
        Buffer = buffer;

    }
    public static MDEvent Parsing(byte[] buffer, ref int offset, MDEvent mdevent)
    {
        int oldoffset = offset;
        int delta = StaticFuns.ReadDeltaTime(buffer, ref offset);
        if (buffer[offset] == 0xFF)
        {
            offset++;
            return MetaEvent.MakeEvent(delta, buffer, ref offset, oldoffset);
        }
        if (buffer[offset] < 0xF0)
        {
            byte preEventType = (mdevent == null) ? (byte)0 : mdevent.EventType;
            return MidiEvent.MakeEvent(buffer[offset++], delta, buffer, ref offset, oldoffset, preEventType);
        }
        return null;
    }
}