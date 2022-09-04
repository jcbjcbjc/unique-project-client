using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C2GNet;
/// <summary>
/// GameLogicLoginService
/// 
/// @Author ¼Ö³¬²©
/// 
/// @Date 2022/4/30
/// </summary>
namespace Assets.scripts.Models
{
    public class User 
    {
        public static User _instance;
        public static User Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new User();
                }
                return _instance;
            }
        }

        public NUser user=null;
        public bool isLogin = false;  //ÊÇ·ñµÇÂ¼
        public NRoom room = null;  //·¿¼ä

    }

}