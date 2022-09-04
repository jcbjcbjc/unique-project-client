using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// GameLogicLoginService
/// 
/// @Author 贾超博
/// 
/// @Date 2022/4/30
/// </summary>

namespace Assets.scripts.Message
{
    public class MessageCenter
    {
        //委托：消息传递
        public delegate void DelMessageDelivery(object obj);

        //消息中心缓存集合
        //<string : 数据大的分类，DelMessageDelivery 数据执行委托>
        public static Dictionary<string, DelMessageDelivery> _dicMessages = new Dictionary<string, DelMessageDelivery>();
        public static Dictionary<string, DelMessageDelivery> _dicOnceMessages = new Dictionary<string, DelMessageDelivery>();

        public static Dictionary<object, List< Tuple <string, DelMessageDelivery>>  > _dicobj = new Dictionary<object, List<Tuple<string, DelMessageDelivery>>>();
        /// <summary>
        /// 增加消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler,object listenerobj)
        {

            if (!_dicMessages.ContainsKey(messageType))
            {
                _dicMessages.Add(messageType, null);
            }
            _dicMessages[messageType] += handler;

            if (!_dicobj.ContainsKey(listenerobj))
            {
                _dicobj.Add(listenerobj, null);
                _dicobj[listenerobj]=new List<Tuple<string, DelMessageDelivery>>();
            }
            _dicobj[listenerobj].Add(new Tuple<string, DelMessageDelivery>(messageType, handler));
        }
        public static void AddMsgOnceListener(string messageType, DelMessageDelivery handler)
        {

            if (!_dicOnceMessages.ContainsKey(messageType))
            {

                _dicOnceMessages.Add(messageType, null);
            }
            _dicOnceMessages[messageType] += handler;
        }

        /// <summary>
        /// 取消消息的监听
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handele">消息委托</param>
        public static void RemoveMsgListener(string messageType, DelMessageDelivery handele, object listenerobj)
        {
            if (_dicMessages.ContainsKey(messageType))
            {
                _dicMessages[messageType] -= handele;
            }
            if (!_dicobj.ContainsKey(listenerobj))
            {
                _dicobj.Add(listenerobj, null);
                return;
            }
            List<Tuple<string, DelMessageDelivery>> tuples = _dicobj[listenerobj];
            foreach (Tuple<string, DelMessageDelivery> tuple in tuples)
            {
                if (tuple.Item1 == messageType && tuple.Item2 == handele) {
                    _dicobj[listenerobj].Remove(tuple);
                }
            }
        }
        
        public static void RemoveMsgListener(object listenerobj)
        {
            if (!_dicobj.ContainsKey(listenerobj))
            {
                _dicobj.Add(listenerobj, null);
                return;
            }
            List<Tuple<string, DelMessageDelivery>> tuples = _dicobj[listenerobj];
            
            foreach (Tuple<string, DelMessageDelivery> tuple in tuples) {
                if (_dicMessages.ContainsKey(tuple.Item1))
                {
                    _dicMessages[tuple.Item1] -= tuple.Item2;
                }
            }
            _dicobj[listenerobj] = null;
        }
        public static void RemoveMsgOnceListener(string messageType, DelMessageDelivery handele)
        {
            if (_dicOnceMessages.ContainsKey(messageType))
            {
                _dicOnceMessages[messageType] -= handele;
            }
        }
        /// <summary>
        /// 取消所有指定消息的监听
        /// </summary>
        public static void ClearALLMsgListener()
        {
            if (_dicMessages != null)
            {
                _dicMessages.Clear();
            }
            if (_dicobj != null)
            {
                _dicobj.Clear();
            }
        }
        public static void ClearALLMsgOnceListener()
        {
            if (_dicOnceMessages != null)
            {
                _dicOnceMessages.Clear();
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息的分类</param>
        /// <param name="kv">键值对(对象)</param>
        public static void dispatch(string messageType, object obj)
        {
            DelMessageDelivery del;                         //委托

            if (_dicMessages.TryGetValue(messageType, out del))
            {

                if (del != null)
                {
                    //调用委托
                    del(obj);
                    return;
                }
                else
                {
                    Debug.Log("Normaldel == null!, Please check the messageType ！ param messageType = " + messageType);
                    return;
                }
            }
            else if (_dicOnceMessages.TryGetValue(messageType, out del)) {
                if (del != null)
                {
                    //调用委托
                    del(obj);
                    _dicOnceMessages[messageType]=null;
                    _dicOnceMessages.Remove(messageType);
                    return;
                }
                else
                {
                    Debug.Log("Oncedel == null!, Please check the messageType ！ param messageType = " + messageType);
                    return;
                }
            }
        }
    }
}
