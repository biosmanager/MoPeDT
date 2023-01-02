using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCalibration : MonoBehaviour
{
    public Transform headTracker;


    public void CalibrateCenter()
    {
        this.transform.localPosition = -headTracker.localPosition;
    }
}
