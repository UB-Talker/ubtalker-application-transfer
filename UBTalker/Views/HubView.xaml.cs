using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;
using UBTalker.Services;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for HubView.xaml
    /// </summary>
    public partial class HubView : Page, IKeyboardReceiver
    {
        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();
        private readonly IPhraseService _phraseService = ServiceLocator.Current.GetInstance<IPhraseService>();

        public HubView()
        {
            InitializeComponent();
            Menu.Options = GetOptions();
        }

        public void OnKeyboardCancel() { }

        public void OnKeyboardInput(string message)
        {
            _parent.ShowModal(message);
        }

        private List<GazeButtonData[]> GetOptions()
        {
            List<GazeButtonData[]> options = new List<GazeButtonData[]>();
            options.Add(new GazeButtonData[] {
                new GazeButtonData("Phrases", "/Views/PhrasesView.xaml"),
                new GazeButtonData("Keyboard", "cmd:keyboard"),
                new GazeButtonData(""),
                new GazeButtonData("")
            });
            return options;
        }

        private void PagedGazeMenu_GazeSelect(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GazeButton)
            {
                var button = e.OriginalSource as GazeButton;

                if (button.Link != null)
                {
                    if (button.Link == "cmd:keyboard")
                        _parent.ShowKeyboard();
                    else
                        _parent.SetPage(new Uri(button.Link, UriKind.RelativeOrAbsolute));
                }
                
            }
        }
    }
}
