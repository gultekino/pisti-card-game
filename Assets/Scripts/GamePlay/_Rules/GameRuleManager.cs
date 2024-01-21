using System.Collections.Generic;

public class GameRuleManager
{
    private List<IGameRule> rules = new();

    public GameRuleManager()
    {
        rules.Add(new JTakesAllRule());
        rules.Add(new MatchingNumberRule());
    }

    public void AddRule(IGameRule rule)
    {
        rules.Add(rule);
    }

    public IGameRule CheckRules(Card cardOnTop, Card playedCard)
    {
        foreach (var rule in rules)
        {
            if (rule.Apply(cardOnTop, playedCard))
            {
                return rule;
            }
        }
        return null;
    }
}