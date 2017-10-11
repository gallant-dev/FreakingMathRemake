using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FreakingMathRemake
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FreakingMathRemake.MainPage();
        }

        Random newRandom = new Random();

        protected override void OnStart()
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
            Score.score = 0;
            Score.highScore = 0;
            Equation.firstNumber = newRandom.Next(1, 4);
            Equation.secondNumber = newRandom.Next(1, 4);
            Equation.answer = newRandom.Next(1, 7);

        }
    }
}
