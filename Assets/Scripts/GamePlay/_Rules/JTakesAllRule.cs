using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTakesAllRule : GameRule
{
    public override bool Apply(Card cardOnTop, Card playedCard)
    {
        return cardOnTop != null && playedCard.Number == CardNum.J;
    }
}
