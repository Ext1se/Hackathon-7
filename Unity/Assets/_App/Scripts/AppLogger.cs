using UnityEngine;

namespace App.Utils
{
    // TODO: Update Logger
    public class AppLogger
    {
        private const string APP_TAG = "Cheburashka AR";
        
        public static void Log(string message)
        {
            Log(APP_TAG, message);
        }

        public static void Log(string tag, string message)
        {
            Debug.Log($"{tag}: {message}");
        }

        public static void LogError(string message)
        {
            LogError(APP_TAG, message);
        }

        public static void LogError(string tag, string message)
        {
            Debug.LogError($"{tag}: {message}");
        }

        public static void LogError(string message, Object context)
        {
            Debug.LogError($"{APP_TAG}: {message}", context);
        }
    }
}