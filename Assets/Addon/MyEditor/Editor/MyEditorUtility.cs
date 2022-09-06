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
        /// 世界坐标尺寸转Scene窗口尺寸（用屏幕高度的倍数表示）
        /// </summary>
        public static float WorldToSceneSize(float size)
        {
            return size / SceneView.currentDrawingSceneView.camera.orthographicSize / 2;
        }
        /// <summary>
        /// Scene窗口尺寸（用屏幕高度的倍数表示）转世界坐标尺寸
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
        /// 计算某点到某射线原点的向量在该射线方向上的投影的长度
        /// </summary>
        public static float DistanceOnDirection(Ray ray, Vector3 point)
        {
            Vector3 v = point - ray.origin;
            return Vector3.Dot(v, ray.direction);
        }
        /// <summary>
        /// 过指定点作与射线方向垂直的平面，返回与射线的交点
        /// </summary>
        public static Vector3 GetPointOnRay(Ray ray, Vector3 worldPosition)
        {
            return ray.GetPoint(DistanceOnDirection(ray, worldPosition));
        }
    }
}