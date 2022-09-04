using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.NetWork;
using Assets.scripts.Message;
using Assets.scripts.NetWork.NetClient;
using C2BNet;
using C2GNet;
using System;

public class MessageDispatcher : MonoBehaviour
{
    public static List<NetMessage> msgList = new List<NetMessage>();

    // Start is called before the first frame update
    void Start()
    {
        //NetBattleClient.Instance.connect("127.0.0.1",8001);
    }

    // Update is called once per frame
    void Update()
    {
        //每帧处理消息
        for (int i = 0; i < NetConfig.MessageDispatchSpeed; i++)
        {
            if (msgList.Count > 0)
            {
                Dispatch(msgList[0]);
                lock (msgList)
                {
                    msgList.RemoveAt(0);
                }
            }
            else
            {
                break;
            }
        }
    }
    private bool Dispatch(NetMessage msg)
    {
        try
        {
            switch (msg.Net_TypeBorG)
            {
                case NetTypeBorG.FromB:
                    C2BNetMessageResponse message = msg.C2B_NetMessage.Response;

                    //战斗服

                    if (message.RepairFrameRes != null)
                    {

                        MessageCenter.dispatch(MessageType.OnRepairFrame, message.RepairFrameRes);

                    }
                    if (message.FrameHandleRes != null)
                    {

                        MessageCenter.dispatch(MessageType.OnFrameHandle, message.FrameHandleRes);

                    }
                    if (message.PercentForwardRes != null)
                    {

                        MessageCenter.dispatch(MessageType.OnPercentForward, message.PercentForwardRes);

                    }
                    if (message.LiveFrameRes != null)
                    {

                        MessageCenter.dispatch(MessageType.OnLiveFrame, message.LiveFrameRes);

                    }
                    break;
                case NetTypeBorG.FromG:
                    NetMessageResponse messag=msg.C2G_NetMessage.Response;
                    //大厅服
                    if (messag.UserRegister != null)
                    {
                        MessageCenter.dispatch(MessageType.OnUserRegister, messag.UserRegister);
                    }
                    if (messag.UserLogin != null)
                    {
                        MessageCenter.dispatch(MessageType.OnUserLogin, messag.UserLogin);
                    }
                    if (messag.UnLockRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnUnLock, messag.UnLockRes);
                    }
                    if (messag.StatusNotify != null)
                    {
                        MessageCenter.dispatch(MessageType.OnStatusNotify, messag.StatusNotify);
                    }
                    if (messag.CharacterDetail != null)
                    {
                        MessageCenter.dispatch(MessageType.OnCharacterDetail, messag.CharacterDetail);
                    }
                    if (messag.SwitchCharacterRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnSwitchCharacter, messag.SwitchCharacterRes);
                    }
                    //if (messag.AttrPromote != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnAttrPromoteInfo, message.attrPromote);
                    //}
                    //if (messag.BagHandleRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnBagHandle, message.bagHandleRes);
                    //}
                    //if (messag.ItemBuy != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnItemBuy, message.itemBuy);
                    //}
                    //if (messag.CombatPowerRanking != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnCombatPowerRanking, message.combatPowerRanking);
                    //}
                    if (messag.FollowRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnFollowRes, messag.FollowRes);
                    }
                    if (messag.UserStatusChangeRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnUserStatusChange, messag.UserStatusChangeRes);
                    }
                    //if (messag.QueryTranRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnQueryTran, message.queryTranRes);
                    //}
                    //if (messag.BuyTranRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnBuyTran, message.buyTranRes);
                    //}
                    if (messag.HeartBeatRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnHeartBeat, messag.HeartBeatRes);
                    }
                    //if (messag.OfferPriceRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnOfferPrice, message.offerPriceRes);
                    //}
                    //if (messag.AuctionRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnAuction, message.auctionRes);
                    //}
                    //if (message.getAuctionListRes != null)
                    //{
                    //    MessageCenter.dispatch(MessageType.OnGetAuctionList, message.getAuctionListRes);
                    //}
                    if (messag.TipsRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnTips, messag.TipsRes);
                    }
                    if (messag.MyRoomRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnMyRoom, messag.MyRoomRes);
                    }
                    if (messag.InviteRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnInviteResponse, messag.InviteRes);
                    }
                    if (messag.InviteReq != null)
                    {
                        MessageCenter.dispatch(MessageType.OnInviteRequest, messag.InviteReq);
                    }
                    if (messag.KickOutRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnKickOut, messag.KickOutRes);
                    }
                    if (messag.RoomStartGameRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnRoomStartGame, messag.RoomStartGameRes);
                    }
                    if (messag.NickNameSearchRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnNickNameSearch, messag.NickNameSearchRes);
                    }
                    if (messag.AddRoomReq != null)
                    {
                        MessageCenter.dispatch(MessageType.OnAddRoomRequest, messag.AddRoomReq);
                    }
                    if (messag.AddRoomRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnAddRoomResponse, messag.AddRoomRes);
                    }
                    if (messag.OutRoomRes != null)
                    { 
                        MessageCenter.dispatch(MessageType.OnOutRoom, messag.OutRoomRes);
                    }
                    if (messag.FollowListRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnFollowList, messag.FollowListRes);
                    }
                    if (messag.ChatRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnChat, messag.ChatRes);
                    }
                    if (messag.UserStatusQueryRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnUserStatusQuery, messag.UserStatusQueryRes);
                    }
                    if (messag.StartMatchRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnStartMatch, messag.StartMatchRes);
                    }
                    if (messag.MatchRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnMatchResponse, messag.MatchRes);
                    }
                    if (messag.AddLiveRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnAddLiveResponse, messag.AddLiveRes);
                    }
                    if (messag.ValidateOpenRoomRes != null)
                    {
                        MessageCenter.dispatch(MessageType.OnValidateOpenRoom, messag.ValidateOpenRoomRes);
                    }
                    break;
            }
        }
        catch (Exception ex)
			{
            
                Debug.Log( ex.Message);
            
        }
        return true;
    }
    public static bool AddTask(NetMessage msg)
    {
        lock (msgList)
        {
            msgList.Add(msg);
        }
        return true;
    }


    public enum NetTypeBorG { 
        FromB=0,
        FromG
    }
    public class NetMessage
    {
        NetTypeBorG netTypeBorG;
        C2GNetMessage c2GNetMessage;
        C2BNetMessage c2BNetMessage;
        public NetTypeBorG Net_TypeBorG { 
            get { return netTypeBorG; }
            set { netTypeBorG = value;}
        }
        public C2GNetMessage C2G_NetMessage { 
            get { return c2GNetMessage; }
            set { c2GNetMessage = value;}
        }
        public C2BNetMessage C2B_NetMessage { 
            get { return c2BNetMessage;}
            set { c2BNetMessage = value;} 
        }

        public NetMessage(C2GNetMessage _c2GNetMessage)
        {
            this.netTypeBorG = NetTypeBorG.FromG;
            this.c2GNetMessage = _c2GNetMessage;
        }
        public NetMessage( C2BNetMessage _c2BNetMessage)
        {
            this.netTypeBorG = NetTypeBorG.FromB;
            this.c2BNetMessage = _c2BNetMessage;
        }
    }
}
