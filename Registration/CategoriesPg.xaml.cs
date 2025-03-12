namespace Registration;

public partial class CategoriesPg : ContentPage
{
    private LocalDbService objLocalDb;

    public CategoriesPg()
	{
		InitializeComponent();
        objLocalDb = new LocalDbService();
        Task.Run(async () => lstCategories.ItemsSource = await objLocalDb.GetCategories());
    }

    private void lstCategories_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var cat = (Category)e.Item;
        Navigation.PushAsync(new AddCatPage(cat.CatID));
    }

    private async void btnAddCat_Clicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new AddCatPage(0));
    }
}