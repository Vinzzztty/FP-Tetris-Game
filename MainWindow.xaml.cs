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
using System.Media;
using System.Windows.Media.Animation;

namespace Tetris_klmpk9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        int x = 1 , y = 1;
        int delay;
        bool pause = false;

        SoundPlayer playSound = new SoundPlayer("Sound\\Studio_Project.wav");

        private readonly ImageSource[] tileimage = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/tidakadaisi.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/pink.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/birutua.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/hijau.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/biru.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/kuning.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/oren.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/merah.png ",UriKind.Relative)),
        };

        private readonly ImageSource[] blokimage = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-empty.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png ",UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png ",UriKind.Relative)),
        };
        private readonly Image[,] imageControls;
        private GameState gameState = new GameState();
        private readonly int maxDelay = 750;
        private readonly int minDelay = 75;
        private readonly int delayberkurang = 20;
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }
        

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] ImageControls = new Image[grid.Baris, grid.Kolom];
            int cellSize = 25;

            for (int b = 0; b < grid.Baris; b++)
            {
                for (int k = 0; k < grid.Kolom; k++)
                {
                    Image imageControls = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                        
                    };

                    Canvas.SetTop(imageControls, (b - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControls, k * cellSize);
                    GameCanvas.Children.Add(imageControls);
                    ImageControls[b, k] = imageControls;
                }
            }
            return ImageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int b = 0; b < grid.Baris; b++)
            {
                for (int k = 0; k < grid.Kolom; k++)
                {
                    int id = grid[b, k];
                    imageControls[b, k].Opacity = 1;
                    imageControls[b, k].Source = tileimage[id];
                    
                }
            }
        }
        
        private void DrawGhostBlok(Blok blok)
        {
            int JarakDrop = gameState.BlokDrop();
            foreach (Posisi p in blok.TilePosisi())
            {
                imageControls[p.Baris + JarakDrop, p.Kolom].Opacity= 0.2;
                imageControls[p.Baris + JarakDrop, p.Kolom].Source = tileimage[blok.Id];
            }
        }
        private void DrawNextBlok(BlokAntrian blokantrian)
        {
            Blok next = blokantrian.NextBlok;
            gambarnext.Source = blokimage[next.Id];
        }

        private void DrawHoldBlok(Blok DiTahanBlok)
        {
            if (DiTahanBlok == null)
            {
                HoldImage.Source = blokimage[0];
            }
            else
            {
                HoldImage.Source = blokimage[DiTahanBlok.Id ];
            }
        }
        private void DrawBlok(Blok blok)
        {
            foreach (Posisi p in blok.TilePosisi())
            {
                imageControls[p.Baris, p.Kolom].Opacity = 1;
                imageControls[p.Baris, p.Kolom].Source = tileimage[blok.Id];
            }
        }

        private async Task GameLoop()
        {
            pause = false;
            Draw(gameState);
            playSound.Play();
            playSound.PlayLooping();
            while (!gameState.GameOver)
            {
                if(y == 0)
                {
                    break;
                }
                
                delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayberkurang));
                
                await Task.Delay(delay);
                gameState.statepause(pause);
                Draw(gameState);
            }
            if (y != 0)
            {
                playSound.Stop();
                GameOverMenu.Visibility = Visibility.Visible;
                score.Text = $"Your Score:  {gameState.Score}";
                isibox.Items.Add($"{x}.");
                if (gameState.Score <= 9)
                {
                    box2.Items.Add($"0{gameState.Score}");
                }
                else
                {
                    box2.Items.Add($"{gameState.Score}");
                }

                box2.Items.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription("",
                        System.ComponentModel.ListSortDirection.Descending));
                x++;
            }




        }
        private void Draw(GameState gameState) 
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlok(gameState.saatiniBlok);
            DrawBlok(gameState.SaatiniBlok);
            DrawNextBlok(gameState.BlokAntrian);
            Scorekita.Text = $"Score :  {gameState.Score}";
            DrawHoldBlok(gameState.DiTahanBlok);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch( e.Key)
            {
                case Key.Up:
                    gameState.PutarBlokCW();
                    break;
                case Key.Left:
                    gameState.PindahKiri();
                    break;
                case Key.Right:
                    gameState.PindahKanan();
                    break;
                case Key.Down:
                    gameState.PindahBawah();
                    break;
                case Key.Z:
                    gameState.PutarBlokCCW();
                    break;
                case Key.C:
                    gameState.TahanBlok();
                    break;
                case Key.Space:
                    gameState.DropBlok();
                    break;
                case Key.P:
                    playSound.Stop();
                    PauseMenu.Visibility = Visibility.Visible;
                    pause = true;

                    break;
                default:
                    return;


            }
            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            if (x == 0)
            {
                await GameLoop();
            }
        }

        private async void Restart_click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility= Visibility.Hidden;
            await GameLoop();
            y = 1;
        }

        private async void Resume_Click(object sender, RoutedEventArgs e)
        {
            playSound.Play();
            PauseMenu.Visibility = Visibility.Hidden;
            pause = false;
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            MenuAwal.Visibility = Visibility.Hidden;
            gameState = new GameState();
            await GameLoop();
            y = 1;
        }

        private async void Mainmenu_click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Hidden;
            MenuAwal.Visibility = Visibility.Visible;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Mainmenup_click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Hidden;
            MenuAwal.Visibility = Visibility.Visible;
            y = 0;
        }
    }
}
