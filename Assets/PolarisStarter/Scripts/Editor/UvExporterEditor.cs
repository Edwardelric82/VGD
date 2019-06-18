using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pinwheel.PolarisStarter
{
    [CustomEditor(typeof(UvExporter))]
    public class UvExporterEditor : Editor
    {
        private UvExporter instance;

        private void OnEnable()
        {
            instance = (UvExporter)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Export"))
            {
                instance.Export();
            }
        }
    }
}