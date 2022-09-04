using Assets.scripts.Managers;
using Assets.scripts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.UI.Common
{
    public class UIMessageBox : BaseUIForm
    {

        private void Awake()
        {
            base.CurrentUIType.UIForms_Type = UIFormType.PopUp;

            base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence/*.Pentrate*/;
        }
        public void update(string message, string title = "", MessageBoxType type = MessageBoxType.Information, string btnOK = "", string btnCancel = "")
        {



        }
        
        private void OnClickYes()
        {
            //console.log('OnClickYes')
            //SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Confirm);
            MessageCenter.dispatch(MessageType.UIMessageBox_OnClickYes, 0);
            Close();
        }

        private void OnClickNo()
        {
            //console.log('OnClickNo'+this.uuid)
            //SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Win_Close);
            MessageCenter.dispatch(MessageType.UIMessageBox_OnClickNo, 0);
            Close();
        }

        public override void Close() {
            MessageCenter.RemoveMsgListener(this);
            CloseUIForm();
        }

    }

    public enum MessageBoxType
    {
        /// <summary>
        /// Information Dialog with OK button
        /// </summary>
        Information = 1,

        /// <summary>
        /// Confirm Dialog whit OK and Cancel buttons
        /// </summary>
        Confirm = 2,

        /// <summary>
        /// Error Dialog with OK buttons
        /// </summary>
        Error = 3
    }
}
