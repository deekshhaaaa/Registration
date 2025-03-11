using System.Threading.Tasks;

namespace Registration
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService objLocalDb;
        private int _Id;
        List<Customer> customerArr = new List<Customer>();
        List<Customer> customerSearch = new List<Customer>();
        decimal Amt;
        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            objLocalDb = dbService;

            Task.Run(async () => customerArr = await objLocalDb.GetCustomers());
            Task.Run(async () => customerSearch = await objLocalDb.GetCustomersByName(txtSearch.Text ?? string.Empty));
            Task.Run(async () => Amt = await objLocalDb.getBalanceAmt());

            if (customerArr.Count == 0 || customerSearch.Count == 0)
            {
                Thread.Sleep(100);
            }

            for (int i = 0; i < customerArr.Count; i++)
            {
                Task.Run(async () => customerArr[i].Amount = await objLocalDb.getBalanceAmtbyID(customerArr[i].Id));
                Thread.Sleep(100);
            }
            for (int j = 0; j < customerSearch.Count; j++)
            {
                Task.Run(async () => customerSearch[j].Amount = await objLocalDb.getBalanceAmtbyID(customerSearch[j].Id));
                Thread.Sleep(100);
            }

            lstCustomer.ItemsSource = customerArr;
            
            if (Amt == 0)
            {
                Thread.Sleep(100);
            }
            lblAmt.Text = Amt.ToString();
        }

        #region Search Work
        private async void lstCustomer_ItemTrapped(object sender, ItemTappedEventArgs e)
        {
            var masterId = (Customer)e.Item;
            await Navigation.PushAsync(new IndiTrnsPage(masterId.Id));
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == null || txtSearch.Text.Trim() == "")
            {
                lstCustomer.ItemsSource = customerArr;
            }
            else
            {
                lstCustomer.ItemsSource = customerSearch;
            }
        }
        #endregion

        private async void btnAddPpl_Clicked(object sender, EventArgs e)
        {    
            await Navigation.PushAsync(new AddCstmrPage(objLocalDb));
        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    lstCustomer.ItemsSource = customerArr;
        //}
    }

}