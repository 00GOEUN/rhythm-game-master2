using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : Chunk, IEnumerable
{
    List<MDEvent> events = new List<MDEvent>();
    public Track(int ctype, int length, byte[] buffer) : base(ctype, length, buffer)
    {
        Parsing(buffer);
    }
    //트랙 버퍼 나누기
    private void Parsing(byte[] buffer)
    {
        int offset = 0;
        MDEvent mdevent = null;
        while (offset < buffer.Length)
        {
            mdevent = MDEvent.Parsing(buffer, ref offset, mdevent);
            if (mdevent == null)
            {
                continue;
            }
            if (mdevent.EventType < 0xF0)
            {
                events.Add(mdevent);
            }
        }
    }

    //트랙 내 이벤트 순회
    public IEnumerator GetEnumerator()
    {
        return events.GetEnumerator();
    }
}