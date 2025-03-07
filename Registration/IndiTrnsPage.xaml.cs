using System.Threading.Tasks;

namespace Registration;

public partial class IndiTrnsPage : ContentPage
{
    private int masterId;
    private LocalDbService objlocaldb;
    public IndiTrnsPage(int _masterId)
    {
        InitializeComponent();

        masterId = _masterId;
        Customer objCustomer=new Customer();
        objlocaldb = new LocalDbService();
        Task.Run(async() => objCustomer = await objlocaldb.getCustomerbyId(masterId));

        if (objCustomer.Name == null)
        {
            Thread.Sleep(100);
        }
        this.Title = objCustomer.Name;
    }

    private async void btnGot_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BtnTrnsPage(masterId, false));
    }

    private async void btnGave_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BtnTrnsPage(masterId, true));
    }
}