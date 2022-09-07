﻿
using Assets.scripts.Utils;

using C2BNet;
using C2GNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.Models;
using Assets.scripts.UI;
using Assets.scripts.UI.Common;
using Assets.scripts.Managers;

namespace NetWork
{
    public class UserService 
    {
        
        private static UserService _instance = new UserService();

        
        private UserService()
        {
        }

        
        public static UserService GetInstance()
        {
            return _instance;
        }
       
        public void init()
        {

            MessageCenter.AddMsgListener(MessageType.OnUserLogin, this.OnUserLogin, this);
            MessageCenter.AddMsgListener(MessageType.OnUserRegister, this.OnUserRegister, this);
            //MessageCenter.AddMsgListener(MessageType.OnUnLock, this.OnUnLock, this);
            //MessageCenter.AddMsgListener(MessageType.OnCharacterDetail, this.OnCharacterDetail, this);
            //MessageCenter.AddMsgListener(MessageType.OnSwitchCharacter, this.OnSwitchCharacter, this);
            //MessageCenter.AddMsgListener(MessageType.OnAttrPromoteInfo, this.OnAttrPromoteInfo, this);
            //MessageCenter.AddMsgListener(MessageType.OnCombatPowerRanking, this.OnCombatPowerRanking, this);
            //MessageCenter.AddMsgListener(MessageType.OnFollowRes, this.OnFollowRes, this);
            MessageCenter.AddMsgListener(MessageType.OnUserStatusChange, this.OnUserStatusChange, this);
            MessageCenter.AddMsgListener(MessageType.OnHeartBeat, this.OnHeartBeat, this);
            MessageCenter.AddMsgListener(MessageType.OnUserStatusQuery, this.OnUserStatusQuery, this);
        }

        public void ConnectToServer(string ip, int port)
        {
            //LogUtil.log("ConnectToServer() Start ");
            NetGameClient.GetInstance().Connect(ip, port);
        }


        /**
        * 请求心跳Game
        */
        public void SendHeartBeat()
        {
            
            LogUtil.log("HeartBeatRequest");
            C2GNetMessage message = new C2GNetMessage
            {
                Request = new NetMessageRequest
                {
                    HeartBeatReq = new C2GNet.HeartBeatRequest()
                }
            };
            NetGameClient.GetInstance().SendMessage(message);
        }

        /**
         * 心跳响应Game
         */
        public void OnHeartBeat(object any)
        {
            C2GNet.HeartBeatResponse response = any as C2GNet.HeartBeatResponse;
            LogUtil.log("HeartBeatResponse");

            MessageCenter.dispatch(MessageType.OnHeartBeat_UI, response);
        }


        /**
        * 请求心跳Game
        */
        public void SendBattleHeartBeat()
        {
            LogUtil.log("HeartBeatRequest");
            C2BNetMessage message = new C2BNetMessage
            {
                Request = new C2BNet.C2BNetMessageRequest
                {
                    HeartBeatRequest = new C2BNet.HeartBeatRequest()
                }
            };
            NetBattleClient.GetInstance().SendMessage(message);
        }

        /**
     * 请求登录
     * @param user 
     * @param psw 
     */
        public void SendLogin(string user, string psw)
        {
            LogUtil.log("UserLoginRequest::user :" + user + " psw:" + psw);
            var Net = new C2GNetMessage
            {
                Request = new NetMessageRequest
                {
                    UserLogin = new UserLoginRequest
                    {
                        UserName = user,
                        Passward = psw
                    }
                }
            };

            NetGameClient.GetInstance().SendMessage(Net);

            if (!NetGameClient.GetInstance().Connected)
            {
                this.ConnectToServer(NetConfig.TcpIp, NetConfig.TcpPort);
            }
            NetGameClient.GetInstance().SendMessage(Net);
            // if (NetClient.Instance.connected) {
            //     // this.pendingMessage = null as unknown as NetMessage;
            //     NetClient.Instance.SendMessage(message);
            // }
            // else {
            //     // this.pendingMessage = message;
            //     this.ConnectToServer();
            // }
        }

        /**
         * 登录响应
         * @param param 
         */
        public void OnUserLogin(object param)
        {
            var response = param as UserLoginResponse;
            LogUtil.log("OnLogin:{0} [{1}]", response.Result, response.Errormsg);

            if (response.Result == Result.Success)
            {//登陆成功逻辑
                Debug.Log("登陆成功逻辑");
                User.Instance.isLogin = true;
                User.Instance.user = response.User;

                //SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);

                //director.loadScene('UIMain');

                //ChatManager.Instance.Init();
                //ChatManager.Instance.InitPrivateUserList();

            }
            else
                TipsManager.Instance.Show(response.Errormsg, "错误", MessageBoxType.Error);
        }

        /**
         * 请求注册
         * @param user 
         * @param psw 
         */
        public void SendRegister(string user, string psw)
        {
            LogUtil.log("UserRegisterRequest::user :" + user + " psw:" + psw);
            var Net = new C2GNetMessage 
            {
                Request = new NetMessageRequest
                {
                    UserRegister = new UserRegisterRequest
                    {
                        UserName = user,
                        Password = psw
                    }
                }
            };


            if (!NetGameClient.GetInstance().Connected)
            {
                this.ConnectToServer(NetConfig.TcpIp, NetConfig.TcpPort);
            }
            NetGameClient.GetInstance().SendMessage(Net);

            // if (NetClient.Instance.connected) {
            //     // this.pendingMessage = null as unknown as NetMessage;
            //     NetClient.Instance.SendMessage(message);
            // }
            // else {
            //     // this.pendingMessage = message;
            //     this.ConnectToServer();
            // }
        }

        /**
         * 注册响应
         * @param param 
         */
        public void OnUserRegister(object param)
        {
            var response = param as UserRegisterResponse;
            LogUtil.log("OnUserRegister:{0} [{1}]", response.Result, response.Errormsg);

            if (response.Result == Result.Success)
            {
                //登录成功，进入角色选择
                Debug.Log("注册成功");
                TipsManager.Instance.Show(response.Errormsg, "注册成功", MessageBoxType.Information);
            }
            else
                TipsManager.Instance.Show(response.Errormsg, "错误", MessageBoxType.Error);
        }


        /**
    * 用户状态变更响应
    */
        public void OnUserStatusChange(object param)
        {
            var response = param as UserStatusChangeResponse;
            LogUtil.log("UserStatusChangeResponse");
            MessageCenter.dispatch(MessageType.OnUserStatusChange_UI, response);
        }

        /**
         * 请求用户在线、离线状态查询 
         */
        public void SendUserStatusQuery(List<int> userIds)
        {
            LogUtil.log("SendUserStatusQuery");
            var Net = new C2GNetMessage
            {
                Request = new NetMessageRequest
                {
                    UserStatusQueryReq = new UserStatusQueryRequest()
                }
            };
            foreach (int userid in userIds) {
                Net.Request.UserStatusQueryReq.UserIds.Add(userid); 
            }

            NetGameClient.GetInstance().SendMessage(Net);
        }

        /**
         * 用户在线、离线状态响应
         */
        public void OnUserStatusQuery(object param)
        {
            var response = param as UserStatusQueryResponse;
            LogUtil.log("OnUserStatusQuery");
            MessageCenter.dispatch(MessageType.OnUserStatusQuery_UI, response.Status);
        }
    }
}
