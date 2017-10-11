using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreakingMathRemake
{
    public partial class SecondPage : ContentPage
    {
        //Generate initial random for the formula.
        Random randomNumber = new Random();

        public SecondPage()
        {
            InitializeComponent();

            //Set the Formula and Answer texts as well as start the clock.
            Formula.Text = Equation.firstNumber + "+" + Equation.secondNumber;
            Answer.Text = "=" + Equation.answer;
            StartTime();
            StartCountDown();
        }

        //Function called when button is clicked.
        async void ButtonClicked(object sender, EventArgs e)
        {
            //Cast sender as a button.
            Button button = (Button)sender;

            //Which button was presed?
            if (button.Text == "Right!")
            {
                //Was the answer right?
                if (Equation.answer == (Equation.firstNumber + Equation.secondNumber))
                {
                    //Increase score, set answered to true for timer, increase the count to increase hardness (at 5 it should increase the max number the random numbers by 1).
                    Score.score += 1;
                    Score.answered = true;
                    Score.hardnessIncreaseCount += 1;

                }
                //If wrong.
                else if (Equation.firstNumber + Equation.secondNumber != Equation.answer)
                {
                    //If your score is the highest set the high score to it.
                    if (Score.score > Score.highScore)
                    {
                        Score.highScore = Score.score;
                    }
                    //Set game over to true for timer.
                    Score.gameOver = true;

                    //Display game over alert window with scores.
                    await DisplayAlert("Game Over", "New " + Score.score + Environment.NewLine + "Best " + Score.highScore, "Retry?");

                    //Randomly assign a new background color.
                    AppBackground.BackgroundColor = new Color(new Random().NextDouble(), new Random().NextDouble(), new Random().NextDouble());

                    //Set the score to 0 to start over.
                    Score.score = 0;
                }
            }
            else if (button.Text == "X")
            {
                //If right.
                if (Equation.firstNumber + Equation.secondNumber != Equation.answer)
                {
                    //Increase score by 1, set answered to true for timer, and increase the hardness count by 1 (at 5 the maximum number that can be randomly generated will be increased by 1).
                    Score.score += 1;
                    Score.answered = true;
                    Score.hardnessIncreaseCount += 1;

                }
                //If wrong.
                else if (Equation.firstNumber + Equation.secondNumber == Equation.answer)
                {
                    //If the score is the highest set the high score to it.
                    if (Score.score > Score.highScore)
                    {
                        Score.highScore = Score.score;
                    }

                    //Set game over to true for timer.
                    Score.gameOver = true;

                    //Display window with scores.
                    await DisplayAlert("Game Over", "New " + Score.score + Environment.NewLine + "Best " + Score.highScore, "Retry?");

                    //Set background color to a random color.
                    AppBackground.BackgroundColor = new Color(randomNumber.NextDouble(), randomNumber.NextDouble(), randomNumber.NextDouble());

                    //Set the score to 0 to restart.
                    Score.score = 0;
                }

            }

            //If hardness count hits 5 increase the max formula number by 1.
            if (Score.hardnessIncreaseCount == 5)
            {
                Score.hardnessIncreaseCount = 0;
                Score.maxNumber += 1;
            }

            //Generate a new random at button click.
            randomNumber = new Random();

            //Get new equations, update text, restart countdown.
            GetNewEquation();
            UpdateTexts();
            StartCountDown();
        }


        void GetNewEquation()
        {
            //Get two random numbers.
            Equation.firstNumber = randomNumber.Next(1, Score.maxNumber);
            Equation.secondNumber = randomNumber.Next(1, Score.maxNumber);

            //Get a random answer from the mean between the first two numbers to a maximum of two times the highest possible minus 1. This was done to increase the likelihood of the number being reasonably believable.
            Equation.answer = randomNumber.Next((Equation.firstNumber + Equation.secondNumber) / 2, (2 * Score.maxNumber) - 1);
        }

        void UpdateTexts()
        {
            //Update the texts on the screen.
            Formula.Text = Equation.firstNumber + "+" + Equation.secondNumber;
            Answer.Text = "=" + Equation.answer;
            ScoreBoard.Text = Score.score.ToString();
        }

        void ResetDifficulty()
        {
            //Reset Dificultly.
            Score.hardnessIncreaseCount = 0;
            Score.maxNumber = 4;
        }

        public async void StartCountDown()
        {
            //Reset the difficulty.
            ResetDifficulty();

            //Set the progress bar to full, wait until it's done before proceeding. 1 means full, the 0 is the time it takes to animate the bar, and the animation is set to linear.
            await TimerBar.ProgressTo(1, 0, Easing.Linear);

            //Set answered bool to false.
            Score.answered = false;

            //The game is no longer over.
            Score.gameOver = false;

            //Set the progress bar to animate from full to 0, over 5 seconds, with a linear animation.
            await TimerBar.ProgressTo(0, 5000, Easing.Linear);

            //If the score isn't answered and the game isn't over at the end.
            if (!Score.answered && !Score.gameOver)
            {
                //If the score is the highest set the high score.
                if (Score.score > Score.highScore)
                {
                    Score.highScore = Score.score;
                }

                //Set game over to true.
                Score.gameOver = true;

                //Display Time Out window, with scores.
                await DisplayAlert("Time Out!", "New " + Score.score + Environment.NewLine + "Best " + Score.highScore, "Retry?");

                //Set game over to false after window is closed.
                Score.gameOver = false;

                //Set answered to false.
                Score.answered = false;

                //Set score to 0.
                Score.score = 0;

                //Restart countdown.
                StartCountDown();
            }
        }

        //Time to be used to display the time if wanted.
        async void StartTime()
        {
            //While on this page.
            while (App.Current.MainPage == this)
            {
                //wait for 1 second.
                await Task.Delay(1000);

                //Increase the time counter by 1.
                Time.time += 1;

            }
        }
    }

}

