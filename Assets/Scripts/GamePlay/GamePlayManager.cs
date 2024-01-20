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
      PlayerManager.Instance.EAPlayerPlayed += StepInGameLoop;
   }

   private void StepInGameLoop(Card playedcard, Player player)
   {
      step = true;
   }

   private IEnumerator GameLoop()
   {
      while (CanPlayAnotherRound(gamePlaySettings))
      {
         GiveCardsToPlayers(gamePlaySettings);

         yield return PlayRounds(gamePlaySettings);
      }
      GameStateHandler.GameState = GameState.GameEnd;
      Debug.Log("HERRRR");
   }
   
   bool CanPlayAnotherRound(GamePlaySettings settings)
   {
      return DeckManager.Instance.DoesPackHasCardsForAnotherRound(settings.PlayerCount, settings.NumCardsToGive);
   }

   void GiveCardsToPlayers(GamePlaySettings settings)
   {
      DeckManager.Instance.GivePlayersCard(settings.PlayerCount, settings.NumCardsToGive);
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
      step = false;
   }
}
