using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Points
{
    private int points;
    private int pistiCount;
    public int GamePoints => points;

    public int CalculatePointOfCards(List<Card> cards)
    {
        int total = cards.Sum(c => c.Points);
        points += (total+10*pistiCount);
        return points;
    }

    public void MadePisti()
    {
        pistiCount++;
    }

    public void TakePoints(int point)
    {
        points += point;
    }
}
