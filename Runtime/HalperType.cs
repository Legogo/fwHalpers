using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

static public class HalperType {

  static public bool isSameType<T>(Object obj) where T : Type
  {
    return (obj as T) != null;
  }
  
  /// <summary>
  /// https://stackoverflow.com/questions/708205/c-sharp-object-type-comparison
  /// </summary>
  /// <param name="a"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  static public bool compareType(Type a, Type b, bool strict = false)
  {
    if(!strict) return a.IsAssignableFrom(b) || b.IsAssignableFrom(a);
    return a == b;
  }

  /// <summary>
  /// pas dit que ça fonctionne ?
  /// </summary>
  static public bool compareComponentType(Component a, Component b, bool strict = false){
    return compareType(a.GetType(), b.GetType(), strict);
  }
}
