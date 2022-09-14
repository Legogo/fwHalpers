using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class HalperPprefs {
  
  static public void clearPprefs()
  {
    PlayerPrefs.DeleteAll(); // halper
    PlayerPrefs.Save();

    Debug.Log("<color=orange>ALL pprefs deleted</color>");
  }

}
