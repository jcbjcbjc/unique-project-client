//
// @brief: 公用数据类
// @version: 1.0.0
// @author helin
// @date: 03/7/2018
// 
// 
//


using C2BNet;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static Assets.scripts.Utils.enums.BattleModeEnum;
using static Assets.scripts.Utils.enums.GameStatusEnum;

public class GameData {
    #region Data For Steplock
    //player frame operations
    public static FrameHandlesFromClient frameHandles = new FrameHandlesFromClient();

    //next operation Id
    public static int NextOperationId = 1;

    //已经处理的帧
    public static int handleFrameId = -1;

    //已经执行的帧
    public static int executeFrameId = -1;

    //all frame operations                         
    public static SortedDictionary<int, IList<FrameHandle>> allFrameHandles = new SortedDictionary<int, IList<FrameHandle>>();  //所有的帧操作

    //predicted Input for player
    public static List<FrameHandle> PredictedInput = new List<FrameHandle>();

    //补帧等待帧数
    public static int repairWaitFrameCount = 5 * 7;

    //当前执行补帧
    public static int currentRepairFrame = 0;  

    //最新帧
    public static int newFrameId = -1;

    //直播未执行帧数
    public static int liveNotExecuteFrameCount = 0;  

    //最后接收帧时间
    public static float lastReceiveFrameTime = 0;

    //最后抽查时间
    public static float lastCheckFrameTime = 0; 

    #endregion

    #region Data For GameStatus
    //Game status
    public static GameStatus gameStatus = GameStatus.None;

    //Game Control
    public static bool isInGame = false;

    #endregion

    #region Data For GameConfig
    //Game Mode
    public static BattleMode battleMode = BattleMode.None;
    #endregion
    //所有士兵的队列
    public static List<BaseSoldier> g_listSoldier = new List<BaseSoldier>();

    //所有塔的队列
    public static List<BaseTower> g_listTower = new List<BaseTower>();

    //所有子弹的队列
    public static List<BaseBullet> g_listBullet = new List<BaseBullet>();

    //所有操作事件的队列
    public static List<battleInfo> g_listUserControlEvent = new List<battleInfo>();

    //所有回放事件的队列
    public static List<battleInfo> g_listPlaybackEvent = new List<battleInfo>();

    //预定的每帧的时间长度
    public static Fix64 g_fixFrameLen = Fix64.FromRaw(273);

    //游戏的逻辑帧
    public static int g_uGameLogicFrame = 0;

    //是否为回放模式
    public static bool g_bRplayMode = false;

    //士兵工厂
    public static SoldierFactory g_soldierFactory = new SoldierFactory();

    //塔工厂
    public static TowerFactory g_towerFactory = new TowerFactory();

    //action主管理器(用于管理各liveobject内部的独立actionManager)
    public static ActionMainManager g_actionMainManager = new ActionMainManager();

    //子弹管理器
    public static BulletFactory g_bulletManager = new BulletFactory();

    //战斗是否结束
    public static bool g_bBattleEnd = false;

    //随机数对象
    public static SRandom g_srand = new SRandom(1000);

    public struct battleInfo
    {
        public int uGameFrame;
        public string sckeyEvent;
    }

    public static void release_data_for_steplock() {
        GameData.NextOperationId = 1;
        
         handleFrameId = -1;

        
         executeFrameId = -1;

                         
         allFrameHandles = new SortedDictionary<int, IList<FrameHandle>>();  

         PredictedInput = new List<FrameHandle>();

         repairWaitFrameCount = 5 * 7;

         currentRepairFrame = 0;

         newFrameId = -1;

         liveNotExecuteFrameCount = 0;

         lastReceiveFrameTime = 0;

         lastCheckFrameTime = 0;
    }
    public static void release_data_for_gamestatus()
    {
        gameStatus = GameStatus.None;
        isInGame = false;
    }

    //- 释放资源
    // 
    // @return none
    public static void release() {
        g_listPlaybackEvent.Clear();

        g_listUserControlEvent.Clear();

        GameData.g_actionMainManager.release();

        release_data_for_steplock();
        release_data_for_gamestatus();
    }
}
