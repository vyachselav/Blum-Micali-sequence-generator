using System;
using System.Windows;
using RandomGenerator.Models;

namespace RandomGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            var context = new ViewModel
                {
                    Count = 5,
                    Length = 100,
                    PlainText = "test",
                    BuiltInGenerator = new BuiltInSequenceGenerator(),
                    BlumMicaliGenerator = new BlumMicaliSequenceGenerator { A = 7, P = 23 }
                };

            DataContext = context;

            InitializeComponent();
        }

        private void UpdateStats()
        {
            try
            {
                var context = DataContext as ViewModel;
                var sequence = SequencesListView.SelectedItem as Sequence;

                if (context == null)
                {
                    return;
                }

                sequence.UpdateZeroesAndOnes();

                context.Zeroes = sequence.Zeroes;
                context.Ones = sequence.Ones;

                double n00,
                       n11;

                context.SingleBitsTest = sequence.SingleBitsTest();
                context.BitsPairsTest = sequence.BitsPairsTest(out n00, out n11);
                context.PokerTest = sequence.PokerTest();
                context.SeriesTest = sequence.SeriesTest(n00, n11);
                context.CorrelationTest = sequence.CorrelationTest();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Najpierw wybierz ciąg!");
            }
        }

        private void BuiltInGenerationButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as ViewModel;

            if (context != null)
            {
                context.Sequences = context.BuiltInGenerator.GenerateMany(context.Length, context.Count);
            }
        }

        private void CustomGenerationButtonClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as ViewModel;

            if (context != null)
            {
                context.Sequences = context.BlumMicaliGenerator.GenerateMany(context.Length, context.Count);
            }
        }

        private void SequencesListViewSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SequencesListView.SelectedItem != null)
            {
                UpdateStats();
            }
        }

        private void ButtonEncryptClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var context = DataContext as ViewModel;
                var sequence = SequencesListView.SelectedItem as Sequence;

                if (context == null)
                {
                    return;
                }

                context.CipherText = sequence.EncryptText(context.PlainText);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Najpierw wybierz ciąg!");
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
