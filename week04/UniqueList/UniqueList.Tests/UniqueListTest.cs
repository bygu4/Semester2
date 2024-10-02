// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace UniqueList.Tests;

using Exceptions;

public class TestBase
{
    public UniqueList testList;

    [SetUp]
    public void Setup()
    {
        testList = new UniqueList();
    }
}

[TestFixture]
public class TestForEmptyList : TestBase
{
    [Test]
    public void TestForAdd_AddToEmptyList_SizeHasChanged()
    {
        testList.Add(9090);
        Assert.That(testList.Size, Is.EqualTo(1));
    }

    [Test]
    public void TestForRemove_TryToRemoveFromEmptyList_ThrowException()
    {
        Assert.Throws<ElementNotFoundException>(() => { testList.Remove(10); });
    }

    [Test]
    public void TestForRemove_AddThenRemoveAndAddAgain_SizeHasChanged()
    {
        testList.Add(888);
        testList.Remove(888);
        testList.Add(-1);
        Assert.That(testList.Size, Is.EqualTo(1));
    }
}

[TestFixture]
public class TestForGeneralCase : TestBase
{
    private void AddElements(int[] elements)
    {
        for (int i = 0; i < elements.Length; ++i)
        {
            testList.Add(elements[i]);
        }
    }

    [SetUp]
    public void GeneralSetup()
    {
        AddElements([-543, 0, 100, -5, 67, 22, 3, -12, 9999, 555]);
    }

    [Test]
    public void TestForAdd_AddNewElement_SizeHasChanged()
    {
        testList.Add(1234567);
        Assert.That(testList.Size, Is.EqualTo(11));
    }

    [Test]
    public void TestForAdd_TryToAddElementThatIsInList_ThrowException()
    {
        Assert.Throws<ElementIsAlreadyInListException>(() => { testList.Add(0); });
        Assert.Throws<ElementIsAlreadyInListException>(() => { testList.Add(67); });
    }

    [Test]
    public void TestForRemove_RemoveElementThatIsInList_CanAddItAgainAndSizeHasChanged()
    {
        testList.Remove(100);
        Assert.That(testList.Size, Is.EqualTo(9));
        testList.Add(100);
    }

    [Test]
    public void TestForRemove_TryToRemoveElementNotInList_ThrowException()
    {
        Assert.Throws<ElementNotFoundException>(() => { testList.Remove(8676767); });
        Assert.Throws<ElementNotFoundException>(() => { testList.Remove(-1); });
    }

    [Test]
    public void TestForGetValue_IndexIsCorrect_ReturnValue()
    {
        Assert.That(testList.GetValue(0), Is.EqualTo(-543));
        Assert.That(testList.GetValue(2), Is.EqualTo(100));
        Assert.That(testList.GetValue(9), Is.EqualTo(555));
    }

    [Test]
    public void TestForGetValue_IndexOutOfRange_ThrowException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => { testList.GetValue(-1); });
        Assert.Throws<IndexOutOfRangeException>(() => { testList.GetValue(10); });
    }

    [Test]
    public void TestForSetValue_IndexIsCorrectAndElementIsNotInList_ValueHasChangedAndSizeIsTheSame()
    {
        testList.SetValue(33333, 4);
        Assert.That(testList.GetValue(4), Is.EqualTo(33333));
        Assert.That(testList.Size, Is.EqualTo(10));
    }

    [Test]
    public void TestForSetValue_SetTheSameValue_ValueStaysTheSame()
    {
        testList.SetValue(22, 5);
        Assert.That(testList.GetValue(5), Is.EqualTo(22));
        Assert.That(testList.Size, Is.EqualTo(10));
    }

    [Test]
    public void TestForSetValue_IndexOutOfRange_ThrowException()
    {
        Assert.Throws<IndexOutOfRangeException>(() => { testList.SetValue(7777, -3); });
        Assert.Throws<IndexOutOfRangeException>(() => { testList.SetValue(455, 13); });
    }

    [Test]
    public void TestForSetValue_ElementIsInList_ThrowException()
    {
        Assert.Throws<ElementIsAlreadyInListException>(() => { testList.SetValue(0, 0); });
        Assert.Throws<ElementIsAlreadyInListException>(() => { testList.SetValue(9999, 6); });
    }
}
