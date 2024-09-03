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
using System.Windows.Shapes;

namespace Frontier.Library.Windows.Wpf.OilUI
{
    /// <summary>
    /// Interaction logic for OilMessageBox.xaml
    /// </summary>
    public partial class OilMessageBox : Window
    {
        /// <summary>
        /// Can make this visible if they'd rather not use
        /// the public static Show methods.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <param name="responseType"></param>
        /// <param name="iconType"></param>
        public OilMessageBox(string text, string title, ResponseType responseType, IconType iconType)
        {
            InitializeComponent();

            // handle the title 
            if (!string.IsNullOrEmpty(title))
            {
                textBlockTitle.Text = title;
            }
            else
            {
                textBlockTitle.Text = string.Empty;
            }

            // display the text
            if (!string.IsNullOrEmpty(text))
            {
                textBlockMessage.Text = text;
            }
            else
            {
                textBlockMessage.Text = string.Empty;
            }

            // handle the response type
            if (responseType == ResponseType.Ok)
            {
                AddOkButton();
            }
            else if (responseType == ResponseType.OkCancel)
            {
                AddOkButton();
                AddCancelButton();
            }
            else if (responseType == ResponseType.YesNo)
            {
                AddYesButton();
                AddNoButton();
            }

            // handle the icon type
            if (iconType == IconType.Information)
            {
                imageIcon.Source = BitmapToSource(OilResources.mbicon_information);
            }
            else if (iconType == IconType.Error)
            {
                imageIcon.Source = BitmapToSource(OilResources.mbicon_error);
            }
            else if (iconType == IconType.Question)
            {
                imageIcon.Source = BitmapToSource(OilResources.mbicon_question);
            }
            else if (iconType == IconType.Warning)
            {
                imageIcon.Source = BitmapToSource(OilResources.mbicon_information);
            }
        }

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

        private void AddOkButton()
        {
            OilButton okButton = new OilButton();
            okButton.Text = "OK";
            okButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            okButton.Width = ButtonWidth;
            okButton.Height = ButtonHeight;
            okButton.OnButtonClick += new MouseButtonEventHandler(okButton_OnButtonClick);
            stackPanelButtons.Children.Add(okButton);
            okButton.Focus();
        }

        private void AddCancelButton()
        {
            OilButton cancelButton = new OilButton();
            cancelButton.Text = "Cancel";
            cancelButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            cancelButton.Width = ButtonWidth;
            cancelButton.Height = ButtonHeight;
            cancelButton.OnButtonClick += new MouseButtonEventHandler(cancelButton_OnButtonClick);
            stackPanelButtons.Children.Add(cancelButton);
        }

        private void AddYesButton()
        {
            OilButton yesButton = new OilButton();
            yesButton.Text = "Yes";
            yesButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            yesButton.Width = ButtonWidth;
            yesButton.Height = ButtonHeight;
            yesButton.OnButtonClick += new MouseButtonEventHandler(yesButton_OnButtonClick);
            stackPanelButtons.Children.Add(yesButton);
            yesButton.Focus();
        }

        private void AddNoButton()
        {
            OilButton noButton = new OilButton();
            noButton.Text = "No";
            noButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            noButton.Width = ButtonWidth;
            noButton.Height = ButtonHeight;
            noButton.OnButtonClick += new MouseButtonEventHandler(noButton_OnButtonClick);
            stackPanelButtons.Children.Add(noButton);
        }
        
        /// <summary>
        /// Displays a non-moveable, non-unfocusable message box with an ok button.
        /// Will return true if the user clicks OK.
        /// </summary>
        /// <param name="text">The text to display without a title.</param>
        /// <returns></returns>
        public static bool? Show(string text)
        {
            OilMessageBox msgBox = new OilMessageBox(text, null, ResponseType.Ok, IconType.Information);
            return msgBox.ShowDialog();
        }

        /// <summary>
        /// Displays a non-moveable, non-unfocusable message box with an ok button.
        /// Will return true if the user clicks OK.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool? Show(string text, string title)
        {
            OilMessageBox msgBox = new OilMessageBox(text, title, ResponseType.Ok, IconType.Information);
            return msgBox.ShowDialog();
        }

        /// <summary>
        /// Displays a non-moveable, non-unfocusable message box with an ok button.
        /// Will return true if the user clicks OK.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool? Show(string text, string title, IconType iconType)
        {
            OilMessageBox msgBox = new OilMessageBox(text, title, ResponseType.Ok, iconType);
            return msgBox.ShowDialog();
        }

        /// <summary>
        /// Displays a non-moveable, non-unfocusable message box with an ok button.
        /// Will return true if the user clicks OK or Yes.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool? Show(string text, string title, IconType iconType, ResponseType responseType)
        {
            OilMessageBox msgBox = new OilMessageBox(text, title, responseType, iconType);
            return msgBox.ShowDialog();
        }

        //
        // these are the dimensions of the buttons added at the bottom
        // inside the stack panel
        private static readonly int ButtonHeight = 27;
        private static readonly int ButtonWidth = 100;

        //
        // Button Event Handlers

        void noButton_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        void yesButton_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
        }

        void cancelButton_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult = false;
        }

        void okButton_OnButtonClick(object sender, MouseButtonEventArgs e)
        {
            // just send back the user acknowledged this
            DialogResult = true;
        }


        //
        // Enums

        /// <summary>
        /// Will determine the buttons that are added to the message box.
        /// </summary>
        public enum ResponseType
        {
            Ok, 
            OkCancel, 
            YesNo
        }

        /// <summary>
        /// Determines the type of icon shown to the right of the message box.
        /// </summary>
        public enum IconType
        {
            Error, 
            Warning, 
            Information,
            Question
        }
    }
}
