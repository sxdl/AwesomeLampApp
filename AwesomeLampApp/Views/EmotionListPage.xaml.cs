namespace AwesomeLampApp.Views;

public partial class EmotionListPage : ContentPage
{
	public EmotionListPage()
	{
		InitializeComponent();
	}

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
}