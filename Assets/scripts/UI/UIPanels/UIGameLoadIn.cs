using Assets.scripts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.UI.UIPanels
{
    public class UIGameLoadIn:BaseUIForm
    {

        public void setMsg(string msg)
        {
            //this.msgLabel.string = msg;
        }

        public override void Close()
        {
            MessageCenter.RemoveMsgListener(this);
            CloseUIForm();
        }
    }
}
