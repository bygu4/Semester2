namespace ByteIO;

public class ByteReader
{
    private const int lengthOfByte = 8;

    private byte[] bytes;

    public int CurrentIndex { get; private set; }
    public int Shift { get; private set; }
    public int LengthOfCode { get; set; }
    public bool LastByteCutOff { get; private set; }

    public ByteReader(byte[] bytes, int lengthOfCode)
    {
        this.bytes = bytes;
        LengthOfCode = lengthOfCode;
    }

    private static int RightBitShift(int number, int shift)
    {
        if (shift < 0)
        {
            return number << -shift;
        }
        return number >> shift;
    }

    public int ReadCode()
    {
        int numberOfUnreadBits = LengthOfCode;
        int code = 0;
        while (numberOfUnreadBits > 0)
        {
            if (CurrentIndex >= bytes.Length)
            {
                LastByteCutOff = true;
                return code;
            }
            int currentByte = bytes[CurrentIndex];
            int bitsToAdd = (currentByte & (byte.MaxValue >> Shift)) - 
                (currentByte & (byte.MaxValue >> (Shift + numberOfUnreadBits)));
            int addedBits = RightBitShift(bitsToAdd, lengthOfByte - numberOfUnreadBits - Shift);
            Shift += numberOfUnreadBits;
            numberOfUnreadBits = Shift - lengthOfByte;
            if (numberOfUnreadBits >= 0)
            {
                ++CurrentIndex;
                Shift = 0;
            }
            code += addedBits;
        }
        return code;
    }

    public string GetString()
    {
        CurrentIndex = 0;
        Shift = 0;
        char[] encodedData = new char[(int)Math.Ceiling(
            (float)bytes.Length * lengthOfByte / LengthOfCode)];
        for (int i = 0; CurrentIndex < bytes.Length; ++i)
        {
            encodedData[i] = (char)ReadCode();
        }
        return new string(encodedData);
    }
}
