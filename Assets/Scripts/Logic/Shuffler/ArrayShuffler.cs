using System;
using System.Collections.Generic;
using System.Linq;

public class ArrayShuffler
{
    private readonly Random _random;

    public ArrayShuffler()
    {
        _random = new Random();
    }

    public IEnumerable<T> Shuffle<T>(IEnumerable<T> collection)
    {
        var list = collection.ToList();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int k = _random.Next(i + 1);
            T temp = list[i];
            list[i] = list[k];
            list[k] = temp;
        }

        return list;
    }
}
