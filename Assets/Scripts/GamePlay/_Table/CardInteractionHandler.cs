public class CardInteractionHandler : ICardInteractionHandler
{
    private GameRuleManager gameRuleManager = new GameRuleManager();

    public IGameRule HandleCardPlayed(Card playedCard, Card cardOnTop)
    {
       return gameRuleManager.CheckRules(cardOnTop, playedCard);
    }

    public void UpdateCardDrawingOrder(Card playedCard,Card preUpperCard)
    {
        playedCard.UpdateVisualSortingOrder(SortingOrder.UpperCard);
        if (preUpperCard!=null)
        {
            preUpperCard.UpdateVisualSortingOrder(SortingOrder.UnderCard);
        }
    }
}