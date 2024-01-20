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
    private GameRule[] gameRules = new GameRule[] { new JTakesAll(), new SameNumberTakeRule() };
    public static TableManager Instance => instance;
    private static TableManager instance;
    
    private void Awake()
    {
        GameStateHandler.EOnGameStateChange += HandleCardOnTable;

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
    private bool HandleGameRules(Card playedCard, Player player)
    {
        if (AfterAddedPlayedCardIsThereWasThereACardOnTop())
            return false;
        
        var cardOnTop = cardsInTheCenter[^2];
        var isPistiPossible = cardsInTheCenter.Count == 2;
        if (ApplyRule(new JTakesAll(), cardOnTop, playedCard, player))
        {
            Debug.Log("JTakesAll");
            return true;
        }

        if (ApplyRule(new SameNumberTakeRule(), cardOnTop, playedCard, player))
        {
            CheckAndHandlePisti(player,isPistiPossible);
            return true;
        }

        return false;
    }

    private bool ApplyRule(GameRule rule, Card cardOnTop, Card playedCard, Player player)
    {
        if (rule.ApplyGameRule(cardOnTop, playedCard))
        {
            player.TakeCardsToTheStash(cardsInTheCenter);
            cardsInTheCenter.Clear();
            return true;
        }

        return false;
    }

    private void CheckAndHandlePisti(Player player,bool isPistiPossible)
    {
        if (isPistiPossible)
        {
            player.MadeAPisti();
        }
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
