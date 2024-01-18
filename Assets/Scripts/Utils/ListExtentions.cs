using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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
            (list[k], list[n]) = (list[n], list[k]);
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
    
    public static void MovePosition<T>(this IList<Card> list, Vector3 loc)
    {
        foreach (var e in list)
            e.transform.position = loc;
    }
}
