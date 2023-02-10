/*using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLampApp.ViewModels
{
    class SocketViewModel
    {
        private Label msgLog;
        private List<Socket> clientScoketLis;
        // 构造函数
        public SocketViewModel()
        {
            clientScoketLis = new List<Socket>();//存储连接服务器端的客户端的Socket
        }
        public void StartCreateSocketServer(Label log)
        {
            msgLog = log;
            // msgLog.Text = "服务端开始等待客户端连接！\n";
            // 创建socket对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)  // 判断当前设备是否接入网络
            {
                msgLog.Text += "网络已连接\n";
                // 获取当前网络IPv4地址
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress serverIP = ipe.AddressList[4];
                msgLog.Text += "当前IPv4地址: " + serverIP.ToString() + "\n";
                // 绑定端口、IP
                const int serverPort = 45000;
                IPEndPoint ipEndPoint = new IPEndPoint(serverIP, serverPort);
                socket.Bind(ipEndPoint);
                // 开启侦听
                socket.Listen(10);  // 10为请求队列长度
                msgLog.Text += "开始侦听! " + serverIP.ToString() + ":" + serverPort.ToString();
                // 使用线程池等待客户端连接
                ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
            }
            else
                msgLog.Text += "网络未连接\n";
        }

        /// <summary>
        /// 等待客户端连接的线程方法
        /// </summary>
        /// <param name="obj">服务端创建的socket对象</param>
        private void AcceptClientConnect(object obj)
        {
            //转换Socket
            var serverSocket = obj as Socket;
            // msgLog.Text += "服务端开始接收客户端连接！\n";

            //不断接受客户端的连接
            while (true)
            {
                // 创建一个负责通信的Socket
                Socket proxSocket = serverSocket.Accept();
                msgLog.Text += string.Format("客户端：{0}连接上了！\n", proxSocket.RemoteEndPoint.ToString());
                //将连接的Socket存入集合
                clientScoketLis.Add(proxSocket);
                //6、不断接收客户端发送来的消息
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
            }
        }

        /// <summary>
        /// 不断接收客户端信息子线程方法
        /// </summary>
        /// <param name="obj">参数Socke对象</param>
        private void ReceiveClientMsg(object obj)
        {
            var proxSocket = obj as Socket;
            //创建缓存内存，存储接收的信息   ,不能放到while中，这块内存可以循环利用
            byte[] data = new byte[1020 * 1024];
            while (true)
            {
                int len;
                try
                {
                    //接收消息,返回字节长度
                    len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception)
                {
                    //7、关闭Socket
                    //异常退出
                    try
                    {
                        ClientExit(string.Format("客户端：{0}非正常退出\n", proxSocket.RemoteEndPoint.ToString()), proxSocket);
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
                        ClientExit(string.Format("客户端：{0}正常退出\n", proxSocket.RemoteEndPoint.ToString()), proxSocket);
                    }
                    catch (Exception)
                    {
                    }
                    return;//让方法结束，终结当前客户端数据的异步线程，方法退出，即线程结束
                }
                //将消息显示到TxtLog
                string msgStr = Encoding.Default.GetString(data, 0, len);
                //拼接字符串
                msgLog.Text += string.Format("接收到客户端：{0}的消息：{1}\n", proxSocket.RemoteEndPoint.ToString(), msgStr);
            }
        }

        /// <summary>
        /// 客户端退出调用
        /// </summary>
        /// <param name="msg"></param>
        private void ClientExit(string msg, Socket proxSocket)
        {
            // AppendTxtLogText(msg);
            msgLog.Text += msg;
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
        /// 向文本框中追加信息
        /// </summary>
        /// <param name="str"></param>
        private void AppendTxtLogText(string str)
        {
            
            if (!(msgLog.Dispatcher.CheckAccess()))//判断跨线程访问
            {
                ////同步方法
                //this.Dispatcher.Invoke(new Action<string>( s => 
                //{
                //    this.txtLog.Text = string.Format("{0}\r\n{1}" , s , txtLog.Text);
                //}) ,str);
                //异步方法
                this.Dispatcher.BeginInvoke(new Action<string>(s =>
                {
                    this.txtLog.Text = string.Format("{0}\r\n{1}", s, txtLog.Text);
                }), str);
            }
            else
            {
                this.txtLog.Text = string.Format("{0}\r\n{1}", str, txtLog.Text);
            }
        }
    }
}
*/