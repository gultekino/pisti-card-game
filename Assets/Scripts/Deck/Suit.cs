using UnityEngine;

[System.Serializable]
public class Suit
{
    [SerializeField] private Shape shape;
    [SerializeField] private CardColor color;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite[] imageSprites;

    public Shape Shape => shape;

    public CardColor Color => color;

    public Sprite DefaultSprite => defaultSprite;

    public Sprite[] ImageSprites => imageSprites;
}