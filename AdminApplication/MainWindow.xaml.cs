using AdminApplication.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminApplication
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

        private void PasswordChangedHandler(Object sender, RoutedEventArgs args)
        {
            if ((Keyboard.GetKeyStates(Key.CapsLock) & KeyStates.Toggled) == KeyStates.Toggled)
            {
                if (PasswordText.ToolTip == null)
                {
                    ToolTip tt = new ToolTip();
                    tt.Content = "Warning: CapsLock is on";
                    tt.PlacementTarget = sender as UIElement;
                    tt.Placement = PlacementMode.Bottom;
                    PasswordText.ToolTip = tt;
                    tt.IsOpen = true;
                }
            }
            else
            {
                var currentToolTip = PasswordText.ToolTip as ToolTip;
                if (currentToolTip != null)
                {
                    currentToolTip.IsOpen = false;
                }

                PasswordText.ToolTip = null;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client.Login(UsernameText.Text, PasswordText.Password);
                AppWindow appWindow = new AppWindow();
                appWindow.Show();
                this.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                UsernameText.Text = "";
                PasswordText.Password = "";
                MessageBox.Show("Invalid username and password");
            }
        }
        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }


    }
}
