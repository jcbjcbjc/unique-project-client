
using NetWork;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIMain : BaseUIForm
    {
        private EventSystem eventSystem;

        private void Awake()
        {
            eventSystem = ServiceLocator.Get<EventSystem>();
        }
       
        public void setMsg(string msg)
        {
            //this.msgLabel.string = msg;
        }

        public override void Close()
        {
            
            CloseUIForm();
        }

        public void Match()
        {
            ServiceLocator.Get<MatchService>().SendStartMatch();
       
        }
    }
}

