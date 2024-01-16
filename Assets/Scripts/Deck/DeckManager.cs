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
        return pack.Count > (playerCount * cardsToGivePlayer);
       /* if (pack.Count>(playerCount*cardsToGivePlayer))
        {
            return true;
        }
        return false;*/
    }

    public void GivePlayersCard(int playerCount, int numCardsToGive)
    {
        List<Card> cardsToPlay = pack.TakeRandom(numCardsToGive*playerCount);
        PlayerManager.Instance.GivePlayersCards(cardsToPlay);
    }
}
