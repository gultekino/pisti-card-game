using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardCounterAIBehaviour : AIBehaviourBase
{
    private const int CARD_COUNT = 14; //13 Cards + never used 0 index
    private int[] CardCounter = new int[CARD_COUNT];
    public override void UpdateKnowledge(CardNum newCardNum)
    {
        CardCounter[(int)newCardNum]++;
    }

    public override Card DecideMove(CardNum cardNumOnTop, List<Card> PlayableCards)
    {
        var sameNumCard = PlayableCards.Where(s => s.Number == cardNumOnTop);
        if (sameNumCard.Count() != 0)
            return sameNumCard.TakeRandom();

        int makeSenseToPlayIndex = 0;
        for (int i = 1; i < PlayableCards.Count; i++)
        {
            if (MostPlayedCard(PlayableCards[makeSenseToPlayIndex],PlayableCards[i]))
                makeSenseToPlayIndex = i;
        }

        return PlayableCards[makeSenseToPlayIndex];
    }

    private bool MostPlayedCard(Card card1,Card card2)
    {
        return CardCounter[(int)card1.Number] > CardCounter[(int)card2.Number];
    }
}
