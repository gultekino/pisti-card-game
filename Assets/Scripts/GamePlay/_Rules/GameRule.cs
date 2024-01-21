using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRule
{
    public abstract bool Apply(Card cardOnTop, Card playedCard);
}