using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.OutOfView
{
    public class SphericalOovVisualization : MonoBehaviour
    {
        public GameObject target;
        public GameObject visualizationObject;
        public float radius;
        public float scale = 1.0f;

        [SerializeField]
        private bool scaleWithDistance = true;
        public bool ScaleWithDistance
        {
            get { return scaleWithDistance; }
            set { scaleWithDistance = value; }
        }
        
        [SerializeField]
        private bool flashWithDistance;
        public bool FlashWithDistance
        {
            get
            {
                return flashWithDistance;
            }
            set
            {
                flashWithDistance = value;
                animator.SetTrigger(value ? "ProximityFlashing" : "Static");
            }
        }
        [SerializeField]
        private bool growAndShrink;
        public bool GrowAndShrink
        {
            get
            {
                return growAndShrink;
            }
            set
            {
                growAndShrink = value;
                animator.SetTrigger(value ? "SubtleGrowAndShrink" : "Static");
            }
        }

        public float minDistance;
        public float maxDistance;
        public float minDistanceScale;
        public float maxDistanceScale;
        public float minDistanceSpeed;
        public float maxDistanceSpeed;

        private Animator animator;

        private void Start()
        {
            animator = visualizationObject.GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            var directionToTarget = (target.transform.position - this.transform.position).normalized;
            visualizationObject.transform.position = this.transform.position + directionToTarget * radius;
                
            var distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if (ScaleWithDistance)
            {
                visualizationObject.transform.localScale = Vector3.one * scale * Mathf.Clamp(Remap(distanceToTarget, minDistance, maxDistance, minDistanceScale, maxDistanceScale), maxDistanceScale, minDistanceScale);
            }
            else
            {
                visualizationObject.transform.localScale = Vector3.one * scale;
            }

            if (FlashWithDistance)
            {
                animator.speed = Mathf.Clamp(Remap(distanceToTarget, minDistance, maxDistance, minDistanceSpeed, maxDistanceSpeed), maxDistanceSpeed, minDistanceSpeed);
            }
            else
            {
                animator.speed = 1.0f;
            }
        }

        private static float Remap(float value, float low1, float high1, float low2, float high2)  
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

        public void SetFlashWithDistance(bool on)
        {
            FlashWithDistance = on;
        }

        public void SetStatic()
        {
            FlashWithDistance = false;
            GrowAndShrink = false;
            animator.speed = 1.0f;
            animator.SetTrigger("Static");
        }
    }
}
