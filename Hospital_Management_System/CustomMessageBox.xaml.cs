using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hospital_MamSys_GUI
{
    public partial class CustomMessageBox : Window
    {
        public enum MessageBoxButton
        {
            OK,
            YesNo
        }

        public enum MessageBoxIcon
        {
            Information,
            Question,
            Warning,
            Error
        }

        public enum MessageBoxResult
        {
            None,
            OK,
            Yes,
            No
        }

        public MessageBoxResult Result { get; private set; }

        private CustomMessageBox(string message, string title, MessageBoxButton buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            
            this.Title = title;
            txtMessage.Text = message;
            SetIcon(icon);
            CreateButtons(buttons);
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    // Use system information icon
                    imgIcon.Source = System.Drawing.SystemIcons.Information.ToImageSource();
                    break;
                case MessageBoxIcon.Question:
                    imgIcon.Source = System.Drawing.SystemIcons.Question.ToImageSource();
                    break;
                case MessageBoxIcon.Warning:
                    imgIcon.Source = System.Drawing.SystemIcons.Warning.ToImageSource();
                    break;
                case MessageBoxIcon.Error:
                    imgIcon.Source = System.Drawing.SystemIcons.Error.ToImageSource();
                    break;
            }
        }

        private void CreateButtons(MessageBoxButton buttons)
        {
            buttonPanel.Children.Clear();

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    Button btnOK = CreateButton("OK", MessageBoxResult.OK);
                    buttonPanel.Children.Add(btnOK);
                    btnOK.Focus();
                    break;

                case MessageBoxButton.YesNo:
                    Button btnYes = CreateButton("Yes", MessageBoxResult.Yes);
                    Button btnNo = CreateButton("No", MessageBoxResult.No);
                    buttonPanel.Children.Add(btnYes);
                    buttonPanel.Children.Add(btnNo);
                    btnYes.Focus();
                    break;
            }
        }

        private Button CreateButton(string content, MessageBoxResult result)
        {
            Button button = new Button
            {
                Content = content,
                Width = 80,
                Height = 32,
                Margin = new Thickness(5, 0, 5, 0),
                FontSize = 13,
                Cursor = System.Windows.Input.Cursors.Hand
            };

            // Create and configure the style BEFORE assigning it to the button
            Style buttonStyle = new Style(typeof(Button));
            
            // Set default properties
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A90E2"))));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(0)));
            
            // Create control template for rounded corners
            var template = new ControlTemplate(typeof(Button));
            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(4));
            borderFactory.SetValue(Border.PaddingProperty, new TemplateBindingExtension(Button.PaddingProperty));
            
            var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            
            borderFactory.AppendChild(contentPresenterFactory);
            template.VisualTree = borderFactory;
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, template));
            
            // Add hover effect trigger
            var trigger = new Trigger { Property = Button.IsMouseOverProperty, Value = true };
            trigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#357ABD"))));
            buttonStyle.Triggers.Add(trigger);
            
            // NOW assign the fully configured style to the button
            button.Style = buttonStyle;

            button.Click += (s, e) =>
            {
                Result = result;
                this.DialogResult = true;
                this.Close();
            };

            return button;
        }

        public static MessageBoxResult Show(string message, string title = "Information", 
            MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message, title, buttons, icon);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }

    // Extension method to convert Icon to ImageSource
    public static class IconExtensions
    {
        public static ImageSource ToImageSource(this System.Drawing.Icon icon)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}

