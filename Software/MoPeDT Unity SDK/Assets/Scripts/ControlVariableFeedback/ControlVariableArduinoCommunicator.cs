using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.ControlVariableFeedback
{
    public class ControlVariableArduinoCommunicator : MonoBehaviour
    {
        public ControlVariableFeedback spatialObjectLocator;
        public SerialController serialController;


        private void Start()
        {
            spatialObjectLocator.onSpeedChanged.AddListener(speed =>
            {
                serialController.SendSerialMessage(speed.ToString());
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
