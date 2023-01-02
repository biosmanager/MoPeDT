using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace MoPeDT.PeripheralInteraction
{
    public class SelectionManager : MonoBehaviour
    {
        public SelectionVolume volume1;
        public SelectionVolume volume2;
        public SelectionVolume volume3;

        public Image selectedImage1;
        public Image selectedImage2;
        public Image selectedImage3;

        public Animator selectedImageAnimator1;
        public Animator selectedImageAnimator2;
        public Animator selectedImageAnimator3;

        public SteamVR_Action_Boolean confirmationAction;
        public SteamVR_Action_Vibration hapticAction;
        public SteamVR_Input_Sources inputSource;

        private Animator currentAnimator = null; 

        private void Start()
        {
            volume1.onSelectionChanged.AddListener(selected =>
            {
                selectedImage1.gameObject.SetActive(selected);

                if (selected)
                {
                    TriggerHapticPulse();
                }
            });
            volume2.onSelectionChanged.AddListener(selected =>
            {
                selectedImage2.gameObject.SetActive(selected);

                if (selected)
                {
                    TriggerHapticPulse();
                }
            });
            volume3.onSelectionChanged.AddListener(selected =>
            {
                selectedImage3.gameObject.SetActive(selected);

                if (selected)
                {
                    TriggerHapticPulse();
                }
            });
        }

        private void TriggerHapticPulse()
        {
            hapticAction.Execute(0, 0.1f, 75, 0.5f, inputSource);
        }

        private void Update()
        {
            if (confirmationAction.GetStateDown(inputSource))
            {
                if (volume1.isInside)
                {
                    selectedImageAnimator1.SetTrigger("Confirm");
                }
                if (volume2.isInside)
                {
                    selectedImageAnimator2.SetTrigger("Confirm");
                }
                if (volume3.isInside)
                {
                    selectedImageAnimator3.SetTrigger("Confirm");
                }
            }
        }
    }
}
