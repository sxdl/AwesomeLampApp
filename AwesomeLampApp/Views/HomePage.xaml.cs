using System;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using InTheHand;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;

namespace AwesomeLampApp.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        // connectBlt();
    }


    public int lightMode = 0;
    private int key = 2;
    

    private StreamWriter sw;
    private void onLightSwitchButtonClicked(object sender, EventArgs e)
    {
        switch(key)
        {
            case 1: setLightMode(3); key = 2; break;
            case 2: setLightMode(0); key = 1; break;
            default: break;
        }
        

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
    
    private void connectBlt(object sender, EventArgs e)
    {
        BluetoothClient btc = new();
        btc.Connect(BluetoothAddress.Parse("E4:5F:01:A8:AD:DD"), new Guid("00001101-0000-1000-8000-00805F9B34FB"));
        MsgBox.Text += btc.RemoteMachineName;
        if (btc.Connected)
        {
            MsgBox.Text += "connected";
            linkState.Text = "已连接";
            linkState.IsEnabled = false;
            linkState.BackgroundColor = Colors.White;
            linkState.TextColor = Color.FromArgb("3E8EED");
            NetworkStream stream = btc.GetStream();
            sw = new StreamWriter(stream, Encoding.UTF8);
            sw.Flush();
            // sw.WriteLine("Hello world!");
            // sw.Close();
        }
        else
        {
            MsgBox.Text += "not connected";
            linkState.Text = "未连接";
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

    private async void onMoreButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MorePage());
    }

    private void onReadingButtonPressed(object sender, EventArgs e)
    {
        readingButton.BackgroundColor = Color.FromArgb("#E1E1E1");
        setLightMode(4);
        
    }

    private void onReadingButtonReleased(object sender, EventArgs e)
    {
        readingButton.BackgroundColor = Colors.White;
    }

    private void onDesktopButtonPressed(object sender, EventArgs e)
    {
        desktopButton.BackgroundColor = Color.FromArgb("#E1E1E1");
        setLightMode(5);
    }

    private void onDesktopButtonReleased(object sender, EventArgs e)
    {
        desktopButton.BackgroundColor = Colors.White;
    }

    private void onNightlightButtonPressed(object sender, EventArgs e)
    {
        nightlightButton.BackgroundColor = Color.FromArgb("#E1E1E1");
        setLightMode(3);
    }

    private void onNightlightButtonReleased(object sender, EventArgs e)
    {
        nightlightButton.BackgroundColor = Colors.White;
    }

    private void onFenweidengButtonPressed(object sender, EventArgs e)
    {
        fenweidengButton.BackgroundColor = Color.FromArgb("#E1E1E1");
        setLightMode(1);
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

    private void setLightMode(int mode)
    {
        // 0: 关灯；1: k1000; 2: k2000; 3: k3000
        string argument = mode.ToString();
        sendMessage("set_led_mode", argument);
    }

    private class MessagePack
    {
        public string func;
        public string args;
    }

    private void sendMessage(string fun, string arg)
    {
        MessagePack messagePack = new()
        {
            func = fun,
            args = arg
        };
        string jsonMsg = JsonConvert.SerializeObject(messagePack);
        int size = Encoding.Default.GetByteCount(jsonMsg);
        sw.Write(size);
        sw.Write(jsonMsg);
        sw.Flush();
    }
}
