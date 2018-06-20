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
    /// Interaction logic for AddRegistryItem.xaml
    /// </summary>
    public partial class AddRegistryItem : Window
    {
        private string registry;
        private AppWindow parent;

        public AddRegistryItem(AppWindow parent,string registry)
        {

            this.parent = parent;
            this.registry = registry;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            RegistryTableItem registryTableItem = new RegistryTableItem();
            registryTableItem.Name = NewItemTextBox.Text;
            registryTableItem.Active = "true";
            try
            {
                Client.AddToRegistry(registry, registryTableItem);
                parent.LoadRegistryTableItems();
                

                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("Invalid new input");
                Console.WriteLine(  ex.ToString());
            }
        }
    }
}
