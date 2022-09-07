﻿using Assets.scripts.GameLogic;
<<<<<<< Updated upstream:Assets/scripts/UI/UIPanels/UIEnterGameLoad.cs
using Managers;

using Models;
using NetWork;
=======
using Assets.scripts.Managers;

using Models;
>>>>>>> Stashed changes:Assets/scripts/Runtime/UI/UIPanels/UIEnterGameLoad.cs


using Assets.scripts.Utils;
using C2BNet;
using C2GNet;
using NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.scripts.Utils.enums.BattleModeEnum;
using MyTimer;
using Services;

namespace UI
{
    public class UIEnterGameLoad:BaseUIForm
    {
        private Metronome timer;
        private EventSystem eventSystem;

        private int percent_ = 0;   //加载进度百分比
        private bool isGoToBattleScene = false; //是否已跳转战斗场景
       
        public void Awake()
        {
            eventSystem= ServiceLocator.Get<EventSystem>();
            timer =new Metronome();
        }
        public void Start()
        {
            
            timer.Initialize(5000);

        }

        public void Init()
        {

            this.InitTeamUser(User.Instance.room);

            //连接到战斗服务器
            var ipPortArr = User.Instance.room.IpPortStr.Split(":");
            NetConfig.UdpIp = ipPortArr[0];
            NetConfig.UdpPort = int.Parse(ipPortArr[2]);

            //console.log('战斗服务器地址：'+NetConfig.websocketBattleUrl)

            NetBattleClient.GetInstance().Connect(NetConfig.UdpIp, NetConfig.UdpPort);
            eventSystem.AddListener<PercentForwardResponse>(EEvent.OnPercentForward, OnPercentForward);

            if (GameData.battleMode == BattleMode.Battle)
            {  //对局模式
               //上传加载进度，需要等所有用户资源都加载完成
                timer = new TimerTask(5000, () =>
                {
                    //console.log('uploadProgress percent_=' + this_.percent_)
                    GameLogicService.GetInstance().SendPercentForward(this.percent_);
                    if (this.percent_ < 100)
                    {
                        this.percent_ += 20;
                    }
                });
            }
            else if (GameData.battleMode == BattleMode.Live)
            {  //观看直播模式
               //加载资源，只需要当前用户的资源加载完成即可
               //跳转战斗场景

                //director.loadScene('Map01');

            }
        }

        private void OnPercentForward(PercentForwardResponse response)
        {
            
            //console.log("OnPercentForward:{0} [{1}]", response.percentForward,response.allUserLoadSucess);
            var userId = response.PercentForward.UserId;
            var percent = response.PercentForward.Percent;

            this.UpdateTeamUserPercent(userId, percent);

            //全部用户资源加载成功
            if (response.AllUserLoadSucess && !this.isGoToBattleScene)
            {
                this.isGoToBattleScene = true;

                //LogUtil.log("跳转战斗场景");
                Scene_Manager.Load(1); //跳转战斗场景
                GameLogicMonobehavior.Instance.init();
                Close();
            }
        }
        /**
        * 初始化队伍信息
        * @param roomUserList 
        * @param teamType 
        */
        private void InitTeamUser(NRoom roomUserList)
        {
            
        }

        /**
         * 更新队伍用户加载进度
         * @param userId 
         * @param percent 
         * @param teamType 
         */
        private void UpdateTeamUserPercent(int userId, int percent)
        {

            foreach (AllTeam allTeam in User.Instance.room.AllTeam) {
                foreach (RoomUser roomUser in allTeam.Team) {
                    if (roomUser.UserId == userId)
                    {
                        //this[percentName + (i + 1)].string= percent + '%';
                    }
                }
            }
        }

        public override void Close()
        {
            if (timer != null) { 
                timer.Stop();
            }
           
            CloseUIForm();
        }
    }
}