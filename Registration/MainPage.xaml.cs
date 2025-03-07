﻿using System.Threading.Tasks;

namespace Registration
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService objLocalDb;
        private int _Id;
        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            objLocalDb = dbService;

            Task.Run(async () => lstCustomer.ItemsSource = await objLocalDb.GetCustomers());
        }

        #region Search Work
        private async void lstCustomer_ItemTrapped(object sender, ItemTappedEventArgs e)
        {
            var masterId = (Customer)e.Item;
            await Navigation.PushAsync(new IndiTrnsPage(masterId.Id));
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == null || txtSearch.Text.Trim() == "")
            {
                lstCustomer.ItemsSource = await objLocalDb.GetCustomers();
            }
            else
            {
                lstCustomer.ItemsSource = await objLocalDb.GetCustomersByName(txtSearch.Text);
            }
        }
        #endregion

    }

}