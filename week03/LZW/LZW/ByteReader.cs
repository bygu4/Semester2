namespace ByteIO;

public class ByteReader
{
    private const int lengthOfByte = 8;

    private byte[] bytes;
    private int currentIndex;
    private int shift;

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
            if (currentIndex >= bytes.Length)
            {
                LastByteCutOff = true;
                return code;
            }
            int currentByte = bytes[currentIndex];
            int bitsToAdd = (currentByte & (byte.MaxValue >> shift)) - 
                (currentByte & (byte.MaxValue >> (shift + numberOfUnreadBits)));
            int addedBits = RightBitShift(bitsToAdd, lengthOfByte - numberOfUnreadBits - shift);
            shift += numberOfUnreadBits;
            numberOfUnreadBits = shift - lengthOfByte;
            if (numberOfUnreadBits >= 0)
            {
                ++currentIndex;
                shift = 0;
            }
            code += addedBits;
        }
        return code;
    }

    public string GetString()
    {
        currentIndex = 0;
        shift = 0;
        char[] encodedData = new char[(int)Math.Ceiling(
            (float)bytes.Length * lengthOfByte / LengthOfCode)];
        for (int i = 0; currentIndex < bytes.Length; ++i)
        {
            encodedData[i] = (char)ReadCode();
        }
        return new string(encodedData);
    }
}
