using Assets.scripts.Message;
using Assets.scripts.Models;
using C2BNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void left() {
        var userId = User.Instance.user.Id;
        FrameHandle frameHandle = new FrameHandle
        {
            UserId = userId,
            Opt = 1,
            OptValue1 = (float)-0.1,
        };
        MessageCenter.dispatch(MessageType.OnAddOptClient, frameHandle);
    }
    public void right() {
        var userId = User.Instance.user.Id;
        FrameHandle frameHandle = new FrameHandle
        {
            UserId = userId,
            Opt = 1,
            OptValue1 = (float)0.1,
        };
        MessageCenter.dispatch(MessageType.OnAddOptClient, frameHandle);
    }
}
