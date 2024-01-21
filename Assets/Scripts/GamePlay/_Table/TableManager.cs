using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : Singleton<TableManager>
{
    [SerializeField] private TableLocationHandler tableLocationHandler;
    private CardInteractionHandler cardInteractionHandler;
    private List<Card> cardsInCenter = new();

    protected override void Awake()
    {
        base.Awake();
        GameStateHandler.OnGameStateChange += OnGameStateChange;
    }

    private void Start()
    {
        PlayerManager.Instance.OnPlayerPlayed += OnPlayerPlayed;
        cardInteractionHandler = new CardInteractionHandler();
    }

    private void OnGameStateChange(GameState gameState)
    {
        if (Player.playerWonTheLast == null || gameState != GameState.GameEnd)
            return;

        Player.playerWonTheLast.CollectCards(cardsInCenter);
    }

    public Transform GetPlayerLocation(int index) => tableLocationHandler.GetPlayerLocation(index);

    public Transform DeckLocation => tableLocationHandler.GetDeckLocation();

    private void OnPlayerPlayed(Card playedCard, Player player)
    {
        playedCard.transform.position = tableLocationHandler.GetCenterLocation().position;
        var cardOnTop = GetCardOnTop();
        cardInteractionHandler.UpdateCardDrawingOrder(playedCard,cardOnTop);
        cardsInCenter.Add(playedCard);
        ProcessGameRules(playedCard, player,cardOnTop);
    }
    

    private bool ProcessGameRules(Card playedCard, Player player, Card cardOnTop)
    {
        if (cardsInCenter.Count < 2) return false;

        var isPistiPossible = cardsInCenter.Count == 2;
        
        var appliedRule = cardInteractionHandler.HandleCardPlayed(playedCard,cardOnTop);
        if (appliedRule==null)
            return false;
        if (isPistiPossible && appliedRule is MatchingNumberRule)
            player.MadeAPisti();

        player.CollectCards(cardsInCenter);
        cardsInCenter.Clear();
        return true;
    }

    private Card GetCardOnTop()
    {
        return cardsInCenter.LastOrDefault();
    }
    public CardNum TopCardNumber => cardsInCenter.LastOrDefault()?.Number ?? CardNum.Default;

    private void OnDisable()
    {
        PlayerManager.Instance.OnPlayerPlayed -= OnPlayerPlayed;
        GameStateHandler.OnGameStateChange -= OnGameStateChange;
    }
}