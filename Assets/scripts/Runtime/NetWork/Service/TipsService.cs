using Assets.scripts.Managers;

using Assets.scripts.UI.Common;
using C2GNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWork
{
    class TipsService
    {
        private static TipsService _instance = new TipsService();


        private TipsService()
        {
        }


        public static TipsService GetInstance()
        {
            return _instance;
        }
        public void Init()
        {
            MessageCenter.AddMsgListener(MessageType.OnTips, this.OnTips, this);
        }

        /**
         * 提示响应
         */
        private void OnTips(object param)
        {
            var response = param as TipsResponse;
            //LogUtil.log("OnTips:{0} [{1}]", response.content, response.tipsType);
            if (response.TipsType == TipsType.Tips)
            {   //字体提示
                TipsManager.Instance.showTips(response.Content);
            }
            else if (response.TipsType == TipsType.Popup)
            {  //弹窗提示
                TipsManager.Instance.Show(response.Content, "提示", MessageBoxType.Information);
            }

            //业务消息

            //console.log('isOutRoom：' + (response.tipsWorkType == TipsWorkType.OutRoom))
            if (response.TipsWorkType == TipsWorkType.AuctionResult)
            {  //拍卖结算
                MessageCenter.dispatch(MessageType.AuctionRefreshUI, 0);
            }
            else if (response.TipsWorkType == TipsWorkType.DismissRoom)
            {  //解散房间
               //director.loadScene('UIMain');
            }
            else if (response.TipsWorkType == TipsWorkType.KickOutRoom)
            {  //踢出房间
               //director.loadScene('UIMain');
            }
            else if (response.TipsWorkType == TipsWorkType.OutRoom)
            {  //退出房间

                MessageCenter.dispatch(MessageType.OnMyRoom_RefieshUI, 0);
            }

        }
    }
}
