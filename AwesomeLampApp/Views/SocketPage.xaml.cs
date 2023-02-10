using System.Net.Sockets;
using System.Net;
using System;
using System.IO;
using System.Text;
using Microsoft.Maui.ApplicationModel;
using System.ComponentModel;
// using AwesomeLampApp.ViewModels;

namespace AwesomeLampApp.Views;

public class DataPackage
{
    public Socket socket;
    public Label label;
    public DataPackage(Socket so, Label la)
    {
        socket = so;
        label = la;
    }
}
public partial class SocketPage : ContentPage
{
    // private SocketViewModel so;
    private List<Socket> clientScoketLis;
    public SocketPage()
	{
		InitializeComponent();
        clientScoketLis = new List<Socket>();//存储连接服务器端的客户端的Socket
        // so = new SocketViewModel();
    }

	private void OnServerCreateButtonClicked(object sender, EventArgs e)
	{
		StartCreateSocketServer();
	}
    private void StartCreateSocketServer()
    {
        //Label msgLog = MsgBox;
        // msgLog.Text = "服务端开始等待客户端连接！\n";
        // 创建socket对象
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // 创建数据包对象 用于传递参数
        DataPackage dataPackage = new DataPackage(socket, MsgBox);
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)  // 判断当前设备是否接入网络
        {
            ServerCreateButton.IsEnabled = false;
            MsgBox.Text += "网络已连接\n";
            // 获取当前网络IPv4地址
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());

            //IPAddress serverIP = ipe.AddressList[0];
            //IPAddress serverIP = IPAddress.Parse("167905380");
            IPAddress serverIP = IPAddress.Parse(IPEntry.Text.ToString());

            MsgBox.Text += "当前IPv4地址: " + serverIP.ToString() + "\n";

            // 绑定端口、IP
            //const int serverPort = 45000;
            int serverPort = int.Parse(PortEntry.Text.ToString());

            IPEndPoint ipEndPoint = new IPEndPoint(serverIP, serverPort);
            socket.Bind(ipEndPoint);
            // 开启侦听
            socket.Listen(10);  // 10为请求队列长度
            MsgBox.Text += "开始侦听! " + serverIP.ToString() + ":" + serverPort.ToString() + "\n";
            // 使用线程池等待客户端连接
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), dataPackage);
        }
        else
            MsgBox.Text += "网络未连接\n";
    }

    /// <summary>
    /// 等待客户端连接的线程方法
    /// </summary>
    /// <param name="obj">服务端创建的socket对象</param>
    private void AcceptClientConnect(object obj)
    {
        //转换Socket
        var dataPack = obj as DataPackage;
        Socket serverSocket = dataPack.socket;
        Label msgLog = dataPack.label;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            msgLog.Text += "服务端开始接收客户端连接！\n";
        });
        //不断接受客户端的连接
        while (true)
        {
            // 创建一个负责通信的Socket
            Socket proxSocket = serverSocket.Accept();
            MainThread.BeginInvokeOnMainThread(() => // 在主线程上运行代码
            {
                msgLog.Text += string.Format("客户端：{0}连接上了！\n", proxSocket.RemoteEndPoint.ToString());
            });
            //将连接的Socket存入集合
            clientScoketLis.Add(proxSocket);
            //6、不断接收客户端发送来的消息
            dataPack.socket = proxSocket;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), dataPack);
        }
    }

    /// <summary>
    /// 不断接收客户端信息子线程方法
    /// </summary>
    /// <param name="obj">参数Socke对象</param>
    private void ReceiveClientMsg(object obj)
    {
        var dataPack = obj as DataPackage;
        var proxSocket = dataPack.socket;
        var msgLog = dataPack.label;
        //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
        byte[] data = new byte[1020 * 1024];
        while (true)
        {
            int len;
            MemoryStream ms = new MemoryStream(); // 创建流，用于接收图片数据
            try
            {
                //接收消息,返回字节长度
                len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                ClearTextLog(msgLog);
                AppendTextLog(String.Format("接收到信息，字节长度为：{0}\n", len.ToString()), msgLog);
            }
            catch (Exception)
            {
                //7、关闭Socket
                //异常退出
                try
                {
                    ClientExit(string.Format("客户端：{0}非正常退出\n", proxSocket.RemoteEndPoint.ToString()), dataPack);
                }
                catch (Exception)
                {
                }
                return;//让方法结束，终结当前客户端数据的异步线程，方法退出，即线程结束
            }

            if (len <= 0)//判断接收的字节数
            {
                //7、关闭Socket
                //小于0表示正常退出
                try
                {
                    ClientExit(string.Format("客户端：{0}正常退出\n", proxSocket.RemoteEndPoint.ToString()), dataPack);
                }
                catch (Exception)
                {
                }
                return;//让方法结束，终结当前客户端数据的异步线程，方法退出，即线程结束
            }
            //将消息显示到TxtLog
            //string msgStr = data.ToString();
            // string msgStr = Encoding.Unicode.GetString(data, 0, len);
            ms.Write(data, 0, len);

            /*Image img = new Image  // 创建图片容器，将流中数据输出到容器中
            {
                Source = ImageSource.FromStream(() => ms)
            };*/

            MainThread.BeginInvokeOnMainThread(() =>  // 跨线程执行代码
            {
                pictureBox.Source = ImageSource.FromStream(() => ms);
            });
            // AppendTextLog(Encoding.Default.GetString(data, 0, len), msgLog);

            //拼接字符串
            //msgStr =  string.Format("接收到客户端：{0}的消息：{1}\n", proxSocket.RemoteEndPoint.ToString(), msgStr);
            // AppendTextLog(msgStr, msgLog);
        }
    }

    /// <summary>
    /// 客户端退出调用
    /// </summary>
    /// <param name="msg"></param>
    private void ClientExit(string msg, DataPackage dataPack)
    {
        // AppendTxtLogText(msg);
        Label msgLog = dataPack.label;
        Socket proxSocket = dataPack.socket;
        AppendTextLog(msg, msgLog);
        clientScoketLis.Remove(proxSocket);//移除集合中的连接Socket

        try
        {
            if (proxSocket.Connected)//如果是连接状态
            {
                proxSocket.Shutdown(SocketShutdown.Both);//关闭连接
                proxSocket.Close(100);//100秒超时间
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// 向指定容器添加信息
    /// </summary>
    /// <param name="str"></param>
    private void AppendTextLog(string str, Object sender)
    {
        var container = sender as Label;
        if (MainThread.IsMainThread)  // 判断是否在主线程
            container.Text += str;
        else
            MainThread.BeginInvokeOnMainThread(() =>  // 跨线程执行代码
            {
                container.Text += str;
            });
    }

    private void ClearTextLog(Object sender)  // 清空日志
    {
        var container = sender as Label;
        if (MainThread.IsMainThread)  // 判断是否在主线程
            container.Text = "";
        else
            MainThread.BeginInvokeOnMainThread(() =>  // 跨线程执行代码
            {
                container.Text = "";
            });
    }
}