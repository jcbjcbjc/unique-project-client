using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.Utils
{
    public class LogUtil
    {
        public static void log(string mm,object a=null,object b=null) {
            Debug.Log(mm+a+b);
        }
        //public static void log(string mm)
        //{
        //    Debug.Log(mm );
        //}
        //public static void log(string mm,object a)
        //{
        //    Debug.Log(mm+a);
        //}

    }
}
