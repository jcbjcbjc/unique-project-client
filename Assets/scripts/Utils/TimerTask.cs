using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.scripts.Utils
{
    public class TimerTask:ITask
    {
        private static int _interval = 1000;//¼ä¸ôÊ±¼ä
        private static bool _isRuning = false;
        private static Action _action = () =>
        {
            while (_isRuning)
            {
                taskAction();
                System.Threading.Thread.Sleep(_interval);
            }
        };
        
        private static Action taskAction;
        Task timer = new Task(_action);
        public TimerTask(int interval, Action action)
        {
            _interval = interval;
            taskAction = action;
        }
        public void execute()
        {
            _isRuning = true;
            if (timer == null)
                timer = new Task(_action);
            timer.Start();
        }
        public void Stop()
        {
            _isRuning = false;
            timer.Wait();
            timer.Dispose();
            timer = null;
        }
    }
}
