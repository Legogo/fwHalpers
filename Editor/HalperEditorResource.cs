﻿using UnityEngine;
using UnityEditor;

namespace fwp.halpers.editor
{
    public class HalperEditorResource : MonoBehaviour
    {

        static public string getAssetFullPath(Object obj)
        {
            return Application.dataPath.Remove(Application.dataPath.LastIndexOf("Assets")) + AssetDatabase.GetAssetPath(obj);

        }// getAssetFullPath()

    }
}