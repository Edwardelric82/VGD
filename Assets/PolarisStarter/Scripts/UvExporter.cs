using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.PolarisStarter
{
    public class UvExporter : MonoBehaviour
    {
        [SerializeField]
        private MeshFilter meshFilterComponent;
        public MeshFilter MeshFilterComponent
        {
            get
            {
                return meshFilterComponent;
            }
            set
            {
                meshFilterComponent = value;
            }
        }

        [SerializeField]
        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = Mathf.Max(1, value);
            }
        }

        [SerializeField]
        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = Mathf.Max(1, value);
            }
        }

        private void OnValidate()
        {
            Width = width;
            Height = height;
        }

        public void Export()
        {
            if (MeshFilterComponent == null || MeshFilterComponent.sharedMesh == null)
                return;
            Texture2D uvLayout = new Texture2D(Width, Height, TextureFormat.RGBA32, false);
            uvLayout.name = string.Format("{0}-UvLayout", MeshFilterComponent.sharedMesh.name);
            Color[] clearColors = new Color[width * height];
            Utilities.Fill(clearColors, new Color(1, 1, 1, 0));
            uvLayout.SetPixels(clearColors);

            int[] tris = MeshFilterComponent.sharedMesh.triangles;
            Vector2[] uv = MeshFilterComponent.sharedMesh.uv;

            DrawUv(uvLayout, tris, uv);
            SaveTexture(uvLayout);
        }

        private void DrawUv(Texture2D tex, int[] tris, Vector2[] uv)
        {
            Color c = Color.black;
            Vector2 uvStart;
            Vector2 uvEnd;
            for (int i = 0; i < tris.Length - 2; i += 3)
            {
                uvStart = uv[tris[i]];
                uvEnd = uv[tris[i + 1]];
                DrawLineOnTexture(tex, uvStart, uvEnd, c);

                uvStart = uv[tris[i + 1]];
                uvEnd = uv[tris[i + 2]];
                DrawLineOnTexture(tex, uvStart, uvEnd, c);

                uvStart = uv[tris[i]];
                uvEnd = uv[tris[i + 2]];
                DrawLineOnTexture(tex, uvStart, uvEnd, c);
            }

            tex.Apply();
        }

        private void DrawLineOnTexture(Texture2D tex, Vector2 uvStart, Vector2 uvEnd, Color c)
        {
            Vector2 startPoint = GetPixelCoord(tex, uvStart);
            Vector2 endPoint = GetPixelCoord(tex, uvEnd);
            Vector2 p = startPoint;
            while (p != endPoint)
            {
                tex.SetPixel(
                    Mathf.RoundToInt(p.x),
                    Mathf.RoundToInt(p.y),
                    c);
                p = Vector2.MoveTowards(p, endPoint, 1);
            }
        }

        private Vector2 GetPixelCoord(Texture2D tex, Vector2 uv)
        {
            return new Vector2(
                Mathf.RoundToInt(uv.x * tex.width),
                Mathf.RoundToInt(uv.y * tex.height));
        }

        private void SaveTexture(Texture2D tex)
        {
            byte[] data = tex.EncodeToPNG();
            string path = string.Format("{0}/{1}{2}.png",
                Application.dataPath,
                tex.name,
                System.DateTime.Now.Millisecond);
            System.IO.File.WriteAllBytes(path, data);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}