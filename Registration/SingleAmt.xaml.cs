using System.Threading.Tasks;

namespace Registration;

public partial class SingleAmt : ContentPage
{
	private LocalDbService objLocalDb;
    private int TrnsID;
    public SingleAmt(int _TrnsID)
	{
		InitializeComponent();
        TrnsID = _TrnsID;
        objLocalDb = new LocalDbService();
        Transaction objTransaction = new Transaction();
        Task.Run(async () => objTransaction = await objLocalDb.getTransactionbyID(TrnsID));

        Customer objCustomer = new Customer();
        Task.Run(async () => objCustomer = await objLocalDb.getCustomerbyId(objTransaction.MstrID));

        if (objTransaction.Amt < 0)
        {
            lblDbtCdt.Text = "You Gave";
            lblDbtCdt.TextColor = Colors.IndianRed;
        }
        else
        {
            lblDbtCdt.Text = "You Got";
            lblDbtCdt.TextColor = Colors.DarkSeaGreen;
        }
        lblDt.Text = objTransaction.TrnsDt.ToString();
        lblAmt.Text = objTransaction.Amt.ToString();
        if (objLocalDb.getBalanceAmt().ToString() == null)
        {
            Thread.Sleep(100);
        }
        lblBal.Text = objLocalDb.getBalanceAmt().ToString();
    }

    private void btnEdit_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alert", "This feature is not available in this version", "OK");
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        var action = await DisplayAlert("Confirmation", "Are you sure you want to delete this entry?", "Yes", "No");

        switch (action)
        {
            case true:
                await objLocalDb.DeleteTransaction(TrnsID);
                await Navigation.PopAsync();
                break;
            case false:
                break;
        }
    }
}