using System;
using System.IO;

public class Chunk
{
    public int CT // 청크유형
    {
        get;
    }
    public int Length // 청크 길이
    {
        get;
    }
    public byte[] Data // 데이터
    {
        get;
    }
    public Chunk(int ctype, int length, byte[] buffer) 
    {
        CT = ctype;
        Length = length;
        Data = buffer;
    }
    //원문 버퍼 복사본
    public byte[] Buffer
    {
        get
        {
            byte[] ct_buf = BitConverter.GetBytes(CT);
            int belen = StaticFuns.ConverterHostorder(Length);
            byte[] len_buf = BitConverter.GetBytes(belen);
            byte[] buffer = new byte[ct_buf.Length + len_buf.Length + Data.Length];
            Array.Copy(ct_buf, buffer, ct_buf.Length);
            Array.Copy(len_buf, 0, buffer, ct_buf.Length, len_buf.Length);
            Array.Copy(Data, 0, buffer, ct_buf.Length + len_buf.Length, Data.Length);
            return buffer;
        }
    }
    //청크 나누기. 헤드청크, 트랙청크.
    public static Chunk Parse(Stream stream)
    {
        try
        {
            BinaryReader br = new BinaryReader(stream);
            int ctype = br.ReadInt32();
            int length = br.ReadInt32();
            length = StaticFuns.ConverterHostorder(length);
            byte[] buffer = br.ReadBytes(length);
            switch (StaticFuns.ConverterHostorder(ctype))
            {
                case 0x4d54726b: return new Track(ctype, length, buffer);
            }
            return new Chunk(ctype, length, buffer);
        }
        catch
        {
            return null;
        }
    }
}
