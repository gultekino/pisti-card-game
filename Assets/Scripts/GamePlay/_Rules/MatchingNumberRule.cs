public class MatchingNumberRule : GameRule
{
    public override bool Apply(Card cardOnTop, Card playedCard)
    {
        return cardOnTop != null && cardOnTop.Number == playedCard.Number;
    }
}