using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
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

        private string currentModalMessage; // Modal view can request this
        private string lastKeyboardResult;  // Needs to be stored because keyboard result must be sent after frame loads, GoBack() is async
        private bool returningFromKeyboard; // Check for when the frame loads content, if the keyboard callback needs to be called
        private bool returningFromModal;    // Currently unused

        public MainWindow()
        {
            InitializeComponent();
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

        #region Internal Events

        /// <summary>
        /// Call light selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCallLightSelected(object sender, RoutedEventArgs e)
        {
            _callLightService.ActivateCallLight();
        }

        /// <summary>
        /// Called when the frame CurrentView finishes loading. At this point, events and callbacks on the
        /// child view can be properly called because it's fully loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentView_Navigated(object sender, NavigationEventArgs e)
        {
            // Current view is now loaded, if returning from keyboard,
            // manually propagage the keyboard result
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
            // Currently unused, modal information passing would go here
            else if (returningFromModal)
            {
                returningFromModal = false;
            }
        }

        #endregion
    }
}
