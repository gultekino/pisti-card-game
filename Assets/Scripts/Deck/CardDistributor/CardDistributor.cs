using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDistributor : ICardDistributor
{
    public List<Card> DealCards(List<Card> deck, int playerCount, int cardsPerPlayer)
    {
        int cardsToDeal = Mathf.Min(deck.Count, playerCount * cardsPerPlayer);
        var cardsToDistribute = deck.TakeLast(cardsToDeal).ToList();
        PlayerManager.Instance.DistributeCardsToPlayers(cardsToDistribute);
        return cardsToDistribute;
    }
}