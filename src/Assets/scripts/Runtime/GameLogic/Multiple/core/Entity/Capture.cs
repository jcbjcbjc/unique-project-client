using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using NetWork;
using C2BNet;

public class Capture : MonoBehaviour
{
    EventSystem eventSystem_;
    // Start is called before the first frame update
    private void Awake()
    {
        eventSystem_ = ServiceLocator.Get<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (hor != 0 || ver != 0)
        {
            var frameHandle = new FrameHandle
            {
                UserId = ServiceLocator.Get<User>().NUser.Id,
                OpretionId = 1,
                OptValue1 = (int)Mathf.Round(hor),
                OptValue2 = (int)Mathf.Round(ver)
            };
            eventSystem_.Invoke<FrameHandle>(EEvent.OnAddOptClient, frameHandle);
        }
    }
}