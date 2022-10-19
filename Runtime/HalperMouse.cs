using UnityEngine;
using UnityEditor;

namespace fwp.halpers
{
    /// <summary>
    /// This is used to find the mouse position when it's over a SceneView.
    /// Used by tools that are menu invoked.
    /// </summary>
    [InitializeOnLoad]
    public class HalperMouse : Editor
    {
        public static Vector2 screen_position = Vector2.zero;
        public static Vector2 world_position = Vector2.zero;

        static HalperMouse()
        {
            //SceneView.onSceneGUIDelegate += UpdateView;
            SceneView.duringSceneGui += UpdateView;
        }

        private static void UpdateView(SceneView sceneViewParam)
        {
            //Debug.Log(sceneView.titleContent);

            if (Event.current != null)
            {
                if(Event.current.type == EventType.MouseDown)
                {
                    screen_position = new Vector2(
                        Event.current.mousePosition.x + sceneViewParam.position.x,
                        Event.current.mousePosition.y + sceneViewParam.position.y);

                    //Debug.Log("selected position : " + screen_position);

                    world_position = rayMousePosition(sceneViewParam.camera);
                }
                
            }

            //sceneViewParam.SendEvent(EditorGUIUtility.CommandEvent("mouse"));
        }

        static public Vector3 rayMousePosition(Camera cam)
        {
            Vector3 output = Vector3.zero;

            Vector3 distanceFromCam = new Vector3(
                cam.transform.position.x,
                cam.transform.position.y,
                0);

            Plane plane = new Plane(Vector3.forward, distanceFromCam);

            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            float enter = 0.0f;

            if (plane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                output = ray.GetPoint(enter);

            }

            //Debug.Log("Mouse Pos" + output);
            return output;
        }
    }
}