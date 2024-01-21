using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Points
{
    private int points;
    private int pistiCount;
    public int GamePoints => points;

    public int CalculateTotalPoints(List<Card> cards)
    {
        int total = cards.Sum(c => c.Points);
        points += (total+10*pistiCount);
        return points;
    }

    public void MadePisti()
    {
        pistiCount++;
    }

    public void AddDirectPoints(int point)
    {
        points += point;
    }

    public void ResetPoints()
    {
        pistiCount = 0;
        points = 0;
    }
}
