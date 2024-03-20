namespace UniqueList;

using List;
using Exceptions;

public class UniqueList : List
{
    private void CheckThatElementIsNotInList(int value, int index)
    {
        int currentIndex = 0;
        for (Vertex? current = tail; current is not null; current = current.next)
        {
            if (current.value == value && currentIndex != index)
            {
                throw new ElementIsAlreadyInListException();
            }
            ++currentIndex;
        }
    }

    public override void Add(int value)
    {
        CheckThatElementIsNotInList(value, -1);
        base.Add(value);
    }

    public override void SetValue(int value, int index)
    {
        CheckThatElementIsNotInList(value, index);
        base.SetValue(value, index);
    }
}
