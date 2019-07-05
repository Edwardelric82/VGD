using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.PolarisStarter
{
    public class FoliageSpawner : MonoBehaviour
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
        private Texture2D distributionMap;
        public Texture2D DistributionMap
        {
            get
            {
                return distributionMap;
            }
            set
            {
                distributionMap = value;
            }
        }

        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab
        {
            get
            {
                return prefab;
            }
            set
            {
                prefab = value;
            }
        }

        [SerializeField]
        private float size;
        public float Size
        {
            get
            {
                return size;
            }
            set
            {
                size = Mathf.Max(0, value);
            }
        }

        [Range(0f, 1f)]
        [SerializeField]
        private float density;
        public float Density
        {
            get
            {
                return density;
            }
            set
            {
                density = Mathf.Clamp01(value);
            }
        }

        [SerializeField]
        private float positionOffset;
        public float PositionOffset
        {
            get
            {
                return positionOffset;
            }
            set
            {
                positionOffset = value;
            }
        }

        [SerializeField]
        private float maxRotation;
        public float MaxRotation
        {
            get
            {
                return maxRotation;
            }
            set
            {
                maxRotation = value;
            }
        }

        [SerializeField]
        private bool followVertexNormal;
        public bool FollowVertexNormal
        {
            get
            {
                return followVertexNormal;
            }
            set
            {
                followVertexNormal = value;
            }
        }

        private void OnValidate()
        {
            Size = size;
            Density = density;
        }

        public void Populate()
        {
            if (MeshFilterComponent == null || MeshFilterComponent.sharedMesh == null)
                return;
            Utilities.ClearChildren(this.transform);
            Vector3[] vertices = MeshFilterComponent.sharedMesh.vertices;
            Vector2[] uvCoords = MeshFilterComponent.sharedMesh.uv;
            Vector3[] normals = MeshFilterComponent.sharedMesh.normals;

            for (int i = 0; i < vertices.Length; ++i)
            {
                float distributionMultiplier = 1;
                if (DistributionMap != null)
                    distributionMultiplier = DistributionMap.GetPixelBilinear(uvCoords[i].x, uvCoords[i].y).a;
                float probability = Density * distributionMultiplier;

                if (Random.value < probability)
                {
                    SpawnAtVertex(vertices[i], uvCoords[i], normals[i]);
                }
            }
        }

        private void SpawnAtVertex(Vector3 position, Vector2 uv, Vector3 localNormal)
        {
            Vector3 worldPosition = MeshFilterComponent.transform.TransformPoint(position);
            Vector3 offset = Random.insideUnitSphere * PositionOffset;
            offset.y = 0;
            float distributionMultiplier = 1;
            if (DistributionMap != null)
                distributionMultiplier = DistributionMap.GetPixelBilinear(uv.x, uv.y).a;
            Vector3 scale = Vector3.one * Size * distributionMultiplier;
            GameObject g = Instantiate(Prefab);
            g.transform.parent = this.transform;
            g.transform.position = worldPosition + offset;
            g.transform.localScale = scale;
            g.name = Prefab.name;

            if (FollowVertexNormal)
                g.transform.up = MeshFilterComponent.transform.TransformPoint(localNormal);
            float rotationDegreeY = MaxRotation * Random.value;
            g.transform.Rotate(0, rotationDegreeY, 0, Space.Self);
        }

        public void CombineMeshes()
        {
            MeshFilter[] childMf = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combineInstances = new CombineInstance[childMf.Length];
            for (int i = 0; i < childMf.Length; ++i)
            {
                combineInstances[i].mesh = childMf[i].sharedMesh;
                combineInstances[i].transform = transform.worldToLocalMatrix * childMf[i].transform.localToWorldMatrix;
            }

            Mesh m = new Mesh();
            m.CombineMeshes(combineInstances, true, true, false);
            m.name = "FoliageGroup";

            GameObject foliageGroup = new GameObject("FoliageGroup");
            foliageGroup.transform.parent = this.transform;
            foliageGroup.transform.localPosition = Vector3.zero;
            foliageGroup.transform.localRotation = Quaternion.identity;
            foliageGroup.transform.localScale = Vector3.one;

            MeshFilter mf = foliageGroup.AddComponent<MeshFilter>();
            mf.sharedMesh = m;

            Material mat = null;
            MeshRenderer childMr = GetComponentInChildren<MeshRenderer>();
            if (childMr != null)
                mat = childMr.sharedMaterial;

            MeshRenderer mr = foliageGroup.AddComponent<MeshRenderer>();
            mr.material = mat;

            foliageGroup.transform.SetAsFirstSibling();

            for (int i = 0; i < childMf.Length; ++i)
            {
                childMf[i].gameObject.SetActive(false);
            }
        }
    }
}