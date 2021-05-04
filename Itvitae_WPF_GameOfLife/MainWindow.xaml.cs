using GameOfLife.Controllers;
using System.Windows;
using System.Windows.Input;
using GameOfLife.Extentions;
using Itvitae_WPF_GameOfLife.Extentions;
using System;

namespace Itvitae_WPF_GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Windows.Point mouseLocation;

        MapController map;

        int mapSize = 0;
        int milisecondsGameSpeed = 0;
        bool GameIsRunning = false;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Activated(object sender, System.EventArgs e)
        {
            mapSize = (int)mapSizeSlider.Value;

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, milisecondsGameSpeed);
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            map.CheckWorldMap(); //Check each cell in the world
            map.UpdateWorldMap(); //Update its status
            map.UpdateBitMapData(); //Generate a Bitmap to display

            MapImage.Source = map.GetBitMapData().Convert(mapSize, 500); //Convert the Bitmap to a type the WPF image thingy can understand.
        }

        private void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, milisecondsGameSpeed);
            if (GameIsRunning)
                StopGame();
            else
                StartGame();
        }

        private void StartGame()
        {
            ResetBtn.IsEnabled = false;
            LoadBtn.IsEnabled = false;
            generateMapBtn.IsEnabled = false;
            mapSizeSlider.IsEnabled = false;
            SpeedSlider.IsEnabled = false;
            spawnRandomBtn.IsEnabled = false;

            StartStopBtn.Content = "Stop";
            dispatcherTimer.Start();

            GameIsRunning = true;
        }
        private void StopGame()
        {
            ResetBtn.IsEnabled = true;
            LoadBtn.IsEnabled = true;
            generateMapBtn.IsEnabled = true;
            mapSizeSlider.IsEnabled = true;
            SpeedSlider.IsEnabled = true;
            spawnRandomBtn.IsEnabled = true;

            StartStopBtn.Content = "Start";
            dispatcherTimer.Stop();

            GameIsRunning = false;
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.GetPosition(null);
            StatusBarLabelLeft.Content = $"X:{mouseLocation.X}|Y:{mouseLocation.Y}";
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseLocation = e.GetPosition(null);

            map.ChangeCellAliveStatus(new Point2(mouseLocation.X, mouseLocation.Y), 500, 500, true);

            map.UpdateBitMapData();
            MapImage.Source = map.GetBitMapData().Convert(mapSize, 500);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            milisecondsGameSpeed = (int)SpeedSlider.Value;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, milisecondsGameSpeed);
            SpeedLabel.Content = milisecondsGameSpeed;
        }

        private void mapSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mapSizeLabel != null) //Gets invoked before label is loaded
            {
                mapSize = (int)mapSizeSlider.Value;
                mapSizeLabel.Content = mapSize;
            }
        }

        private void generateMapBtn_Click(object sender, RoutedEventArgs e)
        {
            map = new MapController(mapSize);

            //Generate the blank world data
            map.GenerateWorldMap();

            //Make sure to populate it with data.
            map.PopulateWorldMap();

            //Convert the map data to a image so we can see
            MapImage.Source = map.GenerateBitMapData().Convert(mapSize, 500);

            StartStopBtn.IsEnabled = true;
            ResetBtn.IsEnabled = true;
            LoadBtn.IsEnabled = true;
            SpeedSlider.IsEnabled = true;
            spawnRandomBtn.IsEnabled = true;
        }

        private void spawnRandomBtn_Click(object sender, RoutedEventArgs e)
        {
            map.RandomAlive();
            map.UpdateBitMapData();

            MapImage.Source = map.GetBitMapData().Convert(mapSize, 500);
        }
    }
}
