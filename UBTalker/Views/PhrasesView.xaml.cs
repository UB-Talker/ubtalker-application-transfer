using Microsoft.Practices.ServiceLocation;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;
using UBTalker.Services;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for PhrasesView.xaml
    /// </summary>
    public partial class PhrasesView : Page
    {

        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();
        private readonly IPhraseService _phraseService = ServiceLocator.Current.GetInstance<IPhraseService>();

        public PhrasesView()
        {
            InitializeComponent();

            // Set the options by mapping each string page to a GazeButtonData page
            PagedMenu.Options = _phraseService.GetPhrases()
                .Select(item => GazeButtonData.FromStringArray(item))
                .ToList();
        }

        private void PagedGazeMenu_GazeSelect(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GazeButton)
            {
                var button = e.OriginalSource as GazeButton;
                System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                _parent.ShowModal(button.Text);
                _phraseService.Speak(button.Text);
            }
        }
    }
}
