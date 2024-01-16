using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance => instance;
    private static DeckManager instance;
    
    private DeckBuilder deckBuilder;
    private List<Card> pack;
    private List<Card> playedCards = new List<Card>();
    
    public delegate void CardClicked(Card playedCard, int playerIndex);
    public event CardClicked EACardClicked;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        BuildPack();
    }

    private void BuildPack()
    {
        deckBuilder = GetComponent<DeckBuilder>();
        pack = deckBuilder.BuildDeck();
        pack.Shuffle();
    }

    public bool DoesPackHasCardsForAnotherRound(int playerCount, int cardsToGivePlayer)
    {
        return pack.Count > 0;
        // return pack.Count > (playerCount * cardsToGivePlayer);
    }

    public void GivePlayersCard(int playerCount, int numCardsToGive)
    {
        int takeRandomNum = numCardsToGive * playerCount;
        if (pack.Count<takeRandomNum)
            takeRandomNum = pack.Count;

        List<Card> cardsToPlay = pack.TakeRandom(takeRandomNum);
        playedCards.AddRange(cardsToPlay);
        PlayerManager.Instance.GivePlayersCard(cardsToPlay);
    }

    public void PlayerInteractedWithCard(Card card,int playerIndex)
    {
        EACardClicked?.Invoke(card,playerIndex);
    }
}
