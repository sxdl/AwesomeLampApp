using System;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using InTheHand;
using InTheHand.Net.Bluetooth;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;


namespace AwesomeLampApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }


    public int lightMode = 0;
    private void onLightSwitchButtonClicked(object sender, EventArgs e)
    {
        

    }

    private void lightModeChange(int mode)
    {
        switch (mode)
        {
            case 0:
                {
                    readingLabel.TextColor = Color.FromArgb("#3E8EED");
                    readingLabel.FontAttributes = FontAttributes.Bold;
                    desktopLabel.TextColor = Colors.Black;
                    desktopLabel.FontAttributes = FontAttributes.None;
                    nightlightLabel.TextColor = Colors.Black;
                    nightlightLabel.FontAttributes = FontAttributes.None;
                    fenweidengLabel.TextColor = Colors.Black;
                    fenweidengLabel.FontAttributes = FontAttributes.None;
                    break;
                }
            case 1:
                {
                    readingLabel.TextColor = Colors.Black;
                    readingLabel.FontAttributes = FontAttributes.None;
                    desktopLabel.TextColor = Color.FromArgb("#3E8EED");
                    desktopLabel.FontAttributes = FontAttributes.Bold;
                    nightlightLabel.TextColor = Colors.Black;
                    nightlightLabel.FontAttributes = FontAttributes.None;
                    fenweidengLabel.TextColor = Colors.Black;
                    fenweidengLabel.FontAttributes = FontAttributes.None;
                    break;
                }
            case 2:
                {
                    readingLabel.TextColor = Colors.Black;
                    readingLabel.FontAttributes = FontAttributes.None;
                    desktopLabel.TextColor = Colors.Black;
                    desktopLabel.FontAttributes = FontAttributes.None;
                    nightlightLabel.TextColor = Color.FromArgb("#3E8EED");
                    nightlightLabel.FontAttributes = FontAttributes.Bold;
                    fenweidengLabel.TextColor = Colors.Black;
                    fenweidengLabel.FontAttributes = FontAttributes.None;
                    break;
                }
            case 3:
                {
                    readingLabel.TextColor = Colors.Black;
                    readingLabel.FontAttributes = FontAttributes.None;
                    desktopLabel.TextColor = Colors.Black;
                    desktopLabel.FontAttributes = FontAttributes.None;
                    nightlightLabel.TextColor = Colors.Black;
                    nightlightLabel.FontAttributes = FontAttributes.None;
                    fenweidengLabel.TextColor = Color.FromArgb("#3E8EED");
                    fenweidengLabel.FontAttributes = FontAttributes.Bold;
                    break;
                }
        }
    }

    private void onMoreButtonPressed(object sender, EventArgs e)
    {
        moreButton.BackgroundColor = Color.FromArgb("#E1E1E1");
    }

    private void onMoreButtonReleased(object sender, EventArgs e)
    {
        moreButton.BackgroundColor = Colors.White;
    }

    private void onMoreButtonClicked(object sender, EventArgs e)
    {
        BluetoothClient btc = new();
        btc.Connect(BluetoothAddress.Parse("E4:5F:01:A8:AD:DD"),new Guid("94f39d29-7d6d-437d-973b-fba39e49d4ee"));
        MsgBox.Text += btc.RemoteMachineName;
        if (btc.Connected)
        {
            NetworkStream stream = btc.GetStream();
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
            sw.WriteLine("Hello world!");
            sw.Flush();
            sw.Close();
        }
    }

    private void onReadingButtonPressed(object sender, EventArgs e)
    {
        readingButton.BackgroundColor = Color.FromArgb("#E1E1E1");
    }

    private void onReadingButtonReleased(object sender, EventArgs e)
    {
        readingButton.BackgroundColor = Colors.White;
    }

    private void onDesktopButtonPressed(object sender, EventArgs e)
    {
        desktopButton.BackgroundColor = Color.FromArgb("#E1E1E1");
    }

    private void onDesktopButtonReleased(object sender, EventArgs e)
    {
        desktopButton.BackgroundColor = Colors.White;
    }

    private void onNightlightButtonPressed(object sender, EventArgs e)
    {
        nightlightButton.BackgroundColor = Color.FromArgb("#E1E1E1");
    }

    private void onNightlightButtonReleased(object sender, EventArgs e)
    {
        nightlightButton.BackgroundColor = Colors.White;
    }

    private void onFenweidengButtonPressed(object sender, EventArgs e)
    {
        fenweidengButton.BackgroundColor = Color.FromArgb("#E1E1E1");
    }

    private void onFenweidengButtonReleased(object sender, EventArgs e)
    {
        fenweidengButton.BackgroundColor = Colors.White;
    }

    private void onReadingButtonClicked(object sender, EventArgs e)
    {
        lightModeChange(0);
    }

    private void onDesktopButtonClicked(object sender, EventArgs e)
    {
        lightModeChange(1);
    }

    private void onNightlightButtonClicked(object sender, EventArgs e)
    {
        lightModeChange(2);
    }

    private void onFenweidengButtonClicked(object sender, EventArgs e)
    {
        lightModeChange(3);
    }
}
