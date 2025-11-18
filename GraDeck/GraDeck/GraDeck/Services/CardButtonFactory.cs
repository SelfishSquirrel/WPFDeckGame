using GraDeck.Models;
using System.Windows.Controls;
using System.Windows.Media;

namespace GraDeck.Services
{
    public class CardButtonFactory
    {
        private const int CardWidth = 90;
        private const int CardHeight = 120;
        private const int CardMargin = 5;

        public Button CreateCardButton(CardItem card)
        {
            return new Button
            {
                Width = CardWidth,
                Height = CardHeight,
                Margin = new System.Windows.Thickness(CardMargin),
                Background = GetClosedCardBrush(),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 32,
                FontWeight = System.Windows.FontWeights.Bold,
                Content = "?",
                Tag = card
            };
        }

        public void OpenCard(Button button, int value)
        {
            button.Content = value.ToString();
            button.Background = GetOpenCardBrush();
            button.IsEnabled = false;
        }

        public void CloseCard(Button button)
        {
            button.Content = "?";
            button.Background = GetClosedCardBrush();
            button.IsEnabled = true;
        }

        private SolidColorBrush GetClosedCardBrush()
        {
            return new SolidColorBrush(Color.FromRgb(100, 150, 200));
        }

        private SolidColorBrush GetOpenCardBrush()
        {
            return new SolidColorBrush(Color.FromRgb(200, 200, 200));
        }
    }
}
