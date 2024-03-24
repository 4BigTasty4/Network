using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM3_PT2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            string text = InputTextBox.Text;

            TextAnalysisResult result = await Task.Run(() => AnalyzeText(text));

            string report = "Отчёт о тексте:\n";

            if (SentencesCheckBox.IsChecked == true)
            {
                report += $"Количество предложений: {result.SentenceCount}\n";
            }
            if (CharactersCheckBox.IsChecked == true)
            {
                report += $"Количество символов: {result.CharacterCount}\n";
            }
            if (WordsCheckBox.IsChecked == true)
            {
                report += $"Количество слов: {result.WordCount}\n";
            }
            if(QuestionsCheckBox.IsChecked == true)
            {
                report += $"Количество вопросительных предложений: {result.QuestionCount}\n";
            }
            if (ExclamationsCheckBox.IsChecked == true)
            {
                report += $"Количество восклицательных предложений: {result.ExclamationCount}\n";
            }

            ReportTextBlock.Text = report;

        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            string report = ReportTextBlock.Text;

            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.DefaultExt = "txt";
            dialog.FileName = "report.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName,report);
                MessageBox.Show("Отчёт успешно сохранен.");
            }

        }

        private TextAnalysisResult AnalyzeText(string text)
        {
            int sentenceCount = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int characterCount = text.Length;
            int wordCount = text.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int questionCount = text.Count(c => c == '?');
            int exclamationCount = text.Count(c => c == '!');

            return new TextAnalysisResult
            {
                SentenceCount = sentenceCount,
                CharacterCount = characterCount,
                WordCount = wordCount,
                QuestionCount = questionCount,
                ExclamationCount = exclamationCount
            };


        }

    }

    public class TextAnalysisResult
    {
        public int SentenceCount { get; set; }
        public int CharacterCount { get; set; }
        public int WordCount { get; set; }
        public int QuestionCount { get; set; }
        public int ExclamationCount { get; set; }
    }

}