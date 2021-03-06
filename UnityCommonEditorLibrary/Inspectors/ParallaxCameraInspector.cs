﻿using UnityCommonLibrary;
using UnityEditor;
using UnityEngine;

namespace UnityCommonEditorLibrary.Inspectors
{
    [CustomEditor(typeof(ParallaxCamera))]
    public class ParallaxCameraInspector : Editor
    {
        private ParallaxCamera options;

        private void Awake()
        {
            options = (ParallaxCamera)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Save Position"))
            {
                options.SavePosition();
                EditorUtility.SetDirty(options);
            }

            if (GUILayout.Button("Restore Position"))
                options.RestorePosition();
        }
    }
}