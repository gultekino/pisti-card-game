    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class StashSlot : MonoBehaviour
    {
       
        private readonly List<Card> cardsInStashSlot = new();

        public List<Card> CardsInStashSlot => cardsInStashSlot;

        public void CarryNewCards(IEnumerable<Card> cards)
        {
            cardsInStashSlot.AddRange(cards);
            foreach (var card in cards)
            {
                DOTween.Sequence().SetDelay(0.3f).Append(       
                    card.transform.DOMove(transform.position, 0.3f).Play());
            }
            if (cards.Any())
            {
                cards.Last().UpdateVisualSortingOrder(SortingOrder.UnderCard);
                cardsInStashSlot[0].UpdateVisualSortingOrder(SortingOrder.UpperCard);
            }
        }

        public void Clear()
        {
            cardsInStashSlot.Clear();
        }
    }
