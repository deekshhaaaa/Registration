using System.Threading.Tasks;

namespace Registration;

public partial class AddCstmrPage : ContentPage
{
    private LocalDbService objLocalDb;
    private int _ID;
    public AddCstmrPage(LocalDbService dbService)
    {
        InitializeComponent();
        objLocalDb = dbService;
        Task.Run(async () => lstCustomer.ItemsSource = await objLocalDb.GetCustomers());
    }
    #region Save Work
    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (txtName.Text == null || txtName.Text.Trim() == "")
        {
            txtName.Placeholder = "Name is Blank";
            txtName.PlaceholderColor = Colors.Red;
            return;
        }

        if (_ID == 0)
        {
            await objLocalDb.Create(new Customer
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Mobile = txtMobile.Text,
                Address = txtAddress.Text
            });
            DisplayAlert("Confirmation", "New contact has been added!", "Okay :)");
        }
        else
        {
            await objLocalDb.Update(new Customer
            {
                Id = _ID,
                Name = txtName.Text,
                Email = txtEmail.Text,
                Mobile = txtMobile.Text,
                Address = txtAddress.Text
            });
            DisplayAlert("Updation", txtName.Text + "'s contact has been updated", "Okay :)");
        }
        txtName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtEmail.Text = string.Empty;
        _ID = 0;

        txtName.Placeholder = "Name";
        txtName.PlaceholderColor = Colors.Gray;

        lstCustomer.ItemsSource = await objLocalDb.GetCustomers();
    }
    #endregion

    private async void lstCustomer_ItemTrapped(object sender, ItemTappedEventArgs e)
    {
        var singleCustomer = (Customer)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action)
        {
            case "Edit":
                _ID = singleCustomer.Id;
                txtName.Text = singleCustomer.Name;
                txtMobile.Text = singleCustomer.Mobile;
                txtAddress.Text = singleCustomer.Address;
                txtEmail.Text = singleCustomer.Email;
                break;

            case "Delete":
                await objLocalDb.Delete(singleCustomer);
                lstCustomer.ItemsSource = await objLocalDb.GetCustomers();
                break;
        }
    }
}