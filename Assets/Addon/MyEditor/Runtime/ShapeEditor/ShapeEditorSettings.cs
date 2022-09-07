using MyTimer;
using UnityEngine;

namespace MyEditor.ShapeEditor
{
    public class ShapeEditorSettings : ScriptableObject
    {
        public float DefaultLineThickness;
        public float DefaultDotSize;

        public Color LineColor;
        public Color PointColor;
        public Color SelectedPointColor;
        public Color NewPointColor;

        public float HitPointSqrDistance;
        public float HitLineDistance;
    }
}