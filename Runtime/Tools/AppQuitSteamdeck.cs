using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

namespace fwp.halpers.tools
{
    public class AppQuitSteamdeck : MonoBehaviour
    {

        private void Update()
        {
            //combinaison ?
            if(Input.GetKeyUp(KeyCode.Backspace))
            {
                quit();
            }

        }

        private void OnGUI()
        {
            if(GUI.Button(new Rect(10, 10, 150, 75), "quit"))
            {
                quit();
            }

        }

        private void quit()
        {
            Debug.LogWarning("app quit");
            Application.Quit();
        }

    }
}

