public interface IGameRule
{
    bool Apply(Card cardOnTop, Card playedCard);
}
