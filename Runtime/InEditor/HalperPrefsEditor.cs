using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

static public class HalperPrefsEditor
{
	public const string ppref_editor_lock_upfold = "ppref_editor_level_unselect";

	static private void setIntPref(string id, int val)
	{
		if (getIntPref(id) == val) return;

#if UNITY_EDITOR
		EditorPrefs.SetInt(id, val);
		Debug.Log($"EditorPrefs:int: {id}={val}");
#endif
	}

	static private int getIntPref(string id)
	{

#if UNITY_EDITOR
		return EditorPrefs.GetInt(id, 0);
#else
    return -1;
#endif
	}

	static public void setFloatPref(string id, float val)
	{
		if (getFloatPref(id) == val) return;

#if UNITY_EDITOR
		EditorPrefs.SetFloat(id, val);
#endif
	}

	static public float getFloatPref(string id, float defaultValue = 0f)
	{
#if UNITY_EDITOR
		return EditorPrefs.GetFloat(id, defaultValue);
#else
    return -1f;
#endif
	}

	static public void setToggle(string id, bool val)
	{
		setIntPref(id, val ? 1 : 0);
	}

	static public bool isToggled(string id)
	{
		int val = getIntPref(id);
		//Debug.Log(id + "?" + val);
		return getIntPref(id) == 1;
	}

	static public int setEnum(string id, int enumIdx)
	{
		setIntPref(id, enumIdx);
		return enumIdx;
	}
	static public int getEnum(string id)
	{
		return getIntPref(id);
	}

	static public bool drawToggle(string label, string id)
	{
#if UNITY_EDITOR
		bool output = EditorGUILayout.Toggle(label, isToggled(id));
		setToggle(id, output);

		return output;
#else
		return false;
#endif
	}

}
