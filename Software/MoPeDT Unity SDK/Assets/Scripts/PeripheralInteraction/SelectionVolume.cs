using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.PeripheralInteraction
{
    public class SelectionVolume : MonoBehaviour
    {
        public UnityEvent<bool> onSelectionChanged;
        public bool isInside = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == this.tag)
            {
                isInside = true;
                onSelectionChanged.Invoke(true);
                //Debug.Log($"Enter {this.name}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == this.tag)
            {
                isInside = false;
                onSelectionChanged.Invoke(false);
                //Debug.Log($"Leave {this.name}");
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == this.tag)
            {
               isInside = true;    
            }
        }
    }
}
