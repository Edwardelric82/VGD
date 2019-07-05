using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.PolarisStarter
{
    public class IslandGenerator : MonoBehaviour
    {
        public enum SurfaceTilingMode
        {
            Quad, Hexagon
        }
        [Header("General")]
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
        private MeshCollider meshColliderComponent;
        public MeshCollider MeshColliderComponent
        {
            get
            {
                return meshColliderComponent;
            }
            set
            {
                meshColliderComponent = value;
            }
        }

        [SerializeField]
        private bool updateImmediately;
        public bool UpdateImmediately
        {
            get
            {
                return updateImmediately;
            }
            set
            {
                updateImmediately = value;
            }
        }

        [SerializeField]
        private bool showGizmos;
        public bool ShowGizmos
        {
            get
            {
                return showGizmos;
            }
            set
            {
                showGizmos = value;
            }
        }

        [Header("Overall Shape")]
        [SerializeField]
        private SurfaceTilingMode tilingMode;
        public SurfaceTilingMode TilingMode
        {
            get
            {
                return tilingMode;
            }
            set
            {
                tilingMode = value;
            }
        }

        [SerializeField]
        private int surfaceTileCountX;
        public int SurfaceTileCountX
        {
            get
            {
                return surfaceTileCountX;
            }
            set
            {
                surfaceTileCountX = Mathf.Max(1, value);
            }
        }

        [SerializeField]
        private int surfaceTileCountZ;
        public int SurfaceTileCountZ
        {
            get
            {
                return surfaceTileCountZ;
            }
            set
            {
                surfaceTileCountZ = Mathf.Max(1, value);
            }
        }

        [SerializeField]
        private float vertexSpacing;
        public float VertexSpacing
        {
            get
            {
                return vertexSpacing;
            }
            set
            {
                vertexSpacing = value;
            }
        }

        [SerializeField]
        private Texture2D heightMap;
        public Texture2D HeightMap
        {
            get
            {
                return heightMap;
            }
        }

        [SerializeField]
        private float baseHeight;
        public float BaseHeight
        {
            get
            {
                return baseHeight;
            }
            set
            {
                baseHeight = Mathf.Max(0, value);
            }
        }

        [Header("Surface")]
        [SerializeField]
        private float surfaceMaxHeight;
        public float SurfaceMaxHeight
        {
            get
            {
                return surfaceMaxHeight;
            }
            set
            {
                surfaceMaxHeight = Mathf.Max(0, value);
            }
        }

        [SerializeField]
        private float roughness;
        public float Roughness
        {
            get
            {
                return roughness;
            }
            set
            {
                roughness = value;
            }
        }

        [SerializeField]
        private float roughnessSeed;
        public float RoughnessSeed
        {
            get
            {
                return roughnessSeed;
            }
            set
            {
                roughnessSeed = value;
            }
        }

        [Header("Ground Thickness")]
        [SerializeField]
        private float groundThickness;
        public float GroundThickness
        {
            get
            {
                return groundThickness;
            }
            set
            {
                groundThickness = Mathf.Clamp(value, 0, BaseHeight);
            }
        }

        [SerializeField]
        [Range(0f, 1f)]
        private float groundThicknessVariation;
        public float GroundThicnknessVariation
        {
            get
            {
                return groundThicknessVariation;
            }
            set
            {
                groundThicknessVariation = Mathf.Clamp01(value);
            }
        }

        [SerializeField]
        private float groundThicknessVariationSeed;
        public float GroundThicknessVariationSeed
        {
            get
            {
                return groundThicknessVariationSeed;
            }
            set
            {
                groundThicknessVariationSeed = value;
            }
        }

        [SerializeField]
        private float groundThicknessNoiseStep;
        private float GroundThicknessNoiseStep
        {
            get
            {
                return groundThicknessNoiseStep;
            }
            set
            {
                groundThicknessNoiseStep = value;
            }
        }

        [Header("Coloring")]
        [SerializeField]
        private Gradient colorByHeight;
        public Gradient ColorByHeight
        {
            get
            {
                if (colorByHeight == null)
                    colorByHeight = new Gradient();
                return colorByHeight;
            }
            set
            {
                colorByHeight = value;
            }
        }

        [SerializeField]
        private Gradient colorByNormal;
        public Gradient ColorByNormal
        {
            get
            {
                if (colorByNormal == null)
                    colorByNormal = new Gradient();
                return colorByNormal;
            }
            set
            {
                colorByNormal = value;
            }
        }

        [SerializeField]
        private Gradient colorBlendFraction;
        public Gradient ColorBlendFraction
        {
            get
            {
                if (colorBlendFraction == null)
                    colorBlendFraction = new Gradient();
                return colorBlendFraction;
            }
            set
            {
                colorBlendFraction = value;
            }
        }

        [SerializeField]
        private Color undergroundColor;
        public Color UndergroundColor
        {
            get
            {
                return undergroundColor;
            }
            set
            {
                undergroundColor = value;
            }
        }

        [Header("Optional")]
        [SerializeField]
        private bool useFlatShading;
        public bool UseFlatShading
        {
            get
            {
                return useFlatShading;
            }
            set
            {
                useFlatShading = value;
            }
        }

        [SerializeField]
        private bool useVertexColor;
        public bool UseVertexColor
        {
            get
            {
                return useVertexColor;
            }
            set
            {
                useVertexColor = value;
            }
        }

        [SerializeField]
        private bool shouldRecalculateBounds;
        public bool ShouldRecalculateBounds
        {
            get
            {
                return shouldRecalculateBounds;
            }
            set
            {
                shouldRecalculateBounds = value;
            }
        }

        [SerializeField]
        private bool shouldRecalculateNormals;
        public bool ShouldRecalculateNormals
        {
            get
            {
                return shouldRecalculateNormals;
            }
            set
            {
                shouldRecalculateNormals = value;
            }
        }

        [SerializeField]
        private bool shouldRecalculateTangents;
        public bool ShouldRecalculateTangents
        {
            get
            {
                return shouldRecalculateTangents;
            }
            set
            {
                shouldRecalculateTangents = value;
            }
        }

        [SerializeField]
        private bool shouldUnwrapUv;
        public bool ShouldUnwrapUv
        {
            get
            {
                return shouldUnwrapUv;
            }
            set
            {
                shouldUnwrapUv = value;
            }
        }

        [SerializeField]
        private bool shouldEncloseBottomPart;
        public bool ShouldEncloseBottomPart
        {
            get
            {
                return shouldEncloseBottomPart;
            }
            set
            {
                shouldEncloseBottomPart = value;
            }
        }

        private Index2D VerticesArrayLength
        {
            get
            {
                int x = SurfaceTileCountX + 1 + SURFACE_INSET * 2;
                int z = SurfaceTileCountZ + 1 + SURFACE_INSET * 2;
                return new Index2D(x, z);
            }
        }

        public Index2D SurfaceMinIndex
        {
            get
            {
                int x = SURFACE_INSET;
                int z = SURFACE_INSET;
                return new Index2D(x, z);
            }
        }

        public Index2D SurfaceMaxIndex
        {
            get
            {
                int x = VerticesArrayLength.X - 1 - SURFACE_INSET;
                int z = VerticesArrayLength.Z - 1 - SURFACE_INSET;
                return new Index2D(x, z);
            }
        }

        public int TotalTileCountX
        {
            get
            {
                return SurfaceTileCountX + 2 * SURFACE_INSET;
            }
        }

        public int TotalTileCountZ
        {
            get
            {
                return SurfaceTileCountZ + 2 * SURFACE_INSET;
            }
        }

        private const int SURFACE_INSET = 3;
        private const float NOISE_STEP_MULTIPLIER = 0.0167f;

        private Mesh generatedMesh;
        public Mesh GeneratedMesh
        {
            get
            {
                if (generatedMesh == null)
                {
                    generatedMesh = new Mesh();
                    generatedMesh.MarkDynamic();
                }
                return generatedMesh;
            }
            private set
            {
                generatedMesh = value;
            }
        }

        private Vector3[][] vertices;
        private int[] triangles;
        private Color[] vertexColors;
        private Vector2[] uvCoords;

        private void Reset()
        {
            MeshFilterComponent = GetComponent<MeshFilter>();
            MeshColliderComponent = GetComponent<MeshCollider>();
            UpdateImmediately = true;
            ShowGizmos = false;
            TilingMode = SurfaceTilingMode.Quad;
            SurfaceTileCountX = 10;
            SurfaceTileCountZ = 10;
            VertexSpacing = 1;
            BaseHeight = 1;
            SurfaceMaxHeight = 0;
            GroundThickness = 0.5f;
            GroundThicnknessVariation = 0;
            Roughness = 0;
            RoughnessSeed = 0;
            UseFlatShading = true;
            UseVertexColor = true;
            ShouldRecalculateBounds = true;
            ShouldRecalculateNormals = true;
            ShouldRecalculateTangents = true;
            ShouldUnwrapUv = true;
            ShouldEncloseBottomPart = true;
            Generate();
        }

        private void OnValidate()
        {
            SurfaceTileCountX = surfaceTileCountX;
            SurfaceTileCountZ = surfaceTileCountZ;
            BaseHeight = baseHeight;
            SurfaceMaxHeight = surfaceMaxHeight;
            GroundThickness = groundThickness;
            GroundThicnknessVariation = groundThicknessVariation;

            if (UpdateImmediately)
                Generate();
        }

        private void Generate()
        {
            GenerateVerticesInfo();
            GenerateTriangles();
            ConstructNewMesh();
        }

        private void ConstructNewMesh()
        {
            Vector3[] v = Utilities.To1dArray(vertices);
            if (UseFlatShading)
            {
                CreateFlatShading(ref v, ref triangles, ref vertexColors, ref uvCoords);
            }

            GeneratedMesh.Clear();
            GeneratedMesh.vertices = v;
            GeneratedMesh.triangles = triangles;
            if (UseVertexColor)
                GeneratedMesh.colors = vertexColors;
            if (ShouldUnwrapUv)
                GeneratedMesh.uv = uvCoords;
            if (ShouldRecalculateBounds)
                GeneratedMesh.RecalculateBounds();
            if (ShouldRecalculateNormals)
                GeneratedMesh.RecalculateNormals();
            if (ShouldRecalculateTangents)
                GeneratedMesh.RecalculateTangents();
            GeneratedMesh.name = "ProceduralIsland";

            if (MeshFilterComponent != null)
                MeshFilterComponent.mesh = GeneratedMesh;
            if (MeshColliderComponent != null)
                MeshColliderComponent.sharedMesh = GeneratedMesh;
        }

        private void GenerateVerticesInfo()
        {
            vertices = Utilities.CreateJaggedArray<Vector3>(VerticesArrayLength.Z, VerticesArrayLength.X);
            vertexColors = new Color[VerticesArrayLength.Z * VerticesArrayLength.X];
            uvCoords = new Vector2[VerticesArrayLength.Z * VerticesArrayLength.X];

            for (int z = SurfaceMinIndex.Z; z <= SurfaceMaxIndex.Z; ++z)
            {
                for (int x = SurfaceMinIndex.X; x <= SurfaceMaxIndex.X; ++x)
                {
                    Index2D i = new Index2D(x, z);
                    GenerateVertexPositionAtIndex(i);
                }
            }

            for (int z = 0; z < VerticesArrayLength.Z; ++z)
            {
                for (int x = 0; x < VerticesArrayLength.X; ++x)
                {
                    Index2D i = new Index2D(x, z);
                    if (!IsSurfaceVertex(i))
                        GenerateVertexPositionAtIndex(i);
                }
            }

            for (int z = 0; z < VerticesArrayLength.Z; ++z)
            {
                for (int x = 0; x < VerticesArrayLength.X; ++x)
                {
                    Index2D i = new Index2D(x, z);
                    if (UseVertexColor)
                        GenerateVertexColorAtIndex(i);
                    if (ShouldUnwrapUv)
                        GenerateVertexUvAtIndex(i);
                }
            }
        }

        private void GenerateVertexPositionAtIndex(Index2D i)
        {
            float vertexX = 0;
            float vertexY = 0;
            float vertexZ = 0;

            Vector3 translation = GetVertexTranslationAtIndex(i);

            vertexX = i.X * VertexSpacing + translation.x;
            vertexY = GetVertexHeight(i);
            vertexZ = i.Z * VertexSpacing + translation.z;

            vertices[i.Z][i.X] = new Vector3(vertexX, vertexY, vertexZ);
        }

        private Vector3 GetVertexTranslationAtIndex(Index2D i)
        {
            float translationX = 0;
            float translationY = 0;
            float translationZ = 0;

            translationX = -TotalTileCountX * VertexSpacing / 2;
            if (i.X < SurfaceMinIndex.X)
            {
                translationX += (SurfaceMinIndex.X - i.X) * VertexSpacing;
            }
            else if (i.X > SurfaceMaxIndex.X)
            {
                translationX += (SurfaceMaxIndex.X - i.X) * VertexSpacing;
            }

            translationZ = -TotalTileCountZ * VertexSpacing / 2;
            if (i.Z < SurfaceMinIndex.Z)
            {
                translationZ += (SurfaceMinIndex.Z - i.Z) * VertexSpacing;
            }
            else if (i.Z > SurfaceMaxIndex.Z)
            {
                translationZ += (SurfaceMaxIndex.Z - i.Z) * VertexSpacing;
            }

            if (WillShiftVertexInHexagonTiling(i))
            {
                translationX += VertexSpacing * 0.5f;
            }

            return new Vector3(translationX, translationY, translationZ);
        }

        private float GetVertexHeight(Index2D i)
        {
            if (!IsSurfaceVertex(i))
                return GetUndergroundVertexHeight(i);
            else
                return GetSurfaceVertexHeight(i);
        }

        private float GetUndergroundVertexHeight(Index2D i)
        {
            if (!Utilities.IsInRangeExclusive(i.X, 0, VerticesArrayLength.X - 1) ||
                !Utilities.IsInRangeExclusive(i.Z, 0, VerticesArrayLength.Z - 1))
            {
                return 0;
            }
            else
            {
                Index2D nearestSurfaceVertexIndex = new Index2D(
                    Mathf.Clamp(i.X, SurfaceMinIndex.X, SurfaceMaxIndex.X),
                    Mathf.Clamp(i.Z, SurfaceMinIndex.Z, SurfaceMaxIndex.Z));
                Vector3 nearestSurfaceVertexPosition = vertices[nearestSurfaceVertexIndex.Z][nearestSurfaceVertexIndex.X];
                float thicknessVariation = 0;
                if (GroundThicnknessVariation > 0)
                {
                    float noiseValue = GetGroundThicknessNoiseValueAtIndex(nearestSurfaceVertexIndex);
                    thicknessVariation = noiseValue * GroundThickness * GroundThicnknessVariation;
                }

                float height = nearestSurfaceVertexPosition.y - GroundThickness + thicknessVariation;
                return height;
            }
        }

        private float GetSurfaceVertexHeight(Index2D i)
        {
            float heightMapMultiplier = 1;
            if (HeightMap != null)
            {
                float xOffset = 0;
                if (WillShiftVertexInHexagonTiling(i))
                {
                    xOffset = 0.5f;
                }
                Vector2 uv = new Vector2(
                    Utilities.GetFraction(i.X + xOffset, SurfaceMinIndex.X, SurfaceMaxIndex.X),
                    Utilities.GetFraction(i.Z, SurfaceMinIndex.Z, SurfaceMaxIndex.Z));

                heightMapMultiplier = HeightMap.GetPixelBilinear(uv.x, uv.y).a;
            }
            float noiseMultiplier = GetSurfaceNoiseValueAtIndex(i);

            return BaseHeight + SurfaceMaxHeight * heightMapMultiplier * noiseMultiplier;
        }

        private float GetSurfaceNoiseValueAtIndex(Index2D i)
        {
            if (Roughness == 0)
                return 1;
            Vector2 noiseOrigin = RoughnessSeed * Vector2.one;
            Vector2 noiseCoord = noiseOrigin + new Vector2(i.X, i.Z) * Roughness * NOISE_STEP_MULTIPLIER;
            if (WillShiftVertexInHexagonTiling(i))
            {
                noiseCoord.x += 0.5f * Roughness * NOISE_STEP_MULTIPLIER;
            }
            float noiseValue = Mathf.PerlinNoise(noiseCoord.x, noiseCoord.y);
            return noiseValue;
        }

        private float GetGroundThicknessNoiseValueAtIndex(Index2D i)
        {
            Vector2 noiseOrigin = GroundThicknessVariationSeed * Vector2.one;
            Vector2 noiseCoord = noiseOrigin + new Vector2(i.X, i.Z) * GroundThicknessNoiseStep;
            float noiseValue = Mathf.PerlinNoise(noiseCoord.x, noiseCoord.y);
            return noiseValue;
        }

        private void GenerateVertexColorAtIndex(Index2D i)
        {
            Color c = Color.white;
            bool isUnderground =
                !Utilities.IsInRange(i.X, SurfaceMinIndex.X - 1, SurfaceMaxIndex.X + 1) ||
                !Utilities.IsInRange(i.Z, SurfaceMinIndex.Z - 1, SurfaceMaxIndex.Z + 1);
            if (isUnderground)
            {
                c = UndergroundColor;
            }
            else
            {
                Vector3 v = vertices[i.Z][i.X];
                float heightFraction = Utilities.GetFraction(v.y - BaseHeight, 0, SurfaceMaxHeight);
                Color cbh = ColorByHeight.Evaluate(heightFraction);

                Vector3 normal = EvaluateNormalAtIndex(i);
                float normalDotUp = Vector3.Dot(normal, Vector3.up);
                Color cbn = ColorByNormal.Evaluate(normalDotUp);

                float blendFraction = ColorBlendFraction.Evaluate(heightFraction).a;
                c = Vector4.Lerp(cbh, cbn, blendFraction);
            }

            int cIndex = To1DIndex(i.X, i.Z);
            vertexColors[cIndex] = c;
        }

        private Vector3 EvaluateNormalAtIndex(Index2D i)
        {
            Vector3 normal = Vector3.zero;

            Vector3 normalTopLeft = Vector3.zero;
            if (Utilities.IsInRange(i.X, 0, VerticesArrayLength.X - 2) &&
                Utilities.IsInRange(i.Z, 0, VerticesArrayLength.Z - 2))
            {
                normalTopLeft = Vector3.Cross(
                    vertices[i.Z + 1][i.X],
                    vertices[i.Z][i.X + 1]);
            }

            Vector3 normalTopRight = Vector3.zero;
            if (Utilities.IsInRange(i.X, 1, VerticesArrayLength.X - 1) &&
                Utilities.IsInRange(i.Z, 0, VerticesArrayLength.Z - 2))
            {
                normalTopRight = Vector3.Cross(
                    vertices[i.Z][i.X - 1],
                    vertices[i.Z + 1][i.X]);
            }

            Vector3 normalBottomRight = Vector3.zero;
            if (Utilities.IsInRange(i.X, 1, VerticesArrayLength.X - 1) &&
                Utilities.IsInRange(i.Z, 1, VerticesArrayLength.Z - 1))
            {
                normalBottomRight = Vector3.Cross(
                    vertices[i.Z - 1][i.X],
                    vertices[i.Z][i.X - 1]);
            }

            Vector3 normalBottomLeft = Vector3.zero;
            if (Utilities.IsInRange(i.X, 0, VerticesArrayLength.X - 2) &&
                Utilities.IsInRange(i.Z, 1, VerticesArrayLength.Z - 1))
            {
                normalBottomLeft = Vector3.Cross(
                    vertices[i.Z][i.X + 1],
                    vertices[i.Z - 1][i.X]);
            }

            normal = (normalTopLeft + normalTopRight + normalBottomRight + normalBottomLeft).normalized;

            return normal;
        }

        private void GenerateVertexUvAtIndex(Index2D i)
        {
            float xOffset = 0;
            if (WillShiftVertexInHexagonTiling(i))
            {
                xOffset = 0.5f;
            }
            Vector2 uv = new Vector2(
                Utilities.GetFraction(i.X + xOffset, 0, VerticesArrayLength.X - 1),
                Utilities.GetFraction(i.Z, 0, VerticesArrayLength.Z - 1));
            uvCoords[To1DIndex(i.X, i.Z)] = uv;
        }

        private void CreateFlatShading(ref Vector3[] vert, ref int[] tris, ref Color[] cs, ref Vector2[] uv)
        {
            Vector3[] newVert = new Vector3[tris.Length];
            Color[] newColors = new Color[tris.Length];
            Vector2[] newUv = new Vector2[tris.Length];

            for (int i = 0; i < tris.Length; i++)
            {
                newVert[i] = vert[tris[i]];
                newColors[i] = cs[tris[i]];
                newUv[i] = uv[tris[i]];
                tris[i] = i;
            }
            vert = newVert;
            cs = newColors;
            uv = newUv;
        }

        private bool IsSurfaceVertex(Index2D i)
        {
            return Utilities.IsInRange(i.X, SurfaceMinIndex.X, SurfaceMaxIndex.X) &&
                Utilities.IsInRange(i.Z, SurfaceMinIndex.Z, SurfaceMaxIndex.Z);
        }

        private bool WillShiftVertexInHexagonTiling(Index2D i)
        {
            return TilingMode == SurfaceTilingMode.Hexagon &&
                Utilities.IsInRangeExclusive(i.X, SurfaceMinIndex.X, SurfaceMaxIndex.X) &&
                Utilities.IsInRange(i.Z, SurfaceMinIndex.Z, SurfaceMaxIndex.Z) &&
                i.Z % 2 == 0;
        }

        private void GenerateTriangles()
        {
            int surfaceAndUndergroundTrisCount = TotalTileCountX * TotalTileCountZ * 2;
            int bottomPartTrisCount = ShouldEncloseBottomPart ? TotalTileCountZ * 2 : 0;
            int trisArrayLength = (surfaceAndUndergroundTrisCount + bottomPartTrisCount) * 3;

            triangles = new int[trisArrayLength];
            int currentTrisIndex = 0;

            WrapSurfaceAndUnderground(ref currentTrisIndex);
            if (ShouldEncloseBottomPart)
                EncloseBottomPart(ref currentTrisIndex);
        }

        private void WrapSurfaceAndUnderground(ref int currentTrisIndex)
        {
            for (int z = 0; z < VerticesArrayLength.Z - 1; ++z)
            {
                for (int x = 0; x < VerticesArrayLength.X - 1; ++x)
                {
                    int[] tris = GetTris(x, z);
                    AddTris(ref currentTrisIndex, tris);
                }
            }
        }

        private void EncloseBottomPart(ref int currentTrisIndex)
        {
            int x = 0;
            int maxX = VerticesArrayLength.X - 1;
            int[] t = new int[6];
            for (int z = 0; z < VerticesArrayLength.Z - 1; ++z)
            {
                t[0] = To1DIndex(maxX, z + 1);
                t[1] = To1DIndex(x, z + 1);
                t[2] = To1DIndex(x, z);

                t[3] = To1DIndex(maxX, z);
                t[4] = To1DIndex(maxX, z + 1);
                t[5] = To1DIndex(x, z);

                AddTris(ref currentTrisIndex, t);
            }
        }

        private int To1DIndex(int x, int z)
        {
            return z * VerticesArrayLength.X + x;
        }

        private int[] GetTris(int x, int z)
        {
            if (TilingMode == SurfaceTilingMode.Quad)
                return GetTrisForQuadTiling(x, z);
            else
                return GetTrisForHexagonTiling(x, z);
        }

        private int[] GetTrisForQuadTiling(int x, int z)
        {
            int[] t = new int[6];
            if ((x + z) % 2 == 0)
            {
                t[0] = To1DIndex(x, z);
                t[1] = To1DIndex(x, z + 1);
                t[2] = To1DIndex(x + 1, z + 1);

                t[3] = To1DIndex(x, z);
                t[4] = To1DIndex(x + 1, z + 1);
                t[5] = To1DIndex(x + 1, z);
            }
            else
            {
                t[0] = To1DIndex(x, z);
                t[1] = To1DIndex(x, z + 1);
                t[2] = To1DIndex(x + 1, z);

                t[3] = To1DIndex(x + 1, z + 1);
                t[4] = To1DIndex(x + 1, z);
                t[5] = To1DIndex(x, z + 1);
            }

            return t;
        }

        private int[] GetTrisForHexagonTiling(int x, int z)
        {
            int[] t = new int[6];
            if (z % 2 == 0)
            {
                t[0] = To1DIndex(x, z);
                t[1] = To1DIndex(x, z + 1);
                t[2] = To1DIndex(x + 1, z + 1);

                t[3] = To1DIndex(x, z);
                t[4] = To1DIndex(x + 1, z + 1);
                t[5] = To1DIndex(x + 1, z);
            }
            else
            {
                t[0] = To1DIndex(x, z);
                t[1] = To1DIndex(x, z + 1);
                t[2] = To1DIndex(x + 1, z);

                t[3] = To1DIndex(x + 1, z + 1);
                t[4] = To1DIndex(x + 1, z);
                t[5] = To1DIndex(x, z + 1);
            }

            return t;
        }

        private void AddTris(ref int currentTrisIndex, int[] tris)
        {
            if (tris.Length % 3 != 0)
                throw new System.ArgumentException("Invalid indices array");
            for (int i = 0; i < tris.Length; ++i)
            {
                triangles[currentTrisIndex] = tris[i];
                currentTrisIndex += 1;
            }
        }

        private void OnDrawGizmos()
        {
            if (vertices == null || !ShowGizmos)
                return;

            for (int z = 0; z < VerticesArrayLength.Z; ++z)
            {
                for (int x = 0; x < VerticesArrayLength.X; ++x)
                {
                    if (IsSurfaceVertex(new Index2D(x, z)))
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.green;
                    Gizmos.DrawCube(transform.position + vertices[z][x], Vector3.one * 0.5f);
                }
            }
        }
    }
}