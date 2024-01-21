using System.Collections.Generic;

public class DeckShuffler : IDeckShuffler
{
    public void ShuffleDeck(List<Card> deck)
    {
        deck.Shuffle();
    }
}