using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace GuessThePercentage
{
    public partial class MainWindow : Window
    {
        private List<Question> questions;
        private Question currentQuestion;
        private int guessPercentage;
        private int questionCount;
        private int totalScore;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuestions();
            StartNewGame();
        }

        private void LoadQuestions()
        {
            try
            {
                XDocument doc = XDocument.Load("QuestionsRus.xml");
                questions = doc.Root
                    .Elements("Question")
                    .Select(q => new Question
                    {
                        Text = q.Element("Text").Value,
                        CorrectPercentage = int.Parse(q.Element("CorrectPercentage").Value)
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке вопросов. " + ex.Message);
            }
        }

        private void StartNewGame()
        {
            questionCount = 5;
            totalScore = 0;

            if (questions != null && questions.Count >= questionCount)
            {
                SetNextQuestion();
                guessPercentage = 50;
                guessSlider.Value = guessPercentage;
                resultLabel.Content = "";
                scoreLabel.Visibility = Visibility.Collapsed;
                restartButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Недостаточно вопросов для игры. Пожалуйста, добавьте больше вопросов.");
                Close();
            }
        }

        private void SetNextQuestion()
        {
            if (questions.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(questions.Count);
                currentQuestion = questions[index];
                questions.RemoveAt(index);
                QuestionText = currentQuestion.Text;
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            int correctPercentage = currentQuestion.CorrectPercentage;
            string resultText = "";

            if (Math.Abs(guessPercentage - correctPercentage) <= 10)
            {
                resultText = "Верно!";
                int score = CalculateScore(Math.Abs(guessPercentage - correctPercentage));
                totalScore += score;
                resultText += " Заработано очков: " + score;
            }
            else
            {
                resultText = "Неверно. Правильный ответ: " + correctPercentage + "%";
            }

            resultLabel.Content = resultText;
            questionCount--;

            if (questionCount > 0)
            {
                SetNextQuestion();
                guessPercentage = 50;
                guessSlider.Value = guessPercentage;
            }
            else
            {
                scoreLabel.Content = "Игра завершена. Ваш итоговый набор баллов: " + totalScore + " из 1000";
                scoreLabel.Visibility = Visibility.Visible;
                restartButton.Visibility = Visibility.Visible;
            }
        }

        private int CalculateScore(int difference)
        {
            int score = 100;

            if (difference > 10)
                score -= difference * 5;

            return Math.Max(score, 0);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            questions.Clear();
            LoadQuestions();
            StartNewGame();
        }

        public string QuestionText
        {
            get { return (string)GetValue(QuestionTextProperty); }
            set { SetValue(QuestionTextProperty, value); }
        }

        public static readonly DependencyProperty QuestionTextProperty =
            DependencyProperty.Register("QuestionText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));
    }

    public class Question
    {
        public string Text { get; set; }
        public int CorrectPercentage { get; set; }
    }
}
