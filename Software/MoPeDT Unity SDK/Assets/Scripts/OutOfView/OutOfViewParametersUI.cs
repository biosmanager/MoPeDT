using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoPeDT.Common;

namespace MoPeDT.OutOfView
{
    public class OutOfViewParametersUI : MonoBehaviour
    {
        public CameraRigManager cameraRigManager;
        public SphericalOovVisualization sphericalOovVisualization;

        public TMP_InputField fovInput;
        public TMP_InputField angleInput;
        public TMP_InputField scaleInput;


        private void Start()
        {
            fovInput.SetTextWithoutNotify(cameraRigManager.rightDisplayCamera.fieldOfView.ToString());
            angleInput.SetTextWithoutNotify(cameraRigManager.rightDisplayCamera.transform.localEulerAngles.y.ToString());
            scaleInput.SetTextWithoutNotify(sphericalOovVisualization.scale.ToString());

            fovInput.onEndEdit.AddListener(value =>
            {
                cameraRigManager.leftDisplayCamera.fieldOfView = float.Parse(value);
                cameraRigManager.rightDisplayCamera.fieldOfView = float.Parse(value);
            });
            angleInput.onEndEdit.AddListener(value =>
            {
                cameraRigManager.leftDisplayCamera.transform.localEulerAngles = -Vector3.up * float.Parse(value);
                cameraRigManager.rightDisplayCamera.transform.localEulerAngles = Vector3.up * float.Parse(value);
            });
            scaleInput.onEndEdit.AddListener(value =>
            {
                sphericalOovVisualization.scale = float.Parse(value);   
            });
        }
    }
}
