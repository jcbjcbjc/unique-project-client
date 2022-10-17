using C2GNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.scripts.Utils;
using C2BNet;
using UnityEngine;

namespace GameLogic
{
    public  class Character:LiveEntity
    {
        int userid_;
        string nickname_;
        int teamid_;
        int positionid_;
        int cCharacterId_;


        public int Userid
        {
            get { return userid_; }
            set { userid_ = value; }
        }
        public string Nickname
        {
            get { return nickname_; }
            set { nickname_ = value; }
        }
        public int Teamid
        {
            get { return teamid_; }
            set { teamid_ = value; }
        }
        public int Positionid { 
            get { return positionid_; }
            set { positionid_ = value; }
        }
        public int CCharacterId
        {
            get { return cCharacterId_; }
            set { cCharacterId_ = value; }
        }
        public void update(FrameHandle fh) {
            transform.Translate(-Vector3.left * fh.OptValue1 * 2);
            transform.Translate(Vector3.forward * fh.OptValue2 * 2);
        }
    }
}
