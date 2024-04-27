namespace Task1.Tests;

using Vector;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(AddTestCases))]
    public int[] AddTest(IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        return vector1.Add(vector2).ToArray();
    }

    [TestCaseSource(nameof(SubstractTestCases))]
    public int[] SubstractTest(IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        return vector1.Substract(vector2).ToArray();
    }

    [TestCaseSource(nameof(GetScalarProductTestCases))]
    public int GetScalarProductTest(IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        return vector1.GetScalarProduct(vector2);
    }

    [TestCaseSource(nameof(IncorrectTestCases))]
    public void AddTest_VectorsOfDifferentLength_ThrowException(
        IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        Assert.Throws<InvalidOperationException>(() => vector1.Add(vector2));
    }

    [TestCaseSource(nameof(IncorrectTestCases))]
    public void SubstractTest_VectorsOfDifferentLength_ThrowException(
        IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        Assert.Throws<InvalidOperationException>(() => vector1.Substract(vector2));
    }

    [TestCaseSource(nameof(IncorrectTestCases))]
    public void GetScalarProductTest_VectorsOfDifferentLength_ThrowException(
        IList<int> array1, IList<int> array2)
    {
        Vector vector1 = new Vector(array1);
        Vector vector2 = new Vector(array2);
        Assert.Throws<InvalidOperationException>(() => vector1.GetScalarProduct(vector2));
    }

    [TestCaseSource(nameof(NullTestCases))]
    public bool IsNullTest(IList<int> inputArray)
    {
        Vector vector = new Vector(inputArray);
        return vector.IsNull;
    }

    private static IEnumerable<TestCaseData> AddTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[0],
                new int[0])
                .Returns(new int[0]);
            yield return new TestCaseData(
                new int[] { 0, 0, 0, 1, 7, 0, 0 },
                new List<int> { 0, 0, 0, 0, 5, 3, 0 })
                .Returns(new int[] { 0, 0, 0, 1, 12, 3, 0 });
            yield return new TestCaseData(
                new List<int> { 54, 11, 0, 0, 43, 10, -432, 0, 0, 55, 0 },
                new List<int> { -2, 22, 1, 0, -10, 999, 0, 0, -100, 9, 0 })
                .Returns(new int[] { 52, 33, 1, 0, 33, 1009, -432, 0, -100, 64, 0 });
        }
    }

    private static IEnumerable<TestCaseData> SubstractTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[0],
                new int[0])
                .Returns(new int[0]);
            yield return new TestCaseData(
                new int[] { 0, 0, 0, 1, 7, 0, 0 },
                new List<int> { 0, 0, 0, 0, 5, 3, 0 })
                .Returns(new int[] { 0, 0, 0, 1, 2, -3, 0 });
            yield return new TestCaseData(
                new List<int> { 54, 11, 0, 0, 43, 10, -432, 0, 0, 55, 0 },
                new List<int> { -2, 22, 1, 0, -10, 999, 0, 0, -100, 9, 0 })
                .Returns(new int[] { 56, -11, -1, 0, 53, -989, -432, 0, 100, 46, 0 });
        }
    }

    private static IEnumerable<TestCaseData> GetScalarProductTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[0],
                new int[0])
                .Returns(0);
            yield return new TestCaseData(
                new int[] { 0, 0, 0, 1, 7, 0, 0 },
                new List<int> { 0, 0, 0, 0, 5, 3, 0 })
                .Returns(35);
            yield return new TestCaseData(
                new List<int> { 1, 7, 0, 0, -10, 22 },
                new List<int> { 45, -2, 3, 0, 7, 3})
                .Returns(27);
        }
    }

    private static IEnumerable<TestCaseData> IncorrectTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[0], new int[1]);
            yield return new TestCaseData(
                new List<int> { 0, 0, 0, 1, 7, },
                new List<int> { 0, 0, 0, 0, 5, 3, 0 });
        }
    }

    private static IEnumerable<TestCaseData> NullTestCases
    {
        get
        {
            yield return new TestCaseData(
                new List<int>()).Returns(true);
            yield return new TestCaseData(
                new int[154]).Returns(true);
            yield return new TestCaseData(
                new List<int> { 1 } ).Returns(false);
            yield return new TestCaseData(
                new int[] { 0, 0, 0, 12, 43, 0, 1, 0, 0 } ).Returns(false);
        }
    }
}