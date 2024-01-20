using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField] [Range(0,1)]private float AIIntelligence = 0f;
    private AIBehaviourBase aiBehaviour;
    private bool executingMove = false;

    private void Start()
    {
        if (AIIntelligence<0.1f)
            aiBehaviour = new DumbBehaviour();

        aiBehaviour = new CardCounterAIBehaviour(AIIntelligence);
        PlayerManager.Instance.EAPlayerPlayed += UpdateKnowledge;
    }

    private void UpdateKnowledge(Card playedcard, Player player)
    {
        aiBehaviour.UpdateKnowledge(playedcard.Number);
    }

    private void Update()
    {
        if (PermissionToPlay == false || executingMove)
            return;
        StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        executingMove = true;
        yield return new WaitForSeconds(0.2f);
        var cardToPlay = aiBehaviour.DecideMove(TableManager.Instance.GetCarNumOnTopCard(), cardsInHand);
        TryPlay(cardToPlay);
        executingMove = false;
    }
}
