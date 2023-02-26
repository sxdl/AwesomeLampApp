using AwesomeLampApp.ViewModels;

namespace AwesomeLampApp.Views;

public partial class MorePage : ContentPage
{
	public MorePage()
	{
		InitializeComponent();
        this.BindingContext = new MoreViewModel();
    }

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private void onItemtapped(object sender, ItemTappedEventArgs e)
	{
		int idx = e.ItemIndex;
		switch(idx)
		{
			case 0: Navigation.PushAsync(new FatigueListPage());break;
            case 1: Navigation.PushAsync(new SceneListPage()); break;
            case 2: Navigation.PushAsync(new VoiceListPage()); break;
            case 3: Navigation.PushAsync(new EmotionListPage()); break;
            case 4: Navigation.PushAsync(new TomatoListPage()); break;
            case 5: Navigation.PushAsync(new MoreSettingListPage()); break;
            default:break;
		}
	}
}