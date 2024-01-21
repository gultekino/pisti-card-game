public class MatchingNumberRule : IGameRule
{
    public bool Apply(Card cardOnTop, Card playedCard)
    {
        return cardOnTop != null && cardOnTop.Number == playedCard.Number;
    }
}