using GraDeck.Models;
using System.Windows;

namespace GraDeck.Services
{
    public class GameStateManager
    {
        private CardItem? _firstCard;
        private CardItem? _secondCard;

        public CardItem? FirstCard => _firstCard;
        public CardItem? SecondCard => _secondCard;

        public void SetFirstCard(CardItem card)
        {
            _firstCard = card;
        }

        public void SetSecondCard(CardItem card)
        {
            _secondCard = card;
        }

        public bool TrySetCard(CardItem card)
        {
            if (IsCardAlreadySelected(card))
                return false;

            if (_firstCard == null)
            {
                SetFirstCard(card);
                return true;
            }

            if (_secondCard == null)
            {
                SetSecondCard(card);
                return true;
            }

            return false;
        }

        public bool HasTwoCardsSelected()
        {
            return _firstCard != null && _secondCard != null;
        }

        public void Reset()
        {
            _firstCard = null;
            _secondCard = null;
        }

        private bool IsCardAlreadySelected(CardItem card)
        {
            return _firstCard?.Index == card.Index && _secondCard == null;
        }
    }
}
