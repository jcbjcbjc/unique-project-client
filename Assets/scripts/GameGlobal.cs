using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.scripts.Utils.enums.BattleModeEnum;
using UnityEngine;
using Assets.scripts.NetWork.NetClient;

namespace Assets.scripts
{
    public class GameGlobal:MonoBehaviour
    {
        public void Awake()
        {
            LoadManager.Instance.init();
            DontDestroyOnLoad(this);
        }
        private void OnDestroy()
        {
            LoadManager.Instance.Close();
            
        }


    }
}
