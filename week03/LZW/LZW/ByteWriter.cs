namespace ByteIO;

public class ByteWriter
{
    private const int lengthOfByte = 8;

    private FileStream stream;
    private int buffer;

    public int Shift { get; private set; }
    public int LengthOfCode { get; set; }

    public ByteWriter(FileStream stream, int lenghtOfCode)
    {
        this.stream = stream;
        LengthOfCode = lenghtOfCode;
    }

    private static int LeftBitShift(int number, int shift)
    {
        if (shift < 0)
        {
            return number >> -shift;
        }
        return number << shift;
    }

    private static int RightBitShift(int number, int shift)
    {
        return LeftBitShift(number, -shift);
    }

    public void WriteCode(int code)
    {
        int numberOfUnwrittenBits = LengthOfCode;
        while (numberOfUnwrittenBits > 0)
        {
            int bitsToAdd = LeftBitShift(code, lengthOfByte - numberOfUnwrittenBits - Shift);
            int sum = buffer | bitsToAdd;
            int addedBits = RightBitShift(sum - buffer, lengthOfByte - numberOfUnwrittenBits - Shift);
            buffer = sum;
            Shift += numberOfUnwrittenBits;
            numberOfUnwrittenBits = Shift - lengthOfByte;
            if (numberOfUnwrittenBits >= 0)
            {
                stream.WriteByte(Convert.ToByte(buffer));
                buffer = 0;
                Shift = 0;
            }
            code -= addedBits;
        }
    }

    public void EmptyBuffer()
    {
        if (Shift > 0)
        {
            stream.WriteByte(Convert.ToByte(buffer));
        }
    }

    public void GetBytesAndWrite(string data, bool cutOffLastByte)
    {
        int lengthAtStart = LengthOfCode;
        for (int i = 0; i < data.Length; ++i)
        {
            int code = data[i];
            if (i == data.Length - 1 && cutOffLastByte)
            {
                LengthOfCode = lengthOfByte - Shift;
                code >>= (lengthAtStart - LengthOfCode);
            }
            WriteCode(code);
        }
        EmptyBuffer();
    }

    public void WriteByte(byte value)
    {
        stream.WriteByte(value);
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
