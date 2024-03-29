using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    private DeckBuilder deckBuilder;
    private List<Card> deck;
    private List<Card> playedCards = new ();
    private ICardDistributor cardDistributor;
    private IDeckShuffler deckShuffler;
    public event Action<Card, int> OnCardClicked;

    protected override void Awake()
    {
        base.Awake();
        cardDistributor = new CardDistributor();
        deckShuffler = new DeckShuffler();
    }

    public void InitializeDeck()
    {
        deckBuilder = GetComponent<DeckBuilder>();
        deck = deckBuilder.BuildDeck();
        deckShuffler.ShuffleDeck(deck);
        SetInitialCardSortingOrder();
        MoveDeckToTable();
    }

    private void SetInitialCardSortingOrder()
    {
        deck.FirstOrDefault()?.UpdateVisualSortingOrder(SortingOrder.UpperCard);
    }

    private void MoveDeckToTable()
    {
        deck.MovePosition<Transform>(TableManager.Instance.DeckLocation.position);
    }

    public bool CanDealAnotherRound(int playerCount, int cardsPerPlayer, bool playUntilNoCardLeft)
    {
        if (playUntilNoCardLeft)
            return deck.Count != 0;
        int requiredCards = playerCount * cardsPerPlayer;
        return deck.Count >= requiredCards;
    }

    public void DealCards(int playerCount, int cardsPerPlayer)
    {
        var cardsToDistribute = cardDistributor.DealCards(deck, playerCount, cardsPerPlayer);
        playedCards.AddRange(cardsToDistribute);
        RemoveDealtCardsFromDeck(cardsToDistribute);
    }

    private void RemoveDealtCardsFromDeck(IEnumerable<Card> dealtCards)
    {
        deck.RemoveAll(card => dealtCards.Contains(card));
    }

    public void PlayerInteractedWithCard(Card card, int playerIndex)
    {
        OnCardClicked?.Invoke(card, playerIndex);
    }
}
