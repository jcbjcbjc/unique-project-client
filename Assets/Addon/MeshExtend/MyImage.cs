using UnityEngine;
using UnityEngine.UI;

namespace MeshExtend
{
    public class MyImage : Image
    {
        public MeshHelper2D myMesh = new MeshHelper2D();

        public void Repaint()
        {
            if (myMesh.b_dirty)
            {
                SetAllDirty();
                myMesh.b_dirty = false;
            }
            Rebuild(CanvasUpdate.PreRender);
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            if (myMesh.Vertices.Count == 0)
            {
                base.OnPopulateMesh(toFill);
                return;
            }
            GenerateMesh(toFill);
        }

        protected virtual void GenerateMesh(VertexHelper toFill)
        {
            toFill.Clear();

            Color32 color32 = color;
            foreach (Vector2 vertex in myMesh.Vertices)
            {
                toFill.AddVert(vertex, color32, myMesh.ToUV(vertex));
            }
            for (int i = 0; i < myMesh.Triangles.Count; i += 3)
            {
                toFill.AddTriangle(myMesh.Triangles[i], myMesh.Triangles[i + 1], myMesh.Triangles[i + 2]);
            }
        }
    }
}