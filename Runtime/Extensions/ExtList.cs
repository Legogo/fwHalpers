using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static public class ExtList
{

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

}
