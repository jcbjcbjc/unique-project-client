using Assets.scripts.GameLogic;


using Assets.scripts.Models;
using Assets.scripts.NetWork;

using Assets.scripts.UI.UIPanels;
using Assets.scripts.Utils;
using Assets.scripts.Utils.enums;
using C2BNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.scripts.Managers;
using static Assets.scripts.Utils.enums.BattleModeEnum;
using static Assets.scripts.Utils.enums.GameStatusEnum;
using static Assets.scripts.Utils.enums.HandlerFrameResultEnum;

using static Assets.scripts.Utils.enums.OptTypeEnum;

namespace Assets.scripts.GameLogic
{
    public class GameLogicManager
    {
        private TimerTask timer;
        private TimerTask recProTimer;
        private TimerTask handleFrameTimer;
        private TimerTask recordUserTimer;


        private GameCoreLogic gameLogic = new GameCoreLogic();

        // public isRecProFlag:boolean = true; //是否恢复进度中

        public void updateLogic()
        {
            if (GameData.isInGame) {
                HandlerFrameResult handlerFrameResult = HandleFrame();
                RepairFrameRequest(handlerFrameResult);
            }
        }
        public void Clear() {
            if(handleFrameTimer!=null) handleFrameTimer.Stop();
            if(recordUserTimer!=null) recordUserTimer.Stop();

            GameData.release();
            
            MessageCenter.RemoveMsgListener(this);
        }
       
        public void init() {
            MessageCenter.AddMsgListener(MessageType.OnFrameHandle, this.OnFrameHandle, this);
            MessageCenter.AddMsgListener(MessageType.OnRepairFrame, this.OnRepairFrame, this);
            MessageCenter.AddMsgListener(MessageType.OnLiveFrame, this.OnLiveFrame, this);
            MessageCenter.AddMsgListener(MessageType.OnAddOptClient, this.AddPlayerOpt, this);

            UIGameLoadIn uIGameLoadIn= (UIGameLoadIn)UIManager.GetInstance() .ShowUIForms("");
            uIGameLoadIn.setMsg("游戏拼命加载中...");
            
          
            CharacterManager.Instance.CreateCharacter(); 

            // change the GameData
            GameData.gameStatus = GameStatus.GameIn;
            GameData.isInGame = true;


            MessageCenter.dispatch(MessageType.OnBattleGameIn,0);


            //var allFrameHandlesStr = LocalStorageUtil.GetItem(LocalStorageUtil.allFrameHandlesKey);
            //if (allFrameHandlesStr!=null)
            //{  //恢复进度
            //    //console.log('恢复进度')

            //    //allFrameHandles = JSON.parse(allFrameHandlesStr).map || { };

            //    //    let this_=this;
            //    //    this.recProTimer=setInterval(async function(){
            //    //     await this_.IntervalProgressRecovery(this_);
            //    //   }, 0);  //2
            //}
            if (GameData.battleMode == BattleMode.Battle)
            {    //对局模式
                handleFrameTimer = new TimerTask(NetConfig.FrameTime, CapturePlayerOpts);
                handleFrameTimer.execute();
            }
            else if (GameData.battleMode == BattleMode.Live)
            {  //观看直播模式


            }

            //recordUserTimer = new TimerTask(1000, () => {
            //    if (GameData.handleFrameId >= 0)
            //    {
            //        recordUserTimer.Stop();
            //        // this_.isRecProFlag = false;
            //    }
            //    GameLogicService.GetInstance().SendRecordUser();
            //});
            
        }


        /*******************************************************************************************************
         * **                                On Message From BattleServer                                   ****
         * *****************************************************************************************************/


        /**
        * 帧操作响应
        */
        public void OnFrameHandle(object obj)
        {
            FrameHandleResponse param= obj as FrameHandleResponse;
            //计算接收两帧之间的时间间隔
            float currentFrameTime = Time.time;
            if (GameData.lastReceiveFrameTime != 0 && currentFrameTime - GameData.lastCheckFrameTime > 3000)
            {  //每3秒抽查下
                var ms = currentFrameTime - GameData.lastReceiveFrameTime;

                //this.uiBattle.updateFrameTime(ms);

                GameData.lastCheckFrameTime = currentFrameTime;
            }
            GameData.lastReceiveFrameTime = currentFrameTime;


            var response = param;

            var frameId = response.Frame;

            GameData.newFrameId = frameId;

            if (GameData.newFrameId - 50 > GameData.handleFrameId)
            {
                //this.uiGameLoadIn.setMsg('游戏进度恢复中...');
                //this.uiGameLoadIn.show();
            }
            else
            {
                //this.uiGameLoadIn.hide();
            }

            //已经处理的帧
            if (frameId <= GameData.handleFrameId)
            {
                return;
            }
            if (!GameData.allFrameHandles.ContainsKey(frameId))
            {
                GameData.allFrameHandles.Add(frameId, response.FrameHandles);//收到帧保存起来
            }
        }

