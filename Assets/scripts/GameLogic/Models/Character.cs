using Assets.scripts.Models;
using C2BNet;
using C2GNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.GameLogic.Models
{
    public class Character :LiveObject
    {
        public NUser user;
        int userid;
        string nickname;
        int teamid;
        int cCharacterId;
        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        public int Teamid
        {
            get { return teamid; }
            set { teamid = value; }
        }
        public int CCharacterId
        {
            get { return cCharacterId; }
            set { cCharacterId = value; }
        }

        public void init(int userid, string nickname, int teamid, int cCharacter)
        {
            Userid = userid;
            Nickname = nickname;
            Teamid = teamid;
            CCharacterId = cCharacter;
        }

        public void CharacterUpdate(FrameHandle fh) { 



        }


        override public void updateRenderPosition(float interpolation)
        {

            if (m_bKilled)
            {
                return;
            }
            //只有会移动的对象才需要采用插值算法补间动画,不会移动的对象直接设置位置即可
            
             m_gameObject.transform.localPosition = Vector3.Lerp(m_fixv3LastPosition.ToVector3(), m_fixv3LogicPosition.ToVector3(), interpolation);
            
        }


    }


}
