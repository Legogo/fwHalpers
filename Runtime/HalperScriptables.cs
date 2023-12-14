using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 2023-12-14
/// must stay out of Editor/ because can also be use in runtime scripts
/// </summary>
namespace fwp.halpers
{
    static public class HalperScriptables
    {

#if UNITY_EDITOR
        
        static public ScriptableObject[] getScriptableObjectsInEditor(System.Type scriptableType)
        {
            string[] all = AssetDatabase.FindAssets("t:" + scriptableType.Name);

            List<ScriptableObject> output = new List<ScriptableObject>();
            for (int i = 0; i < all.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(all[i]), scriptableType);
                ScriptableObject so = obj as ScriptableObject;
                if (so == null) continue;
                output.Add(so);
            }

            //Debug.Log(scriptableType + " x"+output.Count+" / x" + all.Length);

            return output.ToArray();
        }


        static public T[] getScriptableObjectsInEditor<T>() where T : ScriptableObject
        {
            System.Type scriptableType = typeof(T);
            string[] all = AssetDatabase.FindAssets("t:" + scriptableType.Name);

            List<T> output = new List<T>();
            for (int i = 0; i < all.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(all[i]), scriptableType);
                T so = obj as T;
                if (so == null) continue;
                output.Add(so);
            }
            return output.ToArray();
        }

        static public T getScriptableObjectInEditor<T>(string nameContains = "") where T : ScriptableObject
        {
            string[] all = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            for (int i = 0; i < all.Length; i++)
            {
                Object obj = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(all[i]), typeof(T));
                T data = obj as T;

                if (data == null) continue;
                if (nameContains.Length > 0)
                {
                    if (!data.name.Contains(nameContains)) continue;
                }

                return data;
            }
            Debug.LogWarning("can't locate scriptable of type " + typeof(T).Name + " (filter name ? " + nameContains + ")");
            return null;
        }
#endif

    }

}
