public interface ICardInteractionHandler
{
    public IGameRule HandleCardPlayed(Card playedCard, Card cardOnTop);
    public void UpdateCardDrawingOrder(Card playedCard, Card preUpperCard);
}