using System;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public static class MyEditorGUI
    {
        public const float IndentPerLevel = 30f;

        public static Rect Layout(this Rect rect)
        {
            if (rect == default)
                return EditorGUILayout.GetControlRect();
            return rect;
        }

        /// <summary>
        /// 获取编辑器当前位置的宽度
        /// </summary>
        public static float GetWidth()
        {
            Rect rect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            return rect.width - EditorGUI.indentLevel * IndentPerLevel;
        }

        #region 基本数据类型

        public static void IntField(this SerializedProperty property, string label, Rect rect)
            => property.intValue = EditorGUI.IntField(rect.Layout(), label, property.intValue);
        public static void IntField(this SerializedProperty property, string label)
            => property.intValue = EditorGUILayout.IntField(label, property.intValue);
        public static void FloatField(this SerializedProperty property, string label, Rect rect)
           => property.floatValue = EditorGUI.FloatField(rect.Layout(), label, property.floatValue);
        public static void FloatField(this SerializedProperty property, string label)
           => property.floatValue = EditorGUILayout.FloatField(label, property.floatValue);
        public static void BoolField(this SerializedProperty property, string label, Rect rect)
            => property.boolValue = EditorGUI.Toggle(rect.Layout(), label, property.boolValue);
        public static void BoolField(this SerializedProperty property, string label)
           => property.boolValue = EditorGUILayout.Toggle(label, property.boolValue);

        #endregion

        #region 其他数据类型

        public static void PropertyField(this SerializedProperty property, string label, Rect rect)
            => EditorGUI.PropertyField(rect.Layout(), property, new GUIContent(label));
        public static void PropertyField(this SerializedProperty property, string label)
            => EditorGUILayout.PropertyField(property, new GUIContent(label));
        public static void EnumField<T>(this SerializedProperty property, string label, Rect rect) where T : Enum
           => property.enumValueIndex = EditorGUI.EnumPopup(rect.Layout(), label, property.enumValueIndex.ToEnum<T>()).ToInt();
        public static void EnumField<T>(this SerializedProperty property, string label) where T : Enum
           => property.enumValueIndex = EditorGUILayout.EnumPopup(label, property.enumValueIndex.ToEnum<T>()).ToInt();
        public static void TextField(this SerializedProperty property, string label, Rect rect)
           => property.stringValue = EditorGUI.TextField(rect.Layout(), label, property.stringValue);
        public static void TextField(this SerializedProperty property, string label)
          => property.stringValue = EditorGUILayout.TextField(label, property.stringValue);
        public static void TextArea(this SerializedProperty property, string label, Rect rect)
        {
            EditorGUILayout.LabelField(label);
            property.stringValue = EditorGUI.TextArea(rect.Layout(), property.stringValue);
        }
        public static void TextArea(this SerializedProperty property, string label)
        {
            EditorGUILayout.LabelField(label);
            property.stringValue = EditorGUILayout.TextArea(property.stringValue);
        }
        public static void ColorField(this SerializedProperty property, string label, Rect rect)
            => property.colorValue = EditorGUI.ColorField(rect.Layout(), label, property.colorValue);
        public static void ColorField(this SerializedProperty property, string label)
            => property.colorValue = EditorGUILayout.ColorField(label, property.colorValue);

        #endregion

        #region 矢量

        public static void Vector2IntField(this SerializedProperty property, string label, Rect rect)
            => property.vector2IntValue = EditorGUI.Vector2IntField(rect.Layout(), label, property.vector2IntValue);
        public static void Vector2IntField(this SerializedProperty property, string label)
            => property.vector2IntValue = EditorGUILayout.Vector2IntField(label, property.vector2IntValue);
        public static void Vector3IntField(this SerializedProperty property, string label, Rect rect)
           => property.vector3IntValue = EditorGUI.Vector3IntField(rect.Layout(), label, property.vector3IntValue);
        public static void Vector3IntField(this SerializedProperty property, string label)
           => property.vector3IntValue = EditorGUILayout.Vector3IntField(label, property.vector3IntValue);
        public static void Vector2Field(this SerializedProperty property, string label, Rect rect)
           => property.vector2Value = EditorGUI.Vector2Field(rect.Layout(), label, property.vector2Value);
        public static void Vector2Field(this SerializedProperty property, string label)
           => property.vector2Value = EditorGUILayout.Vector2Field(label, property.vector2Value);
        public static void Vector3Field(this SerializedProperty property, string label, Rect rect)
           => property.vector3Value = EditorGUI.Vector3Field(rect.Layout(), label, property.vector3Value);
        public static void Vector3Field(this SerializedProperty property, string label)
           => property.vector3Value = EditorGUILayout.Vector3Field(label, property.vector3Value);
        public static void Vector4Field(this SerializedProperty property, string label, Rect rect)
           => property.vector4Value = EditorGUI.Vector4Field(rect.Layout(), label, property.vector4Value);
        public static void Vector4Field(this SerializedProperty property, string label)
           => property.vector4Value = EditorGUILayout.Vector4Field(label, property.vector4Value);
        public static void RectIntField(this SerializedProperty property, string label, Rect rect)
          => property.rectIntValue = EditorGUI.RectIntField(rect.Layout(), label, property.rectIntValue);
        public static void RectIntField(this SerializedProperty property, string label)
         => property.rectIntValue = EditorGUILayout.RectIntField(label, property.rectIntValue);
        public static void RectField(this SerializedProperty property, string label, Rect rect)
          => property.rectValue = EditorGUI.RectField(rect.Layout(), label, property.rectValue);
        public static void RectField(this SerializedProperty property, string label)
          => property.rectValue = EditorGUILayout.RectField(label, property.rectValue);

        #endregion

        #region 滑动条

        public static void IntSlider(this SerializedProperty property, string label, int min, int max, Rect rect)
            => property.intValue = EditorGUI.IntSlider(rect.Layout(), label, property.intValue, min, max);
        public static void IntSlider(this SerializedProperty property, string label, int min, int max)
           => property.intValue = EditorGUILayout.IntSlider(label, property.intValue, min, max);
        public static void Slider(this SerializedProperty property, string label, float min, float max, Rect rect)
            => property.floatValue = EditorGUI.Slider(rect.Layout(), label, property.floatValue, min, max);
        public static void Slider(this SerializedProperty property, string label, float min, float max)
            => property.floatValue = EditorGUILayout.Slider(label, property.floatValue, min, max);
        public static void MinMaxSlider(SerializedProperty smaller, SerializedProperty bigger, string label, float min, float max, Rect rect)
        {
            float minValue = smaller.floatValue;
            float maxValue = bigger.floatValue;
            EditorGUI.MinMaxSlider(rect.Layout(), label, ref minValue, ref maxValue, min, max);
            smaller.floatValue = minValue;
            bigger.floatValue = maxValue;
        }
        public static void MinMaxSlider(SerializedProperty smaller, SerializedProperty bigger, string label, float min, float max)
        {
            float minValue = smaller.floatValue;
            float maxValue = bigger.floatValue;
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, min, max);
            smaller.floatValue = minValue;
            bigger.floatValue = maxValue;
        }

        public static void IntMinMaxSlider(SerializedProperty smaller, SerializedProperty bigger, string label, int min, int max, Rect rect)
        {
            float minValue = smaller.intValue;
            float maxValue = bigger.intValue;
            EditorGUI.MinMaxSlider(rect.Layout(), label, ref minValue, ref maxValue, min, max);
            smaller.intValue = (int)minValue;
            bigger.intValue = (int)maxValue;
        }
        public static void IntMinMaxSlider(SerializedProperty smaller, SerializedProperty bigger, string label, int min, int max)
        {
            float minValue = smaller.intValue;
            float maxValue = bigger.intValue;
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, min, max);
            smaller.intValue = (int)minValue;
            bigger.intValue = (int)maxValue;
        }

        #endregion

        #region 集合
        public static void ListField(this SerializedProperty property, string label, Rect rect)
         => EditorGUI.PropertyField(rect, property, new GUIContent(label), true);
        public static void ListField(this SerializedProperty property, string label)
           => EditorGUILayout.PropertyField(property, new GUIContent(label), true);

        #endregion

    }
}