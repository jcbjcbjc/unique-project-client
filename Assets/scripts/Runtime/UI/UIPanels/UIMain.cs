using Assets.scripts.Message;
using Assets.scripts.NetWork.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.UI.UIPanels
{
    public class UIMain : BaseUIForm
    {
        public void Awake()
        {
            //定义本窗体的性质(默认数值，可以不写)
            base.CurrentUIType.UIForms_Type = UIFormType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
            /* 给按钮注册事件 */
            //RigisterButtonObjectEvent("Btn_OK", LogonSys);
            //Lamda表达式写法
            

        }
        public void setMsg(string msg)
        {
            //this.msgLabel.string = msg;
        }

        public override void Close()
        {
            MessageCenter.RemoveMsgListener(this);
            CloseUIForm();
        }

        public void Match()
        {
            MatchService.GetInstance().SendStartMatch();
        }
    }
}

