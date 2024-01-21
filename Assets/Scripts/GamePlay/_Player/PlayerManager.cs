using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance => instance;
    private static PlayerManager instance;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private Player aiPrefab;
    [SerializeField] private Transform playerContainer;
    private List<Player> players = new List<Player>();
    
    public delegate void PlayerPlayed(Card playedCard,Player player);
    public event PlayerPlayed EAPlayerPlayed;
    
    public delegate void PlayerTookCards(Card[] playedCard,int playerIndex);
    public event PlayerTookCards EPlayerTookCards;
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
        DeckManager.Instance.OnCardClicked += PlayerClickedACard;
    }

    private void PlayerClickedACard(Card playedCard, int playerIndex)
    {
        players[playerIndex].TryPlayCard(playedCard);
    }

    public void CreatePlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            var playerSpawnLoc = TableManager.Instance.GetPlayerLoc(i);
            var pos = playerSpawnLoc.position;
            var rot = playerSpawnLoc.rotation;
            Player player;
            if (i == 0)
                player = Instantiate(playerPrefab, pos, rot, playerContainer);
            else
                player = Instantiate(aiPrefab, pos, rot, playerContainer);

            player.EPlayerPlayed += OnPlayerMadeMove;
            players.Add(player);

        }
    }

    public void OnPlayerMadeMove(Card playedCard,Player player)
    {
        player.PermissionToPlay = false;
        EAPlayerPlayed?.Invoke(playedCard,player);
    }

    public void TakePlayerPermissionToPlay(int playerIndex)
    {
        players[playerIndex].PermissionToPlay = false;
    }
    
    public void GivePlayerPermissionToPlay(int playerIndex)
    {
        players[playerIndex].PermissionToPlay = true;
    }

    public bool CanPlayersPlayAnotherRound()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].CanPlayARound())
                return false;
        }
        return true;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EPlayerPlayed -= OnPlayerMadeMove;
        }
    }

    public void DistributeCardsToPlayers(IEnumerable<Card> cardsToPlay)
    {
        var howManyCardsShouldEachPlayerHave = cardsToPlay.Count() / players.Count;
        for (int i = 0; i < players.Count; i++)
        {
            var list =cardsToPlay.Skip(i*howManyCardsShouldEachPlayerHave).Take((i+1)*howManyCardsShouldEachPlayerHave);
            var playedCard = list as Card[] ?? list.ToArray();
            players[i].TakeCards(playedCard);
            EPlayerTookCards?.Invoke(playedCard,i);
        }
    }

    private void OnDisable()
    {
        DeckManager.Instance.OnCardClicked -= PlayerClickedACard;
    }

    private void CalculatePlayerPoints()
    {
        GivePointsToPlayerWhoTakeMoreCards();
        foreach (var p in players)
        {
            p.CalculatePoints();
        }
    }

    private void GivePointsToPlayerWhoTakeMoreCards()
    {
        var p = players.OrderByDescending(x => x.GetCardCount()).First();
        if (p!=null)
            p.TakePoints(3);
        if (p==null)
            Debug.Log("PAT");
    }

    public List<int> GetPlayerPoints()
    {
        CalculatePlayerPoints();
        return players.Select(p => p.GetPoints()).ToList();
    }

    public int GetPlayerIndex()
    {
        return 0;
    }

    public void ResetPlayerScores()
    {
        foreach (var p in players)
        {
            p.ResetScore();
        }
    }
}
