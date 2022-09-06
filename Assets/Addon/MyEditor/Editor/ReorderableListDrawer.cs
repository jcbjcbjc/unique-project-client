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
        /// Ϊ ReoderableList��������Ĭ�ϵ�elementHeightCallback��drawHeaderCallback
        /// </summary>
        /// <returns>��Ҫ��drawElementCallback����DrawElemntHelper�еķ���</returns>
        public static DrawElemntHelper SetDefaultCallBack(ReorderableList list, string header)
        {
            ReoderableListCallback callback = new ReoderableListCallback(list, header);
            return callback.helper;
        }

        public ReorderableList list;
        public DrawElemntHelper helper;
        public UnityAction<Func<Rect>, int> DrawElement;

        /// <summary>
        /// ��ݴ���ReorderableList�Ļ�����
        /// </summary>
        /// <param name="serializedObject">Ҫ���Ƶ��ĸ�serializedObject��</param>
        /// <param name="serializedProperty">Ҫͨ���ĸ�serializedProperty����ReorderableList</param>
        /// <param name="DrawElement_">�����б��е���Ԫ�صķ�����ͨ��ֻ��Ҫ��������API�ĵ��ã��ڴ˷����У���Ҫ��ȡRectʱ����Ӧ����Func<Rect>��</param>
        /// <param name="header">����</param>
        /// <param name="b_default">�Ƿ��Ĭ�ϵĻص�����</param>
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
        //�����б�ʱ�����ȵ��ô˷������ٵ��û���Ԫ�صķ�������Ԫ�ظ߶�Ҫ���ƹ�Ԫ�ز���ȷ��
        public float GetElementHeight(int index)
        {
            if (index >= helper.heights.Count)
                return EditorGUIUtility.singleLineHeight;   //��֪��Ϊʲô�������κη�0ֵ������ȷ���Զ�����
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
        /// ÿ��DrawElemntCallBack��ʼʱ��Ӧ�õ��ô˷���
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
        /// ����drawElementCallback�Ļ�ȡRect�ķ���
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