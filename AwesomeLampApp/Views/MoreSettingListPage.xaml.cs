namespace AwesomeLampApp.Views;

public partial class MoreSettingListPage : ContentPage
{
	public MoreSettingListPage()
	{
		InitializeComponent();
	}

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
}