namespace AwesomeLampApp.Views;

public partial class VoiceListPage : ContentPage
{
	public VoiceListPage()
	{
		InitializeComponent();
	}

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
}