using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DumbBehaviour : IAIBehaviour
{
    public Card DecideMove(CardNum cardNumOnTop, List<Card> PlayableCards)
    {
       return PlayableCards.TakeRandom();
    }

    public void UpdateKnowledge(CardNum newCardNum)
    {
    }
}