        /**
         * 直播帧响应
         * @param param 
         */
        public void OnLiveFrame(object obj)
        {
            LiveFrameResponse param= obj as LiveFrameResponse;    
            // let response = param[0] as LiveFrameResponse;
            var response = param;
            var liveFrames = response.LiveFrames;
            for (int i = 0; i < liveFrames.Count; i++)
            {
                var liveFrame = liveFrames[i];
                if (!GameData.allFrameHandles.ContainsKey(liveFrame.Frame)) {
                    GameData.allFrameHandles.Add(liveFrame.Frame, liveFrame.FrameHandles);
                }
                    
            }
            // this.liveNotExecuteFrameCount += liveFrames.length;
        }

        private void OnRepairFrame(object obj)  {
            RepairFrameResponse response = obj as RepairFrameResponse;
            // console.log("OnRepairFrame:{0}", JSON.stringify(response.repairFrames));
            foreach (RepairFrame repairFrame in response.RepairFrames) {
                if (!GameData.allFrameHandles.ContainsKey(repairFrame.Frame)) {
                    GameData.allFrameHandles.Add(repairFrame.Frame, repairFrame.FrameHandles);
                }
            }
        }



        /*******************************************************************************************************
         * **                               Handle Frame                                                    ****
         * *****************************************************************************************************/



        private HandlerFrameResult HandleFrame()
        {
            var frameId = GameData.handleFrameId + 1;

            IList<FrameHandle> frameHandles;
            //获取帧操作集合
            GameData.allFrameHandles.TryGetValue(frameId, out frameHandles);

            if (frameHandles==null)
            {  //无帧数据
                return HandlerFrameResult.NoFrameData;
            }
            if (GameData.executeFrameId >= frameId)
            {
                //LogUtil.log("不能重复执行，已经执行的帧:" + this.executeFrameId);
                return HandlerFrameResult.NotRepeatFrame;
            }
            //update executeFrameId
            GameData.executeFrameId = frameId;

            gameLogic.update(frameHandles);

            //update handleFrameId
            GameData.handleFrameId = frameId; 

            //store data
            if (frameId % 15 == 0)
            {
                //LocalStorageUtil.SetItem(LocalStorageUtil.allFrameHandlesKey, JSON.stringify(allFrameHandles));
            }
            return HandlerFrameResult.Success;
         }

        /**
         * 补帧效验
         * @param handlerFrameResult 
         * @return  是否补帧了
         */
        private void RepairFrameRequest(HandlerFrameResult handlerFrameResult) {
        if(handlerFrameResult == HandlerFrameResult.NoFrameData){
            if(GameData.currentRepairFrame <= 0){
            //补帧请求
            var start = GameData.handleFrameId + 1;
                    var end = this.GetEndFrameId(start);
            if ((end- start)<10)
            {
                return ;
            }
            //console.log('补帧请求 start=' + start + '，' + end + '，handleFrameId=' + this.handleFrameId)
                GameLogicService.GetInstance().SendRepairFrame(start, end);
                    GameData.currentRepairFrame = GameData.repairWaitFrameCount;
        }else{
                    GameData.currentRepairFrame--;
        }
            return;
        }
            GameData.currentRepairFrame =0;
            return;
        }
        /**
        * 获取补帧结束帧
        * @param startFrameId 起始帧
        */
        private int GetEndFrameId(int startFrameId)
        {
            var frameIds = GameData.allFrameHandles.Keys;
           
           
            return frameIds.Max();
        }



        /*******************************************************************************************************
         * **                                collect and send player operation                              ****
         * *****************************************************************************************************/


        public void CapturePlayerOpts(){
            //无操作
            if(GameData.frameHandles.FrameHandles.Count==0){
               return;
            }

            GameData.frameHandles.UserId= User.Instance.user.Id ;
           // LogUtil.log(this.frameHandle);
            //发送操作

            GameLogicService.GetInstance().SendFrameHandle(GameData.frameHandles);

            GameData.frameHandles =new FrameHandlesFromClient();
        }

        public void AddPlayerOpt(object obj)
        {
            FrameHandle frameHandle=obj as FrameHandle;
            frameHandle.UserId = User.Instance.user.Id;
            frameHandle.OpretionId= GameData.NextOperationId++;
            GameData.PredictedInput.Add(frameHandle);
            GameData.frameHandles.FrameHandles.Add(frameHandle);
        }
    }
}
