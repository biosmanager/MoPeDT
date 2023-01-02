using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoPeDT.Notifications
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class NotificationCanvas : MonoBehaviour
    {
        public enum NotificationShapeType
        {
            Dot,
            Circle,
            TopDot,
            Square,
            Triangle
        }

        public enum NotificationType
        {
            Static,
            MovingLeft,
            MovingRight,
            MovingDown,
            MovingUp,
            Blinking,
            Growing,
            RotatingClockwise,
            PoppingUp,
            PoppingUpFromRight
        }

        public enum NotificationColor
        {
            White,
            Red,
            Green,
            Blue
        }

        private NotificationShapeType _currentShapeType;
        public NotificationShapeType CurrentShapeType
        {
            get { return _currentShapeType; }
            set
            {
                _currentShapeType = value;
                if (value == NotificationShapeType.Dot)
                {
                    notificationImage.sprite = dotSprite;
                }
                else if (value == NotificationShapeType.Circle)
                {
                    notificationImage.sprite = circleSprite;
                }
                else if (value == NotificationShapeType.TopDot)
                {
                    notificationImage.sprite = topDotSprite;
                }
                else if (value == NotificationShapeType.Square)
                {
                    notificationImage.sprite = squareSprite;
                }
                else if (value == NotificationShapeType.Triangle)
                {
                    notificationImage.sprite = triangleSprite;
                }
            }
        }

        private NotificationCanvas.NotificationType _currentNotificationType;
        public NotificationCanvas.NotificationType CurrentNotificationType
        {
            get
            {
                return _currentNotificationType;
            }
            set
            {
                _currentNotificationType = value;
                animator.SetTrigger(value.ToString());
            }
        }

        private NotificationColor _currentNotificationColor;
        public NotificationColor CurrentNotificationColor
        {
            get
            {
                return _currentNotificationColor;
            }
            set
            {
                var color = Color.white;
                switch (value)
                {
                    case NotificationColor.White:
                        color = Color.white;
                        break;
                    case NotificationColor.Red:
                        color = Color.red;
                        break;
                    case NotificationColor.Green:
                        color = Color.green;
                        break;
                    case NotificationColor.Blue:
                        color = Color.blue;
                        break;
                }

                notificationImage.color = color;
            }
        }

        public bool IsVisible
        {
            get
            {
                return canvasGroup.alpha == 1.0f;
            }
            set
            {
                if (value)
                {
                    canvasGroup.alpha = 1.0f;
                    // Restart notification animation
                    CurrentNotificationType = CurrentNotificationType;
                }
                else
                {
                    canvasGroup.alpha = 0.0f;
                }

                onVisibilityChanged.Invoke(value);
            }
        }

        public bool IsVisibleNoRestart
        {
            get
            {
                return canvasGroup.alpha == 1.0f;
            }
            set
            {
                canvasGroup.alpha = value ? 1.0f : 0.0f;
                onVisibilityChanged.Invoke(value);
            }
        }


        public Image notificationImage;
        public Animator animator;

        public Sprite dotSprite;
        public Sprite circleSprite;
        public Sprite topDotSprite;
        public Sprite squareSprite;
        public Sprite triangleSprite;

        public UnityEvent<bool> onVisibilityChanged;

        private Canvas canvas;
        private CanvasGroup canvasGroup;


        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();  
        }
    }
}
