using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTakesAll : GameRule
{
    public override bool ApplyGameRule(Card cardOnTop, Card playedCard, bool pistiPosible = false)
    {
        return cardOnTop != null 
               && playedCard.Number == CardNum.J;
    }
}
