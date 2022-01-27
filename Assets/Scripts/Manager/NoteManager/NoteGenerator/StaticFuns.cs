using System;
using System.Net;
using System.Text;

public static class StaticFuns
{
    public static int ConverterHostorder(int data)
    {
        return IPAddress.NetworkToHostOrder(data);
    }
    public static short ConverterHostorder(short data)
    {
        return IPAddress.NetworkToHostOrder(data);
    }
    public static short ConvertHostorderS(byte[] data, int offset)
    {
        return ConverterHostorder(BitConverter.ToInt16(data, offset));
    }
    public static int ReadDeltaTime(byte[] buffer, ref int offset)
    {
        int time = 0;
        byte b;
        do
        {
            b = buffer[offset];
            offset++;
            time = (time << 7) | (b & 0x7F);
        } while (b > 127);
        return time;
    }
}
