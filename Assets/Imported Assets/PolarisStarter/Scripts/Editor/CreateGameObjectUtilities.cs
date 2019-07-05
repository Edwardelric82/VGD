using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Pinwheel.PolarisStarter
{
	public static class CreateGameObjectUtilities
	{
        [MenuItem("GameObject/Low-Poly Terrain", false, 10)]
        public static void CreateDefaultTerrain(MenuCommand menuCmd)
        {
            GameObject g = new GameObject("Terrain");
            GameObjectUtility.SetParentAndAlign(g, menuCmd.context as GameObject);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;
            g.transform.localScale = Vector3.one;

            MeshFilter mf = g.AddComponent<MeshFilter>();
            MeshRenderer mr = g.AddComponent<MeshRenderer>();
            MeshCollider mc = g.AddComponent<MeshCollider>();
            IslandGenerator tg = g.AddComponent<IslandGenerator>();

            tg.MeshFilterComponent = mf;
            tg.MeshColliderComponent = mc;

            mr.sharedMaterial = Resources.Load<Material>("Terrain");

            Undo.RegisterCreatedObjectUndo(g, "Undo creating " + g.name);
        }
    }
}
