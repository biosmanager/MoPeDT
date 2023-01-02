using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoPeDT.Misc
{
    public class SceneManager : MonoBehaviour
    {
        public static void LoadOutOfViewScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Locating out-of-view objects");
        }

        public static void LoadSpatialAttentionShiftScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Spatial attention shift");
        }

        public static void LoadSpatialAttentionShiftArduinoScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Spatial attention shift (Arduino)");
        }

        public static void LoadPeripheralInteractionScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Peripheral interaction");
        }

        public static void LoadNotificationsScene() 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Notifications");
        }

        public static void LoadControlVariableFeedbackScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Control variable feedback");
        }

        public static void LoadBodilyAwarenessHandsScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Bodily awareness");
        }

        public static void LoadBodilyAwarenessBalanceScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Balance");
        }

        public static void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main menu");
        }
    }
}
