using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTakesAllRule : IGameRule
{
    public bool Apply(Card cardOnTop, Card playedCard)
    {
        return cardOnTop != null && playedCard.Number == CardNum.J;
    }
}
