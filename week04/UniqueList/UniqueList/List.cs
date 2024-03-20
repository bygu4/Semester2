namespace List;

using Exceptions;

public class List
{
    protected Vertex? head;
    protected Vertex? tail;
    public int Size { get; private set; }

    protected class Vertex
    {
        public int value;
        public Vertex? next;

        public Vertex(int value)
        {
            this.value = value;
        }
    }

    public List()
    {
    }

    public virtual void Add(int value)
    {
        Vertex newVertex = new Vertex(value);
        if (head is not null)
        {
            head.next = newVertex;
        }
        if (tail is null)
        {
            tail = newVertex;
        }
        head = newVertex;
        ++Size;
    }

    private (Vertex?, Vertex) GetVertexByValue(int value)
    {
        Vertex? previous = null;
        Vertex? current = tail;
        for (; current != null && current.value != value; current = current.next)
        {
            previous = current;
        }
        if (current is null)
        {
            throw new ElementNotFoundException();
        }
        return (previous, current);
    }

    public void Remove(int value)
    {
        var (previous, current) = GetVertexByValue(value);
        if (current == head)
        {
            head = previous;
        }
        if (previous is null)
        {
            tail = tail.next;
        }
        else
        {
            previous.next = current.next;
        }
        --Size;
    }

    private Vertex GetVertexByIndex(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new IndexOutOfRangeException();
        }
        Vertex current = tail;
        for (int i = 0; i < index; ++i)
        {
            current = current.next;
        }
        return current;
    }

    public int GetValue(int index)
    {
        var vertex = GetVertexByIndex(index);
        return vertex.value;
    }

    public virtual void SetValue(int value, int index)
    {
        var vertex = GetVertexByIndex(index);
        vertex.value = value;
    }
}
