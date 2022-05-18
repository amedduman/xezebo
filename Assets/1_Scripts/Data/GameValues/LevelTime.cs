using UnityEngine;

namespace Xezebo.Data
{
    [CreateAssetMenu(fileName = "LevelTime", menuName = "SO/LevelTime", order = 0)]
    public class LevelTime : ScriptableObject
    {
        public int LevelTimeData
        {
            get
            {
                return _levelTime;
            }
            private set
            {
                
            }
        }

        [SerializeField] int _levelTime = 20;
    }
}