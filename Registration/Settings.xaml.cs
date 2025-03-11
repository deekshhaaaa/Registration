using System.Threading.Tasks;

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
        string result = await DisplayPromptAsync("Confirmation","Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
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
}