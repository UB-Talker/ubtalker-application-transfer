using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;
using UBTalker.Services;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for EmailView.xaml
    /// </summary>
    public partial class EmailView : Page, IKeyboardReceiver
    {
        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();
        private readonly IDataStoreService _store = ServiceLocator.Current.GetInstance<IDataStoreService>();

        private static readonly string CURRENT_EMAIL_KEY = "view:email:currentemailtarget";

        private List<GazeButtonData[]> MainMenu = new List<GazeButtonData[]> { new GazeButtonData[] {
            new GazeButtonData("Send", "cmd:send"),
            new GazeButtonData("Receive", "cmd:receive"),
            new GazeButtonData(""),
            new GazeButtonData("")
        } };

        private List<GazeButtonData[]> ContactsMenu = new List<GazeButtonData[]> { new GazeButtonData[] {
            new GazeButtonData("Jeremy", "mailto:jmpetrot@buffalo.edu"),
            new GazeButtonData(""),
            new GazeButtonData(""),
            new GazeButtonData("")
        } };

        public EmailView()
        {
            InitializeComponent();
            PagedMenu.Options = MainMenu;
        }

        public void OnKeyboardCancel() {
            PagedMenu.Options = MainMenu;
        }

        public void OnKeyboardInput(string message)
        {
            string email = _store.Get(CURRENT_EMAIL_KEY) as string;
            System.Console.WriteLine("Send email to " + email + ", with message: " + message);
            PagedMenu.Options = MainMenu;
        }

        private void PagedMenu_GazeSelect(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GazeButton)
            {
                var button = e.OriginalSource as GazeButton;

                if (button.Link != null)
                {
                    // Handle sending
                    if (button.Link.StartsWith("mailto:"))
                    {
                        _store.Set(CURRENT_EMAIL_KEY, button.Link.Split(':')[1]);
                        _parent.ShowKeyboard();
                    }
                    // Handle menu commands
                    else
                    {
                        switch (button.Link)
                        {
                            case "cmd:send":
                                PagedMenu.Options = ContactsMenu;
                                break;
                            case "cmd:receive":
                                break;
                        }
                    }
                }
            }
        }

        private void GoBack_GazeSelect(object sender, RoutedEventArgs e)
        {
            _parent.GoBack();
        }
    }
}
