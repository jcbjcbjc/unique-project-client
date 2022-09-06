using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshExtend
{
    public static class MeshExtendUtility
    {
        public static Vector2 CalculateCenter(params Vector2[] points)
        {
            Vector2 ret = Vector2.zero;
            foreach (Vector2 point in points)
            {
                ret += point;
            }
            ret /= points.Length;
            return ret;
        }

        /// <summary>
        /// 以所给点为原点，对点按顺时针排序
        /// </summary>
        public class Comparer_Vector2_Clockwise : IComparer<Vector2>, IComparer<Vector2Int>, IComparer<Vector3>
        {
            public Vector2 center;

            public Comparer_Vector2_Clockwise(Vector2 _center)
            {
                center = _center;
            }

            public int Compare(Vector2 x, Vector2 y)
            {
                return ((y - center).ToAngle() - (x - center).ToAngle()).Sign();
            }
            public int Compare(Vector2Int x, Vector2Int y)
            {
                return Compare((Vector2)x, (Vector2)y);
            }
            public int Compare(Vector3 x, Vector3 y)
            {
                return Compare((Vector2)x, (Vector2)y);
            }
        }

        /// <summary>
        /// 利用中心划分凸多边形，此方法会增加一个顶点
        /// </summary>
        /// <param name="points">边界的点，不需要按顺序传入，此方法中outline会被改变</param>
        /// <param name="triangles">接收结果,依次返回每个三角形（顺时针返回三角形的三个点坐标）</param>
        public static void DivideConvexPolygon_Center(List<Vector2> points, List<List<Vector2>> triangles)
        {
            int n = points.Count;
            if (n < 3)
                throw new ArgumentException();

            triangles.Clear();
            Vector2 center = CalculateCenter(points.ToArray());
            Comparer_Vector2_Clockwise comparer = new Comparer_Vector2_Clockwise(center);
            points.Sort(comparer);
            points.Add(center);
            for (int i = 0; i < n - 1; i++)
            {
                triangles.Add(new List<Vector2> { points[i], points[i + 1], points[n] });
            }
            triangles.Add(new List<Vector2> { points[n - 1], points[0], points[n] });
        }
    }
}