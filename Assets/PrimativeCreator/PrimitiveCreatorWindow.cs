﻿namespace RedBlueGames.ToolsExamples
{
    using UnityEditor;
    using UnityEngine;
    using System.Collections;

    public class PrimitiveCreatorWindow : EditorWindow
    {
        private PrimitiveCreator.Settings settings;
        private GUIContent primitiveTypeLabel;
        private GUIContent uniformScaleLabel;
        private GUIContent colorLabel;

        [MenuItem("Window/Primitive Creator")]
        private static void ShowCubeCreatorWindow()
        {
            EditorWindow.GetWindow<PrimitiveCreatorWindow>("Primitive Creator");
        }

        private void OnEnable()
        {
            this.LoadLastSettings();

            this.primitiveTypeLabel = new GUIContent("Primitive");
            this.uniformScaleLabel = new GUIContent("Uniform Scale");
            this.colorLabel = new GUIContent("Color");
        }

        private void LoadLastSettings()
        {
            this.settings = new PrimitiveCreator.Settings();

            this.settings.PrimitiveType = (PrimitiveType)
                PlayerPrefs.GetInt(PrimitiveCreatorPrefKeys.Color);
            
            this.settings.UniformScale = PlayerPrefs.GetFloat(PrimitiveCreatorPrefKeys.UniformScale);

            this.settings.Color = (PrimitiveCreator.Settings.PrimitiveColor)
                PlayerPrefs.GetInt(PrimitiveCreatorPrefKeys.PrimitiveType);
        }

        private void OnGUI()
        {
            this.settings.PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup(
                this.primitiveTypeLabel,
                this.settings.PrimitiveType);

            this.settings.UniformScale = EditorGUILayout.FloatField(
                this.uniformScaleLabel,
                this.settings.UniformScale);
        
            this.settings.Color = (PrimitiveCreator.Settings.PrimitiveColor)
                EditorGUILayout.EnumPopup(this.colorLabel, this.settings.Color);

            if (GUILayout.Button("Create Primitive"))
            {
                GameObject shape = PrimitiveCreator.CreatePrimitive(this.settings);

                Selection.activeGameObject = shape;
            }

            this.SaveSettings();
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt(PrimitiveCreatorPrefKeys.Color, (int)this.settings.PrimitiveType);
            PlayerPrefs.SetFloat(PrimitiveCreatorPrefKeys.UniformScale, this.settings.UniformScale);
            PlayerPrefs.SetInt(PrimitiveCreatorPrefKeys.PrimitiveType, (int)this.settings.Color);
        }
    }
}