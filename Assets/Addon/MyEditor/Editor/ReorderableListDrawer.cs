using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace MyEditor
{
    public class ReorderableListDrawer
    {
        /// <summary>
        /// 为 ReoderableList创建并绑定默认的elementHeightCallback、drawHeaderCallback
        /// </summary>
        /// <returns>需要在drawElementCallback调用DrawElemntHelper中的方法</returns>
        public static DrawElemntHelper SetDefaultCallBack(ReorderableList list, string header)
        {
            ReoderableListCallback callback = new ReoderableListCallback(list, header);
            return callback.helper;
        }

        public ReorderableList list;
        public DrawElemntHelper helper;
        public UnityAction<Func<Rect>, int> DrawElement;

        /// <summary>
        /// 快捷创建ReorderableList的绘制器
        /// </summary>
        /// <param name="serializedObject">要绘制到哪个serializedObject上</param>
        /// <param name="serializedProperty">要通过哪个serializedProperty创建ReorderableList</param>
        /// <param name="DrawElement_">绘制列表中单个元素的方法，通常只需要包含绘制API的调用（在此方法中，需要获取Rect时，均应调用Func<Rect>）</param>
        /// <param name="header">标题</param>
        /// <param name="b_default">是否绑定默认的回调方法</param>
        public ReorderableListDrawer(SerializedObject serializedObject, SerializedProperty serializedProperty, UnityAction<Func<Rect>, int> DrawElement_, string header, bool b_default = true)
        {
            DrawElement = DrawElement_;
            list = new ReorderableList(serializedObject, serializedProperty)
            {
                drawElementCallback = DrawElemntCallBack,
            };
            if (b_default)
                helper = SetDefaultCallBack(list, header);
        }

        public virtual void OnInspectorGUI()
        {
            list.DoLayoutList();
        }

        private void DrawElemntCallBack(Rect rect, int index, bool isActive, bool isFocused)
        {
            helper.RecordHeight(rect, index);
            EditorGUI.indentLevel++;
            DrawElement?.Invoke(helper.GetRect, index);
            EditorGUI.indentLevel--;
        }
    }

    public class ReoderableListCallback
    {
        private readonly ReorderableList list;
        public DrawElemntHelper helper;
        public string header;

        public ReoderableListCallback(ReorderableList _list, string _header)
        {
            list = _list;
            header = _header;
            list.elementHeightCallback += GetElementHeight;
            list.drawHeaderCallback += DrawHeader;
            helper = new DrawElemntHelper();
        }

        public void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, header);
        }

        public void DrawFooter(Rect rect)
        {
            EditorGUI.LabelField(rect, "");
        }
        //绘制列表时，会先调用此方法，再调用绘制元素的方法，而元素高度要绘制过元素才能确定
        public float GetElementHeight(int index)
        {
            if (index >= helper.heights.Count)
                return EditorGUIUtility.singleLineHeight;   //不知道为什么，返回任何非0值都能正确地自动缩放
            return helper.heights[index];
        }
    }

    public class DrawElemntHelper
    {
        private int currentIndex;
        public List<float> heights;

        public static Vector2 Delta = new Vector2(0f, EditorGUIUtility.singleLineHeight);

        public Vector2 currentPosition;

        public DrawElemntHelper()
        {
            heights = new List<float>();
        }

        /// <summary>
        /// 每次DrawElemntCallBack开始时都应该调用此方法
        /// </summary>
        public void RecordHeight(Rect rect, int index)
        {
            currentIndex = index;
            for (; heights.Count <= index;)
            {
                heights.Add(default);
            }
            heights[currentIndex] = 0f;
            currentPosition = rect.position;
        }

        /// <summary>
        /// 用于drawElementCallback的获取Rect的方法
        /// </summary>
        public Rect GetRect()
        {
            Rect ret = new Rect(currentPosition, new Vector2(MyEditorGUI.GetWidth(), EditorGUIUtility.singleLineHeight));
            currentPosition += Delta;
            heights[currentIndex] += EditorGUIUtility.singleLineHeight;
            return ret;
        }
    }
}