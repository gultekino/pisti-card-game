using System.Collections;

public class RoundManager
{
    private GamePlaySettings settings;
    private DeckManager deckManager;
    private bool step = false;

    public void StepInRound()
    {
        step = true;
    }

    public RoundManager(GamePlaySettings settings, DeckManager deckManager)
    {
        this.settings = settings;
        this.deckManager = deckManager;
    }

    public IEnumerator PlayRound()
    {
        deckManager.DealCards(settings.PlayerCount, settings.NumCardsToGive);
        yield return PlayRounds(settings);
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

    private IEnumerator WaitForPlayerStep()
    {
        while (!step)
            yield return null;
        step = false;
    }

   
}