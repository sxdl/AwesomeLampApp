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
        clientScoketLis = new List<Socket>();//�洢���ӷ������˵Ŀͻ��˵�Socket
        // so = new SocketViewModel();
    }

	private void OnServerCreateButtonClicked(object sender, EventArgs e)
	{
		StartCreateSocketServer();
	}
    private void StartCreateSocketServer()
    {
        //Label msgLog = MsgBox;
        // msgLog.Text = "����˿�ʼ�ȴ��ͻ������ӣ�\n";
        // ����socket����
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // �������ݰ����� ���ڴ��ݲ���
        DataPackage dataPackage = new DataPackage(socket, MsgBox);
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)  // �жϵ�ǰ�豸�Ƿ��������
        {
            ServerCreateButton.IsEnabled = false;
            MsgBox.Text += "����������\n";
            // ��ȡ��ǰ����IPv4��ַ
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());

            //IPAddress serverIP = ipe.AddressList[0];
            //IPAddress serverIP = IPAddress.Parse("167905380");
            IPAddress serverIP = IPAddress.Parse(IPEntry.Text.ToString());

            MsgBox.Text += "��ǰIPv4��ַ: " + serverIP.ToString() + "\n";

            // �󶨶˿ڡ�IP
            //const int serverPort = 45000;
            int serverPort = int.Parse(PortEntry.Text.ToString());

            IPEndPoint ipEndPoint = new IPEndPoint(serverIP, serverPort);
            socket.Bind(ipEndPoint);
            // ��������
            socket.Listen(10);  // 10Ϊ������г���
            MsgBox.Text += "��ʼ����! " + serverIP.ToString() + ":" + serverPort.ToString() + "\n";
            // ʹ���̳߳صȴ��ͻ�������
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), dataPackage);
        }
        else
            MsgBox.Text += "����δ����\n";
    }

    /// <summary>
    /// �ȴ��ͻ������ӵ��̷߳���
    /// </summary>
    /// <param name="obj">����˴�����socket����</param>
    private void AcceptClientConnect(object obj)
    {
        //ת��Socket
        var dataPack = obj as DataPackage;
        Socket serverSocket = dataPack.socket;
        Label msgLog = dataPack.label;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            msgLog.Text += "����˿�ʼ���տͻ������ӣ�\n";
        });
        //���Ͻ��ܿͻ��˵�����
        while (true)
        {
            // ����һ������ͨ�ŵ�Socket
            Socket proxSocket = serverSocket.Accept();
            MainThread.BeginInvokeOnMainThread(() => // �����߳������д���
            {
                msgLog.Text += string.Format("�ͻ��ˣ�{0}�������ˣ�\n", proxSocket.RemoteEndPoint.ToString());
            });
            //�����ӵ�Socket���뼯��
            clientScoketLis.Add(proxSocket);
            //6�����Ͻ��տͻ��˷���������Ϣ
            dataPack.socket = proxSocket;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), dataPack);
        }
    }

    /// <summary>
    /// ���Ͻ��տͻ�����Ϣ���̷߳���
    /// </summary>
    /// <param name="obj">����Socke����</param>
    private void ReceiveClientMsg(object obj)
    {
        var dataPack = obj as DataPackage;
        var proxSocket = dataPack.socket;
        var msgLog = dataPack.label;
        //���������ڴ棬�洢���յ���Ϣ   ,���ܷŵ�while�У�����ڴ����ѭ������
        byte[] data = new byte[1020 * 1024];
        while (true)
        {
            int len;
            MemoryStream ms = new MemoryStream(); // �����������ڽ���ͼƬ����
            try
            {
                //������Ϣ,�����ֽڳ���
                len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                ClearTextLog(msgLog);
                AppendTextLog(String.Format("���յ���Ϣ���ֽڳ���Ϊ��{0}\n", len.ToString()), msgLog);
            }
            catch (Exception)
            {
                //7���ر�Socket
                //�쳣�˳�
                try
                {
                    ClientExit(string.Format("�ͻ��ˣ�{0}�������˳�\n", proxSocket.RemoteEndPoint.ToString()), dataPack);
                }
                catch (Exception)
                {
                }
                return;//�÷����������սᵱǰ�ͻ������ݵ��첽�̣߳������˳������߳̽���
            }

            if (len <= 0)//�жϽ��յ��ֽ���
            {
                //7���ر�Socket
                //С��0��ʾ�����˳�
                try
                {
                    ClientExit(string.Format("�ͻ��ˣ�{0}�����˳�\n", proxSocket.RemoteEndPoint.ToString()), dataPack);
                }
                catch (Exception)
                {
                }
                return;//�÷����������սᵱǰ�ͻ������ݵ��첽�̣߳������˳������߳̽���
            }
            //����Ϣ��ʾ��TxtLog
            //string msgStr = data.ToString();
            // string msgStr = Encoding.Unicode.GetString(data, 0, len);
            ms.Write(data, 0, len);

            /*Image img = new Image  // ����ͼƬ���������������������������
            {
                Source = ImageSource.FromStream(() => ms)
            };*/

            MainThread.BeginInvokeOnMainThread(() =>  // ���߳�ִ�д���
            {
                pictureBox.Source = ImageSource.FromStream(() => ms);
            });
            // AppendTextLog(Encoding.Default.GetString(data, 0, len), msgLog);

            //ƴ���ַ���
            //msgStr =  string.Format("���յ��ͻ��ˣ�{0}����Ϣ��{1}\n", proxSocket.RemoteEndPoint.ToString(), msgStr);
            // AppendTextLog(msgStr, msgLog);
        }
    }

    /// <summary>
    /// �ͻ����˳�����
    /// </summary>
    /// <param name="msg"></param>
    private void ClientExit(string msg, DataPackage dataPack)
    {
        // AppendTxtLogText(msg);
        Label msgLog = dataPack.label;
        Socket proxSocket = dataPack.socket;
        AppendTextLog(msg, msgLog);
        clientScoketLis.Remove(proxSocket);//�Ƴ������е�����Socket

        try
        {
            if (proxSocket.Connected)//���������״̬
            {
                proxSocket.Shutdown(SocketShutdown.Both);//�ر�����
                proxSocket.Close(100);//100�볬ʱ��
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// ��ָ�����������Ϣ
    /// </summary>
    /// <param name="str"></param>
    private void AppendTextLog(string str, Object sender)
    {
        var container = sender as Label;
        if (MainThread.IsMainThread)  // �ж��Ƿ������߳�
            container.Text += str;
        else
            MainThread.BeginInvokeOnMainThread(() =>  // ���߳�ִ�д���
            {
                container.Text += str;
            });
    }

    private void ClearTextLog(Object sender)  // �����־
    {
        var container = sender as Label;
        if (MainThread.IsMainThread)  // �ж��Ƿ������߳�
            container.Text = "";
        else
            MainThread.BeginInvokeOnMainThread(() =>  // ���߳�ִ�д���
            {
                container.Text = "";
            });
    }
}