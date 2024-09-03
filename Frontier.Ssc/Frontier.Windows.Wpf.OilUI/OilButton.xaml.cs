using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Frontier.Library.Windows.Wpf.OilUI
{
    /// <summary>
    /// Represents a common button to use with applications that make use
    /// of the Oil UI library.
    /// </summary>
    public partial class OilButton : UserControl
    {
        public OilButton()
        {
            InitializeComponent();
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(OilButton));

        /// <summary>
        /// Gets or sets the text inside the button.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Fires when the control is actually clicked.
        /// </summary>
        public event MouseButtonEventHandler OnButtonClick = null;

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickedEvent = true;
            Focus();
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (clickedEvent)
            {
                if (OnButtonClick != null)
                {
                    OnButtonClick(sender, e);
                }                
            }
            clickedEvent = false;
        }

        private bool clickedEvent = false;
        private static readonly Effect focusedEffect = new DropShadowEffect()
        {
            ShadowDepth = 0,
            BlurRadius = 5,
            Color = Colors.White
        }; 

        private void oilButtonMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsFocused)
            {
                if (e.Key == Key.Enter || e.Key == Key.Space)
                {
                    // invoke the event
                    clickedEvent = true;
                    RaiseEvent(new MouseButtonEventArgs(
                        Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
                    {
                        RoutedEvent = UserControl.PreviewMouseLeftButtonDownEvent
                    });
                    RaiseEvent(new MouseButtonEventArgs(
                        Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
                        {                            
                            RoutedEvent = UserControl.PreviewMouseLeftButtonUpEvent
                        });
                }
            }
        }

        private void oilButtonMain_GotFocus(object sender, RoutedEventArgs e)
        {
            Effect = focusedEffect;
        }

        private void oilButtonMain_LostFocus(object sender, RoutedEventArgs e)
        {
            Effect = null;
        }

        private void oilButtonMain_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                // disabled
                Opacity = 0.5;
            }
            else
            {
                // enabled
                Opacity = 1.0;
            }
        }
    }
}
