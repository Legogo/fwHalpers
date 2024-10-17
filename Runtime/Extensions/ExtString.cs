using UnityEngine;

using System.Text;
using System;

/// <summary>
/// no namespace to include extensions automatically ?
/// </summary>

static public class ExtString
{
    static public string upperFirstLetter(this string v)
    {
        return v.Substring(0, 1).ToUpper() + v.Substring(1, v.Length - 1);
    }

    static public string lowerFirstLetter(this string v)
    {
        return v.Substring(0, 1).ToLower() + v.Substring(1, v.Length - 1);
    }

    static public void prepend(this StringBuilder instance, string content)
    {
        instance.Insert(0, content + Environment.NewLine);
    }
}
