using System;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public static class MyEditorUtility
    {
        public static T ToEnum<T>(this int enumIndex) where T : Enum
            => (T)Enum.ToObject(typeof(T), enumIndex);
        public static int ToInt(this Enum e)
            => e.GetHashCode();

        /// <summary>
        /// ��������ߴ�תScene���ڳߴ磨����Ļ�߶ȵı�����ʾ��
        /// </summary>
        public static float WorldToSceneSize(float size)
        {
            return size / SceneView.currentDrawingSceneView.camera.orthographicSize / 2;
        }
        /// <summary>
        /// Scene���ڳߴ磨����Ļ�߶ȵı�����ʾ��ת��������ߴ�
        /// </summary>
        public static float ScreenToWorldSize(float size)
        {
            return size * SceneView.currentDrawingSceneView.camera.orthographicSize * 2;
        }

        public static Vector2 Projection(Vector2 v, Vector2 target)
        {
            if (target == Vector2.zero)
                return Vector2.zero;
            target = target.normalized;
            return target * Vector2.Dot(v, target);
        }
        public static bool Parallel(Vector3 v1, Vector3 v2)
        {
            return Vector3.Cross(v1, v2) == Vector3.zero || Vector3.Cross(v1, -v2) == Vector3.zero;
        }
        /// <summary>
        /// ����ĳ�㵽ĳ����ԭ��������ڸ����߷����ϵ�ͶӰ�ĳ���
        /// </summary>
        public static float DistanceOnDirection(Ray ray, Vector3 point)
        {
            Vector3 v = point - ray.origin;
            return Vector3.Dot(v, ray.direction);
        }
        /// <summary>
        /// ��ָ�����������߷���ֱ��ƽ�棬���������ߵĽ���
        /// </summary>
        public static Vector3 GetPointOnRay(Ray ray, Vector3 worldPosition)
        {
            return ray.GetPoint(DistanceOnDirection(ray, worldPosition));
        }
    }
}