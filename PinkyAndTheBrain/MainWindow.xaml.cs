using PinkyAndTheBrain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PinkyAndTheBrain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<List<Char>> Matrix;
        public int  remainingSprouts = 0;
        public RatPlayer Pinky, Brain;
        char WallEleemet = '#';
        char SproutElement = '@';
        public MainWindow()
        {
            InitializeComponent();
            CreateContext();
            showMatrixInTextbox();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            //Controls for Pinky
           if(e.Key == Key.W)
            {
                MovePlayerInMatrix(Pinky, Pinky.LineIndex -1, Pinky.ColumnIndex);
            }

            if (e.Key == Key.S)
            {
                MovePlayerInMatrix(Pinky, Pinky.LineIndex + 1, Pinky.ColumnIndex);
            }

            if (e.Key == Key.D)
            {
                MovePlayerInMatrix(Pinky, Pinky.LineIndex , Pinky.ColumnIndex + 1);
            }

            if (e.Key == Key.A)
            {
                MovePlayerInMatrix(Pinky, Pinky.LineIndex, Pinky.ColumnIndex - 1);
            }

            //Contorls for Brain
            if (e.Key == Key.I)
            {
                MovePlayerInMatrix(Brain, Brain.LineIndex - 1, Brain.ColumnIndex);
            }

            if (e.Key == Key.K)
            {
                MovePlayerInMatrix(Brain, Brain.LineIndex + 1, Brain.ColumnIndex);
            }

            if (e.Key == Key.L)
            {
                MovePlayerInMatrix(Brain, Brain.LineIndex, Brain.ColumnIndex + 1);
            }

            if (e.Key == Key.J)
            {
                MovePlayerInMatrix(Brain, Brain.LineIndex, Brain.ColumnIndex - 1);
            }

            showMatrixInTextbox();

            if (remainingSprouts == 0)
                showWinner();
        }

        public void showWinner()
        {
            if (Pinky.Score > Brain.Score)
                textBox.Text = "The winner is Pinky with a score of " + Pinky.Score.ToString() + " - " + Brain.Score.ToString();

            if (Pinky.Score < Brain.Score)
                textBox.Text = "The winner is Brain with a score of " + Brain.Score.ToString() + " - " + Pinky.Score.ToString();

            if(Pinky.Score == Brain.Score)
                textBox.Text = "The game ended at a tie of : " + Pinky.Score.ToString() + " - " + Brain.Score.ToString();
        }

        public void MovePlayerInMatrix(RatPlayer player, int futureLineIndex, int futureColumnIndex)
        {
            char affectedElement = Matrix[futureLineIndex][futureColumnIndex]; //get the below element

            if (affectedElement != WallEleemet) //check the above element
            {
                Matrix[futureLineIndex][futureColumnIndex] = player.LetterOfIdentification;
                Matrix[player.LineIndex][player.ColumnIndex] = '=';

                player.LineIndex = futureLineIndex;
                player.ColumnIndex = futureColumnIndex;

                if (affectedElement == SproutElement)
                {
                    player.Score++;
                    remainingSprouts--;
                }
            }
        }
        private void CreateContext()
        {            //Initialize PLayers 
            Pinky = new RatPlayer();
            Pinky.Score = 0;
            Pinky.LetterOfIdentification = 'P';

            Brain = new RatPlayer();
            Brain.Score = 0;
            Brain.LetterOfIdentification = 'B';

            //read map from file
            string path = @"resources\Pinkyandthebrain.maze";

            string[] lines = System.IO.File.ReadAllLines(path);

            Matrix = new List<List<char>>(); 

            for (int i=0; i< lines.Length; i++)
            {
                Matrix.Add(new List<char>());//for each i add a line to Matrix

                char[] CharsInCurrentLine = lines[i].ToCharArray();
                for (int j = 0; j< CharsInCurrentLine.Length; j++)
                {
                    Matrix[i].Add(CharsInCurrentLine[j]); // populate matrix with content of map

                    //calculate number of total sprouts on map
                    if (CharsInCurrentLine[j] == SproutElement)
                        remainingSprouts++;

                    if(CharsInCurrentLine[j] != SproutElement && CharsInCurrentLine[j] != WallEleemet)// if character is neither a sprout, neither a wall
                    {
                        
                        if ((Brain.LineIndex != 0 & Brain.ColumnIndex != 0) &&(Pinky.LineIndex == 0 & Pinky.ColumnIndex == 0))  //  set position of Pinky on first empthy character
                        {
                            Matrix[i][j] = Pinky.LetterOfIdentification;
                            Pinky.LineIndex = i;
                            Pinky.ColumnIndex = j;
                        }

                        if (Brain.LineIndex == 0 & Brain.ColumnIndex == 0)  //  set position of Brain on first empthy character
                        {
                            Matrix[i][j] = Brain.LetterOfIdentification;
                            Brain.LineIndex = i;
                            Brain.ColumnIndex = j;
                        }
                    }
                }          
            }



        }

        public void showMatrixInTextbox()
        {
            textBox.Text = "";
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[0].Count; j++)
                {
                    textBox.Text = textBox.Text + Matrix[i][j];
                }

                textBox.Text = textBox.Text + Environment.NewLine;
            }
        }


    }
}
