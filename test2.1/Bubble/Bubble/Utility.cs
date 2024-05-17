namespace Utility;

public static class Utility
{
    public static void Sort<T>(IList<T> collection, IComparer<T> comparer)
    {
        for (int i = 0; i < collection.Count; ++i)
        {
            for (int j = 1; j < collection.Count; ++j)
            {
                if (comparer.Compare(collection[j - 1], collection[j]) > 0)
                {
                    (collection[j - 1], collection[j]) = (collection[j], collection[j - 1]);
                }
            }
        }
    }
}
