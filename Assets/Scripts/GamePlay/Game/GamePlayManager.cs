using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
   [SerializeField] private GamePlaySettings gamePlaySettings;
   private GameInitializer gameInitializer;
   private RoundManager roundManager;
   private DeckManager deckManager;
   private PlayerManager playerManager;


   private void Awake()
   {
      deckManager = FindObjectOfType<DeckManager>();
      playerManager = FindObjectOfType<PlayerManager>();
      roundManager = new RoundManager(gamePlaySettings, deckManager);
      gameInitializer = new GameInitializer(deckManager,playerManager);
   }

   private void Start()
   {
      gameInitializer.InitializeGame(gamePlaySettings);
      playerManager.OnPlayerPlayed += StepInRound;
      StartCoroutine(GameLoop());
   }

   private void StepInRound(Card playedCard,Player player)
   {
      roundManager.StepInRound();
   }

   private IEnumerator GameLoop()
   {
      GameStateHandler.GameState = GameState.InGame;
      while (CanPlayAnotherRound(gamePlaySettings))
         yield return roundManager.PlayRound();
      GameStateHandler.GameState = GameState.GameEnd;
   }
   
   bool CanPlayAnotherRound(GamePlaySettings settings)
   {
      return DeckManager.Instance.CanDealAnotherRound(settings.PlayerCount, settings.NumCardsToGive,gamePlaySettings.PlayUntilNoCardsLeft);
   }

   private void OnDisable()
   {
      playerManager.OnPlayerPlayed -= StepInRound;
   }
}
