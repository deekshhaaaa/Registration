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
        Transaction objTransaction = new Transaction();
        Task.Run(async() => objCustomer = await objlocaldb.getCustomerbyId(masterId));
        Task.Run(async () => lstTrns.ItemsSource = await objlocaldb.getTransactionByMstrID(masterId));

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

    private async void lstTrns_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var objTrns = (Transaction)e.Item;
        if (objTrns == null)
        {
            Thread.Sleep(100);
        }
        if (objTrns != null)
        {
            await Navigation.PushAsync(new SingleAmt(objTrns.TrnsID));
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () => lstTrns.ItemsSource = await objlocaldb.getTransactionByMstrID(masterId));
    }
}