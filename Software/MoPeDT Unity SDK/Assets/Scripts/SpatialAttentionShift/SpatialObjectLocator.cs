using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.SpatialAttentionShift
{
    public class SpatialObjectLocator : MonoBehaviour
    {
        public GameObject target;
        public float centralFovAngle = 180;
        public float overlapAngle = 20;

        public float minAngleSpeed = 0.5f;
        public float maxAngleSpeed = 2.0f;

        public float minDistance;
        public float maxDistance;
        public float minDistanceScale;
        public float maxDistanceScale;
        public float minDistanceSpeed;
        public float maxDistanceSpeed;

        public Notifications.NotificationCanvas leftNotificationCanvas;
        public Notifications.NotificationCanvas rightNotificationCanvas;

        public RectTransform leftShape;
        public RectTransform rightShape;


        public float Angle { get; private set; }
        public bool EncodeAngleWithSpeed { get; set; } = false;
        public bool EncodeDistanceWithSpeed { get; set; } = false;
        public bool EncodeDistanceWithScale { get; set; } = false;

        private bool _dangerAlert = false;
        public bool DangerAlert
        {
            get { return _dangerAlert; }
            set
            {
                _dangerAlert = value;
                if (_dangerAlert)
                {
                    leftNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.Growing;
                    leftNotificationCanvas.CurrentNotificationColor = Notifications.NotificationCanvas.NotificationColor.Red;
                    leftNotificationCanvas.animator.speed = 3.0f;
                    rightNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.Growing;
                    rightNotificationCanvas.CurrentNotificationColor = Notifications.NotificationCanvas.NotificationColor.Red;
                    rightNotificationCanvas.animator.speed = 3.0f;
                }
                else
                {
                    leftNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.MovingLeft;
                    leftNotificationCanvas.CurrentNotificationColor = Notifications.NotificationCanvas.NotificationColor.White;
                    leftNotificationCanvas.animator.speed = 1.0f;
                    rightNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.MovingRight;
                    rightNotificationCanvas.CurrentNotificationColor = Notifications.NotificationCanvas.NotificationColor.White;
                    rightNotificationCanvas.animator.speed = 1.0f;
                }
            }
        }

        private bool _active = true;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                IsOnLeft = false;
                IsOnRight = false;
                onSideChanged.Invoke();
            }
        }



        public bool IsOnLeft { get; private set; }
        public bool IsOnRight { get; private set; }

        public UnityEvent onSideChanged;


        private void Start()
        {
            DangerAlert = false;
        }

        private void Update()
        {
            if (!Active)
            {
                leftNotificationCanvas.IsVisibleNoRestart = false;
                rightNotificationCanvas.IsVisibleNoRestart = false;

                return;
            }

            var projectedForward = Vector3.ProjectOnPlane(this.transform.forward, Vector3.up);
            var projectedTargetDirection = Vector3.ProjectOnPlane(target.transform.position - this.transform.position, Vector3.up);

            Angle = Vector3.SignedAngle(projectedForward, projectedTargetDirection, Vector3.up);

            leftNotificationCanvas.IsVisibleNoRestart = false;
            rightNotificationCanvas.IsVisibleNoRestart = false;

            // Back left
            if (Angle < -centralFovAngle / 2 || Angle >= 180 - overlapAngle / 2)
            {
                leftNotificationCanvas.IsVisibleNoRestart = true;
                
                if (!IsOnLeft)
                {
                    IsOnLeft = true;
                    onSideChanged.Invoke();
                }
            }
            else
            {
                if (IsOnLeft)
                {
                    IsOnLeft = false;
                    onSideChanged.Invoke();
                }
            }

            // Back right
            if (Angle > centralFovAngle / 2 || Angle <= -180 + overlapAngle / 2)
            {
                rightNotificationCanvas.IsVisibleNoRestart = true;

                if (!IsOnRight)
                {
                    IsOnRight = true;
                    onSideChanged.Invoke();
                }
            }
            else
            {
                if (IsOnRight)
                {
                    IsOnRight = false;
                    onSideChanged.Invoke();
                }
            }

            var distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if (EncodeAngleWithSpeed)
            {
                var leftSpeed = Angle >= 180 - overlapAngle / 2 ? maxAngleSpeed : Mathf.Clamp(Remap(Angle, -centralFovAngle / 2, -180 + overlapAngle / 2, minAngleSpeed, maxAngleSpeed), minAngleSpeed, maxAngleSpeed);
                var rightSpeed = Angle <= -180 + overlapAngle / 2 ? maxAngleSpeed : Mathf.Clamp(Remap(Angle, centralFovAngle / 2, 180 - overlapAngle / 2, minAngleSpeed, maxAngleSpeed), minAngleSpeed, maxAngleSpeed);

                leftNotificationCanvas.animator.speed = leftSpeed;
                rightNotificationCanvas.animator.speed = rightSpeed; 
            }
            else if (EncodeDistanceWithSpeed)
            {
                var speed = Mathf.Clamp(Remap(distanceToTarget, minDistance, maxDistance, minDistanceSpeed, maxDistanceSpeed), maxDistanceSpeed, minDistanceSpeed);

                leftNotificationCanvas.animator.speed = speed;
                rightNotificationCanvas.animator.speed = speed;
            }
            else
            {
                leftNotificationCanvas.animator.speed = 1.0f;
                rightNotificationCanvas.animator.speed = 1.0f;
            }

            if (EncodeDistanceWithScale)
            {
                var scale = Vector3.one * Mathf.Clamp(Remap(distanceToTarget, minDistance, maxDistance, minDistanceScale, maxDistanceScale), maxDistanceScale, minDistanceScale);

                leftShape.localScale = scale;
                rightShape.localScale = scale;
            }
            else
            {
                leftShape.localScale = Vector3.one;
                rightShape.localScale = Vector3.one;
            }
        }

        private static float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }
    }
}