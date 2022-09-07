using System.Reflection;
using UnityEditor;

namespace MyEditor
{
    /// <summary>
    /// ���ڷ�Object���࣬������Ҫ�Զ���༭������
    /// ������������ΪSerializedProperty������Editor���У�Editor����ʹ�ô���
    /// �̳д���ʱ�����е�SerializedPropertyӦ����Ϊpublic
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