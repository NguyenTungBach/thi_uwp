using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Thi_UWP.Entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Thi_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<Contact> listContact = Server.DataInitialization.ListContact();
            ListDataGridCreate.ItemsSource = listContact;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var contact = new Contact()
            {
                Name = name.Text,
                Phone = phone.Text,
            };
            ContentDialog contentDialog = new ContentDialog();
            if (Server.DataInitialization.IsInsertContact(contact))
            {
                contentDialog.Title = "Acction success";
                contentDialog.Content = "Them thanh cong";
            }
            else
            {
                contentDialog.Title = "Acction fail";
                contentDialog.Content = "Them that bai";
            }
            contentDialog.CloseButtonText = "OK";
            await contentDialog.ShowAsync();
            List<Contact> listContact = Server.DataInitialization.ListContact();
            ListDataGridCreate.ItemsSource = listContact;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Contact> listContact = Server.DataInitialization.ListTransactionByName(searchName.Text);
                ListDataGridCreate.ItemsSource = listContact;
                if(listContact == null)
                {
                    ContentDialog contentDialog = new ContentDialog();
                    contentDialog.Title = "Acction success";
                    contentDialog.Content = "Contact not found";
                    contentDialog.CloseButtonText = "Oke";
                    await contentDialog.ShowAsync();
                }
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Lỗi!";
                contentDialog.Content = "Contact not found";
                contentDialog.CloseButtonText = "Oke";
                await contentDialog.ShowAsync();
            }
        }
    }
}
