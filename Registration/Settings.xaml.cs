using System.Threading.Tasks;

namespace Registration;

public partial class Settings : ContentPage
{
    private LocalDbService objLocalDb;

    public Settings()
	{
		//InitializeComponent();
        objLocalDb = new LocalDbService();
    }

    private async void btnDelContacts_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Confirmation", "Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
        if (result == "DELETE")
        {
            await objLocalDb.DeleteAllCustomers();
            await Navigation.PopAsync();
        }
    }

    private async void btnDelTrns_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Confirmation","Enter 'DELETE' to confirm", maxLength: 6, keyboard: Keyboard.Text);
        if (result == "DELETE")
        {
            await objLocalDb.DeleteAllTrns();
            await Navigation.PopAsync();
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
        }
    }
}