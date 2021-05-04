using GameOfLife.Controllers;
using System.Windows;
using System.Windows.Input;
using GameOfLife.Extentions;
using Itvitae_WPF_GameOfLife.Extentions;
using System;
using Itvitae_WPF_GameOfLife.Controllers;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Itvitae_WPF_GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Point mouseLocation;

        MapController map;
        MapDisplayController mapDisplayController;

        int mapSize = 0;
        int milisecondsGameSpeed = 0;
        bool GameIsRunning = false;
        bool enableGhosting = false;
        bool gridShowing = false;
        System.Drawing.Color colorHolder;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Activated(object sender, System.EventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, milisecondsGameSpeed);

            SpeedLabel.Content = milisecondsGameSpeed;
            mapSizeLabel.Content = mapSize;
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            map.CheckWorldMap(true); //Check each cell in the world
                                     //map.UpdateWorldMap(); //Update its status

            ////Perry's own solution

            if (gridShowing)
                map.UpdateBitMapDataIncludingGrid();
            else
                map.UpdateBitMapData(); //Generate a Bitmap to display
            if(map.hasBeenUpdated)  //No need to update if there was nothing that has been changed.
                MapImage.Source = map.GetBitMapData().Convert(mapSize, 500); //Convert the Bitmap to a type the WPF image thingy can understand.

            ////Microsoft solution copy-paste
            //UpdateDisplayMap();
            //MapImage.Source = mapDisplayController.GetMap();
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

        private void UpdateDisplayMap()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    colorHolder = map.GetColorAt(x, y);
                    mapDisplayController.SetPixel(x, y, colorHolder.R, colorHolder.G, colorHolder.B, colorHolder.A);
                }
            }
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

            if (GameIsRunning == false)
            {
                map.ChangeCellAliveStatus(new Point2(mouseLocation.X, mouseLocation.Y), 500, 500, true, true);

                if (gridShowing)
                    map.UpdateBitMapDataIncludingGrid();
                else
                    map.UpdateBitMapData();

                MapImage.Source = map.GetBitMapData().Convert(mapSize, 500);
            }
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            milisecondsGameSpeed = (int)SpeedSlider.Value;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, milisecondsGameSpeed);

            if(SpeedLabel != null) //Gets invoked before label is loaded
                SpeedLabel.Content = milisecondsGameSpeed;
        }

        private void mapSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mapSize = (int)mapSizeSlider.Value;

            if (mapSizeLabel != null) //Gets invoked before label is loaded
                mapSizeLabel.Content = mapSize;

        }

        private void generateMapBtn_Click(object sender, RoutedEventArgs e)
        {
            map = new MapController(mapSize);
            //mapDisplayController = new MapDisplayController(mapSize, mapSize);

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

            map.enableGhosting = enableGhosting;
        }

        public void ShowGrid(bool show)
        {
            if (show)
            {
                GridImage.Visibility = Visibility.Visible;
            }
            else
                GridImage.Visibility = Visibility.Hidden;
        }

        private void spawnRandomBtn_Click(object sender, RoutedEventArgs e)
        {
            map.RandomAlive(20, new Random().Next(5,200));
            map.UpdateBitMapData();

            MapImage.Source = map.GetBitMapData().Convert(mapSize, 500);
        }

        private void enableGhostingCheckbox_Click(object sender, RoutedEventArgs e)
        {
            enableGhosting = (bool)enableGhostingCheckbox.IsChecked;
            map.enableGhosting = enableGhosting; //I know, but we need it in map generation to
        }

        private void gridBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(!gridShowing);
            gridShowing = !gridShowing;
        }
    }
}
