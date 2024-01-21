public class GameInitializer
{
    private DeckManager deckManager;
    private PlayerManager playerManager;
    public GameInitializer(DeckManager deckManager, PlayerManager playerManager)
    {
        this.deckManager = deckManager;
        this.playerManager = playerManager;
    }

    public void InitializeGame(GamePlaySettings settings)
    {
        deckManager.InitializeDeck();
        playerManager.CreatePlayers(settings.PlayerCount);
    }
}