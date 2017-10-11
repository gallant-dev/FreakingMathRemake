using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreakingMathRemake
{
    public static class Score
    {
        public static int score;
        public static int highScore;
        public static int hardnessIncreaseCount;
        public static int maxNumber;
        public static int timer;
        public static bool answered;
        public static bool gameOver;
    }
    public static class Equation
    {
        public static int firstNumber;
        public static int secondNumber;
        public static int answer;
    }

    public static class Time
    {
        public static int time;
    }

    public partial class MainPage : ContentPage
    {
        Random initialRandom = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        async void PlayClicked(object sender, EventArgs e)
        {
            //Set the initial equation values.
            Score.maxNumber = 4;
            Equation.firstNumber = initialRandom.Next(1, Score.maxNumber);
            Equation.secondNumber = initialRandom.Next(1, Score.maxNumber);
            Equation.answer = initialRandom.Next(1, (2 * Score.maxNumber) - 1);

            //Load the next page and wait until its completed. 
            await App.Current.MainPage.Navigation.PushAsync(new SecondPage());
        }
    }

}
