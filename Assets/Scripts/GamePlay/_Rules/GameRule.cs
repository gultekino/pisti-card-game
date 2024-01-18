using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRule
{
    public abstract bool ApplyGameRule(Card cardOnTop, Card playedCard, bool pistiPosible = false);
}
