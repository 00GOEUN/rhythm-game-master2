using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MetaEvent : MDEvent
{
    public byte Msg //어떤 종류의 메타 이벤트인지를 판별
    {
        get;
        private set;
    }

    public byte Length //메타 데이터 길이
    {
        get;
        private set;
    }

    public byte[] Data//메타 데이터
    {
        get;
        private set;
    }

    public MetaEvent(int delta, byte msg, byte len, byte[] data, byte[] orgbuffer) : base(0xFF, delta, orgbuffer)
    {
        Msg = msg;
        Length = len;
        Data = data;
    }

    public static MDEvent MakeEvent(int delta, byte[] buffer, ref int offset, int oldoffset)
    {
        byte msg = buffer[offset++];
        byte len = buffer[offset++];
        byte[] data = null;
        if (msg != 0x2F)
        {
            data = new byte[len];
            Array.Copy(buffer, offset, data, 0, len);
            offset += len;
        }

        byte[] buffer2 = new byte[offset - oldoffset];
        Array.Copy(buffer, oldoffset, buffer2, 0, buffer2.Length);
        return new MetaEvent(delta, msg, len, data, buffer2);
    }
}
