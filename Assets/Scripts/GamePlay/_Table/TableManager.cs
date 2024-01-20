using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerLocationsOnTable;
    [SerializeField] private Transform Center;
    [SerializeField] private Transform DeckLoc;
    private List<Card> cardsInTheCenter = new();
    public static TableManager Instance => instance;
    private static TableManager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        PlayerManager.Instance.EAPlayerPlayed += HandleCardOnTable;
        GameStateHandler.EOnGameStateChange += HandleCardOnTable;

    }

    private void HandleCardOnTable(GameState gameState)
    {
        if (Player.playerWonTheLast == null)
            return;

        if (gameState == GameState.GameEnd)
            Player.playerWonTheLast.TakeCardsToTheStash(cardsInTheCenter);
    }

    public Transform GetPlayerLoc(int index)
    {
        return playerLocationsOnTable[index];
    }

    public Transform GetDeckLoc()
    {
        return DeckLoc;
    }

    private void HandleCardOnTable(Card playedcard, Player player)
    {
        playedcard.transform.position = Center.position;
        cardsInTheCenter.Add(playedcard);
        HandleGameRules(playedcard, player);
        //if it makes a match make player take cards into stash
    }

    private bool AfterAddedPlayedCardIsThereWasThereACardOnTop()
    {
        return cardsInTheCenter.Count - 2 < 0;
    }
    private bool HandleGameRules(Card playedcard, Player player)
    {
        if (AfterAddedPlayedCardIsThereWasThereACardOnTop())
            return false;
        
        SameNumberTakeRule snTR = new SameNumberTakeRule();
        JTakesAll jTakesAll = new JTakesAll();
            
        var CardOnTop = cardsInTheCenter[^2];
        if (snTR.ApplyGameRule(CardOnTop, playedcard))
        {
            bool isPistiPossible = cardsInTheCenter.Count == 2;
            if (isPistiPossible)
                player.MadeAPisti();
            player.TakeCardsToTheStash(cardsInTheCenter);
            cardsInTheCenter.Clear();

            return true;
        }

        if (jTakesAll.ApplyGameRule(CardOnTop,playedcard))
        {
            Debug.Log("JTakesAll");    
            player.TakeCardsToTheStash(cardsInTheCenter);
            cardsInTheCenter.Clear();
            return true;
        }

        return false;
    }

    public CardNum GetCarNumOnTopCard()
    {
        var c = cardsInTheCenter.LastOrDefault();
        return c ? c.Number : CardNum.Default;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.EAPlayerPlayed -= HandleCardOnTable;
    }
}
