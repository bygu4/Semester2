namespace ByteIO;

public class ByteWriter
{
    private const int lengthOfByte = 8;

    private FileStream stream;
    private int buffer;
    private int shift;

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
            int bitsToAdd = LeftBitShift(code, lengthOfByte - numberOfUnwrittenBits - shift);
            int sum = buffer | bitsToAdd;
            int addedBits = RightBitShift(sum - buffer, lengthOfByte - numberOfUnwrittenBits - shift);
            buffer = sum;
            shift += numberOfUnwrittenBits;
            numberOfUnwrittenBits = shift - lengthOfByte;
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
            int code = data[data.Length - 1];
            if (cutOffLastByte)
            {
                LengthOfCode = lengthOfByte - shift;
                code >>= (lengthAtStart - LengthOfCode);
            }
            WriteCode(code);
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
