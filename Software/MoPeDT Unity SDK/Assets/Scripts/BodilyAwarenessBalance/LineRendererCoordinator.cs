using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoPeDT.BodilyAwarenessBalance
{
    public class LineRendererCoordinator : MonoBehaviour
    {
        public List<LineRenderer> lineRenderers;


        public void SetWidthMultiplier(float multiplier)
        {
            foreach (var lineRenderer in lineRenderers)
            {
                lineRenderer.widthMultiplier = multiplier;
            }
        }
    }
}