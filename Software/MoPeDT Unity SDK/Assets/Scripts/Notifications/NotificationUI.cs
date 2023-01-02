using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MoPeDT.Notifications
{
    public class NotificationUI : MonoBehaviour
    {
        public NotificationCanvas notificationCanvas;
        
        public TMP_Dropdown notificationSelectionDropdown;
        public TMP_Dropdown shapeDropdown;
        public TMP_Dropdown colorDropdown;
        public Toggle notficationsToggle;
        public TMP_InputField speedInput;


        private void Start()
        {
            notificationCanvas.onVisibilityChanged.AddListener(value =>
            {
                notficationsToggle.SetIsOnWithoutNotify(value);
            });

            notificationSelectionDropdown.onValueChanged.AddListener(value =>
            {
                notificationCanvas.CurrentNotificationType = (NotificationCanvas.NotificationType)value;
            });
            shapeDropdown.onValueChanged.AddListener(value =>
            {
                notificationCanvas.CurrentShapeType = (NotificationCanvas.NotificationShapeType)value;
            });
            colorDropdown.onValueChanged.AddListener(value =>
            {
                notificationCanvas.CurrentNotificationColor = (NotificationCanvas.NotificationColor)value;
            });
            notficationsToggle.onValueChanged.AddListener(value =>
            {
                notificationCanvas.IsVisible = value;
            });
            speedInput.onEndEdit.AddListener(value =>
            {
                float speed;
                if (float.TryParse(value, out speed))
                {
                    notificationCanvas.animator.speed = speed;
                }
            });

            notificationSelectionDropdown.ClearOptions();
            notificationSelectionDropdown.AddOptions(Enum.GetNames(typeof(NotificationCanvas.NotificationType)).ToList());

            shapeDropdown.ClearOptions();
            shapeDropdown.AddOptions(Enum.GetNames(typeof(NotificationCanvas.NotificationShapeType)).ToList());

            colorDropdown.ClearOptions();
            colorDropdown.AddOptions(Enum.GetNames(typeof(NotificationCanvas.NotificationColor)).ToList());

            notificationSelectionDropdown.value = 0;
            shapeDropdown.value = 0;
            colorDropdown.value = 0;
            notficationsToggle.isOn = false;
            speedInput.text = notificationCanvas.animator.speed.ToString();
        }
    }
}
