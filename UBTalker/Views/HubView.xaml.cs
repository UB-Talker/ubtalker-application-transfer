using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for HubView.xaml
    /// </summary>
    public partial class HubView : Page
    {
        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();

        public HubView()
        {
            InitializeComponent();
            Menu.Options = GetOptions();
        }

        private List<GazeButtonData[]> GetOptions()
        {
            List<GazeButtonData[]> options = new List<GazeButtonData[]>();
            options.Add(new GazeButtonData[] {
                new GazeButtonData("Phrases", "/Views/PhrasesView.xaml"),
                new GazeButtonData(""),
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
                    _parent.SetPage(new Uri(button.Link, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
