namespace Utility;

public static class Utility
{
    public const int lengthOfByte = 8;

    public static byte[] GetBytes(FileStream stream)
    {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();
        return buffer;
    }

    public static int LeftBitShift(int number, int shift)
    {
        if (shift < 0)
        {
            return number >> -shift;
        }
        return number << shift;
    }

    public static int RightBitShift(int number, int shift)
    {
        return LeftBitShift(number, -shift);
    }

    public static int GetLengthOfCode(int numberOfOutcomes)
    {
        if (numberOfOutcomes < 1)
        {
            return 0;
        }
        if (numberOfOutcomes == 1)
        {
            return 1;
        }
        return (int)Math.Ceiling(Math.Log2(numberOfOutcomes));
    }
}
