using System.Collections.Generic;
using UnityEngine;

namespace MeshExtend
{
    [System.Serializable]
    public class MeshHelper2D
    {
        //�����ظ����ͬһ��λ��Ϊ���㣬��ʹ���ֵ���
        private readonly Dictionary<Vector2, int> vertexToIndex = new Dictionary<Vector2, int>();

        [SerializeField]
        private List<Vector2> vertices = new List<Vector2>();
        public List<Vector2> Vertices => vertices;

        [SerializeField]
        private List<int> triangles = new List<int>();
        public List<int> Triangles => triangles;

        [SerializeField]
        private Rect rect = new Rect();
        public Rect Rect
        {
            get => rect;
            set
            {
                rect = value;
                b_dirty = true;
            }
        }

        public bool b_dirty;

        public static MeshHelper2D FromMesh(Mesh mesh)
        {
            //�޸�ͶӰƽ��Ĺ��ܴ�ʵ��
            MeshHelper2D meshHelper = new MeshHelper2D();
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                meshHelper.vertices.Add(mesh.vertices[i]);
            }
            for (int i = 0; i < mesh.triangles.Length; i++)
            {
                meshHelper.triangles.Add(mesh.triangles[i]);
            }
            return meshHelper;
        }

        public Mesh ToMesh(Mesh mesh)
        {
            mesh.vertices = new Vector3[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
            {
                mesh.vertices[i] = vertices[i];
            }
            mesh.triangles = new int[triangles.Count];
            for (int i = 0; i < triangles.Count; i++)
            {
                mesh.triangles[i] = triangles[i];
            }
            return mesh;
        }

        public void Clear()
        {
            vertices.Clear();
            triangles.Clear();
            vertexToIndex.Clear();
            b_dirty = true;
        }

        /// <summary>
        /// ���һ�������Σ�����ȷ����˳ʱ��˳������������,ֻ��ͨ���˷����޸�vertices��triangles
        /// </summary>
        public void AddTriangle_Sorted(List<Vector2> tri)
        {
            if (tri.Count != 3)
            {
                Debug.LogWarning("���������εĶ���������Ϊ3");
                return;
            }
            int index;
            foreach (Vector2 vertex in tri)
            {
                if (vertexToIndex.ContainsKey(vertex))
                    index = vertexToIndex[vertex];
                else
                {
                    index = vertices.Count;
                    vertices.Add(vertex);
                    vertexToIndex.Add(vertex, index);
                }
                triangles.Add(index);
            }
            b_dirty = true;
        }
        /// <summary>
        /// ����һ��͹�����
        /// </summary>
        public void AddConvexPolygon(List<Vector2> outline)
        {
            List<List<Vector2>> tris = new List<List<Vector2>>();
            MeshExtendUtility.DivideConvexPolygon_Center(outline, tris);
            foreach (List<Vector2> tri in tris)
            {
                AddTriangle_Sorted(tri);
            }
        }
        /// <summary>
        /// ����һ�������ľ���
        /// </summary>
        public void AddBox(float x1, float x2, float y1, float y2)
        {
            List<Vector2> outline = new List<Vector2>()
            {
                new Vector2(x1,y1),
                new Vector2(x1,y2),
                new Vector2(x2,y1),
                new Vector2(x2,y2),
            };
            AddConvexPolygon(outline);
        }
        /// <summary>
        /// ����һ���������
        /// </summary>
        /// <param name="center">����</param>
        /// <param name="r">���Բ�뾶</param>
        /// <param name="n">����</param>
        /// <param name="rotateAngle">��ת��</param>
        public void AddRegularPolygon(Vector2 center, float r, int n, float rotateAngle = 0f)
        {
            List<Vector2> outline = new List<Vector2>();
            float deltaAngle = 360f / n;
            for (int i = 0; i < n; i++, rotateAngle += deltaAngle)
            {
                outline.Add(center + r * rotateAngle.ToDirection());
            }
            AddConvexPolygon(outline);
        }

        /// <summary>
        /// ����һ���߶Σ����Σ�
        /// </summary>
        /// <param name="extend">�����Ƿ�Ҫ�ֱ��ӳ�width/2</param>
        public void AddLine(Vector2 a, Vector2 b, float width, bool extend = false)
        {
            Vector2 direction = (b - a).normalized;
            Vector2 normal = new Vector2(direction.y, direction.x);
            float halfWidth = width / 2;
            if (extend)
            {
                a -= halfWidth * direction;
                b += halfWidth * direction;
            }
            List<Vector2> outline = new List<Vector2>
            {
                a + halfWidth * normal,
                a - halfWidth * normal,
                b + halfWidth * normal,
                b - halfWidth * normal
            };
            AddConvexPolygon(outline);
        }

        internal Vector2 ToUV(Vector2 point)
        {
            return new Vector2((point.x - rect.x) / rect.width, (point.y - rect.y) / rect.height);
        }
    }
}