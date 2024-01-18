using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameNumberTakeRule : GameRule
{
    public override bool ApplyGameRule(Card cardOnTop, Card playedCard, bool pistiPosible = false)
    {
        return cardOnTop!= null &&
               cardOnTop.Number == playedCard.Number;
    }
}
