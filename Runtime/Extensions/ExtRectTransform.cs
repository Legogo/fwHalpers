using UnityEngine;

static public class ExtRectTransform
{

    static public Vector2 getBottomCenterPosition(this RectTransform elmt)
    {
        return elmt.anchoredPosition + (Vector2.down * elmt.sizeDelta.y) + (Vector2.right * elmt.sizeDelta.x * 0.5f);
    }

    static public float getHeight(this RectTransform elmt)
    {
        return elmt.sizeDelta.y;
    }
    static public float getWidth(this RectTransform elmt)
    {
        return elmt.sizeDelta.x;
    }

    static public void setWidth(this RectTransform elmt, float val)
    {
        Vector2 size = elmt.sizeDelta;
        size.x = val;
        elmt.sizeDelta = size;
        //Debug.Log(elmt.name + " size : " + elmt.sizeDelta);
    }

    static public void setHeight(this RectTransform elmt, float val)
    {
        Vector2 size = elmt.sizeDelta;
        size.y = val;
        elmt.sizeDelta = size;
    }

    static public void setProporPosition(this RectTransform elmt, Vector2 pos)
    {
        pos.x = pos.x * Screen.width;
        pos.y = pos.y * Screen.height;
        elmt.anchoredPosition = pos;
    }

    static public void setPixelPosition(this RectTransform elmt, Vector2 pos)
    {
        elmt.anchoredPosition = pos;
    }
    static public Vector2 getPixelPosition(this RectTransform elmt)
    {
        return elmt.anchoredPosition;
    }

    /// <summary>
    /// this is cost heavy, don't do it each frame
    /// </summary>
    /// <param name="elmt"></param>
    /// <param name="target"></param>
    /// <param name="offset"></param>
    static public void followTransform(this RectTransform elmt, Transform target, Vector2 offset)
    {
        elmt.position = Camera.main.WorldToScreenPoint(target.position);
    }
    
}
