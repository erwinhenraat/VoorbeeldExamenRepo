using UnityEngine;

namespace swzwij.Singletons
{
    /// <summary>
    /// A generic Unity Singleton pattern implementation for MonoBehaviour-derived classes.
    /// </summary>
    /// <typeparam name="T">The type of the MonoBehaviour to be treated as a Singleton.</typeparam>
    public class SingletonBehaviour<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// The single instance of the MonoBehaviour-derived class.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Accessor property for the Singleton instance.
        /// If no instance exists, it will create one if needed.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<T>();

                if (_instance != null)
                    return _instance;

                GameObject container = new(typeof(T).Name);
                _instance = container.AddComponent<T>();

                return _instance;
            }
        }
    }
}