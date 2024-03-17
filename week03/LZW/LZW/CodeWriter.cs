namespace CodeIO;

using Utility;

public class CodeWriter
{
    private FileStream stream;
    private int buffer;
    private int shift;

    public int LengthOfCode { get; set; }

    public CodeWriter(FileStream stream, int lenghtOfCode)
    {
        this.stream = stream;
        LengthOfCode = lenghtOfCode;
    }

    public void WriteCode(int code)
    {
        int numberOfUnwrittenBits = LengthOfCode;
        while (numberOfUnwrittenBits > 0)
        {
            int bitsToAdd = Utility.LeftBitShift(code, 
                Utility.lengthOfByte - numberOfUnwrittenBits - shift);
            int sum = buffer | bitsToAdd;
            int addedBits = Utility.RightBitShift(sum - buffer, 
                Utility.lengthOfByte - numberOfUnwrittenBits - shift);
            buffer = sum;
            shift += numberOfUnwrittenBits;
            numberOfUnwrittenBits = shift - Utility.lengthOfByte;
            if (numberOfUnwrittenBits >= 0)
            {
                stream.WriteByte(Convert.ToByte(buffer));
                buffer = 0;
                shift = 0;
            }
            code -= addedBits;
        }
    }

    public void EmptyBuffer()
    {
        if (shift > 0)
        {
            stream.WriteByte(Convert.ToByte(buffer));
        }
    }

    public void GetBytesAndWrite(string data, bool cutOffLastByte)
    {
        int lengthAtStart = LengthOfCode;
        for (int i = 0; i < data.Length - 1; ++i)
        {
            WriteCode(data[i]);
        }
        if (data.Length > 0)
        {
            if (cutOffLastByte)
            {
                LengthOfCode = Utility.lengthOfByte - shift;
            }
            WriteCode(data[data.Length - 1]);
        }
        EmptyBuffer();
    }

    public void WriteNumber(int value)
    {
        stream.Write(BitConverter.GetBytes(value));
    }

    public long GetLengthOfStream()
    {
        return stream.Length;
    }

    public void CloseStream()
    {
        stream.Close();
    }
}
