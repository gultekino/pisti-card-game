using System.Collections.Generic;

public interface ICardDistributor
{
    List<Card> DealCards(List<Card> deck, int playerCount, int cardsPerPlayer);
}