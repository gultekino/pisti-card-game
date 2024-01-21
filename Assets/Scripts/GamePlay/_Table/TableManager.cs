using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] private Transform[] playerLocationsOnTable;
    [SerializeField] private Transform centerTransform;
    [SerializeField] private Transform deckLocation;

    private List<Card> cardsInCenter = new List<Card>();
    private GameRule[] gameRules = new GameRule[] { new JTakesAllRule(), new MatchingNumberRule() };

    private static TableManager instance;
    public static TableManager Instance => instance ?? (instance = FindObjectOfType<TableManager>());

    private void Awake()
    {
        GameStateHandler.OnGameStateChange += OnGameStateChange;
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        PlayerManager.Instance.OnPlayerPlayed += OnPlayerPlayed;
    }

    private void OnGameStateChange(GameState gameState)
    {
        if (Player.playerWonTheLast == null || gameState != GameState.GameEnd)
            return;

        Player.playerWonTheLast.CollectCards(cardsInCenter);
    }

    public Transform GetPlayerLocation(int index) => playerLocationsOnTable[index];

    public Transform DeckLocation => deckLocation;

    private void OnPlayerPlayed(Card playedCard, Player player)
    {
        playedCard.transform.position = centerTransform.position;
        cardsInCenter.Add(playedCard);
        UpdateCardDrawingOrder(playedCard);
        ProcessGameRules(playedCard, player);
    }

    private void UpdateCardDrawingOrder(Card playedCard)
    {
        playedCard.UpdateVisualSortingOrder(SortingOrder.UpperCard);
        if (cardsInCenter.Count > 1)
        {
            cardsInCenter[^2].UpdateVisualSortingOrder(SortingOrder.UnderCard);
        }
    }

    private bool ProcessGameRules(Card playedCard, Player player)
    {
        if (cardsInCenter.Count < 2) return false;

        var cardOnTop = cardsInCenter[^2];
        var isPistiPossible = cardsInCenter.Count == 2;

        if (ApplyRule(new JTakesAllRule(), cardOnTop, playedCard, player) ||
            ApplyRule(new MatchingNumberRule(), cardOnTop, playedCard, player))
        {
            if (isPistiPossible) player.MadeAPisti();
            return true;
        }

        return false;
    }

    private bool ApplyRule(GameRule rule, Card cardOnTop, Card playedCard, Player player)
    {
        if (!rule.Apply(cardOnTop, playedCard)) return false;

        player.CollectCards(cardsInCenter);
        cardsInCenter.Clear();
        return true;
    }

    public CardNum TopCardNumber => cardsInCenter.LastOrDefault()?.Number ?? CardNum.Default;

    private void OnDisable()
    {
        PlayerManager.Instance.OnPlayerPlayed -= OnPlayerPlayed;
    }
}
