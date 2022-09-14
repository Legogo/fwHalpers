﻿using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

static public class HalperUnity
{
  /// <summary>
  /// doesn't work yet
  /// </summary>
  static public bool isPlaymodeStopping()
  {
    //Debug.Log("playmode ? "+UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode);
    //Debug.Log("playing ? "+Application.isPlaying);
    //Debug.Log("playing editor ? " + UnityEditor.EditorApplication.isPlaying);
    //Debug.Log("paused editor ? " + UnityEditor.EditorApplication.isPaused);
    //Debug.Log("playing editor ? " + UnityEditor.EditorApplication.playmodeStateChanged);

#if UNITY_EDITOR
    //faux !playmode
    //true quand on lance playmode
    //faux quand on stop playmode
    return UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode;
#else
    return false;
#endif

  }


  /// <summary>
  /// call for GC
  /// </summary>
  static public void callGarbageCollector()
  {
    Debug.Log("calling GC at frame : " + Time.frameCount);
    Resources.UnloadUnusedAssets();
    System.GC.Collect();
  }

  /// <summary>
  /// shuffle list of Object
  /// </summary>
  /// <typeparam name="Object"></typeparam>
  /// <param name="list"></param>
  /// <returns></returns>
  static public List<Object> shuffle<Object>(this List<Object> list)
  {
    for (int i = 0; i < list.Count; i++)
    {
      Object temp = list[i];
      int randomIndex = Random.Range(i, list.Count);
      list[i] = list[randomIndex];
      list[randomIndex] = temp;
    }
    return list;
  }

  /// <summary>
  /// shortcut to load a bunch of object in Resources/
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="path"></param>
  /// <returns></returns>
  static public List<T> loadResources<T>(string path = "") where T : Object
  {
    Object[] list = Resources.LoadAll(path, typeof(T));
    List<T> elements = new List<T>();

    for (int i = 0; i < list.Length; i++)
    {
      if (list[i] as T)
      {
        elements.Add(list[i] as T);
      }
    }

    Debug.Log("loaded " + elements.Count + " objects of type : "+typeof(T).ToString());

    return elements;
  }

  /// <summary>
  /// fetch all TextAsset at path and clean returns lines[] (cleaned from empty lines)
  /// </summary>
  /// <param name="path"></param>
  /// <param name="prefix"></param>
  /// <returns>filename, lines[]</returns>
  static public Dictionary<string, string[]> loadResourcesLines(string path, string prefix = "")
  {
    List<TextAsset> tmp = loadResources<TextAsset>(path);

    Dictionary<string, string[]> list = new Dictionary<string, string[]>();

    for (int i = 0; i < tmp.Count; i++)
    {
      TextAsset ta = tmp[i];
      bool toAdd = true;
      if (prefix.Length > 0 && !ta.name.ToLower().StartsWith(prefix)) toAdd = false;
      if (toAdd)
      {
        string[] splitted = ta.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        list.Add(ta.name, HalperString.extractNoneEmptyLines(splitted));
      }
    }

    return list;
  }
  
  /// <summary>
  /// returns first TextAsset lines[] at path
  /// </summary>
  /// <param name="path"></param>
  /// <param name="prefix"></param>
  /// <returns></returns>
  static public KeyValuePair<string, string[]> loadResourceLine(string path, string prefix)
  {
    Dictionary<string, string[]> files = loadResourcesLines(path, prefix);
    foreach(KeyValuePair<string, string[]> kp in files)
    {
      return kp;
    }
    return default(KeyValuePair<string, string[]>);
  }

}
