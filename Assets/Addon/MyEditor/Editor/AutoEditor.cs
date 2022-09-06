using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public enum EFocusMode
    {
        Default,
        Lock
    }

    public abstract class AutoEditor : Editor
    {
        protected EFocusMode focusMode;
        protected Event currentEvent;
        protected Ray mouseRay;

        protected virtual void OnEnable()
        {
            FieldInfo[] infos = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (target != null)
            {
                foreach (FieldInfo info in infos)
                {
                    if (info.GetAttribute<AutoAttribute>() != null && info.FieldType == typeof(SerializedProperty))
                    {
                        try
                        {
                            info.SetValue(this, serializedObject.FindProperty(info.Name));
                        }
                        catch
                        {
                            Debug.Log($"{this.target.name}�Ҳ���{info.Name}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ͨ������Ӧ����д�˷�������Ӧ����дMyOnInspectorGUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical();

            MyOnInspectorGUI();

            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void MyOnInspectorGUI();


        protected virtual void OnSceneGUI()
        {

            currentEvent = Event.current;
            mouseRay = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
            switch (currentEvent.type)
            {
                //������
                case EventType.Layout:
                    switch (focusMode)
                    {
                        case EFocusMode.Lock:
                            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                            break;
                    }
                    break;
            }
            MyOnSceneGUI();
        }

        protected virtual void MyOnSceneGUI() { }
    }
}