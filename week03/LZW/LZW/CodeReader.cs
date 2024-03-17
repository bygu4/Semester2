namespace CodeIO;

using Utility;

public class CodeReader
{
    private byte[] bytes;
    private int currentIndex;
    private int shift;

    public int LengthOfCode { get; set; }
    public bool LastByteCutOff { get; private set; }

    public CodeReader(byte[] bytes, int lengthOfCode)
    {
        this.bytes = bytes;
        LengthOfCode = lengthOfCode;
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
            int unreadBitsShift = int.Min(numberOfUnreadBits, Utility.lengthOfByte);
            int bitsToAdd = (currentByte & (byte.MaxValue >> shift)) - 
                (currentByte & (byte.MaxValue >> (shift + unreadBitsShift)));
            int addedBits = Utility.RightBitShift(bitsToAdd, 
                Utility.lengthOfByte - numberOfUnreadBits - shift);
            shift += numberOfUnreadBits;
            numberOfUnreadBits = shift - Utility.lengthOfByte;
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
            (float)bytes.Length * Utility.lengthOfByte / LengthOfCode)];
        for (int i = 0; currentIndex < bytes.Length; ++i)
        {
            encodedData[i] = (char)ReadCode();
        }
        return new string(encodedData);
    }
}
