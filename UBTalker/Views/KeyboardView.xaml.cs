using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Controls;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for KeyboardView.xaml
    /// </summary>
    public partial class KeyboardView : Page
    {
        private readonly IKeyboardReceiver _parent = ServiceLocator.Current.GetInstance<IMainWindow>();

        public KeyboardView()
        {
            InitializeComponent();
        }

        private void GazeButton_GazeSelect_Cancel(object sender, RoutedEventArgs e)
        {
            _parent.OnKeyboardCancel();
        }

        private void GazeButton_GazeSelect_OK(object sender, RoutedEventArgs e)
        {
            _parent.OnKeyboardInput("OK");
        }
    }
}
