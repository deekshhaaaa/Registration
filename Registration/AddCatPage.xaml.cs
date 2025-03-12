using System.Threading.Tasks;

namespace Registration;

public partial class AddCatPage : ContentPage
{
    private LocalDbService objLocalDb;
    private Category objCat;
    private int _ID;
    public AddCatPage(int _trapID)
    {
        InitializeComponent();
        objLocalDb = new LocalDbService();
        objCat = new Category();
        _ID = _trapID;

        if (_ID != 0)
        {
            Task.Run(async () => objCat = await objLocalDb.getCategorybyID(_ID));
            Thread.Sleep(100);
            txtCatName.Text = objCat.CatName;

            this.Title = "Edit Category";
            btnDelete.IsVisible = true;
        }
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (txtCatName.Text == null || txtCatName.Text.Trim() == "")
        {
            txtCatName.Placeholder = "Category Name is Blank";
            txtCatName.PlaceholderColor = Colors.Red;
            return;
        }

        if (_ID == 0)
        {
            objCat= await objLocalDb.getCategoryByName(txtCatName.Text.ToUpper());
            if (objCat == null)
            {
                await objLocalDb.Create(new Category
                {
                    CatName = txtCatName.Text
                });
                DisplayAlert("Confirmation", "New Category has been added!", "Okay :)");
            }
            else
            {
                DisplayAlert("Error", "Category already exists!", "Okay :)");
            }
          
        }
        else
        {
            await objLocalDb.Update(new Category
            {
                CatID = _ID,
                CatName = txtCatName.Text
            });
            DisplayAlert("Updation", "Category has been updated", "Okay :)");
        }
    }
    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        bool action = await DisplayAlert("Confirmation", "Are you sure you want to delete this category?", "Yes", "No");
        if (action == true)
        {
            await objLocalDb.DeletebyID(_ID);
        }
    }
}