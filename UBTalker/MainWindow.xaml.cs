using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
using UBTalker.Services;

namespace UBTalker
{
    public interface IMainWindow : IKeyboardReceiver
    {
        void SetPage(Uri uri);

        void ShowKeyboard();

        void ShowModal(string message);

        string GetCurrentModalMessage();

        void GoBack();
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private readonly ICallLightService _callLightService = ServiceLocator.Current.GetInstance<ICallLightService>();
        private string currentModalMessage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCallLightSelected(object sender, RoutedEventArgs e)
        {
            _callLightService.ActivateCallLight();
        }

        #region IMainWindow Implementation

        public void SetPage(Uri uri)
        {
            CurrentView.Navigate(uri);
        }

        public void ShowKeyboard()
        {
            CurrentView.Navigate(new Uri("Views/KeyboardView.xaml", UriKind.Relative));
        }

        public void ShowModal(string message)
        {
            currentModalMessage = message;
            CurrentView.Navigate(new Uri("Views/ModalView.xaml", UriKind.Relative));
        }

        public string GetCurrentModalMessage()
        {
            return currentModalMessage;
        }

        public void GoBack()
        {
            CurrentView.GoBack();
        }

        #endregion

        #region IKeyboardReceiver Implementation

        public void OnKeyboardInput(string message)
        {
            // Go back to the previous page and propagate the result, if possible
            CurrentView.GoBack();
            if (CurrentView.Content is IKeyboardReceiver)
            {
                (CurrentView.Content as IKeyboardReceiver).OnKeyboardInput(message);
            }
        }

        public void OnKeyboardCancel()
        {
            // Go back to the previous page and propagate the result, if possible 
            CurrentView.GoBack();
            if (CurrentView.Content is IKeyboardReceiver)
            {
                (CurrentView.Content as IKeyboardReceiver).OnKeyboardCancel();
            }
        }

        #endregion
    }
}
