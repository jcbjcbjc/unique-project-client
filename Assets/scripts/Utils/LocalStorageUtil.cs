using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Utils
{
    public class LocalStorageUtil
    {

        public static string privateMessagesKey="privateMessagesKey"; 
        public static string allFrameHandlesKey ="allFrameHandlesKey"; 
        public static string GetItem(string key)
        {

            return PlayerPrefs.GetString(key);



        }

        public static bool SetItem(string key, string value)
        {

            PlayerPrefs.SetString(key, value);
            return true;
        }
        public static bool RemoveItem(string key)
        {
            PlayerPrefs.DeleteKey(key);
            return true;
        }
       
    }
}
