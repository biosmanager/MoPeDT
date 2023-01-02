using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MoPeDT.ControlVariableFeedback
{
    public class ControlVariableFeedback : MonoBehaviour
    {
        public MoPeDT.Notifications.NotificationCanvas leftNotificationCanvas;
        public MoPeDT.Notifications.NotificationCanvas rightNotificationCanvas;

        public float setPoint = 100;
        public float actualValue = 100;

        public float maxDifference = 100;
        public float maxSpeed = 2.0f;

        public UnityEvent<float> onSpeedChanged;
        public float intervalSec = 0.5f;

        private float speed = 0f;

        private void Start()
        {
            leftNotificationCanvas.CurrentShapeType = Notifications.NotificationCanvas.NotificationShapeType.Triangle;
            rightNotificationCanvas.CurrentShapeType = Notifications.NotificationCanvas.NotificationShapeType.Triangle;
            leftNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.MovingUp;
            rightNotificationCanvas.CurrentNotificationType = Notifications.NotificationCanvas.NotificationType.MovingUp;

            StartCoroutine(InvokeSpeedRepeatedly());
        }

        private void LateUpdate()
        {
            var difference = actualValue - setPoint;
            speed = -Mathf.Clamp(difference / maxDifference, -maxDifference, maxDifference) * maxSpeed;

            leftNotificationCanvas.animator.SetFloat("SpeedMultiplier", speed);
            rightNotificationCanvas.animator.SetFloat("SpeedMultiplier", speed);

            var angle = difference > 0 ? 180.0f : 0.0f;

            leftNotificationCanvas.notificationImage.transform.localEulerAngles = Vector3.forward * angle;
            rightNotificationCanvas.notificationImage.transform.localEulerAngles = Vector3.forward * angle;
        }

        private IEnumerator InvokeSpeedRepeatedly()
        {
            while (true)
            {
                onSpeedChanged.Invoke(speed);
                yield return new WaitForSeconds(intervalSec);
            }
        }
    }
}
