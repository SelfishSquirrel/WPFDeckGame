using System.Windows;
using System.Windows.Controls;
using GraDeck.Models;
using GraDeck.Services;

namespace GraDeck
{
    public partial class MainWindow : Window
    {
        private readonly CardGameService _gameService = new();
        private readonly GameStateManager _stateManager = new();
        private CardButtonManager? _buttonManager;

        public MainWindow()
        {
            InitializeComponent();
            SubscribeToGameEvents();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _buttonManager = new CardButtonManager(CardsPanel);
            _stateManager.Reset();

            var cards = _gameService.GenerateCards(8);
            _buttonManager.CreateButtonsForCards(cards);
            SubscribeToCardClicks();
        }

        private void SubscribeToCardClicks()
        {
            foreach (var button in _buttonManager.GetAllButtons())
            {
                button.Click += CardButton_Click;
            }
        }

        private void CardButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var card = (CardItem)button.Tag;

            if (card.IsMatched || !_stateManager.TrySetCard(card))
                return;

            _buttonManager.OpenCard(button, card.Value);

            if (_stateManager.HasTwoCardsSelected())
            {
                HandleCardComparison();
            }
        }

        private void HandleCardComparison()
        {
            var firstCard = _stateManager.FirstCard!;
            var secondCard = _stateManager.SecondCard!;

            if (_gameService.CheckMatch(firstCard, secondCard))
            {
                _stateManager.Reset();
            }
            else
            {
                HideCardsAfterDelay(firstCard, secondCard);
            }
        }

        private void HideCardsAfterDelay(CardItem firstCard, CardItem secondCard)
        {
            Task.Delay(1000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    var firstButton = _buttonManager.FindButtonByCardIndex(firstCard.Index);
                    var secondButton = _buttonManager.FindButtonByCardIndex(secondCard.Index);

                    if (firstButton != null && !firstCard.IsMatched)
                        _buttonManager.CloseCard(firstButton);

                    if (secondButton != null && !secondCard.IsMatched)
                        _buttonManager.CloseCard(secondButton);

                    _stateManager.Reset();
                });
            });
        }

        private void SubscribeToGameEvents()
        {
            _gameService.GameWon += ShowVictoryMessage;
        }

        private void ShowVictoryMessage()
        {
            MessageBox.Show(
                "Поздравляем! Вы выиграли!",
                "Победа!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}