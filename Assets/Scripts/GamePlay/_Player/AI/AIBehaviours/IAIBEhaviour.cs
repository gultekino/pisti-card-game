using System.Collections.Generic;

public interface IAIBehaviour
{
    Card DecideMove(CardNum cardNumOnTop, List<Card> PlayableCards);
    void UpdateKnowledge(CardNum newCardNum);
}