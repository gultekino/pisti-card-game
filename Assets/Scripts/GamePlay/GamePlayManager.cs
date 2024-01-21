using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
   [SerializeField] private GamePlaySettings gamePlaySettings;
   private bool step;
   
   private void Start()
   {
      PlayerManager.Instance.CreatePlayers(gamePlaySettings.PlayerCount);
      StartCoroutine(GameLoop());
      PlayerManager.Instance.OnPlayerPlayed += StepInGameLoop;
   }

   private void StepInGameLoop(Card card, Player player)
   {
      step = true;
   }

   private IEnumerator GameLoop()
   {
      GameStateHandler.GameState = GameState.InGame;
      while (CanPlayAnotherRound(gamePlaySettings))
      {
         GiveCardsToPlayers(gamePlaySettings);

         yield return PlayRounds(gamePlaySettings);
      }
      GameStateHandler.GameState = GameState.GameEnd;
   }
   
   bool CanPlayAnotherRound(GamePlaySettings settings)
   {
      return DeckManager.Instance.CanDealAnotherRound(settings.PlayerCount, settings.NumCardsToGive,gamePlaySettings.PlayUntilNoCardsLeft);
   }

   void GiveCardsToPlayers(GamePlaySettings settings)
   {
      DeckManager.Instance.DealCardsToPlayers(settings.PlayerCount, settings.NumCardsToGive);
   }

   IEnumerator PlayRounds(GamePlaySettings settings)
   {
      while (PlayerManager.Instance.CanPlayersPlayAnotherRound())
      {
         for (int i = 0; i < settings.PlayerCount; i++)
         {
            yield return PlaySingleRound(i);
         }
      }
   }

   IEnumerator PlaySingleRound(int playerIndex)
   {
      PlayerManager.Instance.GivePlayerPermissionToPlay(playerIndex);
      yield return WaitForPlayerStep();
   }

   IEnumerator WaitForPlayerStep()
   {
      while (!step)
      {
         yield return null;
      }

      yield return new WaitForSeconds(0.2f);//Movement time
      step = false;
   }
}
