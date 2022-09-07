using MyTimer;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Services
{
    //此脚本用于服务初始化阶段，服务初始化完成后可摧毁
    [DefaultExecutionOrder(-1000)]
    public class ServiceInitializer : MonoBehaviour
    {
        public static ServiceInitializer Instance;
        public event UnityAction AfterService;

        internal ServiceSettings serviceSettings;
        [SerializeField]
        private float t_wait;

        private void Awake()
        {
            Instance = this;
            serviceSettings = Resources.Load("ServiceSettings") as ServiceSettings;
            StartCoroutine(WaitForInitailization());
        }

        internal void RefreshWaitTime()
        {
            t_wait = serviceSettings.t_wait;
        }

        private IEnumerator WaitForInitailization()
        {
            t_wait = serviceSettings.t_wait_init;
            for (; t_wait > 0f;)
            {
                t_wait -= Time.deltaTime;
                yield return null;
            }
            foreach (Service service in ServiceLocator.serviceDict.Values)
            {
                service.GetOtherService();
            }
            foreach (Service service in ServiceLocator.serviceDict.Values)
            {
                service.AfterInitailize();
            }
            AfterService?.Invoke();
        }
    }
}