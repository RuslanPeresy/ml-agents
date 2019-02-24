﻿using UnityEngine;
using UnityEditor;

namespace MLAgents
{
/*
 This code is meant to modify the behavior of the inspector on Brain Components.
 Depending on the type of brain that is used, the available fields will be modified in the inspector accordingly.
*/
    [CustomEditor(typeof(Agent), true)]
    [CanEditMultipleObjects]
    public class AgentEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            SerializedObject serializedAgent = serializedObject;
            serializedAgent.Update();

            SerializedProperty brain = serializedAgent.FindProperty("brain");
            SerializedProperty actionsPerDecision = serializedAgent.FindProperty(
                "agentParameters.numberOfActionsBetweenDecisions");
            SerializedProperty maxSteps = serializedAgent.FindProperty(
                "agentParameters.maxStep");
            SerializedProperty isResetOnDone = serializedAgent.FindProperty(
                "agentParameters.resetOnDone");
            SerializedProperty isODD = serializedAgent.FindProperty(
                "agentParameters.onDemandDecision");
            SerializedProperty cameras = serializedAgent.FindProperty(
                "agentParameters.agentCameras");
            SerializedProperty renderTextures = serializedAgent.FindProperty(
                "agentParameters.agentRenderTextures");
            SerializedProperty rawImages = serializedAgent.FindProperty(
                "agentParameters.agentRawImages");

            EditorGUILayout.PropertyField(brain);

            if (cameras.arraySize > 0 && renderTextures.arraySize > 0)
            {
                EditorGUILayout.HelpBox("Brain visual observations created by first getting all cameras then all render textures and raw images.", MessageType.Info);    
            }
            
            EditorGUILayout.LabelField("Agent Cameras");
            for (int i = 0; i < cameras.arraySize; i++)
            {
                EditorGUILayout.PropertyField(
                    cameras.GetArrayElementAtIndex(i),
                    new GUIContent("Camera " + (i + 1).ToString() + ": "));
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Camera", EditorStyles.miniButton))
            {
                cameras.arraySize++;
            }

            if (GUILayout.Button("Remove Camera", EditorStyles.miniButton))
            {
                cameras.arraySize--;
            }

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("Agent RenderTextures");
            for (int i = 0; i < renderTextures.arraySize; i++)
            {
                EditorGUILayout.PropertyField(
                    renderTextures.GetArrayElementAtIndex(i),
                    new GUIContent("RenderTexture " + (i + 1).ToString() + ": "));
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add RenderTextures", EditorStyles.miniButton))
            {
                renderTextures.arraySize++;
            }

            if (GUILayout.Button("Remove RenderTextures", EditorStyles.miniButton))
            {
                renderTextures.arraySize--;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Agent RawImages");
            for (int i = 0; i < rawImages.arraySize; i++)
            {
                EditorGUILayout.PropertyField(
                    rawImages.GetArrayElementAtIndex(i),
                    new GUIContent("RawImage " + (i + 1).ToString() + ": "));
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add RawImages", EditorStyles.miniButton))
            {
                rawImages.arraySize++;
            }

            if (GUILayout.Button("Remove RawImages", EditorStyles.miniButton))
            {
                rawImages.arraySize--;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(
                maxSteps,
                new GUIContent(
                    "Max Step", "The per-agent maximum number of steps."));
            EditorGUILayout.PropertyField(
                isResetOnDone,
                new GUIContent(
                    "Reset On Done",
                    "If checked, the agent will reset on done. Else, AgentOnDone() will be called."));
            EditorGUILayout.PropertyField(
                isODD,
                new GUIContent(
                    "On Demand Decisions",
                    "If checked, you must manually request decisions."));
            if (!isODD.boolValue)
            {
                EditorGUILayout.PropertyField(
                    actionsPerDecision,
                    new GUIContent(
                        "Decision Frequency",
                        "The agent will automatically request a decision every X" +
                        " steps and perform an action at every step."));
                actionsPerDecision.intValue = Mathf.Max(1, actionsPerDecision.intValue);
            }

            serializedAgent.ApplyModifiedProperties();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            base.OnInspectorGUI();
        }
    }
}
