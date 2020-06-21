using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace X_O
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool playerOne = true;
        public byte[] mass = new byte[9] {0,0,0,0,0,0,0,0,0};
        public string winner;
        public bool win = false;
        byte count = 0;
        public bool reset=false;
        public MainWindow ()
        {            
            InitializeComponent();           
                                                                     
        }
        private void one_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();            

            switch (((Button)sender).Tag)
            {
                case "1":case "2":case "3":case "4":case "5":
                case "6":case "7":case "8":case "9":
                    if (playerOne && ((Button)sender).Content.ToString()!="X" && ((Button)sender).Content.ToString() != "O")
                    {
                        Title = "XO Game -> O";
                        ((Button)sender).Content = "X";
                        ((Button)sender).Foreground = (Brush)bc.ConvertFrom("#FF001470");
                        //if (((Button)sender).IsMouseOver) { ((Button)sender).Background = (Brush)bc.ConvertFrom("#FFF"); };
                        mass[Convert.ToInt32(((Button)sender).Tag.ToString())-1] = 1;                        
                        playerOne = !playerOne;
                        winner = "Player 1";
                        count++;                        
                    }
                    else if(!playerOne && ((Button)sender).Content.ToString() != "X" && ((Button)sender).Content.ToString() != "O")
                    {
                        Title = "XO Game -> X";
                        ((Button)sender).Content = "O";
                        ((Button)sender).Foreground = (Brush)bc.ConvertFrom("#FFDE5100");
                        mass[Convert.ToInt32(((Button)sender).Tag.ToString()) - 1] = 2;
                        playerOne = !playerOne;
                        winner = "Player 2";
                        count++;                        
                    }
                    break;                
            }

            for (int i = 0; i < mass.Length; i++)
            {               
                if (mass[0]!=0 && mass[0]==mass[1]&&mass[1]==mass[2])
                {
                    win = true;break;
                }
                if (mass[3] != 0 && mass[3] == mass[4] && mass[4] == mass[5])
                {
                    win = true; break;
                }
                if (mass[6] != 0 && mass[6] == mass[7] && mass[7] == mass[8])
                {
                    win = true; break;
                }
                if (mass[0] != 0 && mass[0] == mass[3] && mass[3] == mass[6])
                {
                    win = true; break;
                }
                if (mass[1] != 0 && mass[1] == mass[4] && mass[4] == mass[7])
                {
                    win = true; break;
                }
                if (mass[2] != 0 && mass[2] == mass[5] && mass[5] == mass[8])
                {
                    win = true; break;
                }
                if (mass[0] != 0 && mass[0] == mass[4] && mass[4] == mass[8])
                {
                    win = true; break;
                }
                if (mass[2] != 0 && mass[2] == mass[4] && mass[4] == mass[6])
                {
                    win = true; break;
                }                
            }

            if (win)
            {
                MessageBox.Show($"{winner} WIN!");
                reset = true;
            }
            else if (count == 9)
            {
                MessageBox.Show($"DRAW :-\\ ");
                reset = true;
            }
            if (reset)
            {
                one.Content = "";
                two.Content = "";
                three.Content = "";
                four.Content = "";
                five.Content = "";
                six.Content = "";
                seven.Content = "";
                eight.Content = "";
                nine.Content = "";
                win = false;
                count = 0;
                for (int i = 0; i < mass.Length; i++)                
                    mass[i] = 0;                
                reset = false;
            }            
        }

        
    }
}


        
    

