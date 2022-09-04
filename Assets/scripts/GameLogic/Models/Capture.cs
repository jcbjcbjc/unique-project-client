using Assets.scripts.Message;
using Assets.scripts.Models;
using C2BNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.GameLogic.Models
{
    public class Capture : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //获取移动指令
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            if (h != 0)
            {
                var userId = User.Instance.user.Id;
                FrameHandle frameHandle = new FrameHandle
                {
                    UserId = userId,
                    Opt = 1,
                    OptValue1 = h,
                };
                MessageCenter.dispatch(MessageType.OnAddOptClient, frameHandle);
            }
            if (v != 0)
            {
                var userId = User.Instance.user.Id;
                FrameHandle frameHandle = new FrameHandle
                {
                    UserId = userId,
                    Opt = 2,
                    OptValue1 = v,
                };
                MessageCenter.dispatch(MessageType.OnAddOptClient, frameHandle);
            }

        }
    }
}
