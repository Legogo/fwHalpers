﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

/// <summary>
/// % = ctrl
/// # = shit
/// & = alt
/// </summary>

namespace fwp.halpers.editor
{
    public class HalperEditor
    {

        [MenuItem("Tools/Clear console #&c")]
        public static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [MenuItem("Tools/pause %#&w")]
        public static void PauseEditor()
        {
            Debug.Log("PAUSE EDITOR");
            Debug.Break();
            //UnityEditor.EditorApplication.isPlaying = false;

        }

        /// <summary>
        /// shortcut to EditorGUIUtility.PingObject
        /// </summary>
        static public void pingObject(Object asset)
        {
            // Also flash the folder yellow to highlight it
            EditorGUIUtility.PingObject(asset);
        }

        /// <summary>
        /// use : EditorGUIUtility.PingObject
        /// </summary>
        static public void pingFolder(string assetsPath)
        {
            string path = "Assets/" + assetsPath;

            // Load object
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

            // Select the object in the project folder
            Selection.activeObject = obj;

            // Also flash the folder yellow to highlight it
            EditorGUIUtility.PingObject(obj);
        }

        static public T editor_draw_selectObject<T>(T instance = null, string overrideSelectLabel = "") where T : Component
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }

            if (instance == null)
            {
                GUILayout.Label("can't find " + typeof(T) + " in scene");
                return null;
            }

            EditorGUILayout.ObjectField(typeof(T).ToString(), instance, typeof(T), true);
            if (GUILayout.Button(overrideSelectLabel + " " + typeof(T)))
            {
                Selection.activeGameObject = instance.gameObject;
            }

            return instance;
        }

        static public void editorCenterCameraToObject(GameObject obj)
        {
            GameObject tmp = Selection.activeGameObject;

            Selection.activeGameObject = obj;

            if (SceneView.lastActiveSceneView != null)
            {
                SceneView.lastActiveSceneView.FrameSelected();
            }

            if (tmp != null) Selection.activeGameObject = tmp;
        }


        /// <summary>
        /// get the sorting layer names<para/>
        /// from : http://answers.unity3d.com/questions/585108/how-do-you-access-sorting-layers-via-scripting.html
        /// </summary>
        /// <returns>The Sorting Layers (as string[])</returns>
        static public string[] getSortingLayerNames()
        {
            System.Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);

            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            return (string[])sortingLayersProperty.GetValue(null, new object[0]);

        }// getSortingLayerNames()

        /// <summary>
        /// get the unique sorting layer IDs<para/>
        /// from : http://answers.unity3d.com/questions/585108/how-do-you-access-sorting-layers-via-scripting.html
        /// </summary>
        /// <returns>The sorting layer unique IDs (as int[])</returns>
        static public int[] getSortingLayerUniqueIDs()
        {
            System.Type internalEditorUtilityType = typeof(UnityEditorInternal.InternalEditorUtility);

            PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);

            return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);

        }// getSortingLayerUniqueIDs()



        /// <summary>
        /// Retourne l'ID local d'un UnityEngine.Object dans une scène<para/>
        /// Viens de https://forum.unity3d.com/threads/how-to-get-the-local-identifier-in-file-for-scene-objects.265686/
        /// </summary>
        /// <param name="obj">L'objet cible.</param>
        /// <returns>L'ID de l'objet. Retourne 0 ou -1 si pas sauvegardé.</returns>
        static public int getLocalIdInFile(Object obj)
        {
            if (obj == null) return -1;

            PropertyInfo inspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            SerializedObject serializeObject = new SerializedObject(obj);

            inspectorModeInfo.SetValue(serializeObject, InspectorMode.Debug, null);

            SerializedProperty propertyLocalID = serializeObject.FindProperty("m_LocalIdentfierInFile");

            int localID = propertyLocalID.intValue;

            inspectorModeInfo.SetValue(serializeObject, InspectorMode.Normal, null);

            return localID;

        }// getLocalIdInFile()


        static public ScriptableObject getScriptable<T>() where T : ScriptableObject
        {
            string typ = typeof(T).ToString();
            //Debug.Log(typ);
            string[] all = AssetDatabase.FindAssets("t:" + typ);
            //Debug.Log(all.Length);
            for (int i = 0; i < all.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(all[i]), typeof(T));
                T data = obj as T;
                if (data != null) return data;
            }
            return null;
        }

        static public T getScriptableObjectInEditor<T>(string nameEnd = "") where T : ScriptableObject
        {
            string[] all = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            for (int i = 0; i < all.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(all[i]), typeof(T));
                T data = obj as T;

                if (data == null) continue;
                if (nameEnd.Length > 0)
                {
                    if (!data.name.EndsWith(nameEnd)) continue;
                }

                return data;
            }
            Debug.LogWarning("can't locate scriptable of type " + typeof(T).Name + " (filter name ? " + nameEnd + ")");
            return null;
        }
    }

}
