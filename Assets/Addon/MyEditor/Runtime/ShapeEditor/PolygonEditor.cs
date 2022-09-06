using System.Collections.Generic;
using UnityEngine;

namespace MyEditor.ShapeEditor
{
    public enum EPolygonStyle
    {
        ClosedPolyLine,
        PolyLine,
        Point
    }

    public class PolygonEditor : MonoBehaviour
    {
        public EPolygonStyle style;
        public List<Vector3> localPoints;
        public Bounds Bounds
        {
            get
            {
                List<Vector3> points = new List<Vector3>();
                GetWorldPoints(points);
                Rect rect = GeometryTool.GetBoundArea(points);
                return new Bounds(rect.center, rect.size);
            }
        }

        private void Reset()
        {
            localPoints = new List<Vector3>() { new Vector3(-1, -1, 0), new Vector3(-1, +1, 0), new Vector3(+1, +1, 0), new Vector3(+1, -1, 0), };
        }
        public void GetLocalPoints(List<Vector3> ret)
        {
            ret.Clear();
            for (int i = 0; i < localPoints.Count; i++)
            {
                ret.Add(localPoints[i]);
            }
        }
        public void GetLocalPoints(List<Vector2> ret)
        {
            ret.Clear();
            for (int i = 0; i < localPoints.Count; i++)
            {
                ret.Add(localPoints[i]);
            }
        }
        public void GetWorldPoints(List<Vector3> ret)
        {
            ret.Clear();
            for (int i = 0; i < localPoints.Count; i++)
            {
                ret.Add(localPoints[i] + transform.position);
            }
        }
    }
}
