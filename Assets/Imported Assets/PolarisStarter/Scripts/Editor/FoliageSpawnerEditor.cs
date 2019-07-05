using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pinwheel.PolarisStarter
{
    [CustomEditor(typeof(FoliageSpawner))]
    public class FoliageSpawnerEditor : Editor
    {
        private FoliageSpawner instance;

        private void OnEnable()
        {
            instance = (FoliageSpawner)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Populate"))
            {
                instance.Populate();
            }
            if (GUILayout.Button("Combine Meshes"))
            {
                instance.CombineMeshes();
            }
        }
    }
}