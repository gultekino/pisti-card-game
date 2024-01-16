using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtentions
{
    private static Random rng = new Random();

    //Fisher-Yates shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static List<T> TakeRandom<T>(this IList<T> list, int num)
    {
        List<T> randomSelected = new List<T>();
        for (int i = 0; i < num; i++)
        {
            int rnd = rng.Next(0,list.Count-1);
            randomSelected.Add(list[rnd]);
            list.RemoveAt(rnd);
        }

        return randomSelected;
    }
}
