using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MoPeDT.ControlVariableFeedback
{
    public class ControlVariableFeedbackUI : MonoBehaviour
    {
        public ControlVariableFeedback controlVariableFeedback;

        public TextMeshProUGUI setpointValue;
        public TextMeshProUGUI actualValue;
        public Slider slider;


        private void Start()
        {
            slider.SetValueWithoutNotify(controlVariableFeedback.actualValue);
            slider.onValueChanged.AddListener(value =>
            {
                controlVariableFeedback.actualValue = value;
            });
        }

        private void Update()
        {
            setpointValue.text = controlVariableFeedback.setPoint.ToString();
            actualValue.text = controlVariableFeedback.actualValue.ToString();
        }
    }
}
