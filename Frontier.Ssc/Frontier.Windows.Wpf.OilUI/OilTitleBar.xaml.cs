using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frontier.Library.Windows.Wpf.OilUI
{
    /// <summary>
    /// Represents a custom title bar for usage on the OilWindow
    /// </summary>
    public partial class OilTitleBar : UserControl
    {
        public OilTitleBar()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Gets or sets the window state which will determine the image that
        /// is displayed.
        /// </summary>
        public WindowState CurrentWindowState
        {
            get { return currentWindowState; }
            set
            {
                currentWindowState = value;
                if (value == WindowState.Normal)
                {
                    Image image = new Image();
                    image.Source = BitmapToSource(OilResources.window_maximize);
                    buttonMaxNormal.Content = image;
                }
                if (value == WindowState.Maximized)
                {
                    Image image = new Image();
                    image.Source = BitmapToSource(OilResources.window_normal);
                    buttonMaxNormal.Content = image;
                }
            }
        }
        private WindowState currentWindowState = WindowState.Normal;

        /// <summary>
        /// Gets or sets the image in the upper left hand corner.
        /// </summary>
        public ImageSource ApplicationIcon
        {
            get { return imageApplicationIcon.Source; }
            set { imageApplicationIcon.Source = value; }
        }

        /// <summary>
        /// Gets or sets the text shown in the upper left hand corner.
        /// </summary>
        public string ApplicationTitle
        {
            get { return (string)labelApplicationName.Content; }
            set { labelApplicationName.Content = value; }
        }

        //
        // Events

        /// <summary>
        /// Fires when the user clicks on the close button in the top right corner.
        /// </summary>
        public event RoutedEventHandler OnButtonCloseClicked = null;

        /// <summary>
        /// Fires when the user clicks on the Maximize/Normalize in the top right corner.
        /// </summary>
        public event RoutedEventHandler OnButtonMaximizeNormalClicked = null;

        /// <summary>
        /// Fires when the minimize icon is clicked in the top right corner.  
        /// </summary>
        public event RoutedEventHandler OnButtonMinimizeClicked = null;

        public event MouseButtonEventHandler OnLeftMouseButtonDown = null;

        public event MouseButtonEventHandler OnLeftMouseButtonUp = null;

        public event MouseEventHandler OnMouseLeave = null;

        public event MouseEventHandler OnMouseMoved = null;

        public event MouseButtonEventHandler OnMouseDoubleClicked = null;
        
        //
        // Event Wrappers
        
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (OnButtonCloseClicked != null)
            {
                OnButtonCloseClicked(sender, e);
            }
        }

        private void buttonMaxNormal_Click(object sender, RoutedEventArgs e)
        {
            if (OnButtonMaximizeNormalClicked != null)
            {
                OnButtonMaximizeNormalClicked(sender, e);
            }
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            if (OnButtonMinimizeClicked != null)
            {
                OnButtonMinimizeClicked(sender, e);
            }
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (OnLeftMouseButtonDown != null)
            {
                OnLeftMouseButtonDown(sender, e);
            }
        }

        private void UserControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnLeftMouseButtonUp != null)
            {
                OnLeftMouseButtonUp(sender, e);
            }
        }

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (OnMouseMoved != null)
            {
                OnMouseMoved(sender, e);
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (OnMouseLeave != null)
            {
                OnMouseLeave(sender, e);
            }
        }
        
        private void UserControl_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OnMouseDoubleClicked != null)
            {
                OnMouseDoubleClicked(sender, e);
            }
        }

        //
        // Methods

        private ImageSource BitmapToSource(System.Drawing.Bitmap bitmap)
        {
            System.Windows.Media.Imaging.BitmapSource bitmapSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    System.IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
                );
            return bitmapSource;
        }

    }
}
