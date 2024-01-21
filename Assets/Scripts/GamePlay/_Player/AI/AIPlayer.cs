using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField] [Range(0,1)]private float AIIntelligence = 0f;
    private IAIBehaviour aiBehaviour;
    private bool isExecutingMove = false;
   

    private void Start()
    {
        InitializeAIBehaviour();
        PlayerManager.Instance.OnPlayerPlayed += OnOtherPlayerPlayedCard;
    }

    private void InitializeAIBehaviour()
    {
        aiBehaviour = AIIntelligence < 0.1f ? new DumbBehaviour() : new CardCounterAIBehaviour(AIIntelligence);
    }

    private void OnOtherPlayerPlayedCard(Card playedcard, Player player)
    {
        aiBehaviour.UpdateKnowledge(playedcard.Number);
    }

    private void Update()
    {
        if (!PermissionToPlay || isExecutingMove)
            return;
        
        StartCoroutine(ExecuteMoveAfterDelay());
    }

    private IEnumerator ExecuteMoveAfterDelay()
    {
        isExecutingMove = true;
        yield return new WaitForSeconds(0.2f); // Delay to simulate AI thinking time

        PlayChosenCard();
        isExecutingMove = false;
    }
    
    private void PlayChosenCard()
    {
        var cardToPlay = aiBehaviour.DecideMove(TableManager.Instance.TopCardNumber, cardsInHand);
        TryPlayCard(cardToPlay);
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnPlayerPlayed -= OnOtherPlayerPlayedCard;
    }
}