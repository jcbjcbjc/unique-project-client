using System.Reflection;
using UnityEditor;

namespace MyEditor
{
    /// <summary>
    /// 用于非Object子类，但是需要自定义编辑器的类
    /// 此类管理的类作为SerializedProperty出现在Editor类中，Editor类间接使用此类
    /// 继承此类时，所有的SerializedProperty应声明为public
    /// </summary>
    public abstract class IndirectEditor
    {
        public bool foldout;
        public string m_label;

        public virtual void Initialize(SerializedProperty serializedProperty, string label)
        {
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo info in fields)
            {
                if (info.HasAttribute<AutoAttribute>() && info.FieldType == typeof(SerializedProperty))
                    info.SetValue(this, serializedProperty.FindPropertyRelative(info.Name));
            }
            foldout = false;
            m_label = label;
        }

        public virtual void OnInspectorGUI(bool foldable = true)
        {
            if (foldable)
            {
                foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, m_label);
                if (foldout)
                    MyOnInspectorGUI();
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            else
                MyOnInspectorGUI();
        }

        protected abstract void MyOnInspectorGUI();
    }
}