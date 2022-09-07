using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.scripts.GameLogic
{
    public  class GameLogicMonobehavior:MonoBehaviour
    {
        /* 字段 */
        public static GameLogicMonobehavior Instance;

        GameLogicManager gameLogic = new GameLogicManager();

        public void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);
            Instance = this;
        }

        // Use this for initialization
        public void init()
        {
            //#if _CLIENTLOGIC_
            //        battleLogic.init();
            //#else
            gameLogic.init();
//#endif
        }

        // Update is called once per frame
        void Update()
        {
            gameLogic.updateLogic();
#if _CLIENTLOGIC_
        battleLogic.updateLogic();
#endif
        }

        public GameLogicManager getBattleLogic()
        {
            return gameLogic;
        }
    }
}
