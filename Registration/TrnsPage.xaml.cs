using System.Threading.Tasks;

namespace Registration;

public partial class TrnsPage : ContentPage
{
    private readonly LocalDbService objLocalDb;
    public TrnsPage()
    {
        InitializeComponent();
        objLocalDb = new LocalDbService();
        Task.Run(async () => lstTransaction.ItemsSource = await objLocalDb.GetTransactions());
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (txtAmt.Text == null || txtAmt.Text == "")
        {
            txtAmt.Placeholder = "Amount is Blank";
            txtAmt.PlaceholderColor = Colors.Red;
            return;
        }

      
        string DBT_CDT = "";
        if (chkDbtCdt.IsChecked)
        {
            DBT_CDT = "Credit";
        }
        else
        {
            DBT_CDT = "Debit";
        }


        await objLocalDb.Create(new Transaction
        {
            Amt = Convert.ToDecimal(txtAmt.Text),
            //MstrID = Convert.ToInt32(txtMstrID.Text),
            DbtCdt = DBT_CDT,
            TrnsDt = TrnsDt.Date
        });

        DisplayAlert("Confirmation Message", "Your Transaction Has Been Saved Successfully :)", "Okay:)");

        txtAmt.Text = string.Empty;
        TrnsDt.Date = DateTime.Now;
        lstTransaction.ItemsSource = await objLocalDb.GetTransactions();
    }
}