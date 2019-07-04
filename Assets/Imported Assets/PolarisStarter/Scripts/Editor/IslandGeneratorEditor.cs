using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pinwheel.PolarisStarter
{
    [CustomEditor(typeof(IslandGenerator))]
    public class IslandGeneratorEditor : Editor
    {
        //private IslandGenerator instance;

        private Texture2D goProBanner;
        private Texture2D GoProBanner
        {
            get
            {
                if (goProBanner == null)
                {
                    goProBanner = Resources.Load<Texture2D>("GoProBanner");
                }
                return goProBanner;
            }
        }

        private Texture2D basicEditionBanner;
        private Texture2D BasicEditionBanner
        {
            get
            {
                if (basicEditionBanner == null)
                {
                    basicEditionBanner = Resources.Load<Texture2D>("BasicEditionBanner");
                }
                return basicEditionBanner;
            }
        }

        private void OnEnable()
        {
            //instance = (IslandGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            if (GoProBanner != null)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(256), GUILayout.Height(64));
                if (GUI.Button(r, "", GUIStyle.none))
                {
                    Application.OpenURL(VersionInfo.ProVersionLink);
                }
                EditorGUI.DrawTextureTransparent(r, GoProBanner, ScaleMode.ScaleToFit);
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.AddCursorRect(r, MouseCursor.Link);
            }
            
            if (BasicEditionBanner != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(256), GUILayout.Height(64));
                if (GUI.Button(r, "", GUIStyle.none))
                {
                    Application.OpenURL(VersionInfo.BasicVersionLink);
                }
                EditorGUI.DrawTextureTransparent(r, BasicEditionBanner, ScaleMode.ScaleToFit);
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.AddCursorRect(r, MouseCursor.Link);
            }

            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }
}