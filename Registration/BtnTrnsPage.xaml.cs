namespace Registration;

public partial class BtnTrnsPage : ContentPage
{
	private int masterID;
	private bool IsGave;
	public BtnTrnsPage(int _masterID, bool _IsGave)
	{
		InitializeComponent();
		
		masterID = _masterID;
		IsGave = _IsGave;

		if (IsGave == true)
		{
			btnSave.BackgroundColor = Colors.IndianRed;
		}
		else
		{
			btnSave.BackgroundColor = Colors.DarkSeaGreen;
		}
	}
}