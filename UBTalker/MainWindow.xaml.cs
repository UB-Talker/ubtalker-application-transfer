using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        private string lastKeyboardResult;
        private bool returningFromKeyboard;
        private bool returningFromModal;

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
            returningFromModal = true;
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
            lastKeyboardResult = message;
            returningFromKeyboard = true;
            GoBack();
        }

        public void OnKeyboardCancel()
        {
            lastKeyboardResult = null;
            returningFromKeyboard = true;
            GoBack();
        }

        #endregion

        private void CurrentView_Navigated(object sender, NavigationEventArgs e)
        {
            if (returningFromKeyboard)
            {
                returningFromKeyboard = false;

                if (CurrentView.Content is IKeyboardReceiver)
                {
                    if (lastKeyboardResult != null)
                        (CurrentView.Content as IKeyboardReceiver).OnKeyboardInput(lastKeyboardResult);
                    else
                        (CurrentView.Content as IKeyboardReceiver).OnKeyboardCancel();
                }
            }
            else if (returningFromModal)
            {
                returningFromModal = false;
            }
        }
    }
}
