using AwesomeLampApp.ViewModels;

namespace AwesomeLampApp.Views;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();

		this.BindingContext = new FirstViewModel();
	}

	void OnSearchEntryTextChanged(object sender, EventArgs e)
	{
		if(SearchEntry.HorizontalTextAlignment != TextAlignment.Start)
			SearchEntry.HorizontalTextAlignment = TextAlignment.Start;
	}

	void OnSearchEntryCompleted(object sender, EventArgs e)
	{
		SearchEntry.Text = "";
		SearchEntry.HorizontalTextAlignment = TextAlignment.Center;
	}
}