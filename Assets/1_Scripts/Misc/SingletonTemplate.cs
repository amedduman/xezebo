using UnityEngine;

namespace Xezebo.Misc
{
    public class SingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
    { 
        public static T Instance;

        void Awake()
        {
            Instance = this as T;
        }
    }
}