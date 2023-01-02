using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoPeDT.Misc
{
    public class BackgroundColorSettingUI : MonoBehaviour
    {
        public Toggle toggle;

        private void Start()
        {
            toggle.isOn = PlayerPrefs.HasKey("useMagentaBackground");

            toggle.onValueChanged.AddListener(value =>
            {
                SetUseMagentaBackground(value);
            });
        }

        public static void SetUseMagentaBackground(bool value)
        {
            if (value)
            {
                PlayerPrefs.SetString("useMagentaBackground", "");
            }
            else
            {
                PlayerPrefs.DeleteKey("useMagentaBackground");
            }
        }
    }
}