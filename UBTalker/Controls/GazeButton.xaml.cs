using System.Windows;
using System.Windows.Controls;
using EyeXFramework.Wpf;
using System.Windows.Input;
using System;

namespace UBTalker.Controls
{

    #region GazeButtonData

    public class GazeButtonData
    {
        public static GazeButtonData[] FromStringArray(string[] arr)
        {
            GazeButtonData[] data = new GazeButtonData[arr.Length];
            for (var i = 0; i < arr.Length; i++)
                data[i] = new GazeButtonData(arr[i]);

            return data;
        }

        public GazeButtonData(string text, string link)
        {
            Text = text;
            Link = link;
        }

        public GazeButtonData(string text)
        {
            Text = text;
            Link = null;
        }

        public string Text { get; set; }
        public string Link { get; set; }
    }

    #endregion

    /// <summary>
    /// Interaction logic for GazeButton.xaml
    /// </summary>
    public partial class GazeButton : UserControl
    {
        public static readonly int DEFAULT_FOCUS_DELAY = 350;
        public static readonly int DEFAULT_SELECTION_DELAY = 2000;

        #region WPF Properties

        public static readonly DependencyProperty FocusDelayProperty = DependencyProperty.Register("GazeFocusDelay", typeof(int), typeof(GazeButton), new FrameworkPropertyMetadata(DEFAULT_FOCUS_DELAY));
        public static readonly DependencyProperty SelectionDelayProperty = DependencyProperty.Register("GazeSelectionDelay", typeof(int), typeof(GazeButton), new FrameworkPropertyMetadata(DEFAULT_SELECTION_DELAY));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(GazeButton), new UIPropertyMetadata(null));
        public static readonly DependencyProperty LinkProperty = DependencyProperty.Register("Link", typeof(string), typeof(GazeButton), new UIPropertyMetadata(null));

        /// <summary>
        /// How long in milliseconds it takes for this control to become focused
        /// </summary>
        public int GazeFocusDelay
        {
            get { return (int)GetValue(FocusDelayProperty); }
            set { SetValue(FocusDelayProperty, value); }
        }

        /// <summary>
        /// How long in milliseconds it takes for this control to become selected
        /// This includes the GazeFocusDelay
        /// </summary>
        public int GazeSelectionDelay
        {
            get { return (int)GetValue(SelectionDelayProperty); }
            set { SetValue(SelectionDelayProperty, value); }
        }

        /// <summary>
        /// Text on this control
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Additional metadata used to determine what the button is meant for
        /// </summary>
        public string Link
        {
            get { return (string)GetValue(LinkProperty); }
            set { SetValue(LinkProperty, value); }
        }

        #endregion

        #region WPF Events

        public static readonly RoutedEvent GazeFocusEvent = EventManager.RegisterRoutedEvent("GazeFocus", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GazeButton));
        public static readonly RoutedEvent GazeSelectionEvent = EventManager.RegisterRoutedEvent("GazeSelect", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(GazeButton));

        public event RoutedEventHandler GazeFocus
        {
            add { AddHandler(GazeFocusEvent, value); }
            remove { RemoveHandler(GazeFocusEvent, value); }
        }

        public event RoutedEventHandler GazeSelect
        {
            add { AddHandler(GazeSelectionEvent, value); }
            remove { RemoveHandler(GazeSelectionEvent, value); }
        }

        #endregion

        // Data utility for quickly saving and setting properties
        public GazeButtonData Data
        {
            get { return new GazeButtonData(Text, Link); }
            set { Text = value.Text; Link = value.Link; }
        }

        public GazeButton()
        {
            InitializeComponent();
        }

        #region Events

        private void OnHasGazeChanged_Focus(object sender, RoutedEventArgs e)
        {
            var source = e.Source as Grid;
            if (source != null && source.GetHasGaze())
            {
                RaiseEvent(new RoutedEventArgs(GazeButton.GazeFocusEvent));
            }
        }

        private void OnHasGazeChanged_Select(object sender, RoutedEventArgs e)
        {
            var source = e.Source as Label;
            if (source != null && source.GetHasGaze())
            {
                RaiseEvent(new RoutedEventArgs(GazeButton.GazeSelectionEvent));
            }
        }

        #endregion

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            // Only raised if this project is built in Debug mode
            // Used for debugging without an Eye Gaze device
            // When built in Release mode, this will no longer propagate
            RaiseEvent(new RoutedEventArgs(GazeButton.GazeSelectionEvent));
#endif
        }
    }
}
