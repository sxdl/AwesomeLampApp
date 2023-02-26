namespace AwesomeLampApp.Views;

public partial class TomatoListPage : ContentPage
{
	public TomatoListPage()
	{
		InitializeComponent();
	}

	private async void onBackButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }

	private void onSliderValueChanged(object sender, ValueChangedEventArgs e)
	{
        double value = e.NewValue;
        displayLabel.Text = value.ToString("0");

    }

	private void onTimerClicked(object sender, EventArgs e)
	{


    }
}