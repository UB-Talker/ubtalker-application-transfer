using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for KeyboardView.xaml
    /// </summary>
    public partial class KeyboardView : Page
    {
        private readonly IKeyboardReceiver _parent = ServiceLocator.Current.GetInstance<IMainWindow>();
        private List<GazeButtonData[]> Alphabet_Menu;
        private List<GazeButtonData[]> Symbols_Menu;
        private List<GazeButtonData[]> Menu = new List<GazeButtonData[]> { new GazeButtonData[] {
            new GazeButtonData("A-Z", "cmd:alphabet"),
            new GazeButtonData("0-9, .,!?_", "cmd:symbols"),
            new GazeButtonData("Ok", "cmd:ok"),
            new GazeButtonData("Cancel", "cmd:cancel")
        } };

        public KeyboardView()
        {
            InitializeComponent();

            Alphabet_Menu = new List<GazeButtonData[]>();
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "a", "b", "c", "d" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "e", "f", "g", "h" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "i", "j", "k", "l" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "m", "n", "o", "p" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "q", "r", "s", "t" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "u", "v", "w", "x" }));
            Alphabet_Menu.Add(GazeButtonData.FromStringArray(new string[] { "y", "z", "", "" }));

            Symbols_Menu = new List<GazeButtonData[]>();
            Symbols_Menu.Add(GazeButtonData.FromStringArray(new string[] { "0", "1", "2", "3" }));
            Symbols_Menu.Add(GazeButtonData.FromStringArray(new string[] { "4", "5", "6", "7" }));
            Symbols_Menu.Add(GazeButtonData.FromStringArray(new string[] { "8", "9", ".", "," }));
            Symbols_Menu.Add(GazeButtonData.FromStringArray(new string[] { "!", "?", "(space)", "(backspace)" }));

            PagedMenu.Options = Menu;
        }

        private void PagedGazeMenu_GazeSelect(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GazeButton)
            {
                var button = e.OriginalSource as GazeButton;

                if (button.Link != null)
                {
                    switch (button.Link)
                    {
                        case "cmd:alphabet":
                            PagedMenu.Options = Alphabet_Menu;
                            break;
                        case "cmd:symbols":
                            PagedMenu.Options = Symbols_Menu;
                            break;
                        case "cmd:ok":
                            _parent.OnKeyboardInput(CurrentText.Text);
                            break;
                        case "cmd:cancel":
                            _parent.OnKeyboardCancel();
                            break;
                    }
                }
                else
                {
                    switch (button.Text)
                    {
                        case "(space)":
                            CurrentText.Text += " ";
                            break;
                        case "(backspace)":
                            CurrentText.Text = CurrentText.Text.Remove(CurrentText.Text.Length - 1, 1);
                            break;
                        default:
                            CurrentText.Text += button.Text;
                            break;
                    }
                    PagedMenu.Options = Menu;
                }
            }
        }
    }
}
