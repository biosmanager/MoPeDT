using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.Common
{
    public class CameraRigManager : MonoBehaviour
    {
        public Camera leftDisplayCamera;
        public Camera rightDisplayCamera;


        private void Start()
        {
            // Activate secondary displays that are used in HMD
            if (Display.displays.Length < 3)
            {
                Debug.LogWarning("Not enough displays connected. Connect peripheral displays and restart.");
            }
            else
            {
                Display.displays[1].Activate();
                Display.displays[2].Activate();
            }
        }
    }
}
