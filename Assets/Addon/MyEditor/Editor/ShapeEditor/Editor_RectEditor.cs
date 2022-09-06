using UnityEditor;
using UnityEngine;

namespace MyEditor.ShapeEditor
{
    [CustomEditor(typeof(RectEditor))]
    public class Editor_RectEditor : Editor_ShapeEditor
    {
        private ShapeEditorSettings settings;

        [Auto]
        public SerializedProperty offset, size;
        private RectEditor rectEditor;
        private Vector3[] vertices;
        private Vector2[] mids;
        private Vector2[] direction;

        protected override void OnEnable()
        {
            base.OnEnable();
            vertices = new Vector3[5];
            mids = new Vector2[4];
            direction = new Vector2[] { Vector2.left, Vector2.up, Vector2.right, Vector2.down };
            rectEditor = target as RectEditor;
            settings = Resources.Load<ShapeEditorSettings>("ShapeEditorSettings");
        }

        protected override void MyOnInspectorGUI()
        {
            offset.Vector2Field("offset");
            size.Vector2Field("size");
            base.MyOnInspectorGUI();
        }

        protected override void MyOnSceneGUI()
        {
            base.MyOnSceneGUI();
            size.vector2Value = new Vector2(Mathf.Max(0.2f, size.vector2Value.x), Mathf.Max(0.2f, size.vector2Value.y));
        }

        protected override void Draw()
        {
            Rect rect = rectEditor.WorldRect;
            vertices[0] = rect.min;
            vertices[1] = new Vector2(rect.xMin, rect.yMax);
            vertices[2] = rect.max;
            vertices[3] = new Vector2(rect.xMax, rect.yMin);
            vertices[4] = rect.min;
            Handles.color = settings.LineColor;
            Handle.DrawLines(vertices, 2f);
            if (b_edit)
            {
                int index = GetIndex();
                for (int i = 0; i < vertices.Length - 1; i++)
                {
                    mids[i] = 0.5f * (vertices[i] + vertices[i + 1]);
                    Handles.color = index == i ? settings.SelectedPointColor : settings.PointColor;
                    Handle.DrawDot(mids[i], MyEditorUtility.ScreenToWorldSize(settings.DefaultDotSize));
                }
            }
        }

        protected override void OnLeftMouseDown()
        {
            selectedIndex = GetIndex();
        }

        protected override void OnLeftMouseDrag()
        {
            Vector2 mousePosition = MyEditorUtility.GetPointOnRay(mouseRay, mids[selectedIndex]);
            Vector2 delta = MyEditorUtility.Projection(mousePosition - mids[selectedIndex], direction[selectedIndex]);
            mids[selectedIndex] += delta;
            offset.vector2Value = 0.5f * (mids[selectedIndex] + mids[(selectedIndex + 2) % 4]) - rectEditor.transform.position.RemoveZ();
            size.vector2Value += new Vector2(delta.x * direction[selectedIndex].x, delta.y * direction[selectedIndex].y);
        }

        private int GetIndex()
        {
            if (HandleUtility.DistanceToPolyLine(vertices) < settings.HitLineDistance)
            {
                Vector3 closestPoint = HandleUtility.ClosestPointToPolyLine(vertices);
                for (int i = 0; i < vertices.Length - 1; i++)
                {
                    if (MyEditorUtility.Parallel(closestPoint - vertices[i], vertices[i + 1] - closestPoint))
                        return i;
                }
            }
            return -1;
        }
    }
}