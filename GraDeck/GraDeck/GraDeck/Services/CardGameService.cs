using GraDeck.Models;

namespace GraDeck.Services
{
    public class CardGameService
    {
        private readonly List<CardItem> _cards = new();
        private readonly Random _random = new();
        private int _matchedPairs;
        
        public event Action<int>? PairsMatched;
        public event Action? GameWon;

        public List<CardItem> GenerateCards(int pairsCount)
        {
            _cards.Clear();
            _matchedPairs = 0;

            var values = CreateValues(pairsCount);
            ShuffleValues(values);
            CreateCardItems(values);

            return _cards;
        }

        public bool CheckMatch(CardItem firstCard, CardItem secondCard)
        {
            bool isMatch = firstCard.Value == secondCard.Value;

            if (isMatch)
            {
                firstCard.IsMatched = true;
                secondCard.IsMatched = true;
                _matchedPairs++;

                PairsMatched?.Invoke(_matchedPairs);

                if (_matchedPairs == _cards.Count / 2)
                {
                    GameWon?.Invoke();
                }
            }

            return isMatch;
        }

        private List<int> CreateValues(int pairsCount)
        {
            var values = new List<int>();
            for (int i = 1; i <= pairsCount; i++)
            {
                values.Add(i);
                values.Add(i);
            }
            return values;
        }

        private void ShuffleValues(List<int> values)
        {
            for (int i = values.Count - 1; i > 0; i--)
            {
                int randomIndex = _random.Next(i + 1);
                (values[i], values[randomIndex]) = (values[randomIndex], values[i]);
            }
        }

        private void CreateCardItems(List<int> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                _cards.Add(new CardItem { Value = values[i], Index = i });
            }
        }
    }
}
