using GraDeck.Models;
using System.Windows.Controls;

namespace GraDeck.Services
{
    public class CardButtonManager
    {
        private readonly CardButtonFactory _buttonFactory;
        private readonly WrapPanel _cardsPanel;

        public CardButtonManager(WrapPanel cardsPanel)
        {
            _cardsPanel = cardsPanel;
            _buttonFactory = new CardButtonFactory();
        }

        public void CreateButtonsForCards(List<CardItem> cards)
        {
            _cardsPanel.Children.Clear();

            foreach (var card in cards)
            {
                var button = _buttonFactory.CreateCardButton(card);
                _cardsPanel.Children.Add(button);
            }
        }

        public Button? FindButtonByCardIndex(int cardIndex)
        {
            return _cardsPanel.Children
                .OfType<Button>()
                .FirstOrDefault(btn => ((CardItem)btn.Tag).Index == cardIndex);
        }

        public void OpenCard(Button button, int value)
        {
            _buttonFactory.OpenCard(button, value);
        }

        public void CloseCard(Button button)
        {
            _buttonFactory.CloseCard(button);
        }

        public IEnumerable<Button> GetAllButtons()
        {
            return _cardsPanel.Children.OfType<Button>();
        }
    }
}
