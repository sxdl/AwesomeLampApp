using AwesomeLampApp.ViewModels;

namespace AwesomeLampApp.Views;

public partial class RoomPage : ContentPage
{
	public RoomPage()
	{
		InitializeComponent();
		this.BindingContext = new RoomViewModel();

    }
}