using Microsoft.Win32;
using System.Linq;
using System.Windows;

namespace WiFiTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "XML Files|*.xml|All Files|*.*||",
            };

            if (ofd.ShowDialog() == true)
            {
                ConnectAsync(ofd.FileName);
            }
        }

        private async void ConnectAsync(string filename)
        {
            if (await NetShHelper.GetWiFiProfileAsync(filename) is not WLANProfile profile)
            {
                MessageBox.Show($"Failed to get '{filename}'.");
                return;
            }

            var profileNames = await NetShHelper.GetWiFiProfilesAsync();
            if (profileNames.SingleOrDefault(x => x == profile.Name) is string)
            {
                var answer = MessageBox.Show(
                    $"{profile.Name} profile already exists. Would you like to connect?",
                    this.Title,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No);

                if (answer == MessageBoxResult.No)
                {
                    return;
                }
            }
            else
            {
                if (await NetShHelper.AddWiFiProfileAsync(filename) == false)
                {
                    MessageBox.Show($"Failed to add '{filename}'.");
                    return;
                }
            }

            if (await NetShHelper.ConnectWiFiProfileAsync(profile.Name) == false)
            {
                MessageBox.Show($"Failed to connect to '{profile.Name}'.");
                return;
            }

            MessageBox.Show($"Connected to '{profile.Name}'.");
        }
    }
}
