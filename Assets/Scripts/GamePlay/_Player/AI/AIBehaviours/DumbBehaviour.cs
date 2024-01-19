using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DumbBehaviour : AIBehaviourBase
{
    public override Card DecideMove(CardNum cardNumOnTop, List<Card> PlayableCards)
    {
        return PlayableCards.TakeRandom();
    }
}
