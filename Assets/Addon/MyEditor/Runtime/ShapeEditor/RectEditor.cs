using UnityEngine;

namespace MyEditor.ShapeEditor
{
    public class RectEditor : MonoBehaviour
    {
        public Vector2 offset;
        public Vector2 size;

        public Rect LocalRect => new Rect(offset - size / 2, size);
        public Rect WorldRect => new Rect(offset - size / 2 + transform.position.RemoveZ(), size);

        private void Reset()
        {
            offset = Vector2.zero;
            size = Vector2.one;
        }
    }
}