using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.Models;
using Assets.scripts.NetWork.NetClient;
using Assets.scripts.Utils;
using C2BNet;
/// <summary>
/// GameLogicLoginService
/// 
/// @Author 贾超博
/// 
/// @Date 2022/4/30
/// </summary>

namespace Assets.scripts.NetWork.Service
{
    public class GameLogicService
    {
        private static GameLogicService _instance = new GameLogicService();


        private GameLogicService()
        {
        }


        public static GameLogicService GetInstance()
        {
            return _instance;
        }

        public void init()
        {
            
        }

        public void SendFrameHandle(FrameHandlesFromClient frameHandles)
        {
            //LogUtil.log("SendFrameHandle",frameHandle);
            var userId = User.Instance.user.Id;

            frameHandles.UserId=userId ;

            var Net = new C2BNetMessage
            {
                Request = new C2BNetMessageRequest
                {
                    UserId = userId,
                    FrameHandles = frameHandles
                }
            };

            NetBattleClient.GetInstance().SendMessage(Net);
        }


        /**
         * 发送进度转发
         */
        public void SendPercentForward(int percent)
        {
            LogUtil.log("SendPercentForward");
            var userId = User.Instance.user.Id;
            var Net = new C2BNetMessage
            {
                Request = new C2BNetMessageRequest
                {
                    UserId = userId,
                    PercentForward = new PercentForward
                    {
                        UserId = userId,
                        Percent = percent
                    }
                }
            };

            NetBattleClient.GetInstance().SendMessage(Net);

        }


        /**
        * 发送游戏结束
        */
        public void SendGameOver()
        {
            //LogUtil.log("SendGameOver");
            var userId = User.Instance.user.Id;
            var Net = new C2BNetMessage
            {
                Request = new C2BNetMessageRequest
                {
                    UserId = userId,
                    GameOverReq = new GameOverRequest()
                }
            };

            NetBattleClient.GetInstance().SendMessage(Net);


        }

        /**
            * 发送补帧请求
            */
        public void SendRepairFrame(int startFrame, int endFrame)
        {
            // LogUtil.log("SendRepairFrame");
            var userId = User.Instance.user.Id;

            var Net = new C2BNetMessage
            {
                Request = new C2BNetMessageRequest
                {
                    UserId = userId,
                    RepairFrameReq = new RepairFrameRequest
                    {
                        StartFrame = startFrame,
                        EndFrame = endFrame
                    }
                }
            };

            NetBattleClient.GetInstance().SendMessage(Net);
        }

        /**
         * 记录用户请求
         */
        public void SendRecordUser()
        {
            var userId = User.Instance.user.Id;
            //console.log('SendRecordUser')
            var Net = new C2BNetMessage
            {
                Request = new C2BNetMessageRequest
                {
                    UserId = userId
                }
            };

            NetBattleClient.GetInstance().SendMessage(Net);

        }

    }
}
