using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.SpatialAttentionShift
{
    public class SpatialAttentionArduinoCommunicator : MonoBehaviour
    {
        public SpatialObjectLocator spatialObjectLocator;
        public SerialController serialController;

        private bool wasVisible = false;


        private void Start()
        {
            // Turn off display/LED stick
            serialController.SendSerialMessage("0");

            spatialObjectLocator.onSideChanged.AddListener(() =>
            {
                if (spatialObjectLocator.IsOnLeft && !spatialObjectLocator.IsOnRight)
                {
                    Debug.Log("L");
                    if (!wasVisible)
                    {
                        serialController.SendSerialMessage("1");
                        wasVisible = true;
                    }
                    serialController.SendSerialMessage("L");
                }    
                else if (spatialObjectLocator.IsOnRight && !spatialObjectLocator.IsOnLeft)
                {
                    Debug.Log("R");
                    if (!wasVisible)
                    {
                        serialController.SendSerialMessage("1");
                        wasVisible = true;
                    }
                    serialController.SendSerialMessage("R");
                }
                else if (spatialObjectLocator.IsOnLeft && spatialObjectLocator.IsOnRight)
                {
                    Debug.Log("B");
                    if (!wasVisible)
                    {
                        serialController.SendSerialMessage("1");
                        wasVisible = true;
                    }
                    serialController.SendSerialMessage("B");
                }
                else
                {
                    Debug.Log("0");
                    serialController.SendSerialMessage("0");
                    wasVisible = false;
                }
            });
        }

        void OnMessageArrived(string msg)
        {

        }

        void OnConnectionEvent(bool success)
        {
            if (success)
            {
                Debug.Log("Arduino connected.");
            }
            else
            {
                Debug.Log("Arduino disconnected.");
            }
        }
    }
}
