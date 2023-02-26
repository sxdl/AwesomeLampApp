namespace AwesomeLampApp.Views;

public partial class SceneListPage : ContentPage
{
	public SceneListPage()
	{
		InitializeComponent();
	}

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
}