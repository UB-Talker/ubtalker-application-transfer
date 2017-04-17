using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UBTalker.Controls;
using UBTalker.Services;

namespace UBTalker.Views
{
    /// <summary>
    /// Interaction logic for TVControlView.xaml
    /// </summary>
    public partial class RoomControlView : Page
    {
        private readonly IMainWindow _parent = ServiceLocator.Current.GetInstance<IMainWindow>();
        private readonly IDataStoreService _store = ServiceLocator.Current.GetInstance<IDataStoreService>();

        private List<GazeButtonData[]> MainMenu = new List<GazeButtonData[]> { new GazeButtonData[] {
            new GazeButtonData("TV Control", "cmd:tv_control"),
            new GazeButtonData("Fan On/Off", "cmd:fan_on_off"),
            new GazeButtonData("Light On/Off", "cmd:light_on_off"),
            new GazeButtonData("")
        } };
        private List<GazeButtonData[]> TV_Menu;

        public RoomControlView()
        {
            InitializeComponent();
            TV_Menu = new List<GazeButtonData[]> { new GazeButtonData[] {
                new GazeButtonData("On/Off", "cmd:on_off"),
                new GazeButtonData("Channel Up", "cmd:channel_up"),
                new GazeButtonData("Channel Down", "cmd:channel_down"),
                new GazeButtonData("Volume Up", "cmd:volume_up")

            }, new GazeButtonData[] {
                new GazeButtonData("Volume Down", "cmd:volume_down"),
                new GazeButtonData(""),
                new GazeButtonData(""),
                new GazeButtonData("")
            } };
            PagedMenu.Options = MainMenu;
        }

        private void PagedMenu_GazeSelect(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GazeButton)
            {
                var button = e.OriginalSource as GazeButton;

                if (button.Link != null)
                {
                    switch (button.Link)
                    {
                        case "cmd:tv_control":
                            PagedMenu.Options = TV_Menu;
                            break;
                        case "cmd:fan_on_off":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:light_on_off":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:on_off":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:channel_up":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:channel_down":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:volume_up":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        case "cmd:volume_down":
                            System.Console.WriteLine("Selected " + button.Text + ", link = " + button.Link);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void GoBack_GazeSelect(object sender, RoutedEventArgs e)
        {
            // TODO: Maybe delete?
            if(PagedMenu.Options == TV_Menu)
            {
                PagedMenu.Options = MainMenu;
            }
            else
            {
                _parent.GoBack();
            }
        }
    }
}