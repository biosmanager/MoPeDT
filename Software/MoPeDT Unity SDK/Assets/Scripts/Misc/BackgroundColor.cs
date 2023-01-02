using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoPeDT.Misc
{
    [RequireComponent(typeof(Image))]
    public class BackgroundColor : MonoBehaviour
    {
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();

            if (PlayerPrefs.HasKey("useMagentaBackground"))
            {
                image.color = Color.magenta;
            }
            else
            {
                image.color = Color.black;
            }
        }
    }
}
