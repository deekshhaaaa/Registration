using System.Threading.Tasks;

namespace Registration;

public partial class BtnTrnsPage : ContentPage
{
	private LocalDbService objdbService;
	private int masterID;
	private bool IsGave;
	public BtnTrnsPage(int _masterID, bool _IsGave)
	{
		InitializeComponent();
		
		masterID = _masterID;
		IsGave = _IsGave;
		objdbService = new LocalDbService();

        if (IsGave == true)
		{
			btnSave.Text = "You Gave";
			btnSave.BackgroundColor = Colors.IndianRed;
			this.Title = "You Gave";
		}
		else
		{
			btnSave.Text = "You Got";
			btnSave.BackgroundColor = Colors.DarkSeaGreen;
			this.Title = "You Got";
		}
	}
    private async void LoadCategories()
    {
		await objdbService.Create(new Category
		{
			CatName = "Investment",
		});
		await objdbService.Create(new Category
		{
			CatName = "Sales",
		});
		await objdbService.Create(new Category
		{
			CatName = "Food",
		});
		await objdbService.Create(new Category
		{
			CatName = "Salary",
		});
        pkrCategory.ItemsSource = await objdbService.GetCategories();
    }
    private async void btnSave_Clicked(object sender, EventArgs e)
    {
		string dbt_cdt;
		decimal amount = 0;
		if (IsGave == true)
		{
			dbt_cdt = "D";
			amount = -Convert.ToDecimal(txtAmt.Text);
		}
		else
		{
			dbt_cdt = "C";
			amount = Convert.ToDecimal(txtAmt.Text);
		}
		await objdbService.Create(new Transaction
		{
			MstrID = masterID,
			DbtCdt = dbt_cdt,
			Amt = amount,
			TrnsDt = dtTrns.Date
		});
		txtAmt.Text = string.Empty;
		dtTrns.Date = DateTime.Now;
	}
}