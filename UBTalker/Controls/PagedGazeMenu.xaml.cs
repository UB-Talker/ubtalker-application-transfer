using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace UBTalker.Controls
{
    /// <summary>
    /// Interaction logic for PagedGazeMenu.xaml
    /// </summary>
    public partial class PagedGazeMenu : UserControl
    {
        #region WPF Events

        public static readonly RoutedEvent GazeSelectionEvent = EventManager.RegisterRoutedEvent("GazeSelect", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PagedGazeMenu));

        public event RoutedEventHandler GazeSelect
        {
            add { AddHandler(GazeSelectionEvent, value); }
            remove { RemoveHandler(GazeSelectionEvent, value); }
        }

        #endregion

        public List<GazeButtonData[]> Options {
            get { return _options; }
            set { _options = value; Refresh(); }
        }

        private List<GazeButtonData[]> _options;
        private int _index = 0;

        public PagedGazeMenu()
        {
            InitializeComponent();
            Options = new List<GazeButtonData[]>();
        }

        /// <summary>
        /// Force the current selection of options onto the current view
        /// Also performs error-checking and wraparound for index
        /// </summary>
        private void Refresh()
        {
            if (_index >= Options.Count)
                _index = 0;

            if (_index < 0)
                _index = Options.Count - 1;

            if (Options.Count == 0)
                return;

            Option1.Data = Options[_index][0];
            Option2.Data = Options[_index][1];
            Option3.Data = Options[_index][2];
            Option4.Data = Options[_index][3];
        }

        #region Event Handlers

        private void Option_GazeSelect(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(GazeSelectionEvent, e.Source));
        }

        private void Back_GazeSelect(object sender, RoutedEventArgs e)
        {
            _index--;
            Refresh();
        }

        private void Forward_GazeSelect(object sender, RoutedEventArgs e)
        {
            _index++;
            Refresh();
        }

        #endregion
    }
}
