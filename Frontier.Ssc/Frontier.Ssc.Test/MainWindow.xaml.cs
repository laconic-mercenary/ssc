using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Frontier.Library.Windows.Wpf.OilUI;

namespace Frontier.Ssc.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region -- Title Bar Interaction --

        void titleBarMain_OnMouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        void titleBarMain_OnLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        void titleBarMain_OnLeftMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            windowMove = Mouse.GetPosition(null);
        }

        void titleBarMain_OnMouseMoved(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point moveVector = (Point)(windowMove - Mouse.GetPosition(null));
                this.Left -= moveVector.X;
                this.Top -= moveVector.Y;
            }
        }

        void titleBarMain_OnButtonMinimizeClicked(object sender, RoutedEventArgs e)
        {
            CurrentWindowState = System.Windows.WindowState.Minimized;
        }

        void titleBarMain_OnButtonMaximizeNormalClicked(object sender, RoutedEventArgs e)
        {
            if (CurrentWindowState == System.Windows.WindowState.Normal)
            {
                CurrentWindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                CurrentWindowState = System.Windows.WindowState.Normal;
            }
        }

        void titleBarMain_OnButtonCloseClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void titleBarMain_OnMouseDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (CurrentWindowState == System.Windows.WindowState.Normal)
            {
                CurrentWindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                CurrentWindowState = System.Windows.WindowState.Normal;
            }
        }

        //
        // Fields
        private bool isMouseDown = false;
        private Point windowMove = new Point();

        #endregion -- Title Bar Interaction --

        public WindowState CurrentWindowState
        {
            get { return WindowState; }
            set 
            {
                titleBarMain.CurrentWindowState = value;
                WindowState = value;
            }
        }
        
        //
        // Public Methods

        public MainWindow()
        {
            InitializeComponent();
        }

        //
        // Private Methods

        //
        // UI Events

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //titleBarMain.ApplicationIcon = new BitmapImage(new Uri(@"C:\Users\mlcs\documents\visual studio 2010\Projects\Frontier.Ssc\Frontier.Ssc.Test\ATT201940213.jpg"));
            titleBarMain.ApplicationTitle = "Secure Shell Converse - Frontier Outpost LLC (C)";
            CurrentWindowState = System.Windows.WindowState.Normal;
            ResizeMode = System.Windows.ResizeMode.CanResize;

            oilButton1.IsEnabled = false;
            oilButton2.OnButtonClick += new MouseButtonEventHandler(oilButton2_OnButtonClick);

            if (OilMessageBox.Show("Are you sure you wish to exit the application at this time?", "Exit?", OilMessageBox.IconType.Question, OilMessageBox.ResponseType.YesNo) == true)
            {
                OilMessageBox.Show("Success");
            }
            else
            {
                OilMessageBox.Show("FAILED", "", OilMessageBox.IconType.Error);
            }
        }

        void oilButton2_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            oilButton1.IsEnabled = !oilButton1.IsEnabled;
        }

        private void oilButton1_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (oilButton1.Focus())
            {
                MessageBox.Show("!");
            }
        }

        private void TestMethod(object sender)
        {

        }
    }
}
