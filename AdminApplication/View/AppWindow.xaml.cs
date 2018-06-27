using AdminApplication.Controller;
using AdminApplication.Model;
using AdminApplication.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace AdminApplication
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        #region properties

       
        private List<Account> agentAccounts;
        private List<Comment> comments;
        private Comment currentComment;
        private Dictionary<string, string> registriesDictionary;

        private DateTime lastTime;


        #endregion




        public AppWindow()
        {
            
            agentAccounts = new List<Account>();
            lastTime = DateTime.Now;
            
            InitializeComponent();
           
            try
            {
                reloadAgents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                reloadComments();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                reloadProfanities();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            registriesDictionary = new Dictionary<string, string>();
            registriesDictionary.Add("Lodging", "lodging");
            registriesDictionary.Add("Feature", "feature");
            registriesDictionary.Add("Food", "food");
            
        }
        public void AgentAdded()
        {
            reloadAgents();
        }
        private void reloadProfanities()
        {
            ProfanityListBox.Items.Clear();
            List<String> profanities = Client.GetProfanities();

            foreach (var item in profanities)
            {
                ProfanityListBox.Items.Add(item);
            }
            
        }

        private void reloadComments()
        {
            comments = Client.GetComments();

            currentComment = comments[0];
            CommentContentTextBlock.Text = currentComment.Content;
            CommentNumberLabel.Content = comments.Count.ToString();
        }

        private void reloadAgents()
        {
            AgentGrid.Items.Clear();
            List<Account> agentAccounts = Client.GetAccounts();
            foreach (var item in agentAccounts)
            {
                if (PassesFilter(item))
                {
                    AgentGrid.Items.Add(new AccountTableItem(item));
                }
            }
        }

        private bool PassesFilter(Account item)
        {
           
            string accountType = "All";
            if (AccountType.Text.Equals("Visitor"))
            {
                accountType = "VISITOR";
            }
            else if (AccountType.Text.Equals("Agent"))
            {
                accountType = "AGENT";
            }
            else if (AccountType.Text.Equals("Admin"))
            {
                accountType = "ADMIN";
            }
            string email = EmailTextBox.Text;
            if(email==null)
            {
                email = "";
            }

            if (((AccountState.Text.Equals("Inactive") && !item.IsActive) || (AccountState.Text.Equals("Active") && item.IsActive) || (AccountState.Text.Equals("Banned") && item.IsBanned) || (AccountState.Text.Equals("ALL"))) && (accountType.Equals(item.UserType) || accountType.Equals("All")) && (item.Email.Contains(email))) 
            {
                return true;
            }





            return false;
           
        }

       

        private void AgentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkTime();
            try
            {
                AccountTableItem agentTableItem = AgentGrid.SelectedItem as AccountTableItem;
                if (agentTableItem.Active.Equals("Yes"))
                {
                    ActivationButton.Content = "Deactivate";
                    ActivationButton.IsEnabled = true;
                }
                else
                {
                    ActivationButton.Content = "Activate";
                    ActivationButton.IsEnabled = true;
                }

                if (agentTableItem.Banned.Equals("Yes"))
                {
                    BanButton.Content = "Unban";
                    BanButton.IsEnabled = true;
                }
                else
                {
                    BanButton.Content = "Ban";
                    BanButton.IsEnabled = true;
                }
            }catch(Exception ex)
            {

            }
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            AddAgent addAgent = new AddAgent(this);
            addAgent.ShowDialog();
        }

        private void ActivationButton_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            try
            {
                AccountTableItem agentTableItem = AgentGrid.SelectedItem as AccountTableItem;
                Account temp = new Account();
                temp.Id = agentTableItem.Id;
                if(agentTableItem.Banned.Equals("Yes"))
                {
                    temp.Banned = "true";
                }else
                {
                    temp.Banned = "false";
                }
                if(agentTableItem.Active.Equals("Yes") && ActivationButton.Content.Equals("Deactivate"))
                {
                    //deactivate;
                    temp.Active = "false";
                }else if (agentTableItem.Active.Equals("No") && ActivationButton.Content.Equals("Activate"))
                {
                    //activate
                    temp.Active = "true";
                }
                Client.UpdateAccount(temp);
                reloadAgents();
            }
            catch(Exception ex)
            {
            }
        }

        private void BanButton_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            try
            {
                AccountTableItem agentTableItem = AgentGrid.SelectedItem as AccountTableItem;
                Account temp = new Account();
                temp.Id = agentTableItem.Id;
                if (agentTableItem.Active.Equals("Yes"))
                {
                    temp.Active = "true";
                }
                else
                {
                    temp.Active = "false";
                }
                if (agentTableItem.Banned.Equals("Yes") && BanButton.Content.Equals("Unban"))
                {
                    //unban;
                    temp.Banned = "false";
                }
                else if (agentTableItem.Banned.Equals("No") && BanButton.Content.Equals("Ban"))
                {
                    //ban
                    temp.Banned = "true";
                }
                Client.UpdateAccount(temp);
                reloadAgents();
            }
            catch (Exception ex)
            {
            }
        }


        private void checkTime()
        {
            var currentTime = DateTime.Now;
            if(currentTime.Subtract(lastTime).TotalMinutes > 1)
            {
                MessageBox.Show("Your session has expired!");
                this.Close();

            }
            lastTime = currentTime;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            AgentGrid.Items.Clear();
            reloadAgents();

        }

        


        private void AddRegistyItemButton_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            string registry = RegistryNameCheckbox.Text.ToLower();
            AddRegistryItem addRegistryItem = new AddRegistryItem(this, registry);
           
            addRegistryItem.ShowDialog();
        }

        private void RegistryItemActivation_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            RegistryTableItem registryTableItem=null;
            try
            {
                 registryTableItem = new RegistryTableItem(RegistryDataGrid.SelectedItem as RegistryTableItem);
            }
            catch (Exception ex)
            {
                return;
            }
            string registryName = registriesDictionary[RegistryNameCheckbox.Text];


            Client.UpdateRegistry(registryName, registryTableItem);
            LoadRegistryTableItems();
           
        }

        private void AddProfanity_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            if (ProfanityTextBox.Text != null && !ProfanityTextBox.Text.Trim().Equals(""))
            {
                Client.AddProfanity(ProfanityTextBox.Text.Trim());
                ProfanityTextBox.Text = "";
            }
            reloadProfanities();
        }

        private void RemoveProfanities_Click(object sender, RoutedEventArgs e)
        {
            checkTime();
            Client.RemoveProfanity(ProfanityListBox.SelectedItem.ToString());
            reloadProfanities();
        }

        private void RegistryName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            


            LoadRegistryTableItems();
            


        }
        public void LoadRegistryTableItems()
        {
            checkTime();
            RegistryDataGrid.Items.Clear();
            
            string text = (RegistryNameCheckbox.SelectedItem as ComboBoxItem).Content as string;
            string registryName = registriesDictionary[text];
            
            List<RegistryTableItem> registryTableItems = Client.GetRegistryItems(registryName);
            foreach (var item in registryTableItems)
            {
                
                RegistryDataGrid.Items.Add(new RegistryTableItem(item));
            }
        }
        
        private void RegistryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkTime();
            //RegistryItemActivationButton.IsEnabled = true;
            try
            {
                string content = "Activate";
                RegistryTableItem registryTableItem = RegistryDataGrid.SelectedItem as RegistryTableItem;
                if(registryTableItem.Active.Equals("Yes"))
                {
                    content = "Deactivate";
                    
                }
                RegistryItemActivationButton.Content = content;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ApproveCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if(currentComment!=null)
            {
                approveComment("approved");
            }
           
        }

        private void DisapproveCommentButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (currentComment != null)
            {
                approveComment("disapproved");
            }
        }
        private void approveComment(String approval)
        {
            checkTime();
            try
            {
                Client.ApproveComment(approval, currentComment);

                comments.RemoveAt(0);
                if (comments.Count != 0)
                {
                    currentComment = comments[0];
                    CommentContentTextBlock.Text = currentComment.Content;
                    
                }else
                {
                    currentComment = null;
                    CommentContentTextBlock.Text = "No more comments for approval.";
                }
                CommentNumberLabel.Content = comments.Count.ToString();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
