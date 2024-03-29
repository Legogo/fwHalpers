﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace fwp.halpers
{
    static public class HalperUi
    {

        static public Image setupImage(Transform tr, Sprite spr, Color tint, bool visibility = true)
        {
            Image img = tr.GetComponent<Image>();
            if (img == null) return null;

            img.sprite = spr;
            img.color = tint;

            img.enabled = visibility;
            if (img.sprite == null) img.enabled = false;

            return img;
        }

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
        static public void followTransform(this RectTransform elmt, Transform target)
        {
            elmt.followTransform(target, Vector2.zero);
        }





        public static bool IsPointerOverUIElement() => IsPointerRaycastingElements(GetEventSystemRaycastResults());

        ///Returns 'true' if we touched or hovering on Unity UI element.
        static bool IsPointerRaycastingElements(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }

        ///Gets all event systen raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }

        public static Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos, Camera camera)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = camera.WorldToScreenPoint(worldPos);
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
            //Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }

    }

}
