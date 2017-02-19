using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Controls;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for ModalView.xaml
    /// </summary>
    public partial class ModalView : Page
    {
        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();

        public ModalView()
        {
            InitializeComponent();
            Text.Text = _parent.GetCurrentModalMessage();
        }

        private void GazeButton_GazeSelect(object sender, RoutedEventArgs e)
        {
            _parent.GoBack();
        }
    }
}
