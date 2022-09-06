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
            //���屾���������(Ĭ����ֵ�����Բ�д)
            base.CurrentUIType.UIForms_Type = UIFormType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
            /* ����ťע���¼� */
            //RigisterButtonObjectEvent("Btn_OK", LogonSys);
            //Lamda���ʽд��
            

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

