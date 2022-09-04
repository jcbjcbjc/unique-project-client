using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using C2BNet;
using cocosocket4unity;
using static MessageDispatcher;
using System.Threading;
using Assets.scripts.Utils;
using Assets.scripts.NetWork.Service;
using Assets.scripts.Message;
using Google.Protobuf;
/// <summary>
/// GameLogicLoginService
/// 
/// @Author 贾超博
/// 
/// @Date 2022/4/30
/// </summary>
namespace Assets.scripts.NetWork.NetClient
{
    public class NetBattleClient:KcpClient
    {
        private static NetBattleClient _instance = new NetBattleClient();


        private NetBattleClient()
        {
        }


        public static NetBattleClient GetInstance()
        {
            return _instance;
        }


        TimerTask timerTask1 = null;
        TimerTask timerTask2 = null;

        protected override void HandleReceive(ByteBuf buf)
        {
            int length = buf.ReadableBytes();
            
            C2BNetMessage msg = C2BNetMessage.Parser.ParseFrom(buf.GetRaw(),0, length);

            MessageDispatcher.AddTask(new NetMessage( msg));
        }
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ex"></param>
        protected override void HandleException(Exception ex)
        {
            base.HandleException(ex);
        }
        /// <summary>
        /// 超时
        /// </summary>
        protected override void HandleTimeout()
        {
            StopHeartBeat();
            base.HandleTimeout();
        }
        public void connect(string ip,int port) {
            KcpClient client = _instance; /*new NetBattleClient();*/
            client.NoDelay(1, 10, 2, 1);//fast
            client.WndSize(64, 64);
            client.Timeout(10 * 1000);
            client.SetMtu(512);
            client.SetMinRto(10);
            client.SetConv(121106);
            
            client.Connect(ip, port);
            client.Start();

            StartHeartBeat();
        }

        public void Init()
        {
            
        }

        private void StartHeartBeat()
        {
            timerTask1 = new TimerTask(1000, () => { UserService.GetInstance().SendBattleHeartBeat(); });
            timerTask1.execute();
        }
        private void StopHeartBeat()
        {
            if (timerTask1 != null) {
                timerTask1.Stop();
            }
            
        }

        public int SendMessage(C2BNetMessage msg)
        {
            try
            {
                byte[] buffer = msg.ToByteArray();
               
                ByteBuf bb = new ByteBuf(buffer);
                this.Send(bb);
            }
            catch (Exception ex)
            {
                Debug.Log( ex.Message);
                
            }
            return -1;
        }

        public void Close() {
            try
            {
                MessageCenter.RemoveMsgListener(this);
                StopHeartBeat();
            }
            catch (Exception ex)
            {
                Debug.Log("无法关闭连接：" + ex.Message);

            }
        }

        //private void reconnect(int time) {
        //    timerTask2 = new TimerTask(1000, () => {
        //        _instance = new NetBattleClient();
        //        connect();
        //    });
        //}
    }
}
