using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardCounterAIBehaviour : IAIBehaviour
{
    private float intelligence;
    private const int CARD_COUNT = 14; // 13 Cards + never used 0 index
    private int[] cardCounter = new int[CARD_COUNT];

    public CardCounterAIBehaviour(float intelligence)
    {
        this.intelligence = intelligence;
    }
    
    public void UpdateKnowledge(CardNum newCardNum)
    {
        if (Random.Range(0f, 1f) > intelligence)
        {
            cardCounter[(int)newCardNum]++;
        }
    }

    public Card DecideMove(CardNum cardNumOnTop, List<Card> playableCards)
    {
        var sameNumCards = playableCards.Where(card => card.Number == cardNumOnTop).ToList();
        if (sameNumCards.Any())
        {
            return sameNumCards.TakeRandom();
        }

        return FindBestCardToPlay(playableCards);
    }
    
    private Card FindBestCardToPlay(List<Card> playableCards)
    {
        int bestCardIndex = 0;
        for (int i = 1; i < playableCards.Count; i++)
        {
            if (IsMoreFrequentlyPlayed(playableCards[i], playableCards[bestCardIndex]))
            {
                bestCardIndex = i;
            }
        }

        return playableCards[bestCardIndex];
    }

    private bool IsMoreFrequentlyPlayed(Card card1,Card card2)
    {
        return cardCounter[(int)card1.Number] > cardCounter[(int)card2.Number];
    }
}

