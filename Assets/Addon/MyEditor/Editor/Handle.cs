using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    public static class Handle
    {
        public static void DrawLine(Vector3 p1, Vector3 p2, float thickness)
        {
            if (p1 != p2)
                Handles.DrawLine(p1, p2, thickness);
        }

        public static void DrawLines(Vector3[] points, float thickness, bool closed = false)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                DrawLine(points[i], points[i + 1], thickness);
            }
            if (closed && points.Length > 1)
                DrawLine(points[points.Length - 1], points[0], thickness);
        }
        public static void DrawLines(List<Vector3> points, float thickness, bool closed = false)
            => DrawLines(points.ToArray(), thickness, closed);
        public static void DrawDot(Vector3 center, float size)
            => Handles.DotHandleCap(0, center, Quaternion.identity, size, EventType.Repaint);
    }
}