using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehaviourBase
{
    public virtual void UpdateKnowledge(CardNum newCardNum){}
    public abstract Card DecideMove(CardNum cardNumOnTop, List<Card> PlayableCards);
}
