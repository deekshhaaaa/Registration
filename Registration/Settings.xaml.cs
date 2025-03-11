using System.Threading.Tasks;
using communication = Microsoft.Maui.ApplicationModel.Communication;

namespace Registration;

public partial class Settings : ContentPage
{
    private LocalDbService objLocalDb;

    public Settings()
    {
        InitializeComponent();
        objLocalDb = new LocalDbService();
    }

    private async void btnDelContacts_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Confirmation", "Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
        if (result == "DELETE")
        {
            await objLocalDb.DeleteAllCustomers();
            await Navigation.PopAsync();
            await DisplayAlert("Alert", "All contacts deleted", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Deletion cancelled", "OK");
        }
    }

    private async void btnDelTrns_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Confirmation", "Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
        if (result == "DELETE")
        {
            await objLocalDb.DeleteAllTrns();
            await Navigation.PopAsync();
            await DisplayAlert("Alert", "All transactions deleted", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Deletion cancelled", "OK");
        }
    }

    private async void btnDelData_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Confirmation", "Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
        if (result == "DELETE")
        {
            await objLocalDb.DeleteAllCustomers();
            await objLocalDb.DeleteAllTrns();
            await Navigation.PopAsync();
            await DisplayAlert("Alert", "All data deleted", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Deletion cancelled", "OK");
        }
    }

    private async void btnImpCont_Clicked(object sender, EventArgs e)
    {
        var contacts = await communication.Contacts.Default.GetAllAsync();
        Customer dbCustomer;
        int count = 0;
        foreach (var c in contacts)
        {
            dbCustomer = await objLocalDb.getCustomerbyNameAndNumber(c.DisplayName, c.Phones.FirstOrDefault()?.PhoneNumber);
            Thread.Sleep(100);
            if (dbCustomer == null)
            {
                await objLocalDb.Create(new Customer
                {
                    Name = c.DisplayName,
                    Mobile = c.Phones.FirstOrDefault()?.PhoneNumber,
                    Email = c.Emails.FirstOrDefault()?.EmailAddress,
                });
                count++;
            }
        }
        if(count == 0)
        {
            await DisplayAlert("Alert", "The contacts has already been imported", "OK");
        }
        else
        {
            await DisplayAlert("Alert", count + " contact(s) imported", "OK");
        }
    }
}