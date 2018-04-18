using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NetBreakoutPongClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClassicPongGameData gameData;
        //private Driver driver = new Driver();
        private GameServer server = new GameServer();

        private Boolean leftKeyPressed = false;
        private Boolean rightKeyPressed = false;

        private Brush myPaddleColor = Brushes.Blue;
        private Brush oppPaddleColor = Brushes.Red;
        private Brush ballColor = Brushes.Yellow;

        private int myNumScore = 0;
        private int oppNumScore = 0;

        private bool userYes = false;
        private bool userNo = false;

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            this.KeyUp += new KeyEventHandler(OnButtonKeyUp);

            Thread myThread = new Thread(Gameplay);
            myThread.IsBackground = true;
            myThread.Start();


            //This code is for my local driver
            /*while (gameData.ILost == false && gameData.OppLost == false)
            {
                gameData = driver.getGameData();
                PaintGame(gameData);
            }
            gameData = driver.getGameData();
            PaintGame(gameData);
            
            gameData = driver.getGameData();
            PaintGame(gameData);
            gameData = driver.getGameData();
            PaintGame(gameData);
            */


        }

        public void Gameplay()
        {
            do
            {
                gameData = server.GetData();
                PaintGame(gameData);

                while (gameData.ILost == false && gameData.OppLost == false)
                {
                    server.SendKeypress(new Keypress(leftKeyPressed, rightKeyPressed));
                    gameData = server.GetData();
                    PaintGame(gameData);
                }

                server.SendContinue(QueryContinue());

            } while (server.ContinueApproved());            
        }

        public void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Left))
                leftKeyPressed = true;

            if (Keyboard.IsKeyDown(Key.Right))
                rightKeyPressed = true;
            
            if (Keyboard.IsKeyDown(Key.Y))
                userYes = true;

            if (Keyboard.IsKeyDown(Key.N))
                userNo = true;
        }

        public void OnButtonKeyUp(object sender, KeyEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.Left))
                leftKeyPressed = false;

            if (!Keyboard.IsKeyDown(Key.Right))
                rightKeyPressed = false;
        }

        private void PaintGame(ClassicPongGameData data)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Rectangle myPaddle = new Rectangle();
                myPaddle.Fill = myPaddleColor;
                myPaddle.Height = data.myPaddle.Height;
                myPaddle.Width = data.myPaddle.Width;

                Canvas.SetTop(myPaddle, (data.myPaddle.Position.Y - (data.myPaddle.Height / 2)));
                Canvas.SetLeft(myPaddle, (data.myPaddle.Position.X - (data.myPaddle.Width / 2)));

                Rectangle oppPaddle = new Rectangle();
                oppPaddle.Fill = oppPaddleColor;
                oppPaddle.Height = data.oppPaddle.Height;
                oppPaddle.Width = data.oppPaddle.Width;

                Canvas.SetTop(oppPaddle, (data.oppPaddle.Position.Y - (data.oppPaddle.Height / 2)));
                Canvas.SetLeft(oppPaddle, (data.oppPaddle.Position.X - (data.oppPaddle.Width / 2)));

                Ellipse gameBall = new Ellipse();
                gameBall.Width = data.gameBall.Radius * 2;
                gameBall.Height = data.gameBall.Radius * 2;
                gameBall.Fill = ballColor;

                Canvas.SetTop(gameBall, (data.gameBall.Position.Y - (data.gameBall.Radius)));
                Canvas.SetLeft(gameBall, (data.gameBall.Position.X - (data.gameBall.Radius)));

                if (gameCanvas.Children.Count > 0)
                {
                    gameCanvas.Children.Clear();
                }

                gameCanvas.Children.Add(myPaddle);
                gameCanvas.Children.Add(oppPaddle);
                gameCanvas.Children.Add(gameBall);

                if (data.ILost || data.OppLost)
                    PrintGameOver(data.ILost);
            }));
        }

        private bool QueryContinue()
        {
            userNo = userYes = false;
            while (!userNo && !userYes)
                Thread.Sleep(5);
            return userYes;
        }

        private void PrintGameOver(bool ILost)
        {
            TextBlock gameOverBlock = new TextBlock();
            if (ILost)
            {
                gameOverBlock.Text = "You Lost!";
                Canvas.SetLeft(gameOverBlock, 180);
                oppNumScore += 1;
            }
            else
            {
                gameOverBlock.Text = "You Won!";
                Canvas.SetLeft(gameOverBlock, 172);
                myNumScore += 1;
            }
            gameOverBlock.Foreground = Brushes.White;
            gameOverBlock.FontSize = 40;
            Canvas.SetTop(gameOverBlock, 180);
            gameCanvas.Children.Add(gameOverBlock);

            TextBlock playAgainBlock = new TextBlock();
            playAgainBlock.Text = "Play Again? <y/n>";
            playAgainBlock.Foreground = Brushes.White;
            playAgainBlock.FontSize = 20;
            Canvas.SetLeft(playAgainBlock, 175);
            Canvas.SetTop(playAgainBlock, 250);
            gameCanvas.Children.Add(playAgainBlock);

            myScore.Text = myNumScore + "";
            oppScore.Text = oppNumScore + "";
        }
    }
}
