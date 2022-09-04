using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// GameLogicLoginService
/// 
/// @Author 贾超博
/// 
/// @Date 2022/4/30
/// </summary>
namespace Assets.scripts.Message
{
    public static class MessageType
    {
        #region Net Message



        #endregion
        public static readonly string OnUserRegister="OnUserRegister";
        public static readonly string OnUserLogin = "OnUserLogin";
        public static readonly string OnUnLock = "OnUnLock";
        public static readonly string OnStatusNotify = "OnStatusNotify";
        public static readonly string OnCharacterDetail = "OnCharacterDetail";
        public static readonly string OnSwitchCharacter = "OnSwitchCharacter";
        public static readonly string OnAttrPromoteInfo = "OnAttrPromoteInfo";
        public static readonly string OnBagHandle = "OnBagHandle";
        public static readonly string OnItemBuy = "OnItemBuy";
        public static readonly string OnCombatPowerRanking = "OnCombatPowerRanking";
        public static readonly string OnFollowRes = "OnFollowRes";
        public static readonly string OnUserStatusChange = "OnUserStatusChange";
        public static readonly string OnQueryTran = "OnQueryTran";
        public static readonly string OnBuyTran="OnBuyTran";
        public static readonly string OnHeartBeat = "OnHeartBeat";
        public static readonly string OnOfferPrice="OnOfferPrice";
        public static readonly string OnAuction="OnAuction";
        public static readonly string OnGetAuctionList="OnGetAuctionList";
        public static readonly string OnTips="OnTips";
        public static readonly string OnMyRoom="OnMyRoom";
        public static readonly string OnInviteResponse="OnInviteResponse";
        public static readonly string OnInviteRequest="OnInviteRequest";
        public static readonly string OnKickOut="OnKickOut";
        public static readonly string OnRoomStartGame="OnRoomStartGame";
        public static readonly string OnNickNameSearch="OnNickNameSearch";
        public static readonly string OnAddRoomRequest="OnAddRoomRequest";
        public static readonly string OnAddRoomResponse="OnAddRoomResponse";
        public static readonly string OnOutRoom="OnOutRoom";
        public static readonly string OnFollowList="OnFollowList";
        public static readonly string OnChat="OnChat";
        public static readonly string OnUserStatusQuery="OnUserStatusQuery";
        public static readonly string OnStartMatch="OnStartMatch";
        public static readonly string OnMatchResponse="OnMatchResponse";
        public static readonly string OnFrameHandle="OnFrameHandle";
        public static readonly string OnPercentForward="OnPercentForward";
        public static readonly string OnRepairFrame="OnRepairFrame";
        public static readonly string OnAddLiveResponse="OnAddLiveResponse";
        public static readonly string OnLiveFrame="OnLiveFrame";
        public static readonly string OnValidateOpenRoom="OnValidateOpenRoom";

        #region Inner Message




        #endregion
        public static readonly string UIMessageBox_OnClickYes = "UIMessageBox_OnClickYes";
        public static readonly string UIMessageBox_OnClickNo="UIMessageBox_OnClickNo";
        public static readonly string UIWindow_OnClose = "UIWindow_OnClose";
        public static readonly string TabView_OnTabSelect = "TabView_OnTabSelect";
        public static readonly string ListView_OnItemSelected = "ListView_OnItemSelected";
        public static readonly string UICharacterSelect_list = "UICharacterSelect_list";
        public static readonly string OnCharacterDetail_UI = "OnCharacterDetail_UI";
        public static readonly string OnSwitchCharacter_UI = "OnSwitchCharacter_UI";
        public static readonly string OnAttrPromoteInfo_UI="OnAttrPromoteInfo_UI";
        public static readonly string OnBagHandle_UI="OnBagHandle_UI";
        public static readonly string UIInputBox_OnClickYes="UIInputBox_OnClickYes";
        public static readonly string UIInputBox_OnClickNo = "UIInputBox_OnClickNo";
        public static readonly string UserCoinChange = "UserCoinChange";
        public static readonly string OnCombatPowerRanking_UI = "OnCombatPowerRanking_UI";
        public static readonly string OnFollowRes_UI = "OnFollowRes_UI";
        public static readonly string CombatPowerRankingRefieshUI = "CombatPowerRankingRefieshUI";
        public static readonly string OnUserStatusChange_UI = "OnUserStatusChange_UI";
        public static readonly string OnQueryTran_UI = "OnQueryTran_UI";
        public static readonly string OnBuyTran_UI = "OnBuyTran_UI";
        public static readonly string OnHeartBeat_UI = "OnHeartBeat_UI";
        public static readonly string AuctionRefreshUI = "AuctionRefreshUI";
        public static readonly string OnGetAuctionList_UI = "OnGetAuctionList_UI";
        public static readonly string UIBag_OnClickSelected = "UIBag_OnClickSelected";
        public static readonly string OnOfferPrice_UI = "OnOfferPrice_UI";
        public static readonly string OnAuction_UI = "OnAuction_UI";
        public static readonly string OnMyRoom_UI = "OnMyRoom_UI";
        public static readonly string OnKickOut_UI = "OnKickOut_UI";
        public static readonly string OnRoomStartGame_UI = "OnRoomStartGame_UI";
        public static readonly string OnNickNameSearch_UI = "OnNickNameSearch_UI";
        public static readonly string OnMyRoom_RefieshUI = "OnMyRoom_RefieshUI";
        public static readonly string FollowListRefieshUI = "FollowListRefieshUI";
        public static readonly string OnChat_UI = "OnChat_UI";
        public static readonly string OnUserStatusQuery_UI = "OnUserStatusQuery_UI";
        public static readonly string OnPrivateUserStatusChange = "OnPrivateUserStatusChange";
        public static readonly string OnChangeRedNum = "OnChangeRedNum";
        public static readonly string OnBattleGameIn = "OnBattleGameIn";
        public static readonly string OnItemBuy_UI = "OnItemBuy_UI";
        public static readonly string OnWorldElementExecuteOnceSuccess = "OnWorldElementExecuteOnceSuccess";
        public static readonly string OnAddOptClient = "OnAddOptClient";
    }
}
