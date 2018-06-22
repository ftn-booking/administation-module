using AdminApplication.Controller;
using AdminApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminApplication.View
{
    /// <summary>
    /// Interaction logic for AddAgent.xaml
    /// </summary>
    public partial class AddAgent : Window
    {
        private AppWindow parent;
        public AddAgent(AppWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            NewAccountDTO newAccountDTO = new NewAccountDTO();


            newAccountDTO.Email = EmailTextBox.Text;
            newAccountDTO.Name = NameTextBox.Text;
            newAccountDTO.Lastname = LastnameTextBox.Text;
            newAccountDTO.City = CityTextBox.Text;
            newAccountDTO.Phone = PhoneTextBox.Text;
            newAccountDTO.Pid = PIDTextBox.Text;
            
            try
            {
                Client.AddAccount(newAccountDTO);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't add agent");
                Console.WriteLine(ex.ToString());
            }
            parent.AgentAdded();
            this.Close();

        }
    }
}
